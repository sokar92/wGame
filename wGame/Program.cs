using System;
using wGame.Core;

namespace wGame
{
    class Program
    {
        static void Main(string[] args)
        {
            var f = new Fraction(2, 5);
            var t = Tuple.CreateInitial(f);

            foreach (var fraction in t.UpLimits)
            {
                Console.WriteLine(fraction.ToString());
            }
        }
    }
}
