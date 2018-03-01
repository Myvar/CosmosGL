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