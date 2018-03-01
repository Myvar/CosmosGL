#ifndef GFX_H
#define GFX_H

extern void *memcpy(void *dest, const void *src, int count);
extern void *memset(void *dest, char val, int count);
extern unsigned short *memsetw(void *dest, unsigned short val, int count);
extern unsigned int *memsetd(void *dest, unsigned int val, int count);


#endif