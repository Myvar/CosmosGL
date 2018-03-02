#ifndef GFX_H
#define GFX_H

extern void *memcpy(void *dest, const void *src, int count);
extern void *memcpyd(void *dest, const void *src, int count);
extern void *memset(void *dest, char val, int count);
extern unsigned short *memsetw(void *dest, unsigned short val, int count);
extern unsigned int *memsetd(void *dest, unsigned int val, int count);
extern void *malloc(unsigned int size);
extern void free(void *ptr);

typedef unsigned int uint32;
// This needs to be 64-bit on a 64-bit machine and not 32-bit.
typedef uint32 uintptr;
typedef unsigned char uint8;
typedef unsigned short uint16;

typedef struct _KHEAPBLOCKBM {
	struct _KHEAPBLOCKBM	*next;
	uint32					size;
	uint32					used;
	uint32					bsize;
    uint32                  lfb;
} KHEAPBLOCKBM;
 
typedef struct _KHEAPBM {
	KHEAPBLOCKBM			*fblock;
} KHEAPBM;

void k_heapBMInit(KHEAPBM *heap);
int k_heapBMAddBlock(KHEAPBM *heap, uintptr addr, uint32 size, uint32 bsize);
void *k_heapBMAlloc(KHEAPBM *heap, uint32 size);
void k_heapBMFree(KHEAPBM *heap, void *ptr);

struct _LLITEM {
	struct _LLITEM 		*next;
	struct _LLITEM  	*prev;
	unsigned int 		data;
};

typedef struct _LLITEM LLITEM;
void ll_add_next(LLITEM **existing, LLITEM *new);
void ll_rem(LLITEM *item);




extern float cos(float x);
extern float sin(float x);
extern int ceil(float num);
extern double powerOfTen(int num);
extern double squareRoot(double a);
extern double fabs(const double x);
extern double fmod(double x,  const double y);
extern double pow(const double x,  const double y);
extern double ldexp(const double x,  const long long y);

typedef unsigned long size_t;

#define assert(condition) ((void)0)
#define NULL 0



#endif