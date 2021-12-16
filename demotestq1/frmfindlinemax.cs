using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Linq;

namespace demotestq1
{
    public partial class frmfindlinemax : Form
    {
        private List<string> lsts2 = new List<string>();
        public int V;
        public List<int>[] adj;
        List<string> lstrids = new List<string>();

        public List<string>[] lstcrawl;
        public List<string>[] lstlevel;
        public List<int>[] lsttrongso;
        string strout;

        List<int> lstnodefinal = new List<int>();
        int[] arrtrongso = new int[1000];
        int[] arrpath = new int[1000];
        List<string> lstout = new List<string>();

        static int level;
        List<int> lstvitri = new List<int>();
        List<int> lsttrongsosave = new List<int>();







        public frmfindlinemax(List<string> lst)
        {
            InitializeComponent();
            for (int i = 0; i < lst.Count; i++)
            {
                lsts2.Add(lst[i]);
                //richtest.Text += lsts2[i];
            }
            lsts2 = lsts2.Distinct().ToList();
            
        }

        public void addEdge(int v, int w, int weight)
        {

            // split all edges of weight 2 into two
            // edges of weight 1 each. The intermediate
            // vertex number is maximum vertex number + 1,
            // that is V.
            if (weight == 2)
            {
                adj[v].Add(v + this.V);
                adj[v + this.V].Add(w);
            }
            else // Weight is 1
                adj[v].Add(w); // Add w to v's list.
        }
        public int printShortestPath(int[] parent, int s, int d)
        {
            level = 0;

            // If we reached root of shortest path tree
            if (parent[s] == -1)
            {
                txtoutpath.Text = s.ToString();

                lstout.Add(lsts2[s]);
                lstvitri.Add(s);
                return level;
            }

            printShortestPath(parent, parent[s], d);

            level++;
            if (s < this.V)
                txtoutpath.Text = txtoutpath.Text + "->" + s.ToString();
            lstout.Add(lsts2[s]);
            lstvitri.Add(s);

            return level;
        }

        public int findShortestPath(int src, int dest)
        {
            bool[] visited = new bool[2 * this.V];
            int[] parent = new int[2 * this.V];

            // Initialize parent[] and visited[]
            for (int i = 0; i < 2 * this.V; i++)
            {
                visited[i] = false;
                parent[i] = -1;
            }

            // Create a queue for BFS
            List<int> queue = new List<int>();

            // Mark the current node as visited and enqueue it
            visited[src] = true;
            queue.Add(src);

            while (queue.Count != 0)
            {
                // Dequeue a vertex from queue and print it
                int s = queue[0];

                if (s == dest)
                    return printShortestPath(parent, s, dest);
                queue.RemoveAt(0);

                // Get all adjacent vertices of the dequeued vertex s
                // If a adjacent has not been visited, then mark it
                // visited and enqueue it
                foreach (int i in this.adj[s])
                {
                    if (!visited[i])
                    {
                        visited[i] = true;
                        queue.Add(i);
                        parent[i] = s;
                    }
                }
            }
            return 0;
        }


        public int tongtrongso(List<int> lst)
        {
            int s = 0;
            for (int i = 0; i < lst.Count; i++)
            {
                s += lst[i];
            }
            return s;
        }

        private void button2_Click(object sender, EventArgs e)
        {





        }

        private void frmfindlinemax_Load(object sender, EventArgs e)
        {
            try
            {

         

            List<int> lstdaadd = new List<int>();
            string[,] arrcrawl = new string[lsts2.Count, 10];



            List<string> test = new List<string>();
            //txbcuoi.Text = lsts2.Count.ToString();



            V = lsts2.Count();


            adj = new List<int>[2 * V];
            lstcrawl = new List<string>[2 * V];
            lstlevel = new List<string>[2 * V];
            lsttrongso = new List<int>[2 * V];
            for (int i = 0; i < 2 * V; i++)
            {
                adj[i] = new List<int>();
                lstcrawl[i] = new List<string>();
                lstlevel[i] = new List<string>();
            }



            Random rd = new Random();

            //XmlDocument doc = new XmlDocument();

            //XElement xel = new XElement("root");

            // set up 
            int max = 0;
            for (int i = 0; i < lsts2.Count; i++)
            {
                lstcrawl[i].Add(lsts2[i]);
                string[] arrListStr = lsts2[i].Split('/');

                if (arrListStr.Length > max)
                {
                    max = arrListStr.Length;
                }

                for (int j = 0; j < arrListStr.Length; j++)
                {
                    lstcrawl[i].Add(arrListStr[j]);

                }

            }

            try
            {
                int d = 1;
                // while(d<=max)
                //{
                for (int i = 0; i < lsts2.Count; i++)
                {
                    if (lstcrawl[i].Count == 4)
                    {
                        //richtest.Text += "\n" + lstcrawl[i][3];


                        int random = rd.Next(0, 1);
                        //lsttrongso[i].Add(random);
                        addEdge(0, i, random);
                        lstdaadd.Add(i);
                        lstlevel[d].Add(i.ToString());
                        lstlevel[d].Add(lstcrawl[i][3]);
                    }
                    if (lstcrawl[i].Count == 5)
                    {
                        if (arrcrawl[i, 5] == "")
                        {
                            //richtest.Text += "\n" + lstcrawl[i][3];

                            int random = rd.Next(0, 1);
                            //lsttrongso[i].Add(random);
                            addEdge(0, i, random);
                            lstdaadd.Add(i);
                            lstlevel[1].Add(i.ToString());
                            lstlevel[1].Add(lstcrawl[i][3]);
                        }
                    }


                }

                for (int i = 0; i < lsts2.Count; i++)
                {
                    for (int j = 0; j < lstlevel[1].Count; j++)
                    {
                        if (lstcrawl[i].Count == 5)
                        {
                            if (lstcrawl[i][3] == lstlevel[d][j] || lstcrawl[i][4] == lstlevel[d][j] || lstcrawl[i][2] == lstlevel[d][j])
                            {
                                //richtest.Text += " " + lstcrawl[i][4] + " " + lstlevel[d][j];


                                int random = rd.Next(0, 1);
                                //lsttrongso[i].Add(random);
                                addEdge(int.Parse(lstlevel[d][0]), i, random);
                                lstdaadd.Add(i);

                                lstlevel[2].Add(i.ToString());
                                lstlevel[2].Add(lstcrawl[i][4]);
                            }
                        }
                    }

                }

                for (int i = 0; i < lsts2.Count; i++)
                {
                    for (int j = 0; j < lstlevel[2].Count; j++)
                    {
                        if (lstcrawl[i].Count == 6)
                        {
                            if (lstcrawl[i][4] == lstlevel[d][j] || lstcrawl[i][3] == lstlevel[d][j] || lstcrawl[i][5] == lstlevel[d][j])
                            {
                                //richtest.Text += " " + lstcrawl[i][5] + "//" + lstlevel[d][j];


                                int random = rd.Next(0, 1);
                                //lsttrongso[i].Add(random);

                                addEdge(int.Parse(lstlevel[d][0]), i, random);

                                lstdaadd.Add(i);
                                lstlevel[3].Add(i.ToString());
                                lstlevel[3].Add(lstcrawl[i][5]);
                            }
                        }
                    }

                }
            }
            catch (Exception ex)
            {

            }

            // }




            XmlDocument doc = new XmlDocument();
            doc.Load(Application.StartupPath + "\\" + "myXmFile" + ".xml");


            XmlNode root = doc.DocumentElement;

            root.RemoveAll();
            //doc.RemoveAll();


            XmlNode person = doc.CreateElement("per");
            XmlNode name = doc.CreateElement("name1");
            person.AppendChild(name);
            doc.DocumentElement.AppendChild(person);

            doc.Save(Application.StartupPath + "\\" + "myXmFile" + ".xml");

            XmlNode person11 = root.SelectSingleNode("per/name1");
            XmlNode name11 = doc.CreateElement("name2");
            person11.AppendChild(name11);

            doc.Save(Application.StartupPath + "\\" + "myXmFile" + ".xml");

            XmlNode person22 = root.SelectSingleNode("per/name1/name2");
            XmlNode name22 = doc.CreateElement("name3");
            person22.AppendChild(name22);

            doc.Save(Application.StartupPath + "\\" + "myXmFile" + ".xml");
            XmlNode person33 = root.SelectSingleNode("per/name1/name2/name3");
            XmlNode name33 = doc.CreateElement("name4");
            person33.AppendChild(name33);
            doc.Save(Application.StartupPath + "\\" + "myXmFile" + ".xml");
            XmlNode person44 = root.SelectSingleNode("per/name1/name2/name3/name4");
            XmlNode name44 = doc.CreateElement("name5");
            person44.AppendChild(name44);
            doc.Save(Application.StartupPath + "\\" + "myXmFile" + ".xml");
            XmlNode person55 = root.SelectSingleNode("per/name1/name2/name3/name4/name5");
            XmlNode name55 = doc.CreateElement("name6");
            person55.AppendChild(name55);
            doc.Save(Application.StartupPath + "\\" + "myXmFile" + ".xml");
            XmlNode person66 = root.SelectSingleNode("per/name1/name2/name3/name4/name5/name6");
            XmlNode name66 = doc.CreateElement("name7");
            person66.AppendChild(name66);
            doc.Save(Application.StartupPath + "\\" + "myXmFile" + ".xml");
            XmlNode person77 = root.SelectSingleNode("per/name1/name2/name3/name4/name5/name6/name7");
            XmlNode name77 = doc.CreateElement("name8");
            person77.AppendChild(name77);




            // doc.Load(Application.StartupPath + "\\" + "myXmFile1" + ".xml");

            //XmlNode person = doc.CreateElement("name" + 0.ToString() );
            //XmlNode name = doc.CreateElement("name" + 1.ToString() );





            doc.Save(Application.StartupPath + "\\" + "myXmFile" + ".xml");






            addEdge(0, 1, rd.Next(0, 1));


            List<string> lstparent = new List<string>();
            List<int> lsttrung = new List<int>();




            XmlDocument doc1 = new XmlDocument();

            doc1.Load(Application.StartupPath + "\\" + "myXmFile" + ".xml");

            XmlNode root1 = doc1.DocumentElement;










            // doc.Load(Application.StartupPath + "\\" + "myXmFile1" + ".xml");

            //XmlNode person = doc.CreateElement("name" + 0.ToString() );
            //XmlNode name = doc.CreateElement("name" + 1.ToString() );


            XmlNode name1;
            XmlNode person1;


            lstparent.Clear();
            lstparent.Add("name" + 2.ToString());
            lstparent.Add("name" + 3.ToString());
            lsttrung.Add(1);
            lsttrung.Add(2);
            lsttrung.Add(3);
            lsttrung.Add(4);
            //person.AppendChild(name);



            // doc.DocumentElement.AppendChild(person);


            //doc.Save(Application.StartupPath + "\\" + "myXmFile1" + ".xml");





            for (int i = 1; i < lsts2.Count; i++)
            {
                int rad = 0;
                int rad1 = 0;
                bool check = false;

                for (int j = 0; j < lsttrung.Count; j++)
                {
                    rad = rd.Next(1, V);
                    rad1 = rd.Next(1, V);
                    if (rad == lsttrung[j])
                    {
                        check = true;
                    }
                    if (rad1 == lsttrung[j])
                    {
                        check = true;
                    }
                    if (check == true)
                    {
                        rad--;
                        rad1--;
                    }
                }




                //doc1.Load(Application.StartupPath + "\\" + "myXmFile" + ".xml");



                int rad2 = rd.Next(0, lstparent.Count - 1);
                string number = lstparent[rad2];

                //richtest.Text += "  " + number;

                int rdfn = 1;
                Random rdd = new Random();
                rdfn = rdd.Next(1, 8);

                person1 = root1.SelectSingleNode("per");
                //person1 = root.SelectSingleNode("per/name1");
                if (rdfn == 1)
                {
                    person1 = root1.SelectSingleNode("per");
                }
                if (rdfn == 2)
                {
                    person1 = root1.SelectSingleNode("per/name1");
                }
                if (rdfn == 3)
                {
                    person1 = root1.SelectSingleNode("per/name1/name2");
                }
                if (rdfn == 4)
                {
                    person1 = root1.SelectSingleNode("per/name1/name2/name3");
                }
                if (rdfn == 5)
                {
                    person1 = root1.SelectSingleNode("per/name1/name2/name3/name4");
                }
                if (rdfn == 6)
                {
                    person1 = root1.SelectSingleNode("per/name1/name2/name3/name4/name5");
                }
                if (rdfn == 7)
                {
                    person1 = root1.SelectSingleNode("per/name1/name2/name3/name4/name5/name6");
                }

                if (rdfn == 8)
                {
                    person1 = root1.SelectSingleNode("per/name1/name2/name3/name4/name5/name6/name7");
                }




                lsttrung.Add(rad1);
                name1 = doc1.CreateElement("name" + rad1.ToString());

                //person = root.SelectSingleNode("descendant::per");
                person1.AppendChild(name1);

                //person = doc.CreateElement("name" + 231.ToString(), "name" + 1.ToString());

                lstparent.Add("name" + rad1.ToString());







                //doc.DocumentElement.AppendChild(person1);


                doc1.Save(Application.StartupPath + "\\" + "myXmFile" + ".xml");


                int random = rd.Next(0, 1);
                //lsttrongso[i].Add(random);



                addEdge(rad, rad1, random);



            }




            //xel.Save(@"D:\dest.xml");





            richtxbout.Clear();
            lstrids.Clear();


            try
            {
                lstout.Clear();
                lstvitri.Clear();


                string[] arrListStr = lsts2[0].Split('/');
                for (int i = 0; i < lsts2.Count; i++)
                {
                    string[] arrListStr1 = lsts2[i].Split('/');
                    if (arrListStr1.Length > 2)
                    {
                        if (arrListStr[2] != arrListStr1[2])
                        {
                            lstnodefinal.Add(i);
                        }
                    }

                }




                for (int i = 0; i < adj.Length; i++)
                {
                    if (adj[i].Count == 1)
                    {
                        lstnodefinal.Add(adj[i][0]);
                        cbblistnode.Items.Add(adj[i][0]);

                    }
                }

                strout = "";

                int counts2 = 1;

                for (int i = 0; i < lstnodefinal.Count; i++)
                {
                    lstout.Clear();
                    lstvitri.Clear();
                    findShortestPath(0, lstnodefinal[i]);
                    int s = 0;
                    for (int m = 0; m < lstout.Count; m++)
                    {
                        Random rdd1 = new Random();
                        s += rdd1.Next(1, 2);
                    }
                    if (s != 0)
                    {
                        arrtrongso[i] = s;
                        arrpath[i] = counts2;
                        richtxbout.Text = richtxbout.Text + "Path " + counts2.ToString() + " (" + s.ToString() + ")" + ":\n";
                        lstrids.Add("Path " + i.ToString() + "(" + s.ToString() + ")" + ":");
                        for (int j = 0; j < lstout.Count; j++)
                        {

                            richtxbout.Text = richtxbout.Text + "Node" + lstvitri[j] + "." + " " + lstout[j] + "\n";
                            lstrids.Add(lstvitri[j] + "." + " " + lstout[j]);
                        }
                        counts2++;
                        richtxbout.Text = richtxbout.Text + "\n";
                    }
                    




                }
                strout = richtxbout.Text ;


              

         







            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                txbdau.Clear();
                txbcuoi.Clear();
                txtoutpath.Clear();
            }


        }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void txtnumber_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (txtnumber.Text != "")
                {
                    txturl.Text = lsts2[int.Parse(txtnumber.Text)];
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                txtnumber.Clear();
            }
        }

        private void cbblistnode_SelectedIndexChanged(object sender, EventArgs e)
        {
            txbcuoi.Text = cbblistnode.SelectedItem.ToString();
        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }

        private void button6_Click(object sender, EventArgs e)
        {
            frmTreeView frm = new frmTreeView(lsts2);
            frm.Show();
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            string fileLPath = txtpath1.Text;


            //richtest.Text = lstrids[2];
            //System.IO.File.WriteAllText(fileLPath, conttran);
            System.IO.File.WriteAllLines(fileLPath, lstrids);
        }

        private void richtxbout_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            
        }

        private void button3_Click(object sender, EventArgs e)
        {

            Random rd = new Random();
            if(cbbtypelist.SelectedIndex==0)
            {
                
                lstrids.Clear();

                List<int> lstsort = new List<int>();
            richtxbout.Clear();
            //richtest.Clear();
            txbdau.Clear();
            txbcuoi.Clear();

            for (int i = 0; i < lstnodefinal.Count; i++)
            {
                lstout.Clear();
                lstvitri.Clear();
                findShortestPath(0, lstnodefinal[i]);
                lstsort.Add(lstout.Count);
            }
            lstsort.Sort();
                    
            //int dem11 = lstsort[lstsort.Count-1];
            for (int i = 0; i < lstnodefinal.Count; i++)
            {
                lstout.Clear();
                lstvitri.Clear();
                findShortestPath(0, lstnodefinal[i]);
                //int s = 0;
             
                //lstsort.Add(lstout.Count);

                if (lstout.Count >= lstsort[lstsort.Count - (lstsort.Count/10) ])
                {
                  
                    richtxbout.Text = richtxbout.Text + "Path " + arrpath[i].ToString() + " (" + arrtrongso[i].ToString() + ")" + ":\n";
                    lstrids.Add("Path " + i.ToString() + "(" + lstsort.Count.ToString() + ")" + ":");
                    for (int j = 0; j < lstout.Count; j++)
                    {

                        richtxbout.Text = richtxbout.Text + "Node " + lstvitri[j] + "." + " " + lstout[j] + "\n";
                        lstrids.Add(lstvitri[j] + "." + " " + lstout[j]);
                    }
                        richtxbout.Text = richtxbout.Text + "\n";

                    }
                    




                }
            }

            if(cbbtypelist.SelectedIndex == 1)
            {
                richtxbout.Clear();

                richtxbout.Text = strout;
                /*

                richtxbout.Clear();
                lstrids.Clear();
                lstvitri.Clear();
                lstout.Clear();

                try
                {
                    lstout.Clear();
                    lstvitri.Clear();


                    string[] arrListStr = lsts2[0].Split('/');
                    for (int i = 0; i < lsts2.Count; i++)
                    {
                        string[] arrListStr1 = lsts2[i].Split('/');
                        if (arrListStr1.Length > 2)
                        {
                            if (arrListStr[2] != arrListStr1[2])
                            {
                                lstnodefinal.Add(i);
                            }
                        }

                    }




                    for (int i = 0; i < adj.Length; i++)
                    {
                        if (adj[i].Count == 1)
                        {
                            lstnodefinal.Add(adj[i][0]);
                            cbblistnode.Items.Add(adj[i][0]);

                        }
                    }



                    int counts1 = 1;

                    for (int i = 0; i < lstnodefinal.Count; i++)
                    {
                        
                        lstout.Clear();
                        lstvitri.Clear();
                        findShortestPath(0, lstnodefinal[i]);
                        int s = 0;
                        for (int m = 0; m < lstout.Count; m++)
                        {
                            Random rdd1 = new Random();
                            s += rdd1.Next(1, 2);
                        }
                        if (s != 0)
                        {
                            
                            richtxbout.Text = richtxbout.Text + "Path " + counts1.ToString() + " (" + s.ToString() + ")" + ":\n";
                            lstrids.Add("Path " + i.ToString() + "(" + s.ToString() + ")" + ":");
                            for (int j = 0; j < lstout.Count; j++)
                            {

                                richtxbout.Text = richtxbout.Text + "Node" + lstvitri[j] + "." + " " + lstout[j] + "\n";
                                lstrids.Add(lstvitri[j] + "." + " " + lstout[j]);
                                
                            }
                            richtxbout.Text = richtxbout.Text + "\n";
                            counts1++;
                        }
                        



                    }
                    //richtxbout.Text = 












                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    txbdau.Clear();
                    txbcuoi.Clear();
                    txtoutpath.Clear();
                }
                */
            }

            if(cbbtypelist.SelectedIndex == 2)
            {
                List<string> lstnodefinal = new List<string>();

                richtxbout.Clear();
                lstrids.Clear();

                try
                {
                    lstout.Clear();
                    lstvitri.Clear();

                    findShortestPath(int.Parse(txbdau.Text), int.Parse(txbcuoi.Text)).ToString();

                    for (int i = 0; i < adj.Length; i++)
                    {
                        if (adj[i].Count == 0)
                        {
                            lstnodefinal.Add(adj[i].ToString());
                        }
                    }



                    for (int i = 0; i < lstout.Count; i++)
                    {
                        richtxbout.Text = richtxbout.Text + lstvitri[i] + "." + " " + lstout[i] + "\n";
                        lstrids.Add(lstvitri[i] + "." + " " + lstout[i]);
                    }




                }
                catch (Exception ex)
                {
                    //MessageBox.Show(ex.Message);
                    txbdau.Clear();
                    txbcuoi.Clear();
                    txtoutpath.Clear();
                }
            }



        }
    }
}
