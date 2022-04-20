using System;

namespace Lab_7
{
    delegate double Operator(double a, double b);
    delegate double Calc(double x, double y);
    class Program
    {
        public static double Addiction(double x, double y)
        {
            return x + y;
        }
        //zdefiniuj metode Mul typu delegaa Operator
        public static double Mul(double x, double y)
        {
            return x * y;
        }

        public static void PrintIntArray(int[] arr, Func<int, string> formatter)
        {
            foreach (var item in arr)
            {
                Console.WriteLine(formatter.Invoke(item));
            }
        }
        static void Main(string[] args)
        {
            // DELEGATY
            Operator operation = Addiction;
            // równoważnik Addition(4, 6);
            double result = operation.Invoke(4, 6);
            Console.WriteLine("Dodanie: "+result);
            operation = Mul;
            result = operation.Invoke(4, 6);
            Console.WriteLine("Mnożenie: "+result);
            "abc".ToUpper();
            Calc c = Mul;

            // FUNKCJE
            Func<double, double, double> op = Mul;
            op = Addiction;
            Func<int, string> Formatter = delegate (int number)
            {
                return string.Format("0x{0:x}", number);
            };
            Func<int, string> DecFormat = delegate (int number)
            {
                return string.Format("{0}", number);
            };
            Console.WriteLine(Formatter.Invoke(19));
            Predicate<string> OnlyThreeChars = delegate (string s)
            {
                return s.Length == 3;
            };
            Func<int, int, int, bool> InRange = delegate (int value, int min, int max)
            {
                return value > min && value < max;
            };
            Action<string> Print = delegate (string s)
            {
                Console.WriteLine(s);
            };


            //LAMBDY
            Operator AddLambda = (a, b) => a + b;
            Action<string> PrintLambda = s => Console.WriteLine(s);
            Func<int> Lambda = () => 5; // kiedy nie ma argumentów, tak zwany PRODUCENT
            PrintIntArray(new int[] { 1, 5, 78, 34 }, Formatter);
            PrintIntArray(new int[] { 1, 5, 78, 34 }, n => string.Format("{0}", n));
        }
    }
}
