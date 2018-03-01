#include "gfx.h"

unsigned int *VBE_BUFFER = (unsigned int*)0xE0000000;

void clear_screen(unsigned int color, unsigned int w, unsigned int h)
{
	memsetd(VBE_BUFFER, color, w * h);
}