#define M_PI 3.14159265358979
#define M_2_PI 0.636619772367581343076
#define M_PI_2 1.57079632679489661923

float cos(float x) {
  if (x < 0.0f)
    x = -x;
  while (M_PI < x)
    x -= M_2_PI;
  return 1.0f - (x * x / 2.0f) * (1.0f - (x * x / 12.0f) * (1.0f - (x * x / 30.0f) * (1.0f - x * x / 56.0f)));
}

float sin(float x) {
  return cos(x - M_PI_2);
}

int ceil(float num) {
  int inum = (int) num;
  if (num == (float) inum) {
    return inum;
  }
  return inum + 1;
}

double powerOfTen(int num) {
  double rst = 1.0;
  if (num >= 0) {
    for (int i = 0; i < num; i++) {
      rst *= 10.0;
    }
  } else {
    for (int i = 0; i < (0 - num); i++) {
      rst *= 0.1;
    }
  }

  return rst;
}

double squareRoot(double a) {
  /*
        find more detail of this method on wiki methods_of_computing_square_roots

        *** Babylonian method cannot get exact zero but approximately value of the square_root
   */
  double z = a;
  double rst = 0.0;
  int max = 8; // to define maximum digit 
  int i;
  double j = 1.0;
  for (i = max; i > 0; i--) {
    // value must be bigger then 0
    if (z - ((2 * rst) + (j * powerOfTen(i))) * (j * powerOfTen(i)) >= 0) {
      while (z - ((2 * rst) + (j * powerOfTen(i))) * (j * powerOfTen(i)) >= 0) {
        j++;
        if (j >= 10) break;

      }
      j--; //correct the extra value by minus one to j
      z -= ((2 * rst) + (j * powerOfTen(i))) * (j * powerOfTen(i)); //find value of z

      rst += j * powerOfTen(i); // find sum of a

      j = 1.0;

    }

  }

  for (i = 0; i >= 0 - max; i--) {
    if (z - ((2 * rst) + (j * powerOfTen(i))) * (j * powerOfTen(i)) >= 0) {
      while (z - ((2 * rst) + (j * powerOfTen(i))) * (j * powerOfTen(i)) >= 0) {
        j++;

      }
      j--;
      z -= ((2 * rst) + (j * powerOfTen(i))) * (j * powerOfTen(i)); //find value of z

      rst += j * powerOfTen(i); // find sum of a
      j = 1.0;
    }
  }
  // find the number on each digit
  return rst;
}



double fabs(const double x) {
  return (x < 0) ? -x : x;
}

double fmod(double x,  const double y) {
  if (x <= y || x <= 0 || y <= 0) {
    return 0;
  }
  while (x >= y) {
    x -= y;
  }
  return x;
}

double pow(const double x,  const double y) {
  double z = 1;
  if (y > 0) {
    for (unsigned long long i = 0; i < y; i++) {
      z *= x;
    }
  }
  if (y < 0 && x != 0) {
    for (long long i = 0; i > y; i--) {
      z /= x;
    }
  }
  return z;

}

double ldexp(const double x,  const long long y) {
  return x * pow(2, y);
}