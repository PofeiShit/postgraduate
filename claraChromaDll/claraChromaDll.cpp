// claraChromaDll.cpp : 定义 DLL 应用程序的导出函数。
//

#include "stdafx.h"
#include <iostream>
#include <emmintrin.h>
#define SSE2 1
#define FLOAT 1
static const int SCALE = 4;
static const int MAX_ESIZE = 16;
static const float YCbCrYRF = 0.299F;
static const float YCbCrYGF = 0.587F;
static const float YCbCrYBF = 0.114F;
#define WIDTHBYTES(bytes) (((bytes * 8 + 31) / 32) * 4)
typedef unsigned char uchar;
static inline uchar clampToByte(int Value)
{
	return (uchar)((Value | ((int)(255 - Value) >> 31) ) & ~((int)Value >> 31));
}
static void *AllocMemory(unsigned int Size, bool ZeroMemory)	//	https://technet.microsoft.com/zh-cn/library/8z34s9c6
{
	void *Ptr = _aligned_malloc(Size, 16);					//	考虑SSE,AVX等高级函数的需求，分配起始地址使用32字节对齐。其实_mm_malloc就是这个函数
	if (Ptr != NULL && ZeroMemory == true)
		memset(Ptr, 0, Size);
	return Ptr;
}
static void FreeMemory(void *Ptr)
{
	if (Ptr != NULL) 
		_aligned_free(Ptr);		//	_mm_free就是该函数
}
static inline int clip(int x, int a, int b)
{
	return x >= a ? (x < b ? x : b - 1) : a;
}
static void Add(const float* src1, const float* src2, float* dst, int width, int height, int channel){
	int len = WIDTHBYTES(width * channel)* height, i = 0;
	float* D = (float*)dst;
	const float* S1 = (float*)src1;
	const float* S2 = (float*)src2;
#if SSE2
	for( ; i <= len - 8; i += 8){
		__m128 Src1 = _mm_set_ps(S1[i + 3], S1[i + 2], S1[i + 1], S1[i]);
		__m128 Src2 = _mm_set_ps(S1[i + 7], S1[i + 6], S1[i + 5], S1[i +4]);
		__m128 Src3 = _mm_set_ps(S2[i + 3], S2[i + 2], S2[i + 1], S2[i]);
		__m128 Src4 = _mm_set_ps(S2[i + 7], S2[i + 6], S2[i + 5], S2[i + 4]);
		
		_mm_store_ps((D + i), _mm_add_ps(Src1, Src3));
		_mm_store_ps((D + i + 4), _mm_add_ps(Src2, Src4));
	}
#else
	for( ; i <= len - 4; i += 4){
		D[i] = S1[i] + S2[i];
		D[i + 1] = S1[i + 1] + S2[i + 1];
		D[i + 2] = S1[i + 2] + S2[i + 2];
		D[i + 3] = S1[i + 3] + S2[i + 3];
	}
#endif
	for( ;i < len; ++i){
		D[i] = S1[i] + S2[i];
	}
}
static void Sub(const float* src1, const float* src2, float* dst, int width, int height, int channel){
	int len = WIDTHBYTES(width * channel)* height, i = 0;
	float* D = (float*)dst;
	const float* S1 = (float*)src1;
	const float* S2 = (float*)src2;
#if SSE2
	for( ; i <= len - 8; i += 8){
		__m128 Src1 = _mm_set_ps(S1[i + 3], S1[i + 2], S1[i + 1], S1[i]);
		__m128 Src2 = _mm_set_ps(S1[i + 7], S1[i + 6], S1[i + 5], S1[i +4]);
		__m128 Src3 = _mm_set_ps(S2[i + 3], S2[i + 2], S2[i + 1], S2[i]);
		__m128 Src4 = _mm_set_ps(S2[i + 7], S2[i + 6], S2[i + 5], S2[i + 4]);
		
		_mm_store_ps((D + i), _mm_sub_ps(Src1, Src3));
		_mm_store_ps((D + i + 4), _mm_sub_ps(Src2, Src4));
	}
#else
	for( ; i <= len - 4; i += 4){
		D[i] = S1[i] - S2[i];
		D[i + 1] = S1[i + 1] - S2[i + 1];
		D[i + 2] = S1[i + 2] - S2[i + 2];
		D[i + 3] = S1[i + 3] - S2[i + 3];
	}
#endif
	for( ;i < len; ++i){
		D[i] = S1[i] - S2[i];
	}
}
static void Mul(const float* src1, const float* src2, float* dst, int width, int height, int channel){
	int len = WIDTHBYTES(width * channel)* height, i = 0;
	float* D = (float*)dst;
	const float* S1 = (float*)src1;
	const float* S2 = (float*)src2;
#if SSE2
	for( ; i <= len - 8; i += 8){
		__m128 Src1 = _mm_set_ps(S1[i + 3], S1[i + 2], S1[i + 1], S1[i]);
		__m128 Src2 = _mm_set_ps(S1[i + 7], S1[i + 6], S1[i + 5], S1[i +4]);
		__m128 Src3 = _mm_set_ps(S2[i + 3], S2[i + 2], S2[i + 1], S2[i]);
		__m128 Src4 = _mm_set_ps(S2[i + 7], S2[i + 6], S2[i + 5], S2[i + 4]);
		
		_mm_store_ps((D + i), _mm_mul_ps(Src1, Src3));
		_mm_store_ps((D + i + 4), _mm_mul_ps(Src2, Src4));
	}
#else
	for( ; i <= len - 4; i += 4){
		D[i] = S1[i] * S2[i];
		D[i + 1] = S1[i + 1] * S2[i + 1];
		D[i + 2] = S1[i + 2] * S2[i + 2];
		D[i + 3] = S1[i + 3] * S2[i + 3];
	}
#endif
	for( ;i < len; ++i){
		D[i] = S1[i] * S2[i];
	}
}
static void Div(const float* src1, const float* src2, float* dst, float eps, int width, int height, int channel){
	int len = WIDTHBYTES(width * channel)* height, i = 0;
	float* D = (float*)dst;
	const float* S1 = (float*)src1;
	const float* S2 = (float*)src2;
#if SSE2
	__m128 c = _mm_set_ps(eps, eps, eps, eps);
	for( ; i < len - 8; i += 8){
		__m128 Src1 = _mm_set_ps(S1[i + 3], S1[i + 2], S1[i + 1], S1[i]);
		__m128 Src2 = _mm_set_ps(S1[i + 7], S1[i + 6], S1[i + 5], S1[i +4]);
		__m128 Src3 = _mm_set_ps(S2[i + 3], S2[i + 2], S2[i + 1], S2[i]);
		__m128 Src4 = _mm_set_ps(S2[i + 7], S2[i + 6], S2[i + 5], S2[i + 4]);

		_mm_store_ps((D + i), _mm_div_ps(Src1, _mm_add_ps(Src3, c)));
		_mm_store_ps((D + i + 4), _mm_div_ps(Src2, _mm_add_ps(Src4, c)));
	}
#else
	for( ; i <= len - 4; i += 4){
		D[i] = S1[i] / (S2[i] + eps);
		D[i + 1] = S1[i + 1] / (S2[i + 1] + eps);
		D[i + 2] = S1[i + 2] / (S2[i + 2] + eps);
		D[i + 3] = S1[i + 3] / (S2[i + 3] + eps);
	}
#endif
	for( ;i < len; ++i){
		D[i] = S1[i] / (S2[i] + eps); 
	}
}
static int *GetExpandPos(int Length, int Left, int Right)
{
	if (Left < 0 || Length < 0 || Right < 0) return NULL;
	int *Pos = (int *)_aligned_malloc((Left + Length + Right) * sizeof(int), false);
	if (Pos == NULL) return NULL;

	int X;
	for (X = -Left; X < Length + Right; X++)
	{
		if (X < 0)
		{
			//重复边缘像素
			Pos[X + Left] = 0;
		}
		else if (X >= Length)
		{
				Pos[X + Left] = Length - 1;
		}
		else
		{
			Pos[X + Left] = X;
		}
	}
	return Pos;
}
static void BoxFilter(const float* Src, float* Dst, int Width, int Height, int Channel, int Radius)
{
	int X, Y, Z, Size, Index, SumLineBytes, LineWidth;
	float Value, ValueB, ValueG, ValueR;
	int *RowPos, *ColPos;
	float *ColSum, *Diff;

	Size = 2 * Radius + 1;
	float Scale = 1.0 / (Size * Size);
	SumLineBytes = WIDTHBYTES(Width * Channel) * sizeof(float);
	LineWidth = WIDTHBYTES(Width * Channel);
	int Amount = Size * Size;

	RowPos = GetExpandPos(Width, Radius, Radius);
	ColPos = GetExpandPos(Height, Radius, Radius);
	ColSum = (float *)AllocMemory(Width * Channel * sizeof(float), true);
	Diff   = (float *)AllocMemory((Width - 1) * Channel * sizeof(float), true);
	float *RowData = (float *)AllocMemory((Width + 2 * Radius) * Channel * sizeof(float), true);
	float* Sum = (float*)AllocMemory(Height * SumLineBytes, false);

	for (Y = 0; Y < Height; Y++)					//	水平方向的耗时比垂直方向上的大
	{
		float *LinePS = (float*)(Src + Y * LineWidth);
		float *LinePD = Sum + Y * LineWidth;

		//	拷贝一行数据及边缘部分部分到临时的缓冲区中
		if (Channel == 1)
		{
			for (X = 0; X < Radius; X++)				
				RowData[X] = LinePS[RowPos[X]];						
			memcpy(RowData + Radius, LinePS, Width * sizeof(float));																						
			for (X = Radius + Width; X < Radius + Width + Radius; X++)	
				RowData[X] = LinePS[RowPos[X]];								
		}
		else if (Channel == 3)
		{
			for (X = 0; X < Radius; X++)
			{
				Index = RowPos[X] * 3;
				RowData[X * 3] = LinePS[Index];		
				RowData[X * 3 + 1] = LinePS[Index + 1];		
				RowData[X * 3 + 2] = LinePS[Index + 2];		
			}
			memcpy(RowData + Radius * 3, LinePS, Width * 3 * sizeof(float));																						
			for (X = Radius + Width; X < Radius + Width + Radius; X++)	
			{
				Index = RowPos[X] * 3;
 				RowData[X * 3 + 0] = LinePS[Index + 0];		
				RowData[X * 3 + 1] = LinePS[Index + 1];		
				RowData[X * 3 + 2] = LinePS[Index + 2];		
			}
		} else if (Channel == 4)
		{
			for (X = 0; X < Radius; X++)
			{
				Index = RowPos[X] * 4;
				RowData[X * 4] = LinePS[Index];		
				RowData[X * 4 + 1] = LinePS[Index + 1];		
				RowData[X * 4 + 2] = LinePS[Index + 2];		
			}
			memcpy(RowData + Radius * 4, LinePS, Width * 4 * sizeof(float));																						
			for (X = Radius + Width; X < Radius + Width + Radius; X++)	
			{
				Index = RowPos[X] * 4;
 				RowData[X * 4 + 0] = LinePS[Index + 0];		
				RowData[X * 4 + 1] = LinePS[Index + 1];		
				RowData[X * 4 + 2] = LinePS[Index + 2];		
			}
		}

		float *AddPos = RowData + Size * Channel;
		float *SubPos = RowData;
		X = 0;					//	注意这个赋值在下面的循环外部，这可以避免当Width<8时第二个for循环循环变量未初始化			
			for (; X <= (Width - 1) * Channel - 4; X += 4) //单通道
		{
			__m128 Add = _mm_set_ps(AddPos[X + 3], AddPos[X + 2], AddPos[X + 1], AddPos[X]);		
			__m128 Sub = _mm_set_ps(SubPos[X + 3], SubPos[X + 2], SubPos[X + 1], SubPos[X]);		

			_mm_store_ps((Diff + X), _mm_sub_ps(Add, Sub));	//存储反向	
		}
		for(; X < (Width - 1) * Channel; X++)
			Diff[X] = AddPos[X] - SubPos[X];

		//	第一个点要特殊处理
		if (Channel == 1)
		{
			for(Z = 0, Value = 0; Z < Size; Z++)	Value += RowData[Z];
			LinePD[0] = Value;

			for(X = 1; X < Width; X++)
			{
				Value += Diff[X - 1];
				LinePD[X] = Value;
			}
		}
		else if (Channel == 3)
		{
			for(Z = 0, ValueB = ValueG = ValueR = 0; Z < Size; Z++)
			{
				ValueB += RowData[Z * 3 + 0];
				ValueG += RowData[Z * 3 + 1];
				ValueR += RowData[Z * 3 + 2];
			}
			LinePD[0] = ValueB;	LinePD[1] = ValueG;	LinePD[2] = ValueR;

			for(X = 1; X < Width; X++)
			{
				Index = X * 3;	
				ValueB += Diff[Index - 3];		LinePD[Index + 0] = ValueB;
				ValueG += Diff[Index - 2];		LinePD[Index + 1] = ValueG;
				ValueR += Diff[Index - 1];		LinePD[Index + 2] = ValueR;
			}
		}
		else if(Channel == 4)
		{
			for(Z = 0, ValueB = ValueG = ValueR = 0; Z < Size; Z++)
			{
				ValueB += RowData[Z * 4 + 0];
				ValueG += RowData[Z * 4 + 1];
				ValueR += RowData[Z * 4 + 2];
			}
			LinePD[0] = ValueB;	LinePD[1] = ValueG;	LinePD[2] = ValueR;

			for(X = 1; X < Width; X++)
			{
				Index = X * 4;	
				ValueB += Diff[Index - 4];		LinePD[Index + 0] = ValueB;
				ValueG += Diff[Index - 3];		LinePD[Index + 1] = ValueG;
				ValueR += Diff[Index - 2];		LinePD[Index + 2] = ValueR;
			}
		}
	}
	////////////////////////////////////////////////////////////////////////////
	for (Y = 0; Y < Size - 1; Y++)			//	注意没有最后一项哦						
	{
		X = 0;
		float *LinePS = Sum + ColPos[Y] * LineWidth;
		for( ; X <= Width * Channel - 4; X += 4)
		{
			__m128 SumP = _mm_set_ps(ColSum[X + 3], ColSum[X + 2], ColSum[X + 1], ColSum[X]);
			__m128 SrcP = _mm_set_ps(LinePS[X + 3], LinePS[X + 2], LinePS[X + 1], LinePS[X]);
			_mm_store_ps((ColSum + X), _mm_add_ps(SumP, SrcP));
		}

		for( ; X < Width * Channel; X++)	ColSum[X] += LinePS[X];
	}

	for (Y = 0; Y < Height; Y++)
	{
		float* LinePD	= Dst + Y * LineWidth;	
		float *AddPos	= (float*)(Sum + ColPos[Y + Size - 1] * LineWidth);
		float *SubPos	= (float*)(Sum + ColPos[Y] * LineWidth);
			
		X = 0;
		const __m128 Inv = _mm_set1_ps(Scale);
		for( ; X <= Width * Channel - 4; X += 4 )
		{
			__m128 Sub = _mm_set_ps(SubPos[X + 3], SubPos[X + 2], SubPos[X + 1], SubPos[X]);
			__m128 Add = _mm_set_ps(AddPos[X + 3], AddPos[X + 2], AddPos[X + 1], AddPos[X]);
			__m128 Col = _mm_set_ps(ColSum[X + 3], ColSum[X + 2], ColSum[X + 1], ColSum[X]);

			__m128 Sum = _mm_add_ps(Col, Add);

			__m128 Dest = _mm_mul_ps(Inv, Sum);
			
			_mm_store_ps((LinePD + X), Dest);

			_mm_store_ps((ColSum + X), _mm_sub_ps(Sum, Sub));
			
		}
		for( ; X < Width * Channel; X++)
		{
			Value = ColSum[X] + AddPos[X];
			LinePD[X] = Value * Scale;
			ColSum[X] = Value - SubPos[X];
		}

	}
	FreeMemory(RowPos);
	FreeMemory(ColPos);
	FreeMemory(Diff);
	FreeMemory(Sum);
	FreeMemory(ColSum);
	FreeMemory(RowData);
}
void guideSmooth(const float* src, float* dst, int width, int height, int cn, int radius, float eps)
{
	if(NULL == src)return;
	int x, y, srcStep, IStep, dstStep, len;
	float *tmp, *tmp1, *tmp2;
	const float* I = src;
	eps		*= 255 * 255;
	srcStep  = WIDTHBYTES(width * cn);
	len		 = srcStep * height;
	tmp		 = (float*)AllocMemory(len * sizeof(float), false);
	tmp1	 = (float*)AllocMemory(len * sizeof(float), false);
	tmp2	 = (float*)AllocMemory(len * sizeof(float), false);
	BoxFilter(I, tmp, width, height, cn, radius); // meanI = fmean(I, r); tmp = meanI;
	Mul(I, I, tmp1, width, height, cn); // tmp1 = I .* I;
	BoxFilter(tmp1, tmp2, width, height, cn, radius);	//corrI = fmean(I.*I, r); tmp2 = corrI;
	Mul(tmp, tmp, tmp1, width, height, cn);// tmp1 = meanI .* meanI;
	Sub(tmp2, tmp1, tmp2, width, height, cn); // varI = corrI - meanI .* meanI; tmp2 = varI;
	Div(tmp2, tmp2, tmp2, eps, width, height, cn); // a = covIp ./ (varI + eps) tmp2 = a; covIp = varI;
	Mul(tmp2, tmp, tmp1, width, height, cn); // tmp1 = a .* meanI;
	Sub(tmp, tmp1, tmp1, width, height, cn); // b = meanI - a .* meanI; tmp1 = b;

	BoxFilter(tmp2, tmp, width, height, cn, radius);  // meana = fmean(a, r); tmp = meana;
	BoxFilter(tmp1, tmp2, width, height, cn, radius); // meanb = fmean(b, r); tmp2 = meanb;
	x = 0;
	for( ; x <= len - 4; x += 4){
		dst[x] = src[x] * tmp[x] + tmp2[x];
		dst[x + 1] = src[x + 1] * tmp[x + 1] + tmp2[x + 1];
		dst[x + 2] = src[x + 2] * tmp[x + 2] + tmp2[x + 2];
		dst[x + 3] = src[x + 3] * tmp[x + 3] + tmp2[x + 3];
	}
	for( ; x < len; ++x)
		dst[x] = src[x] * tmp[x] + tmp2[x];
	FreeMemory(tmp);
	FreeMemory(tmp1);
	FreeMemory(tmp2);
}
static void rgb2Y(const uchar* Src, float* Dst, int Width, int Height, int srcCn)
{
	int srcStep = WIDTHBYTES(Width * srcCn);
	int dstStep = WIDTHBYTES(Width);
	for(int i = 0; i < Height; ++i){
		int srcPos = i * srcStep;
		int dstPos = i * dstStep;
		for(int j = 0; j < Width; ++j){
			Dst[dstPos + j] = YCbCrYRF * Src[srcPos + srcCn * j + 2] + YCbCrYGF * Src[srcPos + srcCn * j + 1] + YCbCrYBF * Src[srcPos + srcCn * j];
		}
	}
}
static void Y2rgb(const uchar* Src, float* Gain_map, uchar* Dst, int Width, int Height, int srcCn)
{
	int step = WIDTHBYTES(Width * srcCn);
	int mStep = WIDTHBYTES(Width);
	for(int i = 0; i < Height; ++i){
		int pos = i * step;
		int mPos = i * mStep;
		for(int j = 0; j < Width; ++j){
			int idx = srcCn * j;
			float gain = Gain_map[mPos + j];
			Dst[pos + idx] = clampToByte(int(Src[pos + idx] * gain));
			Dst[pos + idx + 1] = clampToByte(int(Src[pos + idx + 1] * gain));
			Dst[pos + idx + 2] = clampToByte(int(Src[pos + idx + 2] * gain));
		}
	}
}
static void Log(float* Src, float* Dst, int width, int height, int channel, float EPS)
{
	int Length = WIDTHBYTES(width * channel)* height, i = 0;
	for(i = 0; i <= Length - 4; i += 4){
		Dst[i] = log(Src[i] + EPS);
		Dst[i + 1] = log(Src[i + 1] + EPS);
		Dst[i + 2] = log(Src[i + 2] + EPS);
		Dst[i + 3] = log(Src[i + 3] + EPS);
	}
	for( ; i < Length; ++i)
		Dst[i] = log(Src[i] + EPS);
}
static void Exp(float* Src, float* Dst, int width, int height, int channel)
{
	int Length = WIDTHBYTES(width * channel)* height, i = 0;
	for(i = 0; i <= Length - 4; i += 4){
		Dst[i] = exp(Src[i]);
		Dst[i + 1] = exp(Src[i + 1]);
		Dst[i + 2] = exp(Src[i + 2]);
		Dst[i + 3] = exp(Src[i + 3]);
	}
	for( ; i < Length; ++i)
		Dst[i] = exp(Src[i]);
}
static void compressed(float* Src1, float Src2, float* Dst, int width, int height, int channel, float C)
{
	int Length = WIDTHBYTES(width * channel)* height, i = 0;
	for(i = 0; i <= Length - 4; i += 4){
		Dst[i] = C * (Src1[i] - Src2) + Src2;
		Dst[i + 1] = C * (Src1[i + 1] - Src2) + Src2;
		Dst[i + 2] = C * (Src1[i + 2] - Src2) + Src2;
		Dst[i + 3] = C * (Src1[i + 3] - Src2) + Src2;
	}
	for( ; i < Length; ++i)
		Dst[i] = C * (Src1[i] - Src2) + Src2;
}
/*
** karl storz Patent: Endoscopic video system with dynamic contrast and detail enhancement
*/
static void test(float* Src, uchar* Dst, int Width, int Height, int srcCn)
{
	int dststep = WIDTHBYTES(Width * srcCn);
	int mStep = WIDTHBYTES(Width);
	for(int i = 0; i < Height; ++i){
		int pos = i * dststep;
		int mPos = i * mStep;
		for(int j = 0; j < Width; ++j){
			int idx = srcCn * j;
			Dst[pos + idx] = Src[mPos + j];
			Dst[pos + idx + 1] = Src[mPos + j];
			Dst[pos + idx + 2] = Src[mPos + j];
		}
	}
}
extern "C" _declspec(dllexport) void clara(const uchar* Src, uchar* Dst, int Width, int Height, float Eps, int Radius, int srcCn, int SetPoint, float C)
{
	if(Src == NULL) return ;
	float *Y, *LogY, *Base_image, *Detail_image, *Base_image_compressed, *En_light_image, *Gain_map, logSP, EPS;
	int Len, srcStep, Channel, srcLen, step;
	EPS						= 0.001;
	Channel					= 1;
	logSP					= std::log((double)SetPoint);
	srcStep					= WIDTHBYTES(Width * srcCn);	
	step					= WIDTHBYTES(Width * Channel);
	Len						= step * Height;
	srcLen					= srcStep * Height;
	Y						= (float*)AllocMemory(Len * sizeof(float), false);
	LogY					= (float*)AllocMemory(Len * sizeof(float), false);
	Base_image				= (float*)AllocMemory(Len * sizeof(float), false);
	Base_image_compressed	= Base_image;
	Detail_image			= Y;
	En_light_image			= Y;
	Gain_map				= Y;
	rgb2Y(Src, Y, Width, Height, srcCn);
	Log(Y, LogY, Width, Height, Channel, EPS);
	guideSmooth(Y, Base_image, Width, Height, Channel, Radius, Eps);
	//test(Base_image, Dst, Width, Height, srcCn);
	Log(Base_image, Base_image, Width, Height, Channel, EPS);

	Sub(LogY, Base_image, Detail_image, Width, Height, Channel);
		
	compressed(Base_image, logSP, Base_image_compressed, Width, Height, Channel, C);
	
	Add(Base_image_compressed, Detail_image, En_light_image, Width, Height, Channel);

	Sub(En_light_image, LogY, Gain_map, Width, Height, Channel);
	
	Exp(Gain_map, Gain_map, Width, Height, Channel);
	Y2rgb(Src, Gain_map, Dst, Width, Height, srcCn);

	FreeMemory(Y);
	FreeMemory(LogY);
	FreeMemory(Base_image);
	//FreeMemory(Detail_image);
}


