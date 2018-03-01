unsigned int *VBE_RAM = (unsigned int*)0xE0000000;

void clear_screen(unsigned int color, unsigned int w, unsigned int h)
{
	int i;
	for( i = 0; i < w * h; i = i + 1 )
	{
		VBE_RAM[i] = color;
	}
}