#include "gfx.h"
#include "truetype.h"

unsigned int *VBE_BUFFER = (unsigned int*)0xE0000000;

void clear_screen(unsigned int color, unsigned int w, unsigned int h)
{
	memsetd(VBE_BUFFER, color, w * h);
}

void blend_image_inplace(unsigned int* background, unsigned int* foreground, int w, int h) {
	int count = w * h;
	
	unsigned int Rf, Gf, Bf, Af;
	unsigned int Rb, Gb, Bb;
	unsigned int R, G, B;

	while (count) {
		Rf =  (*foreground) & 0xff;
		Gf = ((*foreground) >>  8) & 0xff;
		Bf = ((*foreground) >> 16) & 0xff;
		Af = ((*foreground) >> 24) & 0xff;
		
		Rb =  (*background) & 0xff;
		Gb = ((*background) >>  8) & 0xff;
		Bb = ((*background) >> 16) & 0xff;

		R = (Rf * Af)/256 + Rb;
		G = (Gf * Af)/256 + Gb;
		B = (Bf * Af)/256 + Bb;

		if (R > 255) R = 255;
		if (G > 255) G = 255;
		if (B > 255) B = 255;

		(*background) = R | (G << 8) | (B << 16);

		foreground += 1;
		background += 1;
		count -= 1;
	}
}

void blit(unsigned int target, unsigned int src, int x0, int y0, int w, int h)
{
	int y;
	for(y = 0; y < h; y++)
	{
		unsigned int yoff = ((y + y0) * w * 4);
		memcpyd(target + (yoff) + (x0 * 4), src + (yoff) + (x0 * 4), w);
	}
}

void fillrect(unsigned int target, int x0, int y0, int w, int h, unsigned int color, int cw)
{
	int y;
	for(y = 0; y < h; y++)
	{
		unsigned int yoff = ((y + y0) * cw * 4);
		memsetd(target + (yoff) + x0, color, w);
	}
}

void clear(unsigned int target, unsigned int color, unsigned int w, unsigned int h)
{
	memsetd(target, color, w * h);
}