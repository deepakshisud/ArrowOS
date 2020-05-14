using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Threading;
using Sys = Cosmos.System;

namespace FirstOS
{
    public class Kernel : Sys.Kernel
    {
        string version = "ArrowOS 1.0";
        string pass = "Deepakshi";
        string error = "Unknown command. For complete list of commands, use HELP";
        public static string file;
        public bool FS = false;
        string current_path = @"0:\";
        public bool SudoY = false;
        public void deleteFile(string fname)
        {
            if (File.Exists(fname))
            {
                File.Delete(fname);
            }
            else
            {
                Console.WriteLine("File doesn't exist!");
            }
        }
        public void deleteDirectory(string fname)
        {
            if (Directory.Exists(fname))
            {
                Directory.Delete(fname);
            }
            else
            {
                Console.WriteLine("Directory doesn't exist!");
            }
        }

        public void copyFile(string path, string fname, string destination)
        {
            if (File.Exists(fname) && Directory.Exists(destination))
            {
                File.Copy(path + fname, destination);
            }
            else
            {
                Console.WriteLine("File or Directory doesn't exist!");
            }
        }
        public void moveFile(string path, string fname, string destination)
        {
            copyFile(path, fname, destination);
            deleteFile(path + fname);
        }
        private string FileExists(string directory, string filename)
        {
            //Console.WriteLine(directory);
            string exists = null;
            var fileNameToCheck = Path.Combine(directory, filename);
            if (Directory.Exists(directory))
            {
                //check directory for file
                //exists = Directory.GetFiles(directory).Any(x => x.Equals(fileNameToCheck, StringComparison.OrdinalIgnoreCase));
                foreach (var dir in Directory.GetFiles(directory))
                {
                    if (dir == filename)
                    {
                        exists = Path.Combine(directory, dir);
                    }
                }
                //check subdirectories for file
                if (exists == null)
                {
                    foreach (var dir in Directory.GetDirectories(directory))
                    {
                        var dirNew = Path.Combine(directory, dir);
                        exists = FileExists(dirNew, filename);

                        if (exists != null) break;
                    }
                }
            }
            return exists;
        }
        protected override void BeforeRun()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.BackgroundColor = ConsoleColor.Black;
        file_system:
            Console.Write("Do you want to enable the file system?(Y/N)");
            var filesys = Console.ReadLine();
            if (filesys == "Y")
            {
                FS = true;
                Console.Write("File system will be Initialized");
                var fs = new Sys.FileSystem.CosmosVFS(); //in-built function to make a new file system
                Sys.FileSystem.VFS.VFSManager.RegisterVFS(fs);
            }
            else if (filesys == "N")
            {
                Console.WriteLine("File System will not be Initiated");
            }
            else
                goto file_system;
            try
            {
                filesystem.createDir("0:\\System1"); //initial directory creation
                filesystem.createDir("0:\\User");
                filesystem.createDir("0:\\User\\Documents");
                Sys.KeyboardManager.SetKeyLayout(new Sys.ScanMaps.US_Standard()); //keeping keyboard layout as US standarad
            }
            catch (Exception exc)
            {
                goto fatto;
            }
        fatto:
            Console.Clear();
            logo.Logo();
            Console.WriteLine("                                                  ");
            Console.WriteLine("           Successfully Booted                    ");
            Console.WriteLine("                                                  ");
        inizia:
            Console.WriteLine("Please enter password to log in! (Type N to shutdown)");
            var sino = Console.ReadLine();
            if (sino == pass)
            {
                Console.Clear();
                Console.Write("Welcome to ArrowOS!!\n");
            }
            else if (sino == "N" || sino == "n")
            {
                Stop();
            }
            else { goto inizia; }

        }

        protected override void Run()
        {
            if (!Directory.Exists(@"0:\RecycleBin"))
            {
                Directory.CreateDirectory(@"0:\RecycleBin");
            }
            Console.Write(current_path + "@root: ");
            var input = Console.ReadLine();
            var co = input;
            var vars = "";
            if (input.ToLower().IndexOf('/') != -1)
            {

                string[] parts = input.Split('/');
                co = parts[0];
                vars = parts[1];
            }
            try
            {
                switch (co)
                {

                    case "reboot":    //Reboots the machine
                        Cosmos.System.Power.Reboot();
                        break;

                    case "shutdown":   //Shuts down the machine
                        Console.WriteLine("now you can power off your system");
                        Stop();
                        break;
                    case "clear":   //Clears the screen
                        Console.Clear();
                        break;

                    case "help":  //All the commands
                        Console.WriteLine("Help 1: Normal Commands");
                        Console.WriteLine("--------------------------------------------------------");
                        Console.WriteLine("                                ");
                        Console.WriteLine("Reboot = reboot");
                        Console.WriteLine("Shutdown = shutdown");
                        Console.WriteLine("Clear = clear");
                        Console.WriteLine("About IOTA OS = about");
                        Console.WriteLine("Lock = lock");
                        Console.WriteLine("Print something on screen = print/things to print");
                        Console.WriteLine("Become user with sudo privilges = sudo");
                        Console.WriteLine("his - opens the command history");
                        Console.WriteLine("Help page 2 (FileSystem) = help2");
                        Console.WriteLine("Help page 3 (Calculator) = help3");
                        Console.WriteLine("Help page 4 (Miscellaneous) = help4");
                        break;
                    case "help2":
                        Console.WriteLine("Help 2: FileSystem");
                        Console.WriteLine("--------------------------------------------------------");
                        Console.WriteLine("                                ");
                        Console.WriteLine("Go to specified directory = cd/directory");
                        Console.WriteLine("Create directory = md/new directory's name");
                        Console.WriteLine("Show current directories = dir");
                        Console.WriteLine("Deletes the specified directory[sudo] = dd/directory*");
                        Console.WriteLine("                                ");
                        Console.WriteLine("*type helpdir to know what directories not to delete");
                        Console.WriteLine("open file - Open a file and read it(should it exist)");
                        Console.WriteLine("df - Deletes a file(should it exist)");
                        Console.WriteLine("dd - Deletes the directory(should it exist)");
                        Console.WriteLine("mv - moves the file to another directory");
                        Console.WriteLine("cp - copies the file to another directory");
                        Console.WriteLine("cat - concatenates two files");
                        Console.WriteLine("search - search for a specific file in the entire system");
                        Console.WriteLine("ds - find the current disk space");
                        Console.WriteLine("recl - moves a file to the RecycleBin");
                        Console.WriteLine("clcrecl - clears the RecycleBin");
                        break;

                    case "help3":
                        Console.WriteLine("Help 3: Calculator*");
                        Console.WriteLine("--------------------------------------------------------");
                        Console.WriteLine("                                ");
                        Console.WriteLine("Add two numbers together = add/num1#num2");
                        Console.WriteLine("Subtract a number to an other = subtract/num1#num2");
                        Console.WriteLine("Muliply two numbers together = multiply/num1#num2");
                        Console.WriteLine("Divide one number with another number = divide/num1#num2");
                        Console.WriteLine("One nuber to the power of another = power/num1#num2");
                        Console.WriteLine("Least Common Number of two numbers = lcm/num1#num2");
                        Console.WriteLine("Greatest Common Factor of two numbers = gcf/num1#num2");
                        Console.WriteLine("                                ");
                        Console.WriteLine("*it not works with decimals(0.1 for example)");
                        break;

                    case "help4":
                        Console.WriteLine("Help 4 : Miscellaneous");
                        Console.WriteLine("--------------------------------------------------------");
                        Console.WriteLine("                                ");
                        Console.WriteLine("Open Text Editor = text_editor");
                        Console.WriteLine("Play the game snake=run snake");
                        Console.WriteLine("date - gives the date and time of OS");
                        break;
                    case "helpdir":
                        Console.WriteLine("Do not delete the directories TEST, Testing and 0 because");
                        Console.WriteLine("they are system's directoryes and deleting them will cause");
                        Console.WriteLine("the Blue Screen of Error");
                        break; //HAIL BSOD .. guide to initiate BSOD ^_^ ^_^

                    case "lock":
                        Console.Write("Set Passcode: "); //user authentication
                        pass = Console.ReadLine();
                        sys_lock.lockpass(pass);
                        break;

                    case "print":   //Prints something
                        Console.WriteLine(vars);
                        break;

                    case "about":  //Some information
                        Console.WriteLine("ArrowOS 1.0.0");
                        break;
                    case "cd":  //Changes current directory 
                        if (FS)
                        {
                            if (vars == "")
                            {
                                current_path = @"0:\";
                            }
                            else if (Directory.Exists(vars))
                            {
                                current_path = current_path + vars;
                            }
                            else
                            {
                                Console.WriteLine("Directory Doesn't Exists");
                            }
                        }
                        else
                        {
                            Console.WriteLine("File System Not Enabled!");
                        }
                        break;
                    case "md":  // Makes new directory
                        if (FS)

                            {
                                filesystem.createDir(current_path + vars);
                            }
                            else
                            {
                                Console.WriteLine("File System Not Enabled!");
                            }
                        break;

                    case "dir": // Displays current location
                        if (FS)
                        {
                            string[] back = filesystem.readFiles(current_path);
                            string[] front = filesystem.readDirectories(current_path);
                            string[] combined = new string[front.Length + back.Length];
                            Array.Copy(front, combined, front.Length);
                            Array.Copy(back, 0, combined, front.Length, back.Length);
                            foreach (var item in combined)
                            {
                                Console.WriteLine(item.ToString());
                            }
                        }
                        else
                        {
                            Console.WriteLine("File System Not Enabled!");
                        }
                        break;
                    case "add": // Adds given numbers
                        string[] inputvarsa = vars.Split('#');
                        Console.WriteLine(Calci.Add(inputvarsa[0], inputvarsa[1]));
                        break;

                    case "subtract": // Subtracts given numbers
                        string[] inputvarsb = vars.Split('#');
                        Console.WriteLine(Calci.Subtract(inputvarsb[0], inputvarsb[1]));
                        break;

                    case "multiply": // Multiplys given numbers
                        string[] inputvarsc = vars.Split('#');
                        Console.WriteLine(Calci.Multiply(inputvarsc[0], inputvarsc[1]));
                        break;

                    case "divide": // Divides given numbers
                        string[] inputvarsd = vars.Split('#');
                        Console.WriteLine(Calci.Divide(inputvarsd[0], inputvarsd[1]));
                        break;

                    case "power": // Raises given number to other given number
                        string[] inputvarse = vars.Split('#');
                        Console.WriteLine(Calci.ToPower(inputvarse[0], inputvarse[1]));
                        break;

                    case "gcd": // Gives gcd conversion of given numbers
                        string[] inputvarsf = vars.Split('#');
                        Console.WriteLine(Calci.GcdCon(inputvarsf[0], inputvarsf[1]));
                        break;

                    case "lcm": // Gives lcm conversion of given numbers
                        string[] inputvarsg = vars.Split('#');
                        Console.WriteLine(Calci.LcmCon(inputvarsg[0], inputvarsg[1]));
                        break;
                    case "text_editor": //text_editor
                        Console.Clear();
                        File.AppendAllText(@"0:\history", input);
                        text_editor.init(current_path);
                        break;

                    case "open file": //open a file
                        Console.WriteLine("Enter filename of file to read:");
                        var file = Console.ReadLine();
                        File.AppendAllText(@"0:\history", input + " " + file + "\n");
                        string[] read;
                        read = File.ReadAllLines(current_path + file);
                        foreach (string s in read)
                        {
                            Console.WriteLine(s);
                        }
                        break;
                    case "sudo": //Become sudo user
                        Console.Write("Enter sudo password?(Y/N)");
                        var sicuro = Console.ReadLine();
                        if (sicuro == pass)
                        {
                            SudoY = true;
                            Console.WriteLine("Sudo Access Granted!");
                        }
                        else
                        {
                            SudoY = false;
                        }
                        break;
                    case "dd": //delete directory
                        if (SudoY)
                        {
                            Console.WriteLine("Enter name of directory to be deleted:");
                            var directory = Console.ReadLine();
                            File.AppendAllText(@"0:\history", input + " " + directory + "\n");
                            deleteDirectory(current_path + directory);
                        }
                        else
                        {
                            Console.WriteLine("I'm sorry, you aren't a sudo user");
                        }
                        break;

                    case "df":
                        if (SudoY)
                        {
                            Console.WriteLine("Enter name of file to be deleted:");
                            var filename = Console.ReadLine();
                            File.AppendAllText(@"0:\history", input + " " + filename + "\n");
                            deleteFile(current_path + filename);
                        }
                        else
                        {
                            Console.WriteLine("I'm sorry, you aren't a sudo user");
                        }
                        break;

                    case "run snake":  //game implementation
                        SnakeGame snk = new SnakeGame();
                        snk.Run();
                        break;
                    case "ds":
                        foreach (DriveInfo drive in DriveInfo.GetDrives())
                        {
                            if (drive.IsReady)
                            {
                                Console.WriteLine("Available free space(in Bytes):");
                                Console.WriteLine(drive.TotalFreeSpace + drive.Name);
                            }
                        }
                        break;
                    case "mv":
                        Console.WriteLine("Enter name of file to be moved:");
                        var movefile = Console.ReadLine();
                        Console.WriteLine("Enter path to where file is to be moved:");
                        var movePath = Console.ReadLine();
                        File.AppendAllText(@"0:\history", input + " " + movefile + " " + movePath + "\n");
                        moveFile(current_path, movefile, movePath);
                        break;

                    case "cp":
                        Console.WriteLine("Enter name of file to be copied:");
                        var copyfile = Console.ReadLine();
                        Console.WriteLine("Enter path to where file is to be copied:");
                        var copyPath = "";
                        copyPath = Console.ReadLine();
                        File.AppendAllText(@"0:\history", input + " " + copyfile + " " + copyPath + "\n");
                        copyFile(current_path, copyfile, copyPath);
                        break;

                    case "date":
                        File.AppendAllText(@"0:\history", input + "\n");
                        DateTime now = DateTime.Now;
                        Console.WriteLine(now);
                        break;
                    case "cat":
                        Console.WriteLine("Enter original file name:");
                        string origfname = Console.ReadLine();
                        //File.Create(current_path + fname);
                        Console.WriteLine("Enter filename of file to read:");
                        var fileCon = Console.ReadLine();
                        File.AppendAllText(@"0:\history", input + " " + origfname + " " + fileCon + "\n");
                        string[] readCon;
                        readCon = File.ReadAllLines(current_path + fileCon);
                        foreach (string str in readCon)
                        {
                            File.AppendAllText(current_path + origfname, str);
                        }
                        break;
                    case "search":
                        Console.WriteLine("Enter name of file to search:");
                        string fsearch = Console.ReadLine();
                        File.AppendAllText(@"0:\history", input + " " + fsearch + "\n");
                        var answer = FileExists(@"0:\", fsearch);
                        if (answer == null)
                        {
                            Console.WriteLine("File Doesn't Exist");
                        }
                        else
                        {
                            Console.WriteLine("File Found!!!");
                            Console.WriteLine(answer);
                        }
                        break;
                    case "recl":
                        Console.WriteLine("Enter file to be moved to recycle bin:");
                        var reclFile = Console.ReadLine();
                        File.AppendAllText(@"0:\history", input + " " + reclFile + "\n");
                        moveFile(current_path, reclFile, @"0:\RecycleBin\");
                        break;
                    case "clcrecl":
                        File.AppendAllText(@"0:\history", input + "\n");
                        foreach (var fileRecl in Directory.GetFiles(@"0:\RecycleBin\"))
                        {
                            deleteFile(@"0:\RecycleBin\" + fileRecl);
                        }
                        break;
                    case "his":
                        string[] readHis;
                        readHis = File.ReadAllLines(@"0:\history");
                        foreach (string s in readHis)
                        {
                            Console.WriteLine(s);
                        }
                        break;

                    default:
                        Console.WriteLine(error);
                        break;
                }
            }
            catch (Exception e) //BlueScreenOfDeath-like thing I wanted to make noerror false but it bugs
            {

                Console.BackgroundColor = ConsoleColor.Blue;
                Console.Clear();
            spegni:
                Console.Write("   Do you want to reboot or shutdown?(R/S)");
                var risp = Console.ReadLine();
                if (risp == "R" || risp == "r")
                {
                    Sys.Power.Reboot();
                }
                else if (risp == "S" || risp == "s")
                {
                    Stop();
                }
                else
                {
                    goto spegni;
                }

            }
        }
    }
}



