function z = funcSinc(x,y)

r = sqrt(x * x + y * y);

if r == 0
    z = 1;
else
    z = sin(r) / r;
end;

z = 1 - z;