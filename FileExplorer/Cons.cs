using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileExplorer
{
    class Cons
    {
        public  static int id=0;
        protected static int AutoId()
        {

            return id++;

        }
        public  static int idf=0;
        protected static int AutoIdF()
        {

            return idf++;

        }


        public Cons()
        {
            Console.WriteLine("mkdir:create folder\n" +
         "touch: create file\n" +
         "ls: list content\n" +
         "cd: go to directory\n" +
         "close: terminate\n"
         );
            while (true)
            {
                start();

            }

        }


        //-----
        public static String currentDirectoryName="desktop";
        //------
        public static int CurrentDirectoryId;


        //get info about the current directory

        //add folder
        //function to start the program
        public static void start()
        {

            Database.getCurrent();
            Database.HighestId();
            Database.HighestIdFiles();

            Console.WriteLine(currentDirectoryName + "@" + "User");

            string input= Console.ReadLine();
            //get the first word from user unput
            string choice=input.Split(' ')[0];

            switch (choice)
            {

                //    case "login":
                //    case "LOGIN":
                //        try
                //        {


                //        }
                //        catch (Exception e)

                //        {
                //            Console.WriteLine(e.Message);

                //        }

                //break;
                case "mkdir":
                case "MKDIR":
                case "m":
                    try
                    {
                        if (true/*currentuser.getWritepermissions()*/)
                        {
                            String Dname=input.Split(' ')[1];
                            Database.addFolder( Dname, CurrentDirectoryId, 0, 0);
                            // Console.WriteLine("directory added\n press any key to continue");
                            // Console.ReadKey(true);
                            break;
                        }




                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.Message);
                        Console.WriteLine("press any key to continue");
                        Console.ReadKey(true);
                        break;
                    }


                case "touch":
                case "TOUCH":
                case "t":
                    String Fname=input.Split(' ')[1];

                    if (true /*currentuser.getWritepermissions()*/)
                    {
                        Database.addFile(AutoIdF(), Fname, CurrentDirectoryId, 0);
                        break;
                    }
                    else
                        break;



                case "LS":
                case "ls":
                    if (true/*currentuser.getReadpermissions()*/)
                    {
                        //Console.WriteLine("directories");
                        //ListD();
                        //Console.WriteLine("files");
                        //ListF();
                        SqlDataReader reader = Database.ListD();
                        while (reader.Read())
                        {
                            Console.WriteLine("-" + reader["Name"].ToString()); ;

                        }
                        Console.WriteLine("files");
                        SqlDataReader reader2 = Database.ListF();
                        while (reader.Read())
                        {
                            Console.WriteLine("-" + reader2["Name"].ToString()); ;

                        }

                        break;

                    }
                    else break;

                case "LS-F":
                case "ls-f":
                    if (true/*currentuser.getReadpermissions()*/)
                    {

                        Database.ListF();

                        Console.WriteLine("press any key to continue");
                        Console.ReadKey(true);

                        break;
                    }
                    else break;

                case "rmdir":
                case "RMDIR":

                    if (true/*currentuser.getWritepermissions()*/)
                    {
                        try
                        {
                            Database.removeD(input.Split(' ')[1]);


                            break;
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine(e.Message);
                            Console.WriteLine("press any key to continue");
                            Console.ReadKey(true);
                            break;
                        }


                    }
                    else break;

                case "rm":
                case "RM":
                    try
                    {
                        Database.removeF(input.Split(' ')[1]);
                        break;
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.Message);
                        break;
                    }

                case "cd":
                case "CD":
                    try
                    {
                        String destination=input.Split(' ')[1];
                        Database.ChangeCurrentD(destination);

                        break;
                    }
                    catch (Exception e)
                    {

                        Console.WriteLine(e.Message);
                        Console.WriteLine("press any key to continue");
                        Console.ReadKey(true);
                        break;

                    }
                case "rename":
                case "RENAME":
                case "r":
                    try
                    {

                        Database.rename(input.Split(' ')[1], input.Split(' ')[2]);

                        break;
                    }
                    catch (Exception)
                    {
                        break;

                    }

                case "sudo":
                case "SUDO":
                    try
                    {

                        break;

                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.Message);
                        Console.WriteLine("press any key to continue");
                        Console.ReadKey(true);
                        break;
                    }
                case "close":
                case "CLOSE":

                    Environment.Exit(0);
                    break;

                case "clear":
                case "CL":
                case "cl":
                case "CLEAR":

                    Console.Clear();
                    break;
            }

        }

    }

}
