using System;



namespace FirstOS

{

    class Calci

    {
        public static Int32 GcdCon(string number1, string number2)
        {
            int num1 = Int32.Parse(number1);
            int num2 = Int32.Parse(number2);
            while (num1 != num2)
            {
                if (num1 > num2)
                    num1 = num1 - num2;
                if (num2 > num1)
                    num2 = num2 - num1;
            }
            return num1;
        }

        public static Int32 LcmCon(string number1, string number2)
        {
            int num1 = Int32.Parse(number1);
            int num2 = Int32.Parse(number2);
            return (num1 * num2) / GcdCon(number1, number2);
        }

        public static long ToPower(string num, string power)
        {
            int Number = Int32.Parse(num);
            int PowerOf = Int32.Parse(power);
            long Result = Number;
            for (int i = PowerOf; i > 1; i--)
            {
                Result = (Result * Number);
            }
            Console.WriteLine(Result.ToString());
            return Result;
        }

        public static Int32 Add(string val1, string val2)
        {
            int val1int = Int32.Parse(val1);
            int val2int = Int32.Parse(val2);
            return val1int + val2int;
        }

        public static Int32 Subtract(string val1, string val2)
        {
            int val1int = Int32.Parse(val1);
            int val2int = Int32.Parse(val2);
            return val1int - val2int;
        }

        public static Int32 Multiply(string val1, string val2)
        {
            int val1int = Int32.Parse(val1);
            int val2int = Int32.Parse(val2);
            return val1int * val2int;
        }

        public static Int32 Divide(string val1, string val2)
        {
            int val1int = Int32.Parse(val1);
            int val2int = Int32.Parse(val2);
            if (val2int == 0)
            {
                Console.WriteLine("Go and study some math!");
                return 0;
            }
            else
            {
                return val1int / val2int;
            }
        }
    }
}
