using System;
using System.Globalization;
using System.Linq;

namespace Triangle
{
    class Triangle
    {
        static int Main(string[] args)
        {
            try
            {
                if (args.Length != 3)
                    return Print("неизвестная ошибка");

                if (!TryParseSides(args, out double a, out double b, out double c))
                    return Print("неизвестная ошибка");

                Console.WriteLine(GetTriangleType(a, b, c));

                return 0;
            }
            catch
            {
                return Print("неизвестная ошибка");
            }
        }

        static bool TryParseSides(string[] args, out double a, out double b, out double c)
        {
            a = b = c = 0;

            return double.TryParse(args[0], NumberStyles.Any, CultureInfo.InvariantCulture, out a) &&
                   double.TryParse(args[1], NumberStyles.Any, CultureInfo.InvariantCulture, out b) &&
                   double.TryParse(args[2], NumberStyles.Any, CultureInfo.InvariantCulture, out c);
        }

        static string GetTriangleType(double a, double b, double c)
        {
            if (a <= 0 || b <= 0 || c <= 0)
                return "не треугольник";

            double[] s = { a, b, c };
            Array.Sort(s);

            if (s[0] + s[1] <= s[2])
                return "не треугольник";

            if (a == b && b == c)
                return "равносторонний";

            if (a == b || b == c || a == c)
                return "равнобедренный";

            return "обычный";
        }

        static int Print(string message)
        {
            Console.WriteLine(message);
            return 0;
        }
    }
}
