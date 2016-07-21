using System;
using wGame.Core;

namespace wGame
{
    public class Tuple
    {
        public Fraction[] UpLimits;

        public static Tuple CreateInitial(Fraction frac)
        {
            if (ReferenceEquals(frac, null))
                throw new ArgumentNullException("frac");

            if (frac > 1L)
                throw new ArgumentException("weigth cannot be greater than 1");

            var ceil = (1L/frac).Ceil();

            var tuple = new Tuple
            {
                UpLimits = new Fraction[ceil]
            };

            var q = frac;
            for (var i = 0; i < ceil; i++)
            {
                tuple.UpLimits[i] = q;
                q += frac;
            }

            if (tuple.UpLimits[tuple.UpLimits.Length - 1] > 1L)
                tuple.UpLimits[tuple.UpLimits.Length - 1] = 1L;

            return tuple;
        }
    }
}
