#include "gfx.h"

void *memcpy(void *dest, const void *src, int count)
{
  const char *sp = (const char *)src;
  char *dp = (char *)dest;
  for (; count != 0; count--)
    *dp++ = *sp++;
  return dest;
}

void *memcpyd(void *dest, const void *src, int count)
{
  const unsigned int *sp = (const unsigned int *)src;
  unsigned int *dp = (unsigned int *)dest;
  for (; count != 0; count--)
    *dp++ = *sp++;
  return dest;
}

void *memset(void *dest, char val, int count)
{
  char *temp = (char *)dest;
  for (; count != 0; count--)
    *temp++ = val;
  return dest;
}

unsigned short *memsetw(void *dest, unsigned short val, int count)
{
  unsigned short *temp = (unsigned short *)dest;
  for (; count != 0; count--)
    *temp++ = val;
  return dest;
}

unsigned int *memsetd(void *dest, unsigned int val, int count)
{
  unsigned int *temp = (unsigned int *)dest;
  for (; count != 0; count--)
    *temp++ = val;
  return dest;
}

KHEAPBM g_k_heap;

void *malloc(unsigned int size)
{
  void *tmp;
  
  tmp = k_heapBMAlloc(&g_k_heap, size);

  return tmp;
}

void free(void *ptr)
{
  k_heapBMFree(&g_k_heap, ptr);
}

void heap_init(unsigned int ptr, unsigned int size)
{
	k_heapBMInit(&g_k_heap);
	k_heapBMAddBlock(&g_k_heap, ptr, size, 16);
}