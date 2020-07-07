using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FileGestion_v1._0
{
    public partial class Form1 : Form
    {
        int idCopier;
        String nomCopier;
        int copyOrCut = -1;
        int currentDirectory = 0;
        List<int> route = new List<int>();//route 
        int createOrRename = -1;
        int searchOrNot = 0;
        int ForD = -1;
        //int lastDir;
        String path = "Root";
        string minusPath = Application.StartupPath + Path.DirectorySeparatorChar + "minus.png";
        string plusPath = Application.StartupPath + Path.DirectorySeparatorChar + "plus.png";
        string nodePath = Application.StartupPath + Path.DirectorySeparatorChar + "directory.png";


        public Form1()
        {
            InitializeComponent();
            //this.treeView1.DrawMode = TreeViewDrawMode.OwnerDrawAll;
            //this.treeView1.DrawNode += new DrawTreeNodeEventHandler(treeView1_DrawNode);
            
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //----------------*TreeShit*--------------------   
            //TreeNode n = new TreeNode();
            //tree(0, n);
            //treeView1.Nodes.Add(n);
            treeDemo(0, treeView1);
            
            //---------------------------------------------- 
            
            /*contextMenuStrip1.Items[0].Enabled = false;
            contextMenuStrip1.Items[1].Enabled = false;
            contextMenuStrip1.Items[2].Enabled = false;
            contextMenuStrip1.Items[3].Enabled = false;
            contextMenuStrip1.Items[4].Enabled = false;
            contextMenuStrip1.Items[5].Enabled = false;
            contextMenuStrip1.Items[6].Enabled = true;*/

            urlBox.Text = path;
            listView1.View = View.LargeIcon;
            listView1.ContextMenuStrip = contextMenuStrip2;
            
            afficher(currentDirectory);

        }
        private void state1()
        {
            /*contextMenuStrip1.Items[0].Enabled = false;
            contextMenuStrip1.Items[0].Enabled = false;
            contextMenuStrip1.Items[1].Enabled = false;
            contextMenuStrip1.Items[2].Enabled = false;
            contextMenuStrip1.Items[3].Enabled = false;
            contextMenuStrip1.Items[4].Enabled = false;
            contextMenuStrip1.Items[5].Enabled = false;
            contextMenuStrip1.Items[6].Enabled = true;*/
        }
        private void state2()
        {
            /*contextMenuStrip1.Items[0].Enabled = true;
            contextMenuStrip1.Items[0].Enabled = true;
            contextMenuStrip1.Items[1].Enabled = true;
            contextMenuStrip1.Items[2].Enabled = true;
            contextMenuStrip1.Items[3].Enabled = true;
            contextMenuStrip1.Items[4].Enabled = true;
            contextMenuStrip1.Items[5].Enabled = true;
            contextMenuStrip1.Items[6].Enabled = false;*/
        }

        //-----------------------------------COPY SECTION---------------------------------------------

        //COPY
        private void CopyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedIndices.Count <= 0)
            {
                return;
            }
            int intselectedindex = listView1.SelectedIndices[0];
            if (intselectedindex >= 0)
            {
                if(listView1.Items[intselectedindex].ImageIndex == 0)
                {
                    nomCopier = listView1.Items[intselectedindex].Text;
                    Metier m = new Metier();
                    idCopier = m.returnId(currentDirectory, nomCopier);
                    copyOrCut = 1;
                    ForD = 0;
                }else if(listView1.Items[intselectedindex].ImageIndex == 1)
                {
                    nomCopier = listView1.Items[intselectedindex].Text;
                    Metier m = new Metier();
                    idCopier = m.returnIdFichier(currentDirectory, nomCopier);
                    copyOrCut = 1;
                    ForD = 1;
                }
                
            }
        }

        //COPY TO FROM NAVBAR
        private void PictureBox6_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedIndices.Count <= 0)
            {
                return;
            }
            int intselectedindex = listView1.SelectedIndices[0];
            if (intselectedindex >= 0)
            {
                if (listView1.Items[intselectedindex].ImageIndex == 0)
                {
                    nomCopier = listView1.Items[intselectedindex].Text;
                    Metier m = new Metier();
                    idCopier = m.returnId(currentDirectory, nomCopier);
                    copyOrCut = 1;
                    ForD = 0;
                }
                else if (listView1.Items[intselectedindex].ImageIndex == 1)
                {
                    nomCopier = listView1.Items[intselectedindex].Text;
                    Metier m = new Metier();
                    idCopier = m.returnIdFichier(currentDirectory, nomCopier);
                    copyOrCut = 1;
                    ForD = 1;
                }

            }
        }
        //COPY PATH
        private void Button9_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(urlBox.Text);
        }

        //COPY FROM NAVBAR
        private void Button6_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedIndices.Count <= 0)
            {
                return;
            }
            int intselectedindex = listView1.SelectedIndices[0];
            if (intselectedindex >= 0)
            {
                if (listView1.Items[intselectedindex].ImageIndex == 0)
                {
                    nomCopier = listView1.Items[intselectedindex].Text;
                    Metier m = new Metier();
                    idCopier = m.returnId(currentDirectory, nomCopier);
                    copyOrCut = 1;
                    ForD = 0;
                }
                else if (listView1.Items[intselectedindex].ImageIndex == 1)
                {
                    nomCopier = listView1.Items[intselectedindex].Text;
                    Metier m = new Metier();
                    idCopier = m.returnIdFichier(currentDirectory, nomCopier);
                    copyOrCut = 1;
                    ForD = 1;
                }

            }
        }


        //----------------------------------PASTE SECTION---------------------------------------------

        //Paste
        private void DeleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedIndices.Count <= 0)
            {
                if (copyOrCut == 0)
                {
                    if(ForD == 0)
                    {
                        Metier m = new Metier();
                        m.cutDossier(idCopier, currentDirectory);
                    }else if(ForD == 1)
                    {
                        Metier m = new Metier();
                        m.cutFichier(idCopier, currentDirectory);
                    }
                    copyOrCut = -1;
                    ForD = -1;
                }
                if (copyOrCut == 1)
                {
                    if(ForD == 0)
                    {
                        Metier m = new Metier();
                        m.copyDossier(idCopier, currentDirectory);
                        //m.copyDemo(idCopier, currentDirectory);
                    }else if(ForD == 1)
                    {
                        Metier m = new Metier();
                        m.copyFichier(idCopier, currentDirectory);
                    }
                    
                    ForD = -1;
                    copyOrCut = -1;
                }
                listView1.Items.Clear();
                afficher(currentDirectory);
                //Rebuid the Tree after each folder created
                treeView1.Nodes.Clear();
                treeDemo(0, treeView1);
            }
        }

        //Paste From the NavBar
        private void Button7_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedIndices.Count <= 0)
            {
                if (copyOrCut == 0)
                {
                    if (ForD == 0)
                    {
                        Metier m = new Metier();
                        m.cutDossier(idCopier, currentDirectory);
                    }
                    else if (ForD == 1)
                    {
                        Metier m = new Metier();
                        m.cutFichier(idCopier, currentDirectory);
                    }
                    copyOrCut = -1;
                    ForD = -1;
                }
                if (copyOrCut == 1)
                {
                    if (ForD == 0)
                    {
                        Metier m = new Metier();
                        m.copyDossier(idCopier, currentDirectory);
                    }
                    else if (ForD == 1)
                    {
                        Metier m = new Metier();
                        m.copyFichier(idCopier, currentDirectory);
                    }

                    ForD = -1;
                    copyOrCut = -1;
                }
                listView1.Items.Clear();
                afficher(currentDirectory);
                //Rebuid the Tree after each folder created
                treeView1.Nodes.Clear();
                treeDemo(0, treeView1);
            }
        }

        //---------------------------------CUT SECTION------------------------------------------------

        //CUT
        private void CutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedIndices.Count <= 0)
            {
                return;
            }
            int intselectedindex = listView1.SelectedIndices[0];
            if (intselectedindex >= 0)
            {
                if(listView1.Items[intselectedindex].ImageIndex == 0)
                {
                    nomCopier = listView1.Items[intselectedindex].Text;
                    Metier m = new Metier();
                    idCopier = m.returnId(currentDirectory, nomCopier);
                    copyOrCut = 0;
                    ForD = 0;
                }else if(listView1.Items[intselectedindex].ImageIndex == 1)
                {
                    nomCopier = listView1.Items[intselectedindex].Text;
                    Metier m = new Metier();
                    idCopier = m.returnIdFichier(currentDirectory, nomCopier);
                    copyOrCut = 0;
                    ForD = 1;
                }
                
            }
        }

        //CUT TO FROM NAVBAR
        private void PictureBox5_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedIndices.Count <= 0)
            {
                return;
            }
            int intselectedindex = listView1.SelectedIndices[0];
            if (intselectedindex >= 0)
            {
                if (listView1.Items[intselectedindex].ImageIndex == 0)
                {
                    nomCopier = listView1.Items[intselectedindex].Text;
                    Metier m = new Metier();
                    idCopier = m.returnId(currentDirectory, nomCopier);
                    copyOrCut = 0;
                    ForD = 0;
                }
                else if (listView1.Items[intselectedindex].ImageIndex == 1)
                {
                    nomCopier = listView1.Items[intselectedindex].Text;
                    Metier m = new Metier();
                    idCopier = m.returnIdFichier(currentDirectory, nomCopier);
                    copyOrCut = 0;
                    ForD = 1;
                }

            }
        }
        //CUT FROM NAVBAR
        private void Button8_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedIndices.Count <= 0)
            {
                return;
            }
            int intselectedindex = listView1.SelectedIndices[0];
            if (intselectedindex >= 0)
            {
                if (listView1.Items[intselectedindex].ImageIndex == 0)
                {
                    nomCopier = listView1.Items[intselectedindex].Text;
                    Metier m = new Metier();
                    idCopier = m.returnId(currentDirectory, nomCopier);
                    copyOrCut = 0;
                    ForD = 0;
                }
                else if (listView1.Items[intselectedindex].ImageIndex == 1)
                {
                    nomCopier = listView1.Items[intselectedindex].Text;
                    Metier m = new Metier();
                    idCopier = m.returnIdFichier(currentDirectory, nomCopier);
                    copyOrCut = 0;
                    ForD = 1;
                }

            }
        }


        //---------------------------------CREATE FOLDER ---------------------------------------------

        //new Folder from navbar
        private void Button13_Click(object sender, EventArgs e)
        {
            createOrRename = 0;
            ForD = 0;
            Metier m = new Metier();
            ListViewItem itm = new ListViewItem("New Folder");
            itm.ImageIndex = 0;
            listView1.Items.Add(itm);
            itm.BeginEdit();
            int intselectedindex = listView1.SelectedIndices[0];
            m.createDossier(listView1.Items[intselectedindex].Text, currentDirectory);
            //Rebuid the Tree after each folder created
            treeView1.Nodes.Clear();
            treeDemo(0, treeView1);
        }
        private void FolderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Dossier d = new Dossier("New Folder", currentDirectory);
            Metier m = new Metier();
            int i = 0;
            while (m.find(d.getNom(), currentDirectory))
            {
                d.setNom("New Folder (" + i + ")");
                i++;
            }
            m.createDossier(d.getNom(), currentDirectory);
            listView1.Items.Clear();
            afficher(currentDirectory);

            //Rebuid the Tree after each folder created
            treeView1.Nodes.Clear();
            treeDemo(0, treeView1);
        }

        //---------------------------------CREATE FILE ---------------------------------------------
        //CREATE FILE
        private void FileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Dossier d = new Dossier("New File", currentDirectory);
            Metier m = new Metier();
            int i = 0;
            while (m.findFichier(d.getNom(), currentDirectory))
            {
                d.setNom("New File (" + i + ")");
                i++;
            }
            m.createFichier(d.getNom(), currentDirectory);
            listView1.Items.Clear();
            afficher(currentDirectory);

            //Rebuid the Tree after each folder created
            treeView1.Nodes.Clear();
            treeDemo(0, treeView1);
        }
        //---------------------------------OPEN FOLDER------------------------------------------------
        //OPEN
        private void ListView1_DoubleClick(object sender, EventArgs e)
        {
            if (listView1.SelectedIndices.Count <= 0)
            {
                return;
            }
            int intselectedindex = listView1.SelectedIndices[0];
            if (intselectedindex >= 0)
            {
                if (listView1.Items[intselectedindex].ImageIndex == 0)
                {
                    String text = listView1.Items[intselectedindex].Text;
                    setPath(text);
                    Metier m = new Metier();
                    int id = m.returnId(currentDirectory, text);
                    listView1.Items.Clear();
                    afficher(id);
                    cd(currentDirectory, text);
                }
            }
        }
        //open
        private void OpenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedIndices.Count <= 0)
            {
                return;
            }
            int intselectedindex = listView1.SelectedIndices[0];
            if (intselectedindex >= 0)
            {
                if (listView1.Items[intselectedindex].ImageIndex == 0)
                {
                    String text = listView1.Items[intselectedindex].Text;
                    setPath(text);
                    Metier m = new Metier();
                    int id = m.returnId(currentDirectory, text);
                    listView1.Items.Clear();
                    afficher(id);
                    cd(currentDirectory, text);
                }
            }
        }
        //OPEN FROM THE NAVBAR
        private void PictureBox11_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedIndices.Count <= 0)
            {
                return;
            }
            int intselectedindex = listView1.SelectedIndices[0];
            if (intselectedindex >= 0)
            {
                if (listView1.Items[intselectedindex].ImageIndex == 0)
                {
                    String text = listView1.Items[intselectedindex].Text;
                    setPath(text);
                    Metier m = new Metier();
                    int id = m.returnId(currentDirectory, text);
                    listView1.Items.Clear();
                    afficher(id);
                    cd(currentDirectory, text);
                }
            }
        }


        //-----------------------------------RENAME FOLDER--------------------------------------------
 
        //rename from navbar
        private void Button12_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedIndices.Count <= 0)
            {
                return;
            }
            int intselectedindex = listView1.SelectedIndices[0];
            if (intselectedindex >= 0)
            {
                if (listView1.Items[intselectedindex].ImageIndex == 0)
                {
                    listView1.Items[intselectedindex].BeginEdit();
                    createOrRename = 1;//Rename
                    ForD = 0;
                }
                else if (listView1.Items[intselectedindex].ImageIndex == 1)
                {
                    listView1.Items[intselectedindex].BeginEdit();
                    createOrRename = 1;//Rename
                    ForD = 1;
                }
            }
        }

        //rename from contextMenuStrip
        private void RenameToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedIndices.Count <= 0)
            {
                return;
            }
            int intselectedindex = listView1.SelectedIndices[0];
            if (intselectedindex >= 0)
            {
                if(listView1.Items[intselectedindex].ImageIndex == 0)
                {
                    listView1.Items[intselectedindex].BeginEdit();
                    createOrRename = 1;//Rename
                    ForD = 0;
                }else if(listView1.Items[intselectedindex].ImageIndex == 1)
                {
                    listView1.Items[intselectedindex].BeginEdit();
                    createOrRename = 1;//Rename
                    ForD = 1;
                }
            }
        }

        private void ListView1_AfterLabelEdit(object sender, LabelEditEventArgs e)
        {
            if (e.Label == null)
                return;

            Metier m = new Metier();
            int intselectedindex = listView1.SelectedIndices[0];
            String text = listView1.Items[intselectedindex].Text;
            if(ForD == 0 && listView1.Items[intselectedindex].ImageIndex == 0)
            {
                int id = m.returnId(currentDirectory, text);
                if (createOrRename == 0)
                {
                    if (m.find(e.Label.ToString(), currentDirectory))
                        m.createDossier(e.Label.ToString(), currentDirectory);
                }
                if (!m.renameDossier(id, e.Label.ToString(), currentDirectory))
                {
                    MessageBox.Show("This Destination already contains a folder named " + e.Label.ToString());
                    listView1.Items[intselectedindex].BeginEdit();
                }
            }else if(ForD == 1 && listView1.Items[intselectedindex].ImageIndex == 1)
            {
                int id = m.returnIdFichier(currentDirectory, text);
                if (createOrRename == 0)
                {
                    if (m.findFichier(e.Label.ToString(), currentDirectory))
                        m.createFichier(e.Label.ToString(), currentDirectory);
                }
                //------------------------------------------
                if (!m.renameFichier(id, e.Label.ToString(), currentDirectory))
                {
                    MessageBox.Show("This Destination already contains a file named " + e.Label.ToString());
                    listView1.Items[intselectedindex].BeginEdit();
                }
                //------------------------------------------
            }


            createOrRename = -1;
            //Rebuid the Tree after each folder created
            treeView1.Nodes.Clear();
            treeDemo(0, treeView1);
        }
        //----------------------------------TREE SHIT-------------------------------------------------
        //TREE
        private void tree(int id, TreeNode node)
        {
            Metier db = new Metier();
            List<Dossier> d = new List<Dossier>();
            d = db.listerDossier(id);
            foreach (Dossier dd in d)
            {
                TreeNode t = new TreeNode();
                t.Text = dd.getNom();
                node.Nodes.Add(t);

                tree(dd.getIdDossier(), t);
            }
        }
        private void treeDemo(int id, TreeView node)
        {
            Metier db = new Metier();
            List<Dossier> d = new List<Dossier>();
            d = db.listerDossier(id);
            foreach (Dossier dd in d)
            {
                TreeNode t = new TreeNode();
                t.Text = dd.getNom();
                node.Nodes.Add(t);
                tree(dd.getIdDossier(), t);
            }
        }

        /*void treeView1_DrawNode(object sender, DrawTreeNodeEventArgs e)
        {
            //treeView1.ForeColor = Color.White;
            Rectangle nodeRect = e.Node.Bounds;

            /*--------- 1. draw expand/collapse icon ---------*/
            /*Point ptExpand = new Point(nodeRect.Location.X - 20, nodeRect.Location.Y + 2);
            Image expandImg = null;
            if (e.Node.IsExpanded || e.Node.Nodes.Count < 1)
                expandImg = Image.FromFile(minusPath);
            else
                expandImg = Image.FromFile(plusPath);
            Graphics g = Graphics.FromImage(expandImg);
            IntPtr imgPtr = g.GetHdc();
            g.ReleaseHdc();
            e.Graphics.DrawImage(expandImg, ptExpand);

            /*--------- 2. draw node icon ---------*/
            /*Point ptNodeIcon = new Point(nodeRect.Location.X - 4, nodeRect.Location.Y + 2);
            Image nodeImg = Image.FromFile(nodePath);
            g = Graphics.FromImage(nodeImg);
            imgPtr = g.GetHdc();
            g.ReleaseHdc();
            e.Graphics.DrawImage(nodeImg, ptNodeIcon);

            /*--------- 3. draw node text ---------*/
            /*Font nodeFont = e.Node.NodeFont;
            if (nodeFont == null)
                nodeFont = ((TreeView)sender).Font;
            Brush textBrush = SystemBrushes.WindowText;
            //to highlight the text when selected
            if ((e.State & TreeNodeStates.Focused) != 0)
                textBrush = SystemBrushes.HotTrack;
            //Inflate to not be cut
            Rectangle textRect = nodeRect;
            //need to extend node rect
            textRect.Width += 40;
            e.Graphics.DrawString(e.Node.Text, nodeFont, textBrush,
                Rectangle.Inflate(textRect, -12, 0));
        }*/

        //----------------------------------BACK AND FORWARD BUTTONS ---------------------------------------
        //Back Button
        private void BackButton_Click_1(object sender, EventArgs e)
        {

            if (searchOrNot == 1)
            {
                listView1.Items.Clear();
                afficher(currentDirectory);
                searchOrNot = 0;
            }
            if (currentDirectory > 0)
            {
                listView1.Items.Clear();
                cdBack(currentDirectory);
                afficher(currentDirectory);
                setPathBack(urlBox.Text);
            }
        }
        //Forward Click
        private void Button1_Click(object sender, EventArgs e)
        {
            if (route.Count() > 0)
            {
                Metier m = new Metier();
                int i = route.Count();
                listView1.Items.Clear();
                currentDirectory = route[i - 1];
                afficher(currentDirectory);
                setPath(m.searchWithId(route[i - 1]));
                //m.searchWithId(route[i - 1]);
                route.RemoveAt(i - 1);
            }
        }
        //UP BUTTON
        private void Button4_Click(object sender, EventArgs e)
        {
            if (searchOrNot == 1)
            {
                listView1.Items.Clear();
                afficher(currentDirectory);
                searchOrNot = 0;
            }
            if (currentDirectory > 0)
            {
                listView1.Items.Clear();
                cdBack(currentDirectory);
                afficher(currentDirectory);
                setPathBack(urlBox.Text);
            }
        }

        //----------------------------------CURRENT DIR SHIT ---------------------------------------
        //CHANGE DIR
        private void cd(int i, String str)
        {
            Metier m = new Metier();
            currentDirectory = m.returnId(i, str);
        }
        private void cdBack(int i)
        {
            route.Add(currentDirectory);
            Metier m = new Metier();
            currentDirectory = m.returnFatherId(i);
        }
        //----------------------------------PATH SHIT ---------------------------------------
        //SET PATH
        private void setPath(String str)
        {
            urlBox.Text = urlBox.Text + "/" + str;
        }
        private void setPathBack(String str)
        {
            String[] strSplitted = str.Split('/');
            int count = 0;
            foreach (String s in strSplitted)
                count++;
            String r = "";
            for (int i = 0; i < count - 1; i++)
            {
                if (i > 0)
                {
                    r += "/" + strSplitted[i];
                }
                else

                    r += strSplitted[i];
            }
            urlBox.Text = r;
        }

        //----------------------------------DELETE SHIT ---------------------------------------
        //DELETE FROM NAVBAR
        private void Button11_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedIndices.Count <= 0)
            {
                return;
            }
            int intselectedindex = listView1.SelectedIndices[0];
            if (intselectedindex >= 0)
            {
                if(listView1.Items[intselectedindex].ImageIndex == 0)
                {
                    
                        String text = listView1.Items[intselectedindex].Text;
                        Metier m = new Metier();
                        int id = m.returnId(currentDirectory, text);
                        m.deleteDossier(id);
                    
                        
                }else if(listView1.Items[intselectedindex].ImageIndex == 1)
                {
                    String text = listView1.Items[intselectedindex].Text;
                    Metier m = new Metier();
                    int id = m.returnIdFichier(currentDirectory, text);
                    m.deleteFichier(id);
                }
                        
                
                listView1.Items.Clear();
                afficher(currentDirectory);
                //Rebuid the Tree after each folder created
                treeView1.Nodes.Clear();
                treeDemo(0, treeView1);
            }
        }
        //DELETE
        private void RenameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedIndices.Count <= 0)
            {
                return;
            }
            int intselectedindex = listView1.SelectedIndices[0];
            if (intselectedindex >= 0)
            {
                if (listView1.Items[intselectedindex].ImageIndex == 0)
                {
                    String text = listView1.Items[intselectedindex].Text;
                    Metier m = new Metier();
                    int id = m.returnId(currentDirectory, text);
                    m.deleteDossier(id);
                }
                else if (listView1.Items[intselectedindex].ImageIndex == 1)
                {
                    String text = listView1.Items[intselectedindex].Text;
                    Metier m = new Metier();
                    int id = m.returnIdFichier(currentDirectory, text);
                    m.deleteFichier(id);
                }

                listView1.Items.Clear();
                afficher(currentDirectory);
                //Rebuid the Tree after each folder created
                treeView1.Nodes.Clear();
                treeDemo(0, treeView1);
            }
        }

        //----------------------------------SEARCH SHIT ---------------------------------------
        //--
        private List<Dossier> search(int id, String name)
        {
            Metier m = new Metier();
            return m.search(id, name);
        }
        private void SearchBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                listView1.Items.Clear();
                List<Dossier> d = new List<Dossier>();
                d = search(currentDirectory, searchBox.Text);
                for (int i = 0; i < d.Count(); i++)
                {
                    string h = d[i].getNom();
                    //MessageBox.Show("h");
                    ListViewItem itm = new ListViewItem(h);
                    itm.ImageIndex = 0;
                    listView1.Items.Add(itm);
                }
            }
            searchOrNot = 1;
        }

        //----------------------------------AFFICHAGE SHIT ---------------------------------------
        //--
        public void afficher(int id)
        {
            try
            {
                Metier db = new Metier();
                List<Dossier> d = new List<Dossier>();
                d = db.listerDossier(id);
                for (int i = 0; i < d.Count; i++)
                {
                    string h = d[i].getNom();
                    //MessageBox.Show("h");
                    ListViewItem itm = new ListViewItem(h);
                    itm.ImageIndex = 0;
                    listView1.Items.Add(itm);
                }
                List<Fichier> f = new List<Fichier>();
                f = db.listerFichier(id);
                for (int i = 0; i < f.Count; i++)
                {
                    string h = f[i].getName();
                    //MessageBox.Show("h");
                    ListViewItem itm = new ListViewItem(h);
                    itm.ImageIndex = 1;
                    listView1.Items.Add(itm);
                }

                itemsCounter.Text = d.Count + f.Count + " items  |";
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
            }
        }

        //--------------FUNCTIONS THT DOES NO FUCKING THING BUT DO NOT FUCKING DELETE THEM -----------
        //--
        private void ListView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            //ImageList imgs = new ImageList();
            //imgs.ImageSize = new Size(100, 100);
        }
        private void SearchBox_TextChanged(object sender, EventArgs e)
        {
            //DialogResult dialogResult = MessageBox.Show("Vous etes sure vous voulez supprimer cet etudiant", "Vérification", MessageBoxButtons.YesNo);
            //Console.WriteLine("hello");
        }

        private void SearchBox_Validated(object sender, EventArgs e)
        {
            //DialogResult dialogResult = MessageBox.Show("Vous etes sure vous voulez supprimer cet etudiant", "Vérification", MessageBoxButtons.YesNo);
        }

        private void SearchBox_Enter(object sender, EventArgs e)
        {
            //DialogResult dialogResult = MessageBox.Show("Vous etes sure vous voulez supprimer cet etudiant", "Vérification", MessageBoxButtons.YesNo);
        }

        private void SearchBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            //if(e.key == Keys.Enter)
            /*{
                listView1.Items.Clear();
                List<Dossier> d = new List<Dossier>();
                d = search(currentDirectory, searchBox.Text);
                for(int i = 0; i<d.Count(); i++)
                {
                    string h = d[i].getNom();
                    //MessageBox.Show("h");
                    ListViewItem itm = new ListViewItem(h);
                    itm.ImageIndex = 0;
                    listView1.Items.Add(itm);
                }
            }*/
            
        }

        private void ListView1_MouseEnter(object sender, EventArgs e)
        {
            
        }
        private void Panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void Panel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void Button10_Click(object sender, EventArgs e)
        {

        }

        private void NewToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
        //Drag and Drop
        private void ListView1_DragDrop(object sender, DragEventArgs e)
        {
            listView1.Items.Add(e.Data.ToString());
        }

        private void ListView1_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Copy;
        }

        private void ListView1_ItemDrag(object sender, ItemDragEventArgs e)
        {

        }
        private void Label5_Click(object sender, EventArgs e)
        {

        }

        private void TreeView1_AfterExpand(object sender, TreeViewEventArgs e)
        {

        }

        private void TreeView1_AfterCollapse(object sender, TreeViewEventArgs e)
        {

        }

        private void ListView1_MouseDown(object sender, MouseEventArgs e)
        {
            
        }
    }
}
