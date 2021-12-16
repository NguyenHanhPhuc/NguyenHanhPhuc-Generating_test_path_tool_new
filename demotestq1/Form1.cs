//using IronPython.Hosting;
using Microsoft.Scripting.Hosting;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.Linq;

using System.Net.Http;
using HtmlAgilityPack;

using System.Data.SqlClient;
using System.Text.RegularExpressions;

namespace demotestq1
{
    public partial class Form1 : Form
    {
        public static string str;
        private string linkgoc;
        private string linkweb;
        List<string> lst = new List<string>();
        public static List<string> visitedList = new List<string>();
        private int biendiemtimer = 0;

        int lbtotalurl;
        int depthlabel;
        int depth = 0;
        Timer time1 = new Timer();
        static int deep = 50;
        bool finish = false;



        public Form1()
        {
            InitializeComponent();
            cbbdepth.SelectedIndex = 0;
            //time1.Interval = 30000;
            //txtpath.Text = "";




        }



       
        public static DataTable ConvertListToDataTable(List<string> list)
        {
            // New table.
            DataTable table = new DataTable();

            // Get max columns.
            int columns = 0;
            foreach (var array in list)
            {
                if (array.Length > columns)
                {
                    columns = array.Length;
                }
            }

            // Add columns.
            for (int i = 0; i < columns; i++)
            {
                table.Columns.Add();
            }

            // Add rows.
            foreach (var array in list)
            {
                table.Rows.Add(array);
            }

            return table;
        }




        private void button1_Click(object sender, EventArgs e)
        {


        }





        private void Form1_Load(object sender, EventArgs e)
        {

            linkweb = txtlink.Text;

        }

        private void btnGraph_Click(object sender, EventArgs e)
        {
            // frm2 = new FrmDrawGraph(txtlink.Text);
            // frm2.Show();
            FrmDrawGraph frm3 = new FrmDrawGraph(lst);
            frm3.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            int dem = 0;
            depthlabel = 0;
            //timemain.Enabled = !timemain.Enabled;
            //timemain.Start();
            //textBox1.Text = giay.ToString();
            //time1.Start();
            listBox1.Items.Clear();
            lst.Clear();


            //lbtotalurl = 0;
            lbstate.Text = "Is crawling links";
            try
            {
                
                button2.Enabled = false;
                if (button2.Enabled == true)
                {
                    lbstate.Text = "";
                }
                else
                {
                    lbstate.Text = "Is crawling links";
                }
                lbstate.Text = "Is crawling links";
                WebClient webClient = new WebClient();
                webClient.OpenReadCompleted += new OpenReadCompletedEventHandler(WebClient_OpenReadCompleted);
                Uri URL = new Uri(txtlink.Text);
                webClient.OpenReadAsync(URL);

                progressBarcrawl.Minimum = 0;

                var url = txtlink.Text;
                //txtpath.Text = biendiemtimer.ToString();
                //check1.Checked = false;
                visitedList.Add(url);
                lbstate.Text = "Is crawling links";
                crawlOriginalUrl(url, lst, progressBarcrawl,finish,lbdepth, depthlabel);
                lbtotalurl = depthlabel;
                //textBox1.Text = giay.ToString();

                //Console.ReadLine();


                HtmlWeb web = new HtmlWeb();
                    HtmlAgilityPack.HtmlDocument doc = new HtmlAgilityPack.HtmlDocument();
                    doc = web.Load(txtlink.Text);
                //List<string> lst = new List<string>();

                //doc.DocumentNode.RemoveAll();
                foreach (HtmlNode link in doc.DocumentNode.SelectNodes("//a[@href]"))
                    {
                        HtmlAttribute att = link.Attributes["href"];
                    //if (att.Value.Contains("a"))
                    //{lbstate.Text = "Is crawling links";

                    // Console.WriteLine(att.Value);
                    //depthlabel++;

                    lbstate.Text = "Is crawling links";
                    lst.Add(att.Value.ToString());
                        //lbtotalurl++;
                        lbtotalaction.Text = "Total action: " + lbtotalurl.ToString();

                    //}
                }
          


                lst = lst.Distinct().ToList();

                
                for(int i=0;i<lst.Count;i++)
                {
                    if(lst[i].IndexOf("/")==-1)
                    {
                        //richtestt.Text += "\n" + lst[i];
                        lst[i] = "";
                    }
                }

                for (int i = 0; i < lst.Count; i++)
                {
                    if (lst[i].IndexOf("")!=-1)
                    {
                        lst.Remove("");
                    }
                }
                //lbdepth.Text = "depth: " + lbtotalurl;
                lbtotalaction.Text = "Total action: " + (lst.Count-1);

                for (int i = 0; i < lst.Count; i++)
                {
                    listBox1.Items.Add(i.ToString() + " . " + lst[i]);
                    //dem++;

                }


                button2.Enabled = true;

                if(button2.Enabled == true)
                {
                    lbstate.Text = "";
                }
                else
                {
                    lbstate.Text = "Is crawling links";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Nhập đúng định dạng!!! VD: https://hoctructuyen.vimaru.edu.vn ");
                button2.Enabled = true;
                
                txtlink.Clear();
                txtlink.Focus();
            }
            //time1.Stop();
            //time1.Stop();
            //progressBarcrawl.Value


            //txtc.Text = lst.Count.ToString();



            //lbstate.Text = "craw finish";



            //dataGridView1.AutoGenerateColumns = false;


            //dataGridView1.Columns.Remove("Column2");













        }

        private void WebClient_OpenReadCompleted(Object sender, OpenReadCompletedEventArgs e)
        {
            if (e.Error != null)
            {
                MessageBox.Show(e.Error.Message);

            }
            else
            {
                const string PATTERN = @"a href=""(?<link>.+?)""";
                Regex regex = new Regex(PATTERN, RegexOptions.IgnoreCase);
                TextReader TR = new StreamReader(e.Result);
                string content = TR.ReadToEnd();
                TR.Close();
                MatchCollection MC = regex.Matches(content);
                foreach (Match match in MC)
                {
                    //listBox1.Items.Add(match.Groups["link"]);
                    lst.Add(match.Groups["link"].ToString());
                }
                //txttest.Text = lst[1];
            }
        }

        public static List<string> getAllLinks(string webAddress)
        {
            try
            {
                HtmlWeb web = new HtmlAgilityPack.HtmlWeb();
                HtmlAgilityPack.HtmlDocument newdoc = web.Load(webAddress);
               //HtmlDocument newdoc = web.Load(webAddress)         
                return newdoc.DocumentNode.SelectNodes("//a[@href]")
                          .Where(y => y.Attributes["href"].Value.StartsWith("http"))
                          .Select(x => x.Attributes["href"].Value)
                          .ToList<string>();
            }
            catch
            {
                return null;
            }
        }


        public static void crawlOriginalUrl(string seedSite,List<string> lst,ProgressBar proa,bool finish,Label lbdep,int depthh)
        {

            
            //depthlabel =5 ;
            //time1.Start();
            //time1.Tick += Time1_Tick;
            //time1.Interval = 1000;
            if (getAllLinks(seedSite) != null)
            {
                depthh++;
                var websiteLinks = getAllLinks(seedSite);//get's all the links

                proa.Maximum = websiteLinks.Count;
                //lbdep.Text = "Depth: " + depthh.ToString();
                for (int i = 0; i < websiteLinks.Count; i++)
                {
                    

                    //Console.WriteLine(websiteLinks[i]);

                    try
                    {
                        lst.Add(websiteLinks[i]);

                        depthh++;

                        proa.Value = i;
                        //lbtotal++;
                        //lbtotalaction.Text = "Total action: " + lbtotal.ToString();

                        crawlLinksFound(websiteLinks[i], lst, finish, lbdep, depthh);
                    }
                    
                    catch(Exception ex)
                    {
                        deep = 400;
                    }
                    if(finish == true)
                    {
                        proa.Value = websiteLinks.Count;
                        break;
                    }    

                    if (lst.Count()> deep)
                    {
                        proa.Value = websiteLinks.Count;
                        //lbtotalurl = websiteLinks.Count;
                        break;

                    }
                    
                    
                    
                }
                
            }
        }

        private static void Time1_Tick(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        // crawling the links found
        public static void crawlLinksFound(string seedURI, List<string> lst,bool finish,Label lbtotalaction1,int depthlabel1)
        {
            depthlabel1++;
            
            if (getAllLinks(seedURI) != null)
            {
                var websiteLinks = getAllLinks(seedURI);//get's all the links
                for (int i = 0; i < websiteLinks.Count; i++)
                {

                    depthlabel1++;
                    lbtotalaction1.Text = "Depth: " + depthlabel1.ToString();
                    if (visitedList.Contains(websiteLinks[i]))
                    {
                        //Console.WriteLine(websiteLinks[i] + " already added.");
                    }
                    else
                    {
                        try
                        {
                            //depthlabel++;
                            lst.Add(websiteLinks[i]);
                            //lbtotal++;
                            //lbtotalaction.Text = "Total action: " + lbtotal.ToString();
                            visitedList.Add(websiteLinks[i]);
                        }
                        //Console.WriteLine(websiteLinks[i]);
                        catch(Exception ex)
                        {
                            deep = 400;
                        }
                    }
                    if(finish == true)
                    {
                        break;
                    }
                    if (lst.Count() > deep)
                    {
                        //proa.Value = websiteLinks.Count;
                        break;
                    }
                   
                }
            }
        }


      


        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            //txtpath.Text = "D:\output.txt";






        }

        public static int countSL(string s, char c)
        {
            int res = 0;

            for (int i = 0; i < s.Length; i++)
            {
                if (s[i] == c)
                    res++;
            }

            return res;
        }
        public static int compe(string a, string b)
        {
            int count = 0;
            string[] arrstr = a.Split('/');
            string[] arrstr1 = b.Split('/');
            int index = 0;
            int check = 5;
            if (arrstr.Length > arrstr1.Length)
            {

                while (index < arrstr.Length)
                {
                    for (int j = 0; j < arrstr1.Length; j++)
                    {
                        if (arrstr[index] == arrstr1[j])
                        {
                            check = 1;
                        }
                    }
                    if (check == 1)
                    {
                        count++;
                        check = 5;
                    }
                    index++;
                }
            }
            else
            {

                while (index < arrstr1.Length)
                {
                    for (int j = 0; j < arrstr.Length; j++)
                    {
                        if (arrstr1[index] == arrstr[j])
                        {
                            check = 1;
                        }
                    }
                    if (check == 1)
                    {
                        count++;
                        check = 5;
                    }
                    index++;
                }
            }






            return count;
        }


        private void findLineMax()
        {



            TreeNode tNode;
            // tNode = treeView1.Nodes.Add(linkweb);
            linkweb = "https://hoctructuyen.vimaru.edu.vn/";
            string[] arrListStr = linkweb.Split('/');
            string strcompe = arrListStr[2];

            List<string> lstchild = new List<string>();
            for (int i = 0; i < lst.Count; i++)
            {
                int lastindex = lst[i].LastIndexOf('/');

                string[] arrstrloop = lst[i].Split('/');
                if (countSL(lst[i], '/') >= 3 && lastindex + 1 < lst[i].Length && arrstrloop[2] == strcompe)
                {
                    //treeView1.Nodes[0].Nodes.Add(lst[i]);
                    lstchild.Add(lst[i]);
                }

            }


            int[] level = new int[lstchild.Count()];

            //int dem = 0;
            for (int i = 0; i < lstchild.Count(); i++)
            {
                level[i] = countSL(lstchild[i], '/');
            }
            int max = 0;
            for (int i = 0; i < lstchild.Count(); i++)
            {
                if (level[i] > max)
                {
                    max = level[i];
                }
            }



            int poslinkweb = -1;
            int index = 4;
            Random rdtrongso = new Random();
            int biendem = 0;

            List<int> lstCanh = new List<int>();
            int[] arrposx = new int[1000];
            int[] arrposy = new int[1000];

            for (int i = 0; i < lstchild.Count; i++)
            {
                if (countSL(lstchild[i], '/') == 3)
                {
                    lstCanh.Add(rdtrongso.Next(0, 1));
                    arrposx[biendem++] = i;

                    arrposy[biendem++] = poslinkweb;
                }
            }
            while (index <= max)
            {
                for (int i = 0; i < lstchild.Count; i++)
                {
                    if (countSL(lstchild[i], '/') == index)
                    {
                        for (int j = 0; j < lstchild.Count; j++)
                        {
                            if (countSL(lstchild[j], '/') == index - 1)
                            {
                                lstCanh.Add(rdtrongso.Next(0, 1));
                                arrposx[biendem++] = i;
                                arrposy[biendem++] = j;
                            }
                        }

                    }
                }
                index++;
            }

         /*   List<string> outputLst = new List<string>();
            for (int i = arrposx.Length - 1; i > 0; i--)
            {
                //if(countSL(lstchild[arrposx[i]],'/') ==max)
                //{
                outputLst.Add(lstchild[arrposx[i]]);
                //}
            }
         */
            //List<string> uniqueList = outputLst.Distinct().ToList();

            // Write to console

            List<string> lstnode = new List<string>();

            for (int i = 0; i < lst.Count; i++)
            {


                string str = lst[i].Substring(8);
                lstnode.Add(str);



            }

            List<string> lstnodefinal = new List<string>();
            for (int i = 0; i < lstnode.Count; i++)
            {
                string[] arrListStrnode = lstnode[i].Split('/');

                for (int j = 0; j < arrListStrnode.Length; j++)
                {
                    if (arrListStrnode[j] != "")
                    {
                        lstnodefinal.Add(arrListStrnode[j]);
                    }
                }
            }

            /*

            List<string> uniqueListnode = lstnodefinal.Distinct().ToList();

            List<string> linenode = new List<string>();

            string kq = "";
            for (int i = 0; i < uniqueList.Count; i++)
            {
                string[] arrListStrfind = uniqueList[i].Split('/');
                for (int j = 0; j < arrListStrfind.Length; j++)
                {
                    for (int k = 0; k < uniqueListnode.Count; k++)
                    {
                        if (arrListStrfind[j] == uniqueListnode[k])
                        {
                            kq = kq + " - " + k.ToString();
                        }
                    }

                }
                linenode.Add(kq);
                kq = "";
            }

            for (int i = 0; i < linenode.Count; i++)
            {
                //treeView1.Nodes[0].Nodes.Add(linenode[i]);
            }


            List<string> outputposint = new List<string>();



            string fileLPath = @"D:\listnode.txt";
            string fileLPath1 = @"D:\Listurl.txt";



            System.IO.File.WriteAllLines(fileLPath, uniqueListnode);

            System.IO.File.WriteAllLines(fileLPath1, uniqueList);

            // duong dai nhat giam dan
            /*
             
            for (int i = 0; i < uniqueList.Count; i++)
            {
                treeView1.Nodes[0].Nodes.Add(uniqueList[i]);
            }
            */

              // string fileLPath = 


            

            //   System.IO.File.WriteAllLines(fileLPath, lstsavefile);

        }


        private void btnfile_Click(object sender, EventArgs e)
        {
            //findLineMax();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            //findLineMax();
        }

        private void button4_Click_1(object sender, EventArgs e)
        {
            //txtpath.Text = "D:\output.txt";

            if (txtpath.Text == "")
            {
                MessageBox.Show("Fill in the link in the box");
            }
            Random rd = new Random();

            List<string> lsttrongso = new List<string>();
            
            for(int i=0;i<lst.Count;i++)
            {
                if (countSL(lst[i],'/')>3)
                {
                    lsttrongso.Add(rd.Next(0, 1).ToString());
                }
            }
            string linkmain = txtlink.Text;
            string competest = linkmain.Substring(7);
            
            
            List<string> lstloc = new List<string>();
            for(int i=0;i<lst.Count;i++)
            {
                if(countSL(lst[i],'/')>1)
                { 
                
                string strtest = lst[i].Substring(8);
                lstloc.Add(strtest);
                }

            }
            string contran = "";

            string[] arrListStr = linkmain.Split('/');

            string conttran = arrListStr[2];

            List<string> lstfinal = new List<string>();

            
            for (int i = 0; i < lst.Count; i++)
            {
                if(countSL(lst[i],'/')>=1)
                {
                    if(conttran.Contains(lst[i]) == false)
                    {
                        lstfinal.Add(lst[i]);
                    }
                }
                
                


            }

            
            //dataGridView1.DataSource = result;

            string fileLPath = txtpath.Text;



            //System.IO.File.WriteAllText(fileLPath, conttran);
            System.IO.File.WriteAllLines(fileLPath, lstfinal);
        }

        private void button5_Click(object sender, EventArgs e)
        {
          

          
        }

        private void button5_Click_1(object sender, EventArgs e)
        {

        }

        private void button6_Click(object sender, EventArgs e)
        {
            
        }

        private void button7_Click(object sender, EventArgs e)
        {
            txtlink.Text = "";
            txtlink.Focus();

        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }
        int i = 0;
        private void timer1_Tick(object sender, EventArgs e)
        {
            //i++;
           //txtpath.Text = i.ToString();
        }

        private void backgroundWorker2_DoWork(object sender, DoWorkEventArgs e)
        {
           
        }

      

        private void backgroundWorker2_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            //lblProgress.Text = String.Format("{0} files found", listView1.Items.Count);
            if (progressBarcrawl.Value < progressBarcrawl.Maximum)
            {
               // lblProgress.Text = "Searching cancelled. " + lblProgress.Text;
            }
            button2.Text = "ok";
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(cbbdepth.SelectedIndex == 0)
            {
                deep = 50;
            }
            if (cbbdepth.SelectedIndex == 1)
            {
                deep = 100;

            }
            if (cbbdepth.SelectedIndex == 2)
            {
                deep = 200;
            }
            if (cbbdepth.SelectedIndex == 3)
            {
                deep = 400;
            }
            if (cbbdepth.SelectedIndex == 4)
            {
                deep = 1000;
            }
        }

        int giay = 0;

        private void button7_Click_1(object sender, EventArgs e)
        {
            frmfindlinemax f = new frmfindlinemax(lst);
            f.Show();
        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            try
            {
                if (txtpath.Text == "")
                {
                    MessageBox.Show("Fill in the link in the box");
                }
                string Url = txtlink.Text;
                HttpWebRequest myRequest = (HttpWebRequest)WebRequest.Create(Url);
                myRequest.Method = "GET";
                WebResponse myResponse = myRequest.GetResponse();
                StreamReader sr = new StreamReader(myResponse.GetResponseStream(), System.Text.Encoding.UTF8);
                string result = sr.ReadToEnd();
                sr.Close();
                myResponse.Close();

                //dataGridView1.DataSource = result;

                string fileLPath = txtpath.Text;




                System.IO.File.WriteAllText(fileLPath, result);
                //Console.WriteLine(result);
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void button4_Click_2(object sender, EventArgs e)
        {
            finish = true;
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            formPath f = new formPath(lst);
            f.Show();
        }
    }
}



