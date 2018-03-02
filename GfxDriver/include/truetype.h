
#ifndef TRUETYPE_H
#define TRUETYPE_H

#define STB_TRUETYPE_IMPLEMENTATION 

#define STBTT_ifloor(x)   ((int)(x))
#define STBTT_iceil(x)    ((int) ceil(x))
#define STBTT_sqrt(x)      squareRoot(x)
#define STBTT_pow(x,y)     pow(x,y)
#define STBTT_fmod(x,y)    fmod(x,y)
#define STBTT_cos(x)       cos(x)
#define STBTT_acos(x)      acos(x)
#define STBTT_fabs(x)      fabs(x)
#define STBTT_malloc(x,u)  ((void)(u),malloc(x))
#define STBTT_free(x,u)    ((void)(u),free(x))
#define STBTT_assert(x)    assert(x)
#define STBTT_strlen(x)    strlen(x)
#define STBTT_memcpy       memcpy
#define STBTT_memset       memset


#include "stb_truetype.h"

#endif