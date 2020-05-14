using Cosmos.HAL;
using System;
using System.Collections.Generic;
using System.Text;
using Sys = Cosmos.System;


namespace FirstOS
{
    class logo
    {
        internal static void Logo()
        {
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine(" ------------------------------------------------------------------------------ ");
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine("|||||||||  |||||||||  |||||||||   |||||||   ||       ||    OOOOOOO   OOOOOOOOO");
            Console.WriteLine("||     ||  ||     ||  ||     ||  ||     ||  ||       ||   OO     OO  OO");
            Console.WriteLine("||     ||  ||     ||  ||     ||  ||     ||  ||       ||   OO     OO  OO");
            Console.WriteLine("|||||||||  |||||||||  |||||||||  ||     ||  ||       ||   OO     OO  OOOOOOOOO");
            Console.WriteLine("||     ||  ||   ||    ||   ||    ||     ||  ||  |||  ||   OO     OO         OO");
            Console.WriteLine("||     ||  ||    ||   ||    ||   ||     ||  || || || ||   OO     OO         OO");
            Console.WriteLine("||     ||  ||     ||  ||     ||   |||||||   ||||   ||||    OOOOOOO   OOOOOOOOO");
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine(" ------------------------------------------------------------------------------ ");
        }
    }
}