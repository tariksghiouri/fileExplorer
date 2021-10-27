using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileExplorer
{
    class Folder
    {
        private int Id;
        private String Name;
        private int Size;
        private int ParentId;
        private DateTime CreationDate;
        private  bool IsCurrent;



        public Folder(int id, String name, int parentId, int size = 0, bool v = false)
        {
            this.Id1 = id;
            this.ParentId1 = parentId;
            Name1 = name;
            Size1 = size;
            IsCurrent = v;
            CreationDate = DateTime.Today;

        }

        public int Id1 { get => Id; set => Id = value; }
        public string Name1 { get => Name; set => Name = value; }
        public int Size1 { get => Size; set => Size = value; }
        public int ParentId1 { get => ParentId; set => ParentId = value; }
        public DateTime CreationDate1 { get => CreationDate; set => CreationDate = value; }
        public bool IsCurrent1 { get => IsCurrent; set => IsCurrent = value; }
    }

}
