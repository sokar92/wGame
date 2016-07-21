using System;
using System.Globalization;

namespace wGame.Core
{
    public class Fraction : IEquatable<Fraction>, IComparable, IComparable<Fraction>
    {
        private long Counter { get; set; }
        private long Denominator { get; set; }

        public bool IsNaN
        {
            get { return Counter == 0L && Denominator == 0L; }
        }

        public bool IsInfinity
        {
            get { return Counter != 0L && Denominator == 0L; }
        }

        public bool IsPositive
        {
            get { return Counter > 0L; }
        }

        public bool IsNegative
        {
            get { return Counter < 0L; }
        }

        public bool IsPositiveInfinity
        {
            get { return IsInfinity && IsPositive; }
        }

        public bool IsNegativeInfinity
        {
            get { return IsInfinity && IsNegative; }
        }

        public bool IsZero
        {
            get { return Counter == 0L && Denominator != 0L; }
        }
        
        public Fraction(long l)
        {
            Counter = l;
            Denominator = 1L;
        }

        public Fraction(long counter, long denominator)
        {
            if (denominator == 0L)
            {
                Denominator = 0L;
                if (counter > 0L) Counter = 1L;
                if (counter < 0L) Counter = -1L;
                if (counter == 0L) Counter = 0L;
            }
            else
            {
                if (denominator < 0L)
                {
                    counter = -counter;
                    denominator = -denominator;
                }

                var gcd = Math.Gcd(counter < 0L ? -counter : counter, denominator);
                Counter = counter / gcd;
                Denominator = denominator / gcd;
            }
        }

        public static implicit operator Fraction(long l)
        {
            return new Fraction(l);
        }

        public static Fraction operator -(Fraction a)
        {
            return new Fraction(-a.Counter, a.Denominator);
        }

        public static Fraction operator +(Fraction a, Fraction b)
        {
            return new Fraction(a.Counter*b.Denominator + a.Denominator*b.Counter, a.Denominator*b.Denominator);
        }

        public static Fraction operator -(Fraction a, Fraction b)
        {
            return a + (-b);
        }

        public static Fraction operator *(Fraction a, Fraction b)
        {
            return new Fraction(a.Counter*b.Counter, a.Denominator*b.Denominator);
        }

        public static Fraction operator /(Fraction a, Fraction b)
        {
            return new Fraction(a.Counter*b.Denominator, a.Denominator*b.Counter);
        }

        public int CompareTo(Fraction other)
        {
            if (ReferenceEquals(other, null))
                return +1;

            // a/b < c/d => ad < cb
            var ad = Counter * other.Denominator;
            var cb = Denominator * other.Counter;

            if (ad == cb) return 0;
            return ad < cb ? -1 : +1;
        }

        public int CompareTo(object obj)
        {
            return CompareTo(obj as Fraction);
        }

        public bool Equals(Fraction other)
        {
            return this == other;
        }

        public override bool Equals(object obj)
        {
            var frac = obj as Fraction;
            return !ReferenceEquals(frac, null) && Equals(frac);
        }

        public override int GetHashCode()
        {
            return Counter.GetHashCode() ^ Denominator.GetHashCode();
        }

        public static bool operator ==(Fraction a, Fraction b)
        {
            if (ReferenceEquals(a, null) && ReferenceEquals(b, null)) return true;
            if (ReferenceEquals(a, null) || ReferenceEquals(b, null)) return false;
            return a.Counter == b.Counter && a.Denominator == b.Denominator;
        }

        public static bool operator !=(Fraction a, Fraction b)
        {
            return !(a == b);
        }

        public static bool operator <(Fraction a, Fraction b)
        {
            if (ReferenceEquals(a, null))
                return !ReferenceEquals(b, null);

            return a.CompareTo(b) < 0;
        }

        public static bool operator >(Fraction a, Fraction b)
        {
            return b < a;
        }

        public static bool operator <=(Fraction a, Fraction b)
        {
            return (a == b) || (a < b);
        }

        public static bool operator >=(Fraction a, Fraction b)
        {
            return b <= a;
        }

        public override string ToString()
        {
            if (IsNaN) return "NaN";
            if (IsPositiveInfinity) return "+Inf";
            if (IsNegativeInfinity) return "-Inf";
            if (Denominator == 1L) return Counter.ToString(CultureInfo.InvariantCulture);

            return Counter.ToString(CultureInfo.InvariantCulture) + "/" +
                   Denominator.ToString(CultureInfo.InvariantCulture);
        }

        public long Floor()
        {
            if (IsNaN || IsInfinity)
                throw new Exception("Cannot get floor from NaN or Inf");

            if (IsNegative)
                return (-this).Ceil();

            return Counter/Denominator;
        }

        public long Ceil()
        {
            if (IsNaN || IsInfinity)
                throw new Exception("Cannot get floor from NaN or Inf");

            if (IsNegative)
                return (-this).Floor();

            var q = Counter/Denominator;
            var r = Counter - Denominator*q;

            return r == 0L ? q : q + 1L;
        }
    }
}
