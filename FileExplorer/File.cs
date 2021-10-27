using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileExplorer
{
    class File
    {
        private int id;
        private String Name;
        private int Size;
        private int parentId;
        private DateTime dateCreated;
        private String ext; 



        public File(int id, string name, int ParentId,String ext, int size = 0)
        {
            Name = name;
            parentId = ParentId;
            this.id = id;
            dateCreated = DateTime.Now;
            this.ext = ext;

        }

        public int Id { get => id; set => id = value; }
        public string Name1 { get => Name; set => Name = value; }
        public int Size1 { get => Size; set => Size = value; }
        public int ParentId { get => parentId; set => parentId = value; }
        public DateTime DateCreated { get => dateCreated; set => dateCreated = value; }
        public string Ext { get => ext; set => ext = value; }
    }

}
