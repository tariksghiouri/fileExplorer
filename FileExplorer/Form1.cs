using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FileExplorer
{
    public partial class Form1 : Form
    {
        int HisId;
        static List<Folder> FoldersToShow= new List<Folder>();
        static List<File> FilesToShow = new List<File>();
        String selected;
        int idselected;
        bool isFile;
        String SelectedFromList;

        private void FillFoldersToShow(List<Folder> list)
        {
            foreach (var listitem in list)
            {
                if (listitem.ParentId1 == idselected)
                {
                    FoldersToShow.Add(listitem);
                }
            }
        }
        private void fillFilesToShow(List<File> list)
        {
            foreach (var listitem in list)
            {
                if (listitem.ParentId == idselected)
                {
                    FilesToShow.Add(listitem);
                }
            }

        }

        public Form1()
        {
            InitializeComponent();

        }


        private void Form1_Load(object sender, EventArgs e)
        {
            label1.Text = listView1.Items.Count.ToString();
            //MessageBox.Show(Database.GiveMeAnId().ToString());
            Database.Fil_list();
            Database.Fil_list2();
            // treeView1.Nodes[0].Expand();
            listFill(1);
            comboBox1.Text = "Desktop";

            treeView1.Nodes.Add("desktop");
            TreeLoad(1, treeView1.Nodes[0]);
            listView1.ContextMenuStrip = contextMenuStrip2;
            //PopulateTree( Database.FoldersList);
            foreach (TreeNode tn in treeView1.Nodes)
            {
                tn.Expand();
            }

        }

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            comboBox1.Text = treeView1.SelectedNode.FullPath;
            label1.Text = listView1.Items.Count.ToString();
            listView1.ContextMenuStrip = contextMenuStrip1;
            Database.Fil_list2();
            listView1.Items.Clear();
            selected = treeView1.SelectedNode.Text;
            idselected = Database.FoldersList.Find(x => x.Name1.Equals(selected)).Id1;

            listFill(idselected);


        }

        void ShowThings()
        {




            //label1.Text = (Database.count(idselected)).ToString();

            var filesAndfolders = Database.FoldersList.Zip(Database.FilesList, (folder, file) => new { Folder = folder, File =file });

            foreach (var folder in filesAndfolders)
            {



                if (folder.Folder.ParentId1 == idselected)
                {
                    ListViewItem listViewItem=new ListViewItem(folder.Folder.Name1.ToString());
                    listViewItem.ImageIndex = 1;
                    listViewItem.SubItems.Add("file folder");
                    listViewItem.SubItems.Add(folder.Folder.Size1.ToString());
                    listViewItem.SubItems.Add(folder.Folder.CreationDate1.ToString());
                    listView1.Items.Add(listViewItem);

                }



                if (folder.File.ParentId == idselected)
                {
                    ListViewItem listViewItem=new ListViewItem(folder.File.Name1.ToString());
                    switch (folder.File.Ext)
                    {
                        case "txt":
                            listViewItem.ImageIndex = 13;
                            break;
                        case "mp3":
                            listViewItem.ImageIndex = 5;
                            break;
                        case "docs":
                            listViewItem.ImageIndex = 14;
                            break;
                        case "img":
                            listViewItem.ImageIndex = 4;
                            break;
                        default:
                            listViewItem.ImageIndex = 99;
                            break;


                    }
                    listViewItem.SubItems.Add(folder.File.Ext);
                    listViewItem.SubItems.Add(folder.File.Size1.ToString());
                    listViewItem.SubItems.Add(folder.File.DateCreated.ToString());
                    listView1.Items.Add(listViewItem);

                }

            }
        }


        private void PopulateTree(List<Folder> folders)
        {


            foreach (Folder folder in folders)
            {
                if (folder.ParentId1 == 0)
                {

                    TreeNode node=new TreeNode
                    {
                        Text=folder.Name1,
                        Tag=folder.Id1

                    };
                    treeView1.Nodes.Add(node);
                }
                for (int i = 0; i < treeView1.Nodes.Count; i++)
                {
                    if ((int)(treeView1.Nodes[i].Tag) == folder.ParentId1 && (int)treeView1.Nodes[i].Tag != 0)
                    {
                        treeView1.Nodes[i].Nodes.Add(folder.Name1);

                    }
                }
            }

        }
        private void test()
        {
            foreach (var folder in Database.FoldersList)
            {
                MessageBox.Show(folder.Name1);
            }

        }
        private void PopulateTree2(ref TreeNode root, List<Folder> folders)
        {
            if (root == null)
            {
                root = new TreeNode();
                root.Text = "Tarik-pc";
                root.Tag = null;
                // get all departments in the list with parent is null
                var details=folders.Where(t=>t.Id1==0);
                foreach (var detail in details)
                {
                    var child= new TreeNode()
                    {
                        Text=detail.Name1,
                        Tag=detail.Id1,
                    };
                    PopulateTree2(ref child, folders);
                    root.Nodes.Add(child);
                }
            }
            else
            {
                var id=(int)root.Tag;
                var details=folders.Where(t=>t.ParentId1==id);
                foreach (var detail in details)
                {
                    var child= new TreeNode()
                    {
                        Text=detail.Name1,
                        Tag=detail.Id1,
                    };
                    PopulateTree2(ref child, folders);
                    root.Nodes.Add(child);
                }
            }
            treeView1.Nodes.Add(root);
        }

        private void addImageToItem(ListViewItem list)

        {
            foreach (var item in Database.FilesList)
            {
                switch (item.Ext)
                {
                    default:
                        break;
                }
            }



        }

        private void button4_Click(object sender, EventArgs e)
        {
            listView1.View = View.LargeIcon;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            listView1.View = View.Details;
        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void toolStripMenuItem4_Click(object sender, EventArgs e)
        {

        }

        private void listView1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            label1.Text = listView1.Items.Count.ToString();
            listView1.Items.Clear();
            if (listView1.SelectedItems.Count > 0)
            {
                var item = listView1.SelectedItems[0];
                //rest of your logic
            }

            comboBox1.Text += "\\" + SelectedFromList;

            idselected = Database.FoldersList.Find(x => x.Name1.Equals(SelectedFromList)).Id1;

            listFill(idselected);
            /* foreach (Folder folder in Database.FoldersList)
            {

                if (folder.ParentId1 == idselected)
                {
                    ListViewItem listViewItem=new ListViewItem(folder.Name1.ToString());
                    listViewItem.ImageIndex = 1;
                    listViewItem.SubItems.Add("file folder");
                    listViewItem.SubItems.Add(folder.Size1.ToString());
                    listViewItem.SubItems.Add(folder.CreationDate1.ToString());
                    listView1.Items.Add(listViewItem);

                }
            }
            Database.Fil_list2();
           
            foreach (File file in Database.FilesList)
            {
                if (file.ParentId == idselected)
                {
                    ListViewItem listViewItem=new ListViewItem(file.Name1.ToString());
                    switch (file.Ext)
                    {
                        case "txt":
                            listViewItem.ImageIndex = 13;
                            break;
                        case "mp3":
                            listViewItem.ImageIndex = 5;
                            break;
                        case "docs":
                            listViewItem.ImageIndex = 14;
                            break;
                        case "img":
                            listViewItem.ImageIndex = 4;
                            break;
                        default:
                            listViewItem.ImageIndex = 99;
                            break;


                    }
                    listViewItem.SubItems.Add(file.Ext);
                    listViewItem.SubItems.Add(file.Size1.ToString());
                    listViewItem.SubItems.Add(file.DateCreated.ToString());
                    listView1.Items.Add(listViewItem);

                }


            }
            */
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {


            if (listView1.SelectedItems.Count > 0)
            {
                SelectedFromList = listView1.SelectedItems[0].Text;
                //idSelectedFormlist = Database.FoldersList.Find(x => x.Name1.Equals(selected)).Id1;
            }
            var filesAndfolders = Database.FoldersList.Zip(Database.FilesList, (folder, file) => new { Folder = folder, File =file });

            foreach (var item in filesAndfolders)
            {
                if (item.File.Name1 == SelectedFromList)
                {
                    isFile = true;
                }
                else if (item.Folder.Name1 == SelectedFromList)
                {
                    isFile = false;
                }
            }
        }

        private void toolStripMenuItem3_Click(object sender, EventArgs e)
        {


        }

        private void newFolderToolStripMenuItem_Click(object sender, EventArgs e)
        {

            Folder toAdd=new Folder(Database.GiveMeAnId(),"new folder",idselected);
            Database.addFolder("new folder", idselected, 0);
            MessageBox.Show((Database.GiveMeAnId()).ToString() + " n " + idselected.ToString());
            treeView1.Nodes.Clear();
            TreeLoad(1, new TreeNode("Desktop"));


        }

        private void contextMenuStrip2_Opening(object sender, CancelEventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void ribbonBar1_ItemClick(object sender, EventArgs e)
        {

        }

        private void buttonItem2_Click(object sender, EventArgs e)
        {

        }

        private void applicationButton1_Click(object sender, EventArgs e)
        {

        }

        private void buttonItem14_Click(object sender, EventArgs e)
        {
            Folder toAdd=new Folder(Database.GiveMeAnId(),FolderName(),idselected);
            Database.addFolder(FolderName(), idselected, 0);
            //  MessageBox.Show((Database.GiveMeAnId()).ToString() + " n " + idselected.ToString());
            Database.FoldersList.Clear();
            Database.FilesList.Clear();
            Database.Fil_list();
            Database.Fil_list2();
            treeView1.Nodes.Clear();
            treeView1.Nodes.Add("desktop");
            TreeLoad(1, treeView1.Nodes[0]);
            listFill(idselected);
            foreach (TreeNode tn in treeView1.Nodes)
            {
                tn.Expand();
            }
            //TreeLoad(1, new TreeNode("Desktop"));

        }

        private void TreeLoad(int id, TreeNode tree)
        {
            foreach (var item in Database.FoldersList)
            {
                if (item.ParentId1 == id)
                {
                    tree.Nodes.Add(item.Name1);
                    TreeLoad(item.Id1, tree.Nodes[tree.Nodes.Count - 1]);
                }
            }
        }

        void ititlist()
        {
            Database.Fil_list();
            foreach (var folder in Database.FoldersList)
            {
                if (folder.ParentId1 == 1)
                {
                    ListViewItem listViewItem=new ListViewItem(folder.Name1.ToString());
                    listViewItem.ImageIndex = 1;
                    listViewItem.SubItems.Add("file folder");
                    listViewItem.SubItems.Add(folder.ToString());
                    listViewItem.SubItems.Add(folder.CreationDate1.ToString());
                    listView1.Items.Add(listViewItem);

                }



            }


        }

        private void buttonItem6_Click(object sender, EventArgs e)
        {
            if (isFile)
            {
                Database.removeF(SelectedFromList);
                Database.FoldersList.Clear();
                Database.FilesList.Clear();
                Database.Fil_list();
                Database.Fil_list2();
                treeView1.Nodes.Clear();
                treeView1.Nodes.Add("desktop");
                TreeLoad(1, treeView1.Nodes[0]);
                listFill(idselected);
                treeView1.ExpandAll(

                    );
            }
            else
            {
                Database.removeD(SelectedFromList);
                listView1.Items.Clear();
                Database.FoldersList.Clear();
                Database.FilesList.Clear();
                Database.Fil_list();
                Database.Fil_list2();
                treeView1.Nodes.Clear();
                treeView1.Nodes.Add("desktop");
                TreeLoad(1, treeView1.Nodes[0]);
                listFill(idselected);
                treeView1.ExpandAll();
                
            }
        }

        private void buttonItem5_Click(object sender, EventArgs e)
        {
            new SetClipboardHelper(DataFormats.Text, comboBox1.Text).Go();
        }

        abstract class StaHelper
        {
            readonly ManualResetEvent _complete = new ManualResetEvent( false );

            public void Go()
            {
                var thread = new Thread( new ThreadStart( DoWork ) )
                {
                    IsBackground = true,
                };
                thread.SetApartmentState(ApartmentState.STA);
                thread.Start();
            }

            // Thread entry method
            private void DoWork()
            {
                try
                {
                    _complete.Reset();
                    Work();
                }
                catch (Exception ex)
                {
                    if (DontRetryWorkOnFailed)
                        throw;
                    else
                    {
                        try
                        {
                            Thread.Sleep(1000);
                            Work();
                        }
                        catch
                        {
                            // ex from first exception

                        }
                    }
                }
                finally
                {
                    _complete.Set();
                }
            }

            public bool DontRetryWorkOnFailed { get; set; }

            // Implemented in base class to do actual work.
            protected abstract void Work();


        }
        class SetClipboardHelper : StaHelper
        {
            readonly string _format;
            readonly object _data;

            public SetClipboardHelper(string format, object data)
            {
                _format = format;
                _data = data;
            }

            protected override void Work()
            {
                var obj = new System.Windows.Forms.DataObject(
            _format,
            _data
        );

                Clipboard.SetDataObject(obj, true);
            }
        }

        private void buttonItem8_Click(object sender, EventArgs e)
        {
            listView1.LabelEdit = true;
            listView1.SelectedItems[0].BeginEdit();
        }

        private void listView1_AfterLabelEdit(object sender, LabelEditEventArgs e)
        {
            String new_name="name";

            if (e.Label.ToString() != null)
                new_name = e.Label.ToString();
            else
                new_name = "name";

            if (new_name.Contains('@') || new_name.Contains('/') || new_name.Contains('.') || new_name.Contains('%') || new_name.Contains('-') || new_name.Contains('[') || new_name.Contains(']'))
            { 
                goto tarik;
            }
            Database.rename(SelectedFromList, new_name);
            Database.FoldersList.Clear();
            Database.FilesList.Clear();
            Database.Fil_list();
            Database.Fil_list2();
            treeView1.Nodes.Clear();
            treeView1.Nodes.Add("desktop");
            TreeLoad(1, treeView1.Nodes[0]);
            ShowThings();
            foreach (TreeNode tn in treeView1.Nodes)
            {
                tn.Expand();
            }
            tarik: MessageBox.Show("name not valid");


        }

        private void buttonItem16_Click(object sender, EventArgs e)
        {
            listView1.Items.Clear();
           // if(Database.FoldersList.Find(x => x.Name1.Equals(ToCut)).Id1;)
            Database.Move(idselected, HisId);

            Database.FoldersList.Clear();
            Database.FilesList.Clear();
            Database.Fil_list();
            Database.Fil_list2();
            treeView1.Nodes.Clear();
            treeView1.Nodes.Add("desktop");
            TreeLoad(1, treeView1.Nodes[0]);
            listFill(idselected);
            foreach (TreeNode tn in treeView1.Nodes)
            {
                tn.Expand();
            }
        }



        private void buttonItem15_Click(object sender, EventArgs e)
        {
            buttonItem16.Enabled = true;




        }

        private void buttonItem2_Click_1(object sender, EventArgs e)
        {
            buttonItem16.Enabled = true;
            String ToCut=SelectedFromList;
            HisId = Database.FoldersList.Find(x => x.Name1.Equals(ToCut)).Id1;
        }

        private void button1_Click(object sender, EventArgs e)
        {

            int ID;
            String[] path=comboBox1.Text.Split('\\');

            // MessageBox.Show(current);
            try
            {
                String current=path[path.Length-2];
                listView1.Items.Clear();
                ID = Database.FoldersList.Find(x => x.Name1.Equals(current)).Id1;
                listFill(ID);

                //foreach (Folder folder in Database.FoldersList)
                //{

                //    if (folder.ParentId1 == ID)
                //    {
                //        ListViewItem listViewItem=new ListViewItem(folder.Name1.ToString());
                //        listViewItem.ImageIndex = 1;
                //        listViewItem.SubItems.Add("file folder");
                //        listViewItem.SubItems.Add(folder.Size1.ToString());
                //        listViewItem.SubItems.Add(folder.CreationDate1.ToString());
                //        listView1.Items.Add(listViewItem);

                //    }
                //}
                //Database.Fil_list2();

                //foreach (File file in Database.FilesList)
                //{
                //    if (file.ParentId == idselected)
                //    {
                //        ListViewItem listViewItem=new ListViewItem(file.Name1.ToString());
                //        switch (file.Ext)
                //        {
                //            case "txt":
                //                listViewItem.ImageIndex = 13;
                //                break;
                //            case "mp3":
                //                listViewItem.ImageIndex = 5;
                //                break;
                //            case "docs":
                //                listViewItem.ImageIndex = 14;
                //                break;
                //            case "img":
                //                listViewItem.ImageIndex = 4;
                //                break;
                //            default:
                //                listViewItem.ImageIndex = 99;
                //                break;


                //        }
                //        listViewItem.SubItems.Add(file.Ext);
                //        listViewItem.SubItems.Add(file.Size1.ToString());
                //        listViewItem.SubItems.Add(file.DateCreated.ToString());
                //        listView1.Items.Add(listViewItem);

                //    }


                //}


            }
            catch (Exception)
            {
                MessageBox.Show("can't Go back");
            }
            // listView1.Items.Clear();
            String current2=path[path.Length-1];
            if (comboBox1.Text.Contains(current2))
            {
                comboBox1.Text = comboBox1.Text.Replace("\\" + current2, "");
            }
            if (listView1.SelectedItems.Count > 0)
            {
                var item = listView1.SelectedItems[0];
                //rest of your logic
            }

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            listView1.Items.Clear();
            String SearchText=textBox1.Text;
            var filesAndfolders = Database.FoldersList.Zip(Database.FilesList, (folder, file) => new { Folder = folder, File =file });
            if (SearchText != "")
            {
                foreach (var Folder in Database.FoldersList)
                {

                    if (Folder.Name1.Contains(SearchText) && Folder.ParentId1==idselected)
                    {


                        ListViewItem listViewItem=new ListViewItem(Folder.Name1.ToString());
                        listViewItem.ImageIndex = 1;
                        listViewItem.SubItems.Add("file folder");
                        listViewItem.SubItems.Add(Folder.Size1.ToString());
                        listViewItem.SubItems.Add(Folder.CreationDate1.ToString());
                        listView1.Items.Add(listViewItem);


                    }


                }
                foreach (var File in Database.FilesList)
                {

                
                    if (File.Name1.Contains(SearchText)&& File.ParentId==idselected)
                    {
                        ListViewItem listViewItem=new ListViewItem(File.Name1.ToString());
                        switch (File.Ext)
                        {
                            case "txt":
                                listViewItem.ImageIndex = 13;
                                break;
                            case "mp3":
                                listViewItem.ImageIndex = 5;
                                break;
                            case "docs":
                                listViewItem.ImageIndex = 14;
                                break;
                            case "img":
                            case "png":
                                listViewItem.ImageIndex = 4;
                                break;
                            default:
                                listViewItem.ImageIndex = 99;
                                break;


                        }
                        listViewItem.SubItems.Add(File.Ext);
                        listViewItem.SubItems.Add(File.Size1.ToString());
                        listViewItem.SubItems.Add(File.Ext);
                        listViewItem.SubItems.Add(File.DateCreated.ToString());
                        listView1.Items.Add(listViewItem);

                    }

                }
            }
        }
        private void listFill(int id)
        {
            listView1.Items.Clear();
            Database.FoldersList.Clear();
            Database.FilesList.Clear();
            Database.Fil_list();
            Database.Fil_list2();

            foreach (var item in Database.FoldersList)
            {
                if (item.ParentId1 == id)
                {
                    ListViewItem listViewItem=new ListViewItem(item.Name1.ToString());
                    listViewItem.ImageIndex = 1;
                    listViewItem.SubItems.Add("file folder");
                    listViewItem.SubItems.Add(item.Size1.ToString());
                    listViewItem.SubItems.Add(item.CreationDate1.ToString());
                    listView1.Items.Add(listViewItem);

                }

            }
            foreach (var item in Database.FilesList)
            {
                if (item.ParentId == id)
                {

                    ListViewItem listViewItem=new ListViewItem(item.Name1.ToString());
                    switch (item.Ext)
                    {
                        case "txt":
                            listViewItem.ImageIndex = 13;
                            break;
                        case "mp3":
                            listViewItem.ImageIndex = 5;
                            break;
                        case "docs":
                            listViewItem.ImageIndex = 14;
                            break;
                        case "img":
                        case "png":
                            listViewItem.ImageIndex = 4;
                            break;
                        default:
                            listViewItem.ImageIndex = 99;
                            break;


                    }
                    listViewItem.SubItems.Add(item.Ext);
                    listViewItem.SubItems.Add(item.Size1.ToString());
                    listViewItem.SubItems.Add(item.DateCreated.ToString());
                    listView1.Items.Add(listViewItem);
                }

            }


        }

        private String FolderName()
        {
            return "New folder" + Database.GiveMeAnId().ToString();

        }

        private void ribbonControl1_Click(object sender, EventArgs e)
        {

        }
    }
}


