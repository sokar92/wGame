using System;
using wGame.Core;

namespace wGame
{
    public class PhaseStateDescription
    {
        public Fraction[] UpLimits;
        public ulong[] Count;

        public static PhaseStateDescription CreateInitial(Fraction frac)
        {
            if (ReferenceEquals(frac, null))
                throw new ArgumentNullException("frac");

            if (frac > 1L)
                throw new ArgumentException("weigth cannot be greater than 1");

            var ceil = (1L/frac).Ceil();

            var tuple = new PhaseStateDescription
            {
                UpLimits = new Fraction[ceil],
                Count = new ulong[ceil]
            };

            var q = frac;
            for (var i = 0; i < ceil; i++)
            {
                tuple.UpLimits[i] = q;
                tuple.Count[i] = 0;
                q += frac;
            }

            tuple.UpLimits[tuple.UpLimits.Length - 1] = 1L;
            return tuple;
        }
    }
}
