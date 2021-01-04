function z = funcManyPeaks(x,y)

z = 15 * x * y * (1 - x) * (1 - y) * sin(9 * pi * y);
z = z * z;
z = 1 - z;