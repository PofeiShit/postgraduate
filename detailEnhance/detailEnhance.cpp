// detailEnhance.cpp : 定义 DLL 应用程序的导出函数。
//

#include "stdafx.h"

// guidedFilterDll.cpp : 定义 DLL 应用程序的导出函数。
//
// 内窥镜专用，针对1280 * 800  3或者4通道图像
#include "stdafx.h"
#include <emmintrin.h>
#include <iostream>
#include <assert.h>
#include <stdlib.h>
#include <Windows.h>
#include <process.h>
static const int SCALE = 4;
#define SSE2 1
typedef unsigned char uchar;
struct threadPack
{
	const uchar* input;
	uchar* output;
	float* upA;
	float* upB;
	float lamda;
	int len;
	int idx;
	int cn;
};
static char logFile[64];
static const int MAX_ESIZE = 16;
static HANDLE hShowThread;
CRITICAL_SECTION g_cs;
static const int Thread_Num = 4;
static inline size_t alignSize(size_t sz, int n)
{
    assert((n & (n - 1)) == 0); // n is a power of 2
    return (sz + n-1) & -n;
}
static inline int clip(int x, int a, int b)
{
	return x >= a ? (x < b ? x : b - 1) : a;
}
static inline uchar clampToByte(int Value)
{
	return (uchar)((Value | ((int)(255 - Value) >> 31) ) & ~((int)Value >> 31));
}
static void Add(const float* src1, const float* src2, float* dst, int width, int height, int channel){
	int len = width * height * channel, i = 0;
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
	int len = width * height * channel, i = 0;
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
	int len = width * height * channel, i = 0;
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
	int len = width * height * channel, i = 0;
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
	int X, Y, Z, Size, Index, SumStep, LineWidth;
	float Value, ValueB, ValueG, ValueR;
	int *RowPos, *ColPos;
	float *ColSum, *Diff;

	Size = 2 * Radius + 1;
	float Scale = 1.0 / (Size * Size);
	SumStep = Width * Channel * sizeof(float);
	LineWidth = Width * Channel;
	int Amount = Size * Size;

	RowPos = GetExpandPos(Width, Radius, Radius);
	ColPos = GetExpandPos(Height, Radius, Radius);
	ColSum = (float *)AllocMemory(Width * Channel * sizeof(float), true);
	Diff   = (float *)AllocMemory((Width - 1) * Channel * sizeof(float), true);
	float *RowData = (float *)AllocMemory((Width + 2 * Radius) * Channel * sizeof(float), true);
	float* Sum = (float*)AllocMemory(Height * SumStep, false);

	for (Y = 0; Y < Height; Y++)					//	水平方向的耗时比垂直方向上的大
	{
		float *LinePS = (float*)(Src + Y * LineWidth);
		float *LinePD = Sum + Y * LineWidth;

		//	拷贝一行数据及边缘部分部分到临时的缓冲区中
		if (Channel == 1)
		{
			for (X = 0; X < Radius; X++)				
				RowData[X] = LinePS[RowPos[X]];						
			memcpy(RowData + Radius, LinePS, Width);																						
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
		}
		else if (Channel == 4)
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
static void upResizeTwo(const float *src1, const float *src2, float *dst1, float *dst2, int swidth, int sheight, int dwidth, int dheight, int cn)
{
	int srcStep = swidth * cn;
	int dstStep = dwidth * cn;
	int ksize = 2, ksize2 = 1, xmin = 0, xmax = dwidth, width = dwidth * cn;
	uchar *_buffer = (uchar*)malloc((width + dheight) * (sizeof(int) + sizeof(float) * ksize));
	int* xofs = (int*)_buffer;
	int* yofs = xofs + width;
	float* alpha = (float*)(yofs + dheight);
	float* beta = alpha + width * ksize;
	float cbuf[MAX_ESIZE];
	int k, sx, sy, dx, dy;
	float fx, fy;
	double scale_x = 0.25;//(double)swidth / dwidth;
	double scale_y = 0.25;//(double)sheight / dheight;

	for( dx = 0; dx < dwidth; dx++ ){
		fx = (float)((dx + 0.5) * scale_x - 0.5);
		sx = std::floor(fx);
		fx -= sx;
		if(sx < ksize2 - 1){
			xmin = dx + 1;
			if(sx < 0)
				fx = 0, sx = 0;
		}
		if(sx + ksize2 >= swidth){
			xmax = min(xmax, dx);
			if(sx >= swidth - 1)
				fx = 0, sx = swidth - 1;
		}
		for(k = 0, sx *= cn; k < cn; k++)
			xofs[dx * cn + k] = sx + k;
		cbuf[0] = 1.f - fx;
		cbuf[1] = fx;

		for(k = 0; k < ksize; k++)
			alpha[dx * cn * ksize + k] = cbuf[k];
		for( ; k < cn * ksize; k++)
			alpha[dx * cn * ksize + k] = alpha[dx * cn * ksize + k - ksize];
	}
	for( dy = 0; dy < dheight; dy++ ){
		fy = (float)((dy + 0.5) * scale_y - 0.5);
		sy = std::floor(fy);
		fy -= sy;
		
		yofs[dy] = sy;

		cbuf[0] = 1.f - fy;
		cbuf[1] = fy;

		for(k = 0; k < ksize; k++)
			beta[dy * ksize + k] = cbuf[k];
	}
	xmin *= cn;
	xmax *= cn;
	swidth *= cn;
	dwidth *= cn;
	
	int bufstep = (int)alignSize(dwidth, 16);
	float* _Buffer = (float*)malloc(bufstep * ksize * sizeof(float));
	float* _Buffer1 = (float*)malloc(bufstep * ksize * sizeof(float));
	const float* srows[MAX_ESIZE] = {0};
	const float* srows1[MAX_ESIZE] = {0};
	float* rows[MAX_ESIZE] = {0};
	float* rows1[MAX_ESIZE] = {0};
	int prev_sy[MAX_ESIZE];

	for(k = 0; k < ksize; k++){
		prev_sy[k] = -1;
		rows[k] = _Buffer + bufstep * k;
		rows1[k] = _Buffer1 + bufstep * k;
	}

	const float* Beta = beta;
	for(dy = 0; dy < dheight; dy++, Beta += ksize){
		int sy0 = yofs[dy], k0 = ksize, k1 = 0;
		for(k = 0; k < ksize; k++){
			int sy = clip(sy0 + k, 0, sheight);
			for(k1 = max(k1, k); k1 < ksize; k1++){
				if(sy == prev_sy[k1]){
					if(k1 > k){
						memcpy(rows[k], rows[k1], bufstep * sizeof(rows[0][0]));
						memcpy(rows1[k], rows1[k1], bufstep * sizeof(rows1[0][0]));
					}
					break;
				}
			}
			if(k1 == ksize)
				k0 = min(k0, k);
			srows[k] = (float*)(src1 + srcStep * sy);
			srows1[k] = (float*)(src2 + srcStep * sy);
			prev_sy[k] = sy;
		}
		if(k0 < ksize){			
			int count = ksize - k0;
			//行方向
			for(k = 0; k <= count - 2; k++){
				const float *S0 = srows[k0 + k], *S1 = srows[k0 + k + 1];
				const float *S10 = srows1[k0 + k], *S11 = srows1[k0 + k + 1];
				float *D0 = rows[k0 + k], *D1 = rows[k0 + k + 1];
				float *D10 = rows1[k0 + k], *D11 = rows1[k0 + k + 1];
				for(dx = 0; dx < xmax; dx++){
					sx = xofs[dx];
					float a0 = alpha[dx * 2], a1 = alpha[dx * 2 + 1];
					float t0 = S0[sx] * a0 + S1[sx + cn] * a1;
					float t1 = S1[sx] * a0 + S0[sx + cn] * a1;
					D0[dx] = t0; D1[dx] = t1;

					float t10 = S10[sx] * a0 + S11[sx + cn] * a1;
					float t11 = S11[sx] * a0 + S10[sx + cn] * a1;
					D10[dx] = t10; D11[dx] = t11;
				}
				for( ; dx < dwidth; dx++){
					sx = xofs[dx];
					D0[dx] = S0[sx];
					D1[dx] = S1[sx];
					D10[dx] = S10[sx];
					D11[dx] = S11[sx];
				}
			}
			for( ; k < count; k++){
				const float *S = srows[k0 + k];
				const float *S1 = srows1[k0 + k];
				float* D = rows[k0 + k];
				float* D1 = rows1[k0 + k];
				for(dx = 0; dx < xmax; dx++){
					sx = xofs[dx];
					D[dx] = S[sx] * alpha[dx * 2] + S[sx + cn] * alpha[dx * 2 + 1];
					D1[dx] = S1[sx] * alpha[dx * 2] + S1[sx + cn] * alpha[dx * 2 + 1];
				}
				for( ; dx < dwidth; dx++){
					sx = xofs[dx];
					D[dx] = S[sx];
					D1[dx] = S1[sx];
				}
			}
		}
		//列方向
		
		const float *S0 = rows[0], *S1 = rows[1];
		const float *S10 = rows1[0], *S11 = rows1[1];
		float *Dst1 = (float*)(dst1 + dstStep * dy);
		float *Dst2 = (float*)(dst2 + dstStep * dy);
		int x = 0;
#if SSE2
		__m128 b0 = _mm_set1_ps(Beta[0]), b1 = _mm_set1_ps(Beta[1]);
		
		if( ((((size_t)S0 | (size_t)S1) & 15) == 0) && ((((size_t)S10 | (size_t)S11) & 15) == 0) ){
			for( ; x <= width - 8; x += 8){
				__m128 x0, x1, y0, y1, x10, x11, y10, y11;
				x0 = _mm_load_ps(S0 + x);
				x1 = _mm_load_ps(S0 + x + 4);
				y0 = _mm_load_ps(S1 + x);
				y1 = _mm_load_ps(S1 + x + 4);

				x0 = _mm_add_ps(_mm_mul_ps(x0, b0), _mm_mul_ps(y0, b1));
				x1 = _mm_add_ps(_mm_mul_ps(x1, b0), _mm_mul_ps(y1, b1));

				_mm_storeu_ps( Dst1 + x, x0);
				_mm_storeu_ps( Dst1 + x + 4, x1);

				x10 = _mm_load_ps(S10 + x);
				x11 = _mm_load_ps(S10 + x + 4);
				y10 = _mm_load_ps(S11 + x);
				y11 = _mm_load_ps(S11 + x + 4);

				x10 = _mm_add_ps(_mm_mul_ps(x10, b0), _mm_mul_ps(y10, b1));
				x11 = _mm_add_ps(_mm_mul_ps(x11, b0), _mm_mul_ps(y11, b1));

				_mm_storeu_ps( Dst2 + x, x10);
				_mm_storeu_ps( Dst2 + x + 4, x11);
			}
		} else {
			for( ; x <= width - 8; x += 8 )
			{
				__m128 x0, x1, y0, y1, x10, x11, y10, y11;
				x0 = _mm_loadu_ps(S0 + x);
				x1 = _mm_loadu_ps(S0 + x + 4);
				y0 = _mm_loadu_ps(S1 + x);
				y1 = _mm_loadu_ps(S1 + x + 4);

				x0 = _mm_add_ps(_mm_mul_ps(x0, b0), _mm_mul_ps(y0, b1));
				x1 = _mm_add_ps(_mm_mul_ps(x1, b0), _mm_mul_ps(y1, b1));

				_mm_storeu_ps( Dst1 + x, x0);
				_mm_storeu_ps( Dst1 + x + 4, x1);

				x10 = _mm_loadu_ps(S10 + x);
				x11 = _mm_loadu_ps(S10 + x + 4);
				y10 = _mm_loadu_ps(S11 + x);
				y11 = _mm_loadu_ps(S11 + x + 4);

				x10 = _mm_add_ps(_mm_mul_ps(x10, b0), _mm_mul_ps(y10, b1));
				x11 = _mm_add_ps(_mm_mul_ps(x11, b0), _mm_mul_ps(y11, b1));

				_mm_storeu_ps( Dst2 + x, x10);
				_mm_storeu_ps( Dst2 + x + 4, x11);
			}
		}
#endif
	}
	free(_Buffer);
	free(_Buffer1);
	free(_buffer);
}
/* 
 * src : 原图像
 * src1 : upA
 * src2 : upB
 * dst : 目标图像
 */
static void upResize(const uchar* src, float* src1, float* src2, uchar* dst, int swidth, int sheight, int dwidth, int dheight, int cn, float lamda)
{
	int srcStep = swidth * cn;
	int dstStep = dwidth * cn;
	int ksize = 2, ksize2 = 1, xmin = 0, xmax = dwidth, width = dwidth * cn;
	uchar *_buffer = (uchar*)malloc((width + dheight) * (sizeof(int) + sizeof(float) * ksize));
	int* xofs = (int*)_buffer;
	int* yofs = xofs + width;
	float* alpha = (float*)(yofs + dheight);
	float* beta = alpha + width * ksize;
	float cbuf[MAX_ESIZE];
	int k, sx, sy, dx, dy;
	float fx, fy;
	double scale_x = 0.25;//(double)swidth / dwidth;
	double scale_y = 0.25;//(double)sheight / dheight;

	for( dx = 0; dx < dwidth; dx++ ){
		fx = (float)((dx + 0.5) * scale_x - 0.5);
		sx = std::floor(fx);
		fx -= sx;
		if(sx < ksize2 - 1){
			xmin = dx + 1;
			if(sx < 0)
				fx = 0, sx = 0;
		}
		if(sx + ksize2 >= swidth){
			xmax = min(xmax, dx);
			if(sx >= swidth - 1)
				fx = 0, sx = swidth - 1;
		}
		for(k = 0, sx *= cn; k < cn; k++)
			xofs[dx * cn + k] = sx + k;
		cbuf[0] = 1.f - fx;
		cbuf[1] = fx;

		for(k = 0; k < ksize; k++)
			alpha[dx * cn * ksize + k] = cbuf[k];
		for( ; k < cn * ksize; k++)
			alpha[dx * cn * ksize + k] = alpha[dx * cn * ksize + k - ksize];
	}
	for( dy = 0; dy < dheight; dy++ ){
		fy = (float)((dy + 0.5) * scale_y - 0.5);
		sy = std::floor(fy);
		fy -= sy;
		
		yofs[dy] = sy;

		cbuf[0] = 1.f - fy;
		cbuf[1] = fy;

		for(k = 0; k < ksize; k++)
			beta[dy * ksize + k] = cbuf[k];
	}
	xmin *= cn;
	xmax *= cn;
	swidth *= cn;
	dwidth *= cn;
	
	int bufstep = (int)alignSize(dwidth, 16);
	//float* _Buffer = (float*)malloc(bufstep * ksize * sizeof(float));
	float* _Buffer = (float*)AllocMemory(bufstep * ksize * sizeof(float), false);
	float* _Buffer1 = (float*)AllocMemory(bufstep * ksize * sizeof(float), false);
	//float* _Buffer1 = (float*)malloc(bufstep * ksize * sizeof(float));
	const float* srows[MAX_ESIZE] = {0};
	const float* srows1[MAX_ESIZE] = {0};
	float* rows[MAX_ESIZE] = {0};
	float* rows1[MAX_ESIZE] = {0};
	int prev_sy[MAX_ESIZE];

	for(k = 0; k < ksize; k++){
		prev_sy[k] = -1;
		rows[k] = _Buffer + bufstep * k;
		rows1[k] = _Buffer1 + bufstep * k;
	}

	const float* Beta = beta;

	for(dy = 0; dy < dheight; dy++, Beta += ksize){
		int sy0 = yofs[dy], k0 = ksize, k1 = 0;
		for(k = 0; k < ksize; k++){
			int sy = clip(sy0 + k, 0, sheight);
			for(k1 = max(k1, k); k1 < ksize; k1++){
				if(sy == prev_sy[k1]){
					if(k1 > k){
						memcpy(rows[k], rows[k1], bufstep * sizeof(rows[0][0]));
						memcpy(rows1[k], rows1[k1], bufstep * sizeof(rows1[0][0]));
					}
					break;
				}
			}
			if(k1 == ksize)
				k0 = min(k0, k);
			srows[k] = (float*)(src1 + srcStep * sy);
			srows1[k] = (float*)(src2 + srcStep * sy);
			prev_sy[k] = sy;
		}
		if(k0 < ksize){			
			int count = ksize - k0;
			//行方向
			for(k = 0; k <= count - 2; k++){
				const float *S0 = srows[k0 + k], *S1 = srows[k0 + k + 1];
				const float *S10 = srows1[k0 + k], *S11 = srows1[k0 + k + 1];
				float *D0 = rows[k0 + k], *D1 = rows[k0 + k + 1];
				float *D10 = rows1[k0 + k], *D11 = rows1[k0 + k + 1];
				for(dx = 0; dx < xmax; dx++){
					sx = xofs[dx];
					float a0 = alpha[dx * 2], a1 = alpha[dx * 2 + 1];
					float t0 = S0[sx] * a0 + S1[sx + cn] * a1;
					float t1 = S1[sx] * a0 + S0[sx + cn] * a1;
					D0[dx] = t0; D1[dx] = t1;

					float t10 = S10[sx] * a0 + S11[sx + cn] * a1;
					float t11 = S11[sx] * a0 + S10[sx + cn] * a1;
					D10[dx] = t10; D11[dx] = t11;
				}
				for( ; dx < dwidth; dx++){
					sx = xofs[dx];
					D0[dx] = S0[sx];
					D1[dx] = S1[sx];
					D10[dx] = S10[sx];
					D11[dx] = S11[sx];
				}
			}
			for( ; k < count; k++){
				const float *S = srows[k0 + k];
				const float *S1 = srows1[k0 + k];
				float* D = rows[k0 + k];
				float* D1 = rows1[k0 + k];
				for(dx = 0; dx < xmax; dx++){
					sx = xofs[dx];
					D[dx] = S[sx] * alpha[dx * 2] + S[sx + cn] * alpha[dx * 2 + 1];
					D1[dx] = S1[sx] * alpha[dx * 2] + S1[sx + cn] * alpha[dx * 2 + 1];
				}
				for( ; dx < dwidth; dx++){
					sx = xofs[dx];
					D[dx] = S[sx];
					D1[dx] = S1[sx];
				}
			}
		}
		//列方向
		const float *S0 = rows[0], *S1 = rows[1];
		const float *S10 = rows1[0], *S11 = rows1[1];
		const uchar *Src  = src + dstStep * dy;
		uchar *Dst = dst + dstStep * dy;
		int x = 0;

		__m128 b0 = _mm_set1_ps(Beta[0]), b1 = _mm_set1_ps(Beta[1]);
		__m128 L = _mm_set_ps(0.0f, 1.0f, lamda, lamda);
		if( ((((size_t)S0 | (size_t)S1) & 15) == 0) && ((((size_t)S10 | (size_t)S11) & 15) == 0) ){
			for( ; x <= width - 8; x += 8){ // width = dwidth * cn
				__m128 x0, x1, y0, y1, x10, x11, y10, y11, s0, s1;
				s0 = _mm_set_ps(float(Src[x + 3]), float(Src[x + 2]), float(Src[x + 1]), float(Src[x]));
				s1 = _mm_set_ps(float(Src[x + 7]), float(Src[x + 6]), float(Src[x + 5]), float(Src[x + 4]));

				/* upA */
				x0 = _mm_load_ps(S0 + x);
				x1 = _mm_load_ps(S0 + x + 4);
				y0 = _mm_load_ps(S1 + x);
				y1 = _mm_load_ps(S1 + x + 4);

				x0 = _mm_add_ps(_mm_mul_ps(x0, b0), _mm_mul_ps(y0, b1));
				x1 = _mm_add_ps(_mm_mul_ps(x1, b0), _mm_mul_ps(y1, b1));

				/* upB */
				x10 = _mm_load_ps(S10 + x);
				x11 = _mm_load_ps(S10 + x + 4);
				y10 = _mm_load_ps(S11 + x);
				y11 = _mm_load_ps(S11 + x + 4);

				x10 = _mm_add_ps(_mm_mul_ps(x10, b0), _mm_mul_ps(y10, b1));
				x11 = _mm_add_ps(_mm_mul_ps(x11, b0), _mm_mul_ps(y11, b1));
				
				y0 = _mm_add_ps(_mm_mul_ps(x0, s0), x10);
				y1 = _mm_add_ps(_mm_mul_ps(x1, s1), x11);

				x0 = _mm_mul_ps(_mm_sub_ps(s0, y0), L);
				x1 = _mm_mul_ps(_mm_sub_ps(s1, y1), L);
				
				y0 = _mm_add_ps(x0, y0);
				y1 = _mm_add_ps(x1, y1);
				Dst[x + 0] = clampToByte(int(y0.m128_f32[0]));
				Dst[x + 1] = clampToByte(int(y0.m128_f32[1]));
				Dst[x + 2] = clampToByte(int(y0.m128_f32[2]));
				//Dst[x + 3] = clampToByte(int(y0.m128_f32[3]));
				Dst[x + 4] = clampToByte(int(y1.m128_f32[0]));
				Dst[x + 5] = clampToByte(int(y1.m128_f32[1]));
				Dst[x + 6] = clampToByte(int(y1.m128_f32[2]));
				//Dst[x + 7] = clampToByte(int(y1.m128_f32[3]));
				//_mm_storeu_ps( Dst + x, x0);
				//_mm_storeu_ps( Dst + x + 4, x1);
				//Dst[x + 0] = clampToByte(int(x0.m128_f32[0] * Src[x + 0] + x10.m128_f32[0]));
				//Dst[x + 1] = clampToByte(int(x0.m128_f32[1] * Src[x + 1] + x10.m128_f32[1]));
				//Dst[x + 2] = clampToByte(int(x0.m128_f32[2] * Src[x + 2] + x10.m128_f32[2]));
				//Dst[x + 3] = clampToByte(int(x0.m128_f32[3] * Src[x + 3] + x10.m128_f32[3]));
				//Dst[x + 4] = clampToByte(int(x1.m128_f32[0] * Src[x + 4] + x11.m128_f32[0]));
				//Dst[x + 5] = clampToByte(int(x1.m128_f32[1] * Src[x + 5] + x11.m128_f32[1]));
				//Dst[x + 6] = clampToByte(int(x1.m128_f32[2] * Src[x + 6] + x11.m128_f32[2]));
				//Dst[x + 7] = clampToByte(int(x1.m128_f32[3] * Src[x + 7] + x11.m128_f32[3]));
			}
		} else {
			for( ; x <= width - 8; x += 8 )
			{
				__m128 x0, x1, y0, y1, x10, x11, y10, y11, s0, s1;
				s0 = _mm_set_ps(float(Src[x + 3]), float(Src[x + 2]), float(Src[x + 1]), float(Src[x]));
				s1 = _mm_set_ps(float(Src[x + 7]), float(Src[x + 6]), float(Src[x + 5]), float(Src[x + 4]));

				x0 = _mm_loadu_ps(S0 + x);
				x1 = _mm_loadu_ps(S0 + x + 4);
				y0 = _mm_loadu_ps(S1 + x);
				y1 = _mm_loadu_ps(S1 + x + 4);

				x0 = _mm_add_ps(_mm_mul_ps(x0, b0), _mm_mul_ps(y0, b1));
				x1 = _mm_add_ps(_mm_mul_ps(x1, b0), _mm_mul_ps(y1, b1));

				x10 = _mm_loadu_ps(S10 + x);
				x11 = _mm_loadu_ps(S10 + x + 4);
				y10 = _mm_loadu_ps(S11 + x);
				y11 = _mm_loadu_ps(S11 + x + 4);

				x10 = _mm_add_ps(_mm_mul_ps(x10, b0), _mm_mul_ps(y10, b1));
				x11 = _mm_add_ps(_mm_mul_ps(x11, b0), _mm_mul_ps(y11, b1));

				y0 = _mm_add_ps(_mm_mul_ps(x0, s0), x10);
				y1 = _mm_add_ps(_mm_mul_ps(x1, s1), x11);
				Dst[x + 0] = clampToByte(int(y0.m128_f32[0]));
				Dst[x + 1] = clampToByte(int(y0.m128_f32[1]));
				Dst[x + 2] = clampToByte(int(y0.m128_f32[2]));
				//Dst[x + 3] = clampToByte(int(y0.m128_f32[3]));
				Dst[x + 4] = clampToByte(int(y1.m128_f32[0]));
				Dst[x + 5] = clampToByte(int(y1.m128_f32[1]));
				Dst[x + 6] = clampToByte(int(y1.m128_f32[2]));
				//Dst[x + 7] = clampToByte(int(y1.m128_f32[3]));
				//_mm_storeu_ps( Dst + x, x0);
				//_mm_storeu_ps( Dst + x + 4, x1);
				//Dst[x + 0] = clampToByte(int(x0.m128_f32[0] * Src[x + 0] + x10.m128_f32[0]));
				//Dst[x + 1] = clampToByte(int(x0.m128_f32[1] * Src[x + 1] + x10.m128_f32[1]));
				//Dst[x + 2] = clampToByte(int(x0.m128_f32[2] * Src[x + 2] + x10.m128_f32[2]));
				//Dst[x + 3] = clampToByte(int(x0.m128_f32[3] * Src[x + 3] + x10.m128_f32[3]));
				//Dst[x + 4] = clampToByte(int(x1.m128_f32[0] * Src[x + 4] + x11.m128_f32[0]));
				//Dst[x + 5] = clampToByte(int(x1.m128_f32[1] * Src[x + 5] + x11.m128_f32[1]));
				//Dst[x + 6] = clampToByte(int(x1.m128_f32[2] * Src[x + 6] + x11.m128_f32[2]));
				//Dst[x + 7] = clampToByte(int(x1.m128_f32[3] * Src[x + 7] + x11.m128_f32[3]));
			}
		}

	}						  
	 
	FreeMemory(_Buffer);
	FreeMemory(_Buffer1);
	//free(_Buffer);
	//free(_Buffer1);
	free(_buffer);
					  	
}
void writeLog(const char* str, const char* file, int line)
{
	SYSTEMTIME time = {0};
	GetLocalTime(&time);
	char localTime[32];
	sprintf(localTime, "%d-%02d-%02d[%02d:%02d:%02d]", 
		time.wYear,
		time.wMonth,
		time.wDay,
		time.wHour,
		time.wMinute,
		time.wSecond);


	char filename[64];
	sprintf(filename, "LOG%s.txt", logFile);
	FILE* log = fopen(filename, "a+");
	if (NULL == log)
	{
		return;
	}

	char logMessage[128];
	sprintf(logMessage, "%s[%s:%d] %s\n", localTime, file, line, str);

	int m = strlen(logMessage);
	fwrite(logMessage, 1, m, log);
	fclose(log);
}
unsigned int WINAPI showThread(LPVOID pM)
{
	//EnterCriticalSection(&g_cs);
	struct threadPack TPack = *((struct threadPack*)pM);
	//LeaveCriticalSection(&g_cs);
	SetEvent(hShowThread); //触发事件
	int idx = TPack.idx;
	int len = TPack.len;
	float lamda = TPack.lamda;
	const uchar* src = TPack.input + len * idx;
	uchar* dst = TPack.output + len * idx;
	float* upA = TPack.upA + len * idx;
	float* upB = TPack.upB + len * idx;
	int cn = TPack.cn;
	//char log[32];
	//sprintf(log, "正在计算第%d部分", idx);
	//writeLog(log, __FILE__, __LINE__);
	for(int x = 0; x < len; x += cn){
		dst[x] = clampToByte(src[x] * lamda + (src[x] * upA[x] + upB[x]) * (1 - lamda));
		dst[x + 1] = clampToByte(src[x + 1] * lamda + (src[x + 1] * upA[x + 1] + upB[x + 1]) * (1 - lamda));
		dst[x + 2] = src[x + 2];

		//dst[x + 4] = clampToByte(src[x + 4] * lamda + (src[x + 4] * upA[x + 4] + upB[x + 4]) * (1 - lamda));
		//dst[x + 5] = clampToByte(src[x + 5] * lamda + (src[x + 5] * upA[x + 5] + upB[x + 5]) * (1 - lamda));
		//dst[x + 6] = src[x + 6];

		//dst[x + 8] = clampToByte(src[x + 8] * lamda + (src[x + 8] * upA[x + 8] + upB[x + 8]) * (1 - lamda));
		//dst[x + 9] = clampToByte(src[x + 9] * lamda + (src[x + 9] * upA[x + 9] + upB[x + 9]) * (1 - lamda));
		//dst[x + 10] = src[x + 10];


		//dst[x + 12] = clampToByte(src[x + 12] * lamda + (src[x + 12] * upA[x + 12] + upB[x + 12]) * (1 - lamda));
		//dst[x + 13] = clampToByte(src[x + 13] * lamda + (src[x + 13] * upA[x + 13] + upB[x + 13]) * (1 - lamda));
		//dst[x + 14] = src[x + 14];
	}
	return 0;
}
extern "C" __declspec(dllexport) void detailEnhance(const uchar* src, uchar* dst, int width, int height, int cn, int radius, float eps, float lamda, float *t)
{
	/* 40ms */
	int x, y, pix_size, dwidth, dheight, srcStep, IStep, dstStep, *x_ofs, len;
	float *I, *tmp, *tmp1, *tmp2;
	LARGE_INTEGER freq;  
    LARGE_INTEGER start_t, stop_t;  
	QueryPerformanceFrequency(&freq);  
	/////////////////////下采样//////////////////////////

	/* 1ms */
			
	double ifx, ify;
	pix_size = cn;
	eps		 = eps * 255 * 255;
	ifx		 = SCALE;
	ify		 = SCALE;
	dwidth   = width >> 2;
	dheight  = height >> 2;
	I		 = (float*)AllocMemory(dheight * dwidth * cn * sizeof(float), false);
	tmp		 = (float*)AllocMemory(dheight * dwidth * cn * sizeof(float), false);
	tmp1	 = (float*)AllocMemory(dheight * dwidth * cn * sizeof(float), false);
	tmp2	 = (float*)AllocMemory(dheight * dwidth * cn * sizeof(float), false);
	//upA      = (float*)AllocMemory(height * width * cn * sizeof(float), false);
	//upB      = (float*)AllocMemory(height * width * cn * sizeof(float), false);
	if(NULL == I) return ;
	x_ofs    = (int*)malloc(dwidth * sizeof(int));
	if(NULL == x_ofs) return ;
	dstStep  = srcStep  = width * cn;
	IStep    = dwidth * cn;
	len		 = width * height * cn;
	for(x = 0; x < dwidth; ++x){
		int sx = std::floor(x * ifx);
		x_ofs[x] = min(sx, width - 1) * pix_size;
	}
	for(y = 0; y < dheight; ++y){
		float* D       = (float*)(I + IStep * y);
		int sy         = min(std::floor(y * ify), height - 1);
		const uchar* S = src + srcStep * sy;
		for(x = 0; x < dwidth; ++x, D += cn){
			const uchar* _tS = S + x_ofs[x];
			D[0] = float(_tS[0]); D[1] = float(_tS[1]); D[2] = float(_tS[2]);
		}
	}
					/* 10ms */
	/////////////////////////guided filter/////////////////////////////
	BoxFilter(I, tmp, dwidth, dheight, cn, radius); // meanI = fmean(I, r); tmp = meanI;
	Mul(I, I, tmp1, dwidth, dheight, cn); // tmp1 = I .* I;
	BoxFilter(tmp1, tmp2, dwidth, dheight, cn, radius);	//corrI = fmean(I.*I, r); tmp2 = corrI;
	Mul(tmp, tmp, tmp1, dwidth, dheight, cn);// tmp1 = meanI .* meanI;			
	Sub(tmp2, tmp1, tmp2, dwidth, dheight, cn); // varI = corrI - meanI .* meanI; tmp2 = varI;
	Div(tmp2, tmp2, tmp2, eps, dwidth, dheight, cn); // a = covIp ./ (varI + eps) tmp2 = a; covIp = varI;
	Mul(tmp2, tmp, tmp1, dwidth, dheight, cn); // tmp1 = a .* meanI;
	Sub(tmp, tmp1, tmp1, dwidth, dheight, cn); // b = meanI - a .* meanI; tmp1 = b;
	BoxFilter(tmp2, tmp, dwidth, dheight, cn, radius);  // meana = fmean(a, r); tmp = meana;
	BoxFilter(tmp1, tmp2, dwidth, dheight, cn, radius); // meanb = fmean(b, r); tmp2 = meanb;



	/* 8ms */
	upResize(src, tmp, tmp2, dst, dwidth, dheight, width, height, cn, lamda);

	//upResizeTwo(tmp, tmp2, upA, upB, dwidth, dheight, width, height, cn);
	//for(x = 0 ; x < len; x++){
	//	dst[x] = clampToByte(src[x] * upA[x] + upB[x]);
	//}
	//HANDLE hThread[Thread_Num];
	//struct threadPack TPack;
	//TPack.lamda = lamda;
	//int i = 0;
	//int Step = srcStep * height / Thread_Num;
	//TPack.len = Step;
	//TPack.input = src; TPack.output = dst; TPack.upA = upA; TPack.upB = upB; 
	//TPack.cn = cn;
	//hShowThread = CreateEvent(NULL, FALSE, FALSE, NULL);  
	//while(i < Thread_Num){
	//	TPack.idx = i;
	//	hThread[i] = (HANDLE)_beginthreadex(NULL, 0, showThread, &TPack, 0, NULL);
	//	WaitForSingleObject(hShowThread, INFINITE); //等待事件被触发  
	//	++i;
	//}
	//WaitForMultipleObjects(Thread_Num, hThread, true, INFINITE);
	//CloseHandle(hShowThread);  


//	upResize(I, filter, dwidth, dheight, width, height, cn);
	//float lamda1 = 1 - lamda;
	//x = 0;
	//__m128 Lamda = _mm_set_ps(lamda, lamda, lamda, lamda);
	//__m128 Lamda1 = _mm_set_ps(lamda1, lamda1, lamda1, lamda1);
	//for( ; x <= len - 4; x += 4){
	//	__m128 x0 = _mm_set_ps(float(src[x + 3]), float(src[x + 2]), float(src[x + 1]), float(src[x]));
	//	__m128 x2 = _mm_load_ps(filter + x);
	//	x0 = _mm_add_ps(_mm_mul_ps(x0, Lamda), _mm_mul_ps(x2, Lamda1));
	//	dst[x] = clampToByte(x0.m128_f32[0]);
	//	dst[x + 1] = clampToByte(x0.m128_f32[1]);
	//	dst[x + 2] = clampToByte(x0.m128_f32[2]);
	//	dst[x + 3] = clampToByte(x0.m128_f32[3]);
	//}
	//for( ;x < len; x++)
	//	dst[x] = clampToByte(src[x] * lamda + filter[x] * lamda1);


	//float* Dst = (float*)dst;
	//memcpy(Dst, I, dheight * dwidth * cn * sizeof(float));



	FreeMemory(I);
	FreeMemory(tmp);
	FreeMemory(tmp1);
	FreeMemory(tmp2);

	free(x_ofs);
 // 	QueryPerformanceCounter(&stop_t);  
	//*t = 1e3 * (stop_t.QuadPart - start_t.QuadPart) / freq.QuadPart;
}


