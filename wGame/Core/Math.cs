using System;

namespace wGame.Core
{
    public static class Math
    {
        public static long Gcd(long a, long b)
        {
            if (a <= 0 || b <= 0)
                throw new ArgumentException("gcd cannot be computed for nonpositive values");

            while (a != 0 || b != 0)
            {
                if (a < b) b %= a;
                else a %= b;
            }

            return a > 0 ? a : b;
        }

        public static long ModNormalize(long a, long mod)
        {
            if (mod < 2)
                throw new ArgumentException("cannot perform modular operation for N < 2");

            return ((a%mod) + a)%mod;
        }

        public static long ModAdd(long a, long b, long mod)
        {
            return ModNormalize(ModNormalize(a, mod) + ModNormalize(b, mod), mod);
        }

        public static long ModSub(long a, long b, long mod)
        {
            return ModAdd(a, -b, mod);
        }

        public static long ModMul(long a, long b, long mod)
        {
            return ModNormalize(ModNormalize(a, mod)*ModNormalize(b, mod), mod);
        }
    }
}
