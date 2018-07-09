//

#include "stdafx.h"
#include <emmintrin.h>
#include <iostream>
#include <assert.h>

static const int SCALE = 4;
#define WIDTHBYTES(bytes) (((bytes * 8) + 31) / 32 * 4)
#define SSE2 1
typedef unsigned char uchar;
static const int MAX_ESIZE = 16;
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
extern "C" __declspec(dllexport) void boxfilter(const uchar* src, uchar *dst, int width, int height, int radius, int channel)
{
	int len = width * height * channel;
	float *I = (float*)AllocMemory(len * sizeof(float), false);
	for(int i = 0; i < len; i += channel){
		I[i] = src[i];
		I[i + 1] = src[i + 1];
		I[i + 2] = src[i + 2];
	}
	BoxFilter(I, I, width, height, channel, radius);
	for(int i = 0; i < len; i += channel){
		dst[i] = I[i];
		dst[i + 1] = I[i + 1];
		dst[i + 2] = I[i + 2];
	}
}