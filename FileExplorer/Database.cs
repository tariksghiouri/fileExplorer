using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileExplorer
{
    internal class Database
    {
        private static string connetionString;
        public static SqlConnection cnn;
        public  static List<Folder> FoldersList = new List<Folder>();
        public  static List<File> FilesList = new List<File>();

        public static Folder GetFolderById(int id)
        {
            try
            {
                var query ="select * from ProjetFinalC#.dbo.Folder where Id=@id";
                connect();
                SqlCommand cmd=new SqlCommand(query,Database.cnn);
                cmd.Parameters.AddWithValue("@name", id);
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    Folder dir = new Folder((int)reader["id"],(string)reader["name"],(int)reader["parentid"]);
                    return dir;
                }
                return null;
            }
            catch (Exception e)
            {
                return null;
            }
        }
        public static void connect()
        {


            connetionString = "Data Source=TARIK-PC;Initial Catalog=ProjetFinalC#;User ID=login1;Password=password ;MultipleActiveResultSets=true";
            cnn = new SqlConnection(connetionString);
            try
            {
                cnn.Open();
            }
            catch (Exception e)
            {
            }



        }
        public static void HighestId()
        {

            try
            {
                connect();
                var qr1= "select max(id) from [ProjetFinalC#].dbo.Folder  ";
                SqlCommand cmd=new SqlCommand (qr1, cnn);

                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Cons.id = (int)reader["id"];

                }

            }

            catch (Exception e)
            {


            }
        }
        public static void HighestIdFiles()
        {

            try
            {
                connect();
                var qr1= "select *from ProjetFinalC#.dbo.Files  ";
                SqlCommand cmd=new SqlCommand (qr1, cnn);

                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Cons.idf = (int)reader["Id"];

                }

            }

            catch (Exception e)
            {


            }
        }
        static public SqlDataReader ListD()
        {
            try
            {
                connect();
                var qr1= "select *from ProjetFinalC#.dbo.Folder where parentId=@id";
                SqlCommand cmd=new SqlCommand (qr1, cnn);
                cmd.Parameters.AddWithValue("@id", Cons.CurrentDirectoryId);
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Console.WriteLine("-" + reader["Name"].ToString()); ;

                }
                Console.ReadKey(true);
                return reader;
                // Console.WriteLine("!!! empty !!!");

            }
            catch (Exception e)
            {

                return null;
            }

        }
        public static void getCurrent()
        {
            try
            {
                connect();
                var qr1= "select * from  ProjetFinalC#.dbo.Folder where IsCurrent=1";
                SqlCommand cmd=new SqlCommand (qr1,cnn);
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    Cons.CurrentDirectoryId = (int)reader["Id"];
                    Cons.currentDirectoryName = reader["Name"].ToString();
                    //   Console.WriteLine("the current directory Id is\t" + CurrentDirectoryId);
                    //  Console.WriteLine("the current directory name is\t" + currentDirectoryName);
                    //  Console.ReadKey(true);
                }


            }
            catch (Exception e)
            {


            }

        }
        public static SqlDataReader ListF()
        {
            try
            {
                connect();
                var qr1= "select *from ProjetFinalC#.dbo.Files where parentId=@id";
                SqlCommand cmd=new SqlCommand (qr1, cnn);
                cmd.Parameters.AddWithValue("@id", Cons.CurrentDirectoryId);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Console.WriteLine("-" + reader["Name"].ToString()); ;

                }
                //else Console.WriteLine("!!! empty !!!");
                Console.ReadKey(true);
                return reader;
            }
            catch (Exception e)
            {

                return null;
            }
        }
        public static void addFolder(string name, int parentId, int iscurrent, int size = 0)
        {
            try
            {
                var qr1= "insert into  ProjetFinalC#.dbo.Folder values(@name,@size,@parentid,@date,@current)";

                SqlCommand cmd=new SqlCommand (qr1, Database.cnn);
                cmd.Parameters.AddWithValue("@name", name);
                cmd.Parameters.AddWithValue("@parentid", parentId);
                cmd.Parameters.AddWithValue("@size", size);
                cmd.Parameters.AddWithValue("@date", DateTime.Now);
                cmd.Parameters.AddWithValue("@Current", iscurrent);
                cmd.ExecuteNonQuery();

            }
            catch (Exception e)
            {



            }



        }
        //add file to the current folder
        public static void addFile(int Id, string name, int parentId, int size = 0)
        {
            try
            {
                var qr1= "insert into  ProjetFinalC#.dbo.Files values(@id,@name,@size,@parentid,@date)";
                connect();
                SqlCommand cmd=new SqlCommand (qr1,Database.cnn);
                cmd.Parameters.AddWithValue("@id", Cons.id);
                cmd.Parameters.AddWithValue("@name", name);
                cmd.Parameters.AddWithValue("@date", DateTime.Now);
                cmd.Parameters.AddWithValue("@parentid", parentId);
                cmd.Parameters.AddWithValue("@size", size);
                cmd.ExecuteNonQuery();

            }
            catch (Exception e)
            {


            }


        }
        //change the current directory=> CD
        public static void ChangeCurrentD(String Togo)
        {
            try
            {

                connect();

                var query= "update ProjetFinalC#.dbo.Folder set IsCurrent=1 where Folder.Name = @name;" +
                    "update ProjetFinalC#.dbo.Folder set IsCurrent=0 where Folder.Name = @name2";
                //   var query1= "update ProjetFinalC#.dbo.Folder set CurrentD=0 where Directory.Name = @name";
                SqlCommand cmd=new SqlCommand (query,Database.cnn);
                // SqlCommand cmd1=new SqlCommand (query1,Database.Database.cnn);
                cmd.Parameters.AddWithValue("@name", Togo);
                cmd.Parameters.AddWithValue("@name2", Cons.currentDirectoryName);
                cmd.ExecuteNonQuery();
                //  cmd1.Parameters.AddWithValue("@name", currentDirectoryName);
            }
            catch (Exception e)
            {

            }
        }
        //function to remove Directory
        public static void removeD(String name)
        {
            try
            {
                var query ="delete from ProjetFinalC#.dbo.Folder where Name=@name ";
                connect();


                SqlCommand cmd=new SqlCommand(query,Database.cnn);
                cmd.Parameters.AddWithValue("@name", name);
                cmd.ExecuteNonQuery();
            }
            catch (Exception e)
            {

            }
        }
        //remove file
        public static void removeF(String name)
        {
            try
            {
                var query ="delete from ProjetFinalC#.dbo.Files where Name=@name ";
                connect();


                SqlCommand cmd=new SqlCommand(query,Database.cnn);
                cmd.Parameters.AddWithValue("@name", name);
                cmd.ExecuteNonQuery();
            }
            catch (Exception e)
            {


            }
        }
        //rename
        public static void rename(String oldName, String newName)
        {
            try
            {

                connect();

                var query= "update ProjetFinalC#.dbo.Folder set Name=@newName where Folder.Name = @oldName" ;
                //   var query1= "update ProjetFinalC#.dbo.Folder set CurrentD=0 where Directory.Name = @name";
                SqlCommand cmd=new SqlCommand (query,Database.cnn);
                // SqlCommand cmd1=new SqlCommand (query1,Database.Database.cnn);
                cmd.Parameters.AddWithValue("@newName", newName);
                cmd.Parameters.AddWithValue("@oldName", oldName);
                cmd.ExecuteNonQuery();
                //  cmd1.Parameters.AddWithValue("@name", currentDirectoryName);
            }
            catch (Exception e)
            {


            }


        }
        public static void Move(int ParentId, int id)
        {
            try
            {

                connect();

                var query= "update ProjetFinalC#.dbo.Folder set ParentId=@newId where id = @hisId" ;
                //   var query1= "update ProjetFinalC#.dbo.Folder set CurrentD=0 where Directory.Name = @name";
                SqlCommand cmd=new SqlCommand (query,Database.cnn);
                // SqlCommand cmd1=new SqlCommand (query1,Database.Database.cnn);
                cmd.Parameters.AddWithValue("@newId", ParentId);
                cmd.Parameters.AddWithValue("@hisId", id);

                cmd.ExecuteNonQuery();
                //  cmd1.Parameters.AddWithValue("@name", currentDirectoryName);
            }
            catch (Exception e)
            {


            }


        }

        public static void Fil_list()
        {
            {
                connect();

                SqlCommand sqlCommand= new SqlCommand("SELECT * FROM dbo.Folder",cnn);

                SqlDataReader reader = sqlCommand.ExecuteReader();

                while (reader.Read())

                {
                    FoldersList.Add
                         (new Folder(
                             (int)reader["Id"],
                             (String)reader["Name"],
                             (int)reader["parentId"],
                             (int)reader["size"],
                             (bool)reader["IsCurrent"]));

                }
            }
        }
        public static void Fil_list2()
        {
            {
                connect();

                SqlCommand sqlCommand= new SqlCommand("SELECT * FROM dbo.Files",cnn);

                SqlDataReader reader = sqlCommand.ExecuteReader();

                while (reader.Read())

                {
                    FilesList.Add
                         (new File(
                             (int)reader["Id"],
                             (String)reader["Name"],
                             (int)reader["ParentId"],
                             (String)reader["ext"],
                             (int)reader["size"]));

                }
            }
        }


        public static int GiveMeAnId()
        {
            int id=0;

            connect();
            var qr1= "select max(Id) from [ProjetFinalC#].dbo.Folder  ";
            SqlCommand cmd=new SqlCommand (qr1, cnn);




            id = (int)cmd.ExecuteScalar();


            return id++;



        }
        public static int count(int id)
        {
            int i=0;
            int j=0;
            var query ="select count  (*) from [ProjetFinalC#].dbo.Folder where parentId=@id ";
            connect();
            SqlCommand cmd=new SqlCommand(query,Database.cnn);
            cmd.Parameters.AddWithValue("@id", id);
            i = (int)cmd.ExecuteScalar();


            var query2 ="select count  (*) from [ProjetFinalC#].dbo.Files where parentId=@id ";
            connect();
            SqlCommand cmd2=new SqlCommand(query2,Database.cnn);
            cmd2.Parameters.AddWithValue("@id", id);
            j = (int)cmd.ExecuteScalar();

            return i + j;
        }


    }
}
