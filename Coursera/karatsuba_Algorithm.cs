using System;

namespace Coursera
{

    public class kara_mult
    {
        public int kara_mults(int x, int y)
        {
            if (get_digit(x, 1) <= 5)
            {
                return x * y;
            }
            else
            {
                int i = get_digit(x, 1);
                int a = (x / power(10, i / 2));
                int b = (x % power(10, i / 2));
                int c = (y / power(10, i / 2));
                int d = (y % power(10, i / 2));
                int step1 = kara_mults(a, c);
                int step2 = kara_mults(b, d);
                int step3 = kara_mults((a + b), (c + d)) - step1 - step2;

                return power(10, i) * step1 + power(10, i / 2) * step3 + step2;
            }
        }

        public int get_digit(int x, int num)
        {
            if ((x / 10) == 0) return num;
            else
            {
                return get_digit((x / 10), ++num);
            }
        }

        public int power(int b, int p)
        {
            if (p == 1)
            {
                return b;
            }
            else
            {
                return power(b * b, (p - 1));
            }
        }
    }
}