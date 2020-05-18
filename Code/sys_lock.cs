using System;

namespace FirstOS
{
	class sys_lock
	{
        public static void lockpass(string passcode)
        {
            string version = "2020-04-12";
            bool unlocked = false;
            while (!unlocked)
            {
                Console.Clear();
                logo.Logo();
                Console.WriteLine("                                                                                ");
                Console.WriteLine("                                System Locked                                   ");
                Console.WriteLine("                                                                                ");
                Console.Write("Password: ");
                string enterpass = Console.ReadLine();
                if (enterpass == passcode)
                {
                    unlocked = true;
                    Console.Clear();
                }
            }
        }
    }
}
