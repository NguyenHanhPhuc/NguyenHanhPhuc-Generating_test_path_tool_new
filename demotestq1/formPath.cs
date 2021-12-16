using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace demotestq1
{
    public partial class formPath : Form
    {
        public List<string> lsts3 = new List<string>();
        public formPath(List<string> lst)
        {

            InitializeComponent();
            for (int i = 0; i < lst.Count; i++)
            {
                lsts3.Add(lst[i]);
                //richtest.Text += lsts2[i];
            }
            //lsts3 = lsts3.Distinct().ToList();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (lsts3[0] == "")
            {
                MessageBox.Show("Fill in the link in the box");
            }
            string Url = lsts3[0];
            HttpWebRequest myRequest = (HttpWebRequest)WebRequest.Create(Url);
            myRequest.Method = "GET";
            WebResponse myResponse = myRequest.GetResponse();
            StreamReader sr = new StreamReader(myResponse.GetResponseStream(), System.Text.Encoding.UTF8);
            string result = sr.ReadToEnd();
            sr.Close();
            myResponse.Close();

            //dataGridView1.DataSource = result;

            string fileLPath = txtPATH.Text;




            System.IO.File.WriteAllText(fileLPath, result);
            //Console.WriteLine(result);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                string fileLPath = txtPATH.Text;



                //System.IO.File.WriteAllText(fileLPath, conttran);
                System.IO.File.WriteAllLines(fileLPath, lsts3);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
