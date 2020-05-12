using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;

namespace FirstOS
{
	class text_editor
	{
		public static void init(string path)
        {
            Console.Clear();
            Console.WriteLine("###############################################################################");
            Console.WriteLine("# Exit: F12                                               Text_Editor(V1.0.1) #");
            Console.WriteLine("# New: F11                                                Save:F5             #");
            Console.WriteLine("###############################################################################");
            Console.WriteLine("*                                                                             *");
            Console.WriteLine("*                                                                             *");
            Console.WriteLine("*                                                                             *");
            Console.WriteLine("*                                                                             *");
            Console.WriteLine("*                                                                             *");
            Console.WriteLine("*                                                                             *");
            Console.WriteLine("*                                                                             *");
            Console.WriteLine("*                                                                             *");
            Console.WriteLine("*                                                                             *");
            Console.WriteLine("*                                                                             *");
            Console.WriteLine("*                                                                             *");
            Console.WriteLine("*                                                                             *");
            Console.WriteLine("*                                                                             *");
            Console.WriteLine("*                                                                             *");
            Console.WriteLine("*                                                                             *");
            Console.WriteLine("*                                                                             *");
            Console.WriteLine("*                                                                             *");
            Console.WriteLine("*                                                                             *");
            Console.WriteLine("*                                                                             *");
            Console.WriteLine("###############################################################################");
            Console.CursorTop = 4;
            Console.CursorLeft = 1;
            string insertData = "";
            main(insertData, path);
        }

        public static void main(string insertData, string path)
        {
            ConsoleKeyInfo pressed_key = Console.ReadKey(); // read keystroke


            switch (pressed_key.Key)
            {
                case ConsoleKey.UpArrow: //Move cursor up
                    if (Console.CursorTop > 4)
                    {
                        Console.CursorTop = Console.CursorTop - 1;
                    }
                    break;

                case ConsoleKey.DownArrow: //Move cursor down
                    if (Console.CursorTop < 22)
                    {
                        Console.CursorTop = Console.CursorTop + 1;
                    }
                    break;

                case ConsoleKey.LeftArrow: //Move cursor left
                    if (Console.CursorLeft > 1)
                    {
                        Console.CursorLeft = Console.CursorLeft - 1;
                    }
                    break;

                case ConsoleKey.RightArrow: //Move cursor right
                    if (Console.CursorLeft < 77)
                    {
                        Console.CursorLeft = Console.CursorLeft + 1;
                    }
                    break;

                case ConsoleKey.Enter: //Start at the beggining of a new line
                    if (Console.CursorTop < 22)
                    {
                        Console.CursorTop = Console.CursorTop + 1;
                        Console.CursorLeft = 1;
                    }
                    break;
                case ConsoleKey.Backspace: //Remove characters
                    if (Console.CursorLeft > 1)
                    {
                        Console.CursorLeft = Console.CursorLeft - 1;
                        Console.Write(" ");
                        Console.CursorLeft = Console.CursorLeft - 1;
                    }
                    break;

                case ConsoleKey.F11: //Create a new document
                    Console.Clear();
                    init(path);
                    break;

                case ConsoleKey.F5://Save the document
                    Console.Clear();
                    Console.WriteLine("Enter file's name to be saved as:");
                    var file = Console.ReadLine();
                    if (File.Exists(@"0:\" + file))
                    {
                        Console.WriteLine("File already exists! Use a different name.");
                    }
                    else if (!File.Exists(@"0:\" + file))
                    {
                        Console.WriteLine("Creating file!");
                        File.Create(@"0:\" + file);
                        File.WriteAllText(@"0:\" + file, insertData);
                    }
                    break;
                case ConsoleKey.F12: //Closes out of tdit
                    Console.Clear();
                    Console.WriteLine("Press F12 again if you see this."); // Kinda fixes it, bot not all the way.
                    return;
                default:
                    insertData = insertData + pressed_key.KeyChar.ToString();
                    break;

            }
            main(insertData, path);
        }
	}
}
