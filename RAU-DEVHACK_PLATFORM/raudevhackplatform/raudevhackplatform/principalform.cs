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
using System.Data.SqlClient;
using System.Text.RegularExpressions;

namespace raudevhackplatform
{
    public partial class principalform : Form
    {
        public principalform()
        {
            InitializeComponent();
            Date();
            TimerA();
            TimerB();

            
        }

        private bool validarecoda = false;
        private bool validarecodb = false;

        private void closebutton_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (validarecoda == true)
            {
                menupanel.Visible = false;
                papanel.Visible = true;
                pbpanel.Visible = false;
                timer1.Stop();

            }

        }
        string stringcon = System.Configuration.ConfigurationManager.ConnectionStrings["rauhack"].ConnectionString;

        private void Date() {

            SqlConnection con = new SqlConnection(stringcon);
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.Parameters.Clear();
            con.Open();

            cmd.CommandText = "SELECT * FROM UTILIZATOR WHERE codutilizator=@cod";
            cmd.Parameters.AddWithValue("@cod", Form1.codutilizator);

            DataTable dt = new DataTable();
            DataTable dt1 = new DataTable();
            SqlDataAdapter da1 = new SqlDataAdapter(cmd);

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da1.Fill(ds);
            da.Fill(dt);
            foreach (DataRow dr in dt.Rows)
            {
                label30.Text = dr["codutilizator"].ToString();
                label31.Text = dr["numecomplet"].ToString();
                label32.Text = dr["liceu"].ToString();
            }
            con.Close();

        }
        private void button1_Click(object sender, EventArgs e)
        {
            validarecoda = true;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            validarecodb = true;
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            if (validarecodb == true)
            {
                menupanel.Visible = false;
                papanel.Visible = false;
                pbpanel.Visible = true;
                timer2.Stop();
            }
        }

        private void label11_Click(object sender, EventArgs e)
        {
            panel5.Visible = true;
        }
        public string pathcodsursa;
        StreamReader reader3;
        private void customVideoLocationButton1_Click(object sender, EventArgs e)
        {

            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "RAR (*.rar*)|*.rar*";// file types, that will be allowed to upload
            dialog.Multiselect = false; // allow/deny user to upload more than one file at a time
            if (dialog.ShowDialog() == DialogResult.OK) // if user clicked OK
            {
                pathcodsursa = dialog.FileName; // get name of file
                using (reader3 = new StreamReader(new FileStream(pathcodsursa, FileMode.Open), new UTF8Encoding()))
                {
                    customVideoLocationButton1.FileLocation = pathcodsursa;

                }
            }
        }
        private void TimerA()
        {
            SqlConnection con = new SqlConnection(stringcon);
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.Parameters.Clear();
            con.Open();

            cmd.CommandText = "SELECT validare_sec_a FROM optiuni";

            DataTable dt = new DataTable();
            DataTable dt1 = new DataTable();
            SqlDataAdapter da1 = new SqlDataAdapter(cmd);

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da1.Fill(ds);
            da.Fill(dt);
            foreach (DataRow dr in dt.Rows)
            {
                int count = Convert.ToInt32(dr["validare_sec_a"].ToString());

                if (count == 0) { }
                else { validarecoda = true;
                    timer3.Stop();
                    timer5.Start();
                }
            }
            con.Close();
        }

        private void TimerB()
        {
            SqlConnection con = new SqlConnection(stringcon);
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.Parameters.Clear();
            con.Open();

            cmd.CommandText = "SELECT validare_sec_b FROM optiuni";

            DataTable dt = new DataTable();
            DataTable dt1 = new DataTable();
            SqlDataAdapter da1 = new SqlDataAdapter(cmd);

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da1.Fill(ds);
            da.Fill(dt);
            foreach (DataRow dr in dt.Rows)
            {
                int count = Convert.ToInt32(dr["validare_sec_b"].ToString());

                if (count == 0) { }
                else
                {
                    validarecodb = true;
                    timer4.Stop();
                    timer7.Start();

                }
            }
            con.Close();
        }

        private void Timer5()
        {
            SqlConnection con = new SqlConnection(stringcon);
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.Parameters.Clear();
            con.Open();

            cmd.CommandText = "SELECT validare_sec_a FROM optiuni";

            DataTable dt = new DataTable();
            DataTable dt1 = new DataTable();
            SqlDataAdapter da1 = new SqlDataAdapter(cmd);

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da1.Fill(ds);
            da.Fill(dt);
            foreach (DataRow dr in dt.Rows)
            {
                int count = Convert.ToInt32(dr["validare_sec_a"].ToString());

                if (count == 0) {
                    validarecoda = false;
                    papanel.Visible = false;
                    menupanel.Visible = true;
                    timer5.Stop();
                    timer4.Start();
                }

            }
            con.Close();
        }
        private void Timer6()
        {
            SqlConnection con = new SqlConnection(stringcon);
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.Parameters.Clear();
            con.Open();

            cmd.CommandText = "SELECT validare_sec_b FROM optiuni";

            DataTable dt = new DataTable();
            DataTable dt1 = new DataTable();
            SqlDataAdapter da1 = new SqlDataAdapter(cmd);

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da1.Fill(ds);
            da.Fill(dt);
            foreach (DataRow dr in dt.Rows)
            {
                int count = Convert.ToInt32(dr["validare_sec_b"].ToString());

                if (count == 0)
                {
                    sentanswers();

                    SqlCommand cmdi = new SqlCommand();
                    cmdi.Parameters.Clear();

                    cmdi.Connection = con;
                    cmdi.CommandText = "update proba_b set punctaj=@punctaj where id_utilizator=@idutili";

                    cmdi.Parameters.AddWithValue("@punctaj", scorebsection);
                    cmdi.Parameters.AddWithValue("@idutili", Form1.idutilizator);

                    cmdi.ExecuteNonQuery();

                    validarecodb = false;
                    pbpanel.Visible = false;
                    menupanel.Visible = true;
                    timer7.Stop();


                }

            }
            con.Close();
        }
        private void timer3_Tick(object sender, EventArgs e)
        {
            TimerA();
        }

        private void timer5_Tick(object sender, EventArgs e)
        {
            Timer5();
        }

        private void timer4_Tick(object sender, EventArgs e)
        {
            TimerB();
        }
        private void timer7_Tick(object sender, EventArgs e)
        {
            Timer6();
        }


        public byte[] fileinsert1, fileinsert2, fileinsert3;

        private void button4_Click(object sender, EventArgs e)
        {

            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            saveFileDialog1.Filter = "RAR (*.rar*)|*.rar*";
            saveFileDialog1.FileName = "proba_A_codsursa.rar";
            saveFileDialog1.Title = "Salvare fisier..";
            saveFileDialog1.ShowDialog();

            string cnStr = stringcon;
            string sql = "select fisier from proba_A";
            string filename = saveFileDialog1.FileName;
            SqlDataAdapter adp = new SqlDataAdapter(sql, cnStr);
            DataTable dt = new DataTable();

            adp.Fill(dt);
            if (dt.Rows.Count != 0)
            {
                byte[] b = (byte[])dt.Rows[0]["fisier"];
                FileStream fs = new FileStream(filename, FileMode.Create);
                fs.Write(b, 0, b.Length);
                fs.Close();
            }
        }



        private void materialFlatButton1_Click(object sender, EventArgs e)
        {
            if (customVideoLocationButton1.FileLocation == "Încarcă codul sursă aici")
            {
                label17.Visible = true;
                errorfiletimer.Start();
            }
            else
            {
                //SqlConnection con = new SqlConnection(stringcon);
                //SqlCommand cmd = new SqlCommand();
                //cmd.Connection = con;
                //cmd.Parameters.Clear();
                //con.Open();

                //cmd.CommandText = "SELECT fisier FROM proba_a where id_utilizator=" + Form1.idutilizator + "";

                //DataTable dt = new DataTable();
                //DataTable dt1 = new DataTable();
                //SqlDataAdapter da1 = new SqlDataAdapter(cmd);

                //SqlDataAdapter da = new SqlDataAdapter(cmd);
                //DataSet ds = new DataSet();
                //da1.Fill(ds);
                //da.Fill(dt);
                //foreach (DataRow dr in dt.Rows)
                //{
                //    string text = dr["fisier"].ToString();
                //    if (text != "")
                //    {
                //        materialFlatButton1.Enabled = false;
                //        label17.Visible = true;
                //        label17.Text = "Ai introdus un fisier deja!";

                //    }
                //    else
                //    {

                //    }}
                    insertfile1editproject();
                    materialFlatButton1.Enabled = false;
                


            }
        }
        public int countererrorseconds = 0;
        private void errorfiletimer_Tick(object sender, EventArgs e)
        {
            countererrorseconds = countererrorseconds + 100;
            if (countererrorseconds == 4000)
            {
                label17.Visible = false;
                errorfiletimer.Stop();
                countererrorseconds = 0;

            }
            else
                return;
        }

        private void label11_MouseEnter(object sender, EventArgs e)
        {
            label11.ForeColor = Color.FromArgb(255, 205, 0);
        }

        private void label12_MouseEnter(object sender, EventArgs e)
        {
            label12.ForeColor = Color.FromArgb(255, 205, 0);
        }

        private void label14_MouseEnter(object sender, EventArgs e)
        {
            label14.ForeColor = Color.FromArgb(255, 205, 0);
        }

        private void label13_MouseEnter(object sender, EventArgs e)
        {
            label13.ForeColor = Color.FromArgb(255, 205, 0);
        }

        private void label11_MouseLeave(object sender, EventArgs e)
        {
            label11.ForeColor = Color.FromArgb(60, 60, 60);
        }

        private void label12_MouseLeave(object sender, EventArgs e)
        {
            label12.ForeColor = Color.FromArgb(60, 60, 60);
        }

        private void label14_MouseLeave(object sender, EventArgs e)
        {
            label14.ForeColor = Color.FromArgb(60, 60, 60);
        }

        private void label13_MouseLeave(object sender, EventArgs e)
        {
            label13.ForeColor = Color.FromArgb(60, 60, 60);
        }
        //--------------------------------------------------------------------------P2 section-----------------------------
        private void label22_MouseEnter(object sender, EventArgs e)
        {
            label22.ForeColor = Color.FromArgb(255, 205, 0);
        }

        private void label21_MouseEnter(object sender, EventArgs e)
        {
            label21.ForeColor = Color.FromArgb(255, 205, 0);
        }

        private void label20_MouseEnter(object sender, EventArgs e)
        {
            label20.ForeColor = Color.FromArgb(255, 205, 0);
        }

        private void label19_MouseEnter(object sender, EventArgs e)
        {
            label19.ForeColor = Color.FromArgb(255, 205, 0);
        }

        private void label27_MouseEnter(object sender, EventArgs e)
        {
            label27.ForeColor = Color.FromArgb(255, 205, 0);
        }

        private void label25_MouseEnter(object sender, EventArgs e)
        {
            label25.ForeColor = Color.FromArgb(255, 205, 0);
        }

        private void label24_MouseEnter(object sender, EventArgs e)
        {
            label24.ForeColor = Color.FromArgb(255, 205, 0);
        }

        private void label23_MouseEnter(object sender, EventArgs e)
        {
            label23.ForeColor = Color.FromArgb(255, 205, 0);
        }

        private void label40_MouseEnter(object sender, EventArgs e)
        {
            label40.ForeColor = Color.FromArgb(255, 205, 0);
        }

        private void label39_MouseEnter(object sender, EventArgs e)
        {
            label39.ForeColor = Color.FromArgb(255, 205, 0);
        }

        private void label44_MouseEnter(object sender, EventArgs e)
        {
            label44.ForeColor = Color.FromArgb(255, 205, 0);
        }

        private void label43_MouseEnter(object sender, EventArgs e)
        {
            label43.ForeColor = Color.FromArgb(255, 205, 0);
        }

        private void label42_MouseEnter(object sender, EventArgs e)
        {
            label42.ForeColor = Color.FromArgb(255, 205, 0);
        }

        private void label41_MouseEnter(object sender, EventArgs e)
        {
            label41.ForeColor = Color.FromArgb(255, 205, 0);
        }

        private void label38_MouseEnter(object sender, EventArgs e)
        {
            label38.ForeColor = Color.FromArgb(255, 205, 0);
        }

        private void label37_MouseEnter(object sender, EventArgs e)
        {
            label37.ForeColor = Color.FromArgb(255, 205, 0);
        }

        private void label36_MouseEnter(object sender, EventArgs e)
        {
            label36.ForeColor = Color.FromArgb(255, 205, 0);
        }

        private void label35_MouseEnter(object sender, EventArgs e)
        {
            label35.ForeColor = Color.FromArgb(255, 205, 0);
        }

        private void label34_MouseEnter(object sender, EventArgs e)
        {
            label34.ForeColor = Color.FromArgb(255, 205, 0);
        }

        private void label33_MouseEnter(object sender, EventArgs e)
        {
            label33.ForeColor = Color.FromArgb(255, 205, 0);
        }

        private void label54_MouseEnter(object sender, EventArgs e)
        {
            label54.ForeColor = Color.FromArgb(255, 205, 0);
        }

        private void label53_MouseEnter(object sender, EventArgs e)
        {
            label53.ForeColor = Color.FromArgb(255, 205, 0);
        }

        private void label52_MouseEnter(object sender, EventArgs e)
        {
            label52.ForeColor = Color.FromArgb(255, 205, 0);
        }

        private void label51_MouseEnter(object sender, EventArgs e)
        {
            label51.ForeColor = Color.FromArgb(255, 205, 0);
        }

        private void label50_MouseEnter(object sender, EventArgs e)
        {
            label50.ForeColor = Color.FromArgb(255, 205, 0);
        }

        private void label49_MouseEnter(object sender, EventArgs e)
        {
            label49.ForeColor = Color.FromArgb(255, 205, 0);
        }

        private void label48_MouseEnter(object sender, EventArgs e)
        {
            label48.ForeColor = Color.FromArgb(255, 205, 0);
        }

        private void label47_MouseEnter(object sender, EventArgs e)
        {
            label47.ForeColor = Color.FromArgb(255, 205, 0);
        }

        private void label46_MouseEnter(object sender, EventArgs e)
        {
            label46.ForeColor = Color.FromArgb(255, 205, 0);
        }

        private void label45_MouseEnter(object sender, EventArgs e)
        {
            label45.ForeColor = Color.FromArgb(255, 205, 0);
        }

        private void label64_MouseEnter(object sender, EventArgs e)
        {
            label64.ForeColor = Color.FromArgb(255, 205, 0);
        }

        private void label63_MouseEnter(object sender, EventArgs e)
        {
            label63.ForeColor = Color.FromArgb(255, 205, 0);
        }

        private void label62_MouseEnter(object sender, EventArgs e)
        {
            label62.ForeColor = Color.FromArgb(255, 205, 0);
        }

        private void label61_MouseEnter(object sender, EventArgs e)
        {
            label61.ForeColor = Color.FromArgb(255, 205, 0);
        }

        private void label60_MouseEnter(object sender, EventArgs e)
        {
            label60.ForeColor = Color.FromArgb(255, 205, 0);
        }

        private void label59_MouseEnter(object sender, EventArgs e)
        {
            label59.ForeColor = Color.FromArgb(255, 205, 0);
        }

        private void label58_MouseEnter(object sender, EventArgs e)
        {
            label58.ForeColor = Color.FromArgb(255, 205, 0);
        }

        private void label57_MouseEnter(object sender, EventArgs e)
        {
            label57.ForeColor = Color.FromArgb(255, 205, 0);
        }

        private void label56_MouseEnter(object sender, EventArgs e)
        {
            label56.ForeColor = Color.FromArgb(255, 205, 0);
        }

        private void label55_MouseEnter(object sender, EventArgs e)
        {
            label55.ForeColor = Color.FromArgb(255, 205, 0);
        }

        private void label74_MouseEnter(object sender, EventArgs e)
        {
            label74.ForeColor = Color.FromArgb(255, 205, 0);
        }

        private void label73_MouseEnter(object sender, EventArgs e)
        {
            label73.ForeColor = Color.FromArgb(255, 205, 0);
        }

        private void label72_MouseEnter(object sender, EventArgs e)
        {
            label72.ForeColor = Color.FromArgb(255, 205, 0);
        }

        private void label71_MouseEnter(object sender, EventArgs e)
        {
            label71.ForeColor = Color.FromArgb(255, 205, 0);
        }

        private void label70_MouseEnter(object sender, EventArgs e)
        {
            label70.ForeColor = Color.FromArgb(255, 205, 0);
        }

        private void label69_MouseEnter(object sender, EventArgs e)
        {
            label69.ForeColor = Color.FromArgb(255, 205, 0);
        }

        private void label68_MouseEnter(object sender, EventArgs e)
        {
            label68.ForeColor = Color.FromArgb(255, 205, 0);
        }

        private void label67_MouseEnter(object sender, EventArgs e)
        {
            label67.ForeColor = Color.FromArgb(255, 205, 0);
        }

        private void label66_MouseEnter(object sender, EventArgs e)
        {
            label66.ForeColor = Color.FromArgb(255, 205, 0);
        }

        private void label65_MouseEnter(object sender, EventArgs e)
        {
            label65.ForeColor = Color.FromArgb(255, 205, 0);
        }

        private void label77_MouseEnter(object sender, EventArgs e)
        {
            label77.ForeColor = Color.FromArgb(255, 205, 0);
        }







        private void label22_MouseLeave(object sender, EventArgs e)
        {
            label22.ForeColor = Color.FromArgb(60, 60, 60);
        }

        private void label21_MouseLeave(object sender, EventArgs e)
        {
            label21.ForeColor = Color.FromArgb(60, 60, 60);
        }

        private void label20_MouseLeave(object sender, EventArgs e)
        {
            label20.ForeColor = Color.FromArgb(60, 60, 60);
        }

        private void label19_MouseLeave(object sender, EventArgs e)
        {
            label19.ForeColor = Color.FromArgb(60, 60, 60);
        }

        private void label27_MouseLeave(object sender, EventArgs e)
        {
            label27.ForeColor = Color.FromArgb(60, 60, 60);
        }

        private void label25_MouseLeave(object sender, EventArgs e)
        {
            label25.ForeColor = Color.FromArgb(60, 60, 60);
        }

        private void label24_MouseLeave(object sender, EventArgs e)
        {
            label24.ForeColor = Color.FromArgb(60, 60, 60);
        }

        private void label23_MouseLeave(object sender, EventArgs e)
        {
            label23.ForeColor = Color.FromArgb(60, 60, 60);
        }

        private void label40_MouseLeave(object sender, EventArgs e)
        {
            label40.ForeColor = Color.FromArgb(60, 60, 60);
        }

        private void label39_MouseLeave(object sender, EventArgs e)
        {
            label39.ForeColor = Color.FromArgb(60, 60, 60);
        }

        private void label44_MouseLeave(object sender, EventArgs e)
        {
            label44.ForeColor = Color.FromArgb(60, 60, 60);
        }

        private void label43_MouseLeave(object sender, EventArgs e)
        {
            label43.ForeColor = Color.FromArgb(60, 60, 60);
        }

        private void label42_MouseLeave(object sender, EventArgs e)
        {
            label42.ForeColor = Color.FromArgb(60, 60, 60);
        }

        private void label41_MouseLeave(object sender, EventArgs e)
        {
            label41.ForeColor = Color.FromArgb(60, 60, 60);
        }

        private void label38_MouseLeave(object sender, EventArgs e)
        {
            label38.ForeColor = Color.FromArgb(60, 60, 60);
        }

        private void label37_MouseLeave(object sender, EventArgs e)
        {
            label37.ForeColor = Color.FromArgb(60, 60, 60);
        }

        private void label36_MouseLeave(object sender, EventArgs e)
        {
            label36.ForeColor = Color.FromArgb(60, 60, 60);
        }

        private void label35_MouseLeave(object sender, EventArgs e)
        {
            label35.ForeColor = Color.FromArgb(60, 60, 60);
        }

        private void label34_MouseLeave(object sender, EventArgs e)
        {
            label34.ForeColor = Color.FromArgb(60, 60, 60);
        }

        private void label33_MouseLeave(object sender, EventArgs e)
        {
            label33.ForeColor = Color.FromArgb(60, 60, 60);
        }

        private void label54_MouseLeave(object sender, EventArgs e)
        {
            label54.ForeColor = Color.FromArgb(60, 60, 60);
        }

        private void label53_MouseLeave(object sender, EventArgs e)
        {
            label53.ForeColor = Color.FromArgb(60, 60, 60);
        }

        private void label52_MouseLeave(object sender, EventArgs e)
        {
            label52.ForeColor = Color.FromArgb(60, 60, 60);
        }

        private void label51_MouseLeave(object sender, EventArgs e)
        {
            label51.ForeColor = Color.FromArgb(60, 60, 60);
        }

        private void label50_MouseLeave(object sender, EventArgs e)
        {
            label50.ForeColor = Color.FromArgb(60, 60, 60);
        }

        private void label49_MouseLeave(object sender, EventArgs e)
        {
            label49.ForeColor = Color.FromArgb(60, 60, 60);
        }

        private void label48_MouseLeave(object sender, EventArgs e)
        {
            label48.ForeColor = Color.FromArgb(60, 60, 60);
        }

        private void label47_MouseLeave(object sender, EventArgs e)
        {
            label47.ForeColor = Color.FromArgb(60, 60, 60);
        }

        private void label46_MouseLeave(object sender, EventArgs e)
        {
            label46.ForeColor = Color.FromArgb(60, 60, 60);
        }

        private void label45_MouseLeave(object sender, EventArgs e)
        {
            label45.ForeColor = Color.FromArgb(60, 60, 60);
        }

        private void label64_MouseLeave(object sender, EventArgs e)
        {
            label64.ForeColor = Color.FromArgb(60, 60, 60);
        }

        private void label63_MouseLeave(object sender, EventArgs e)
        {
            label63.ForeColor = Color.FromArgb(60, 60, 60);
        }

        private void label62_MouseLeave(object sender, EventArgs e)
        {
            label62.ForeColor = Color.FromArgb(60, 60, 60);
        }

        private void label61_MouseLeave(object sender, EventArgs e)
        {
            label61.ForeColor = Color.FromArgb(60, 60, 60);
        }

        private void label60_MouseLeave(object sender, EventArgs e)
        {
            label60.ForeColor = Color.FromArgb(60, 60, 60);
        }

        private void label59_MouseLeave(object sender, EventArgs e)
        {
            label59.ForeColor = Color.FromArgb(60, 60, 60);
        }

        private void label58_MouseLeave(object sender, EventArgs e)
        {
            label58.ForeColor = Color.FromArgb(60, 60, 60);
        }

        private void label57_MouseLeave(object sender, EventArgs e)
        {
            label57.ForeColor = Color.FromArgb(60, 60, 60);
        }

        private void label56_MouseLeave(object sender, EventArgs e)
        {
            label56.ForeColor = Color.FromArgb(60, 60, 60);
        }

        private void label55_MouseLeave(object sender, EventArgs e)
        {
            label55.ForeColor = Color.FromArgb(60, 60, 60);
        }

        private void label74_MouseLeave(object sender, EventArgs e)
        {
            label74.ForeColor = Color.FromArgb(60, 60, 60);
        }

        private void label73_MouseLeave(object sender, EventArgs e)
        {
            label73.ForeColor = Color.FromArgb(60, 60, 60);
        }

        private void label72_MouseLeave(object sender, EventArgs e)
        {
            label72.ForeColor = Color.FromArgb(60, 60, 60);
        }

        private void label71_MouseLeave(object sender, EventArgs e)
        {
            label71.ForeColor = Color.FromArgb(60, 60, 60);
        }

        private void label70_MouseLeave(object sender, EventArgs e)
        {
            label70.ForeColor = Color.FromArgb(60, 60, 60);
        }

        private void label69_MouseLeave(object sender, EventArgs e)
        {
            label69.ForeColor = Color.FromArgb(60, 60, 60);
        }

        private void label68_MouseLeave(object sender, EventArgs e)
        {
            label68.ForeColor = Color.FromArgb(60, 60, 60);
        }

        private void label67_MouseLeave(object sender, EventArgs e)
        {
            label67.ForeColor = Color.FromArgb(60, 60, 60);
        }

        private void label66_MouseLeave(object sender, EventArgs e)
        {
            label66.ForeColor = Color.FromArgb(60, 60, 60);
        }

        private void label65_MouseLeave(object sender, EventArgs e)
        {
            label65.ForeColor = Color.FromArgb(60, 60, 60);
        }

        private void label77_MouseLeave(object sender, EventArgs e)
        {
            label77.ForeColor = Color.FromArgb(60, 60, 60);
        }




        private void label22_Click(object sender, EventArgs e)
        {
            pictureBox1.Image = Properties.Resources.p1; GC.Collect(); label127.Text = "Secvența 1/100";
        }

        private void label21_Click(object sender, EventArgs e)
        {
            pictureBox1.Image = Properties.Resources.p2; GC.Collect(); label127.Text = "Secvența 2/100";
        }

        private void label20_Click(object sender, EventArgs e)
        {
            pictureBox1.Image = Properties.Resources.p3; GC.Collect(); label127.Text = "Secvența 3/100";
        }

        private void label19_Click(object sender, EventArgs e)
        {
            pictureBox1.Image = Properties.Resources.p4; GC.Collect(); label127.Text = "Secvența 4/100";
        }

        private void label27_Click(object sender, EventArgs e)
        {
            pictureBox1.Image = Properties.Resources.p5; GC.Collect(); label127.Text = "Secvența 5/100";
        }

        private void label25_Click(object sender, EventArgs e)
        {
            pictureBox1.Image = Properties.Resources.p6; GC.Collect(); label127.Text = "Secvența 6/100";
        }

        private void label24_Click(object sender, EventArgs e)
        {
            pictureBox1.Image = Properties.Resources.p7; GC.Collect(); label127.Text = "Secvența 7/100";
        }

        private void label23_Click(object sender, EventArgs e)
        {
            pictureBox1.Image = Properties.Resources.p8; GC.Collect(); label127.Text = "Secvența 8/100";
        }

        private void label40_Click(object sender, EventArgs e)
        {
            pictureBox1.Image = Properties.Resources.p9; GC.Collect(); label127.Text = "Secvența 9/100";
        }

        private void label39_Click(object sender, EventArgs e)
        {
            pictureBox1.Image = Properties.Resources.p10; GC.Collect(); label127.Text = "Secvența 10/100";
        }

        private void label44_Click(object sender, EventArgs e)
        {
            pictureBox1.Image = Properties.Resources.p11; GC.Collect(); label127.Text = "Secvența 11/100";
        }

        private void label43_Click(object sender, EventArgs e)
        {
            pictureBox1.Image = Properties.Resources.p12; GC.Collect(); label127.Text = "Secvența 12/100";
        }

        private void label42_Click(object sender, EventArgs e)
        {
            pictureBox1.Image = Properties.Resources.p13; GC.Collect(); label127.Text = "Secvența 13/100";
        }

        private void label41_Click(object sender, EventArgs e)
        {
            pictureBox1.Image = Properties.Resources.p14; GC.Collect(); label127.Text = "Secvența 14/100";
        }

        private void label38_Click(object sender, EventArgs e)
        {
            pictureBox1.Image = Properties.Resources.p15; GC.Collect(); label127.Text = "Secvența 15/100";
        }

        private void label37_Click(object sender, EventArgs e)
        {
            pictureBox1.Image = Properties.Resources.p16; GC.Collect(); label127.Text = "Secvența 16/100";
        }

        private void label36_Click(object sender, EventArgs e)
        {
            pictureBox1.Image = Properties.Resources.p17; GC.Collect(); label127.Text = "Secvența 17/100";
        }

        private void label35_Click(object sender, EventArgs e)
        {
            pictureBox1.Image = Properties.Resources.p18; GC.Collect(); label127.Text = "Secvența 18/100";
        }

        private void label34_Click(object sender, EventArgs e)
        {
            pictureBox1.Image = Properties.Resources.p19; GC.Collect(); label127.Text = "Secvența 19/100";
        }

        private void label33_Click(object sender, EventArgs e)
        {
            pictureBox1.Image = Properties.Resources.p20; GC.Collect(); label127.Text = "Secvența 20/100";
        }

        private void label54_Click(object sender, EventArgs e)
        {
            pictureBox1.Image = Properties.Resources.p21; GC.Collect(); label127.Text = "Secvența 21/100";
        }

        private void label53_Click(object sender, EventArgs e)
        {
            pictureBox1.Image = Properties.Resources.p22; GC.Collect(); label127.Text = "Secvența 22/100";
        }

        private void label52_Click(object sender, EventArgs e)
        {
            pictureBox1.Image = Properties.Resources.p23; GC.Collect(); label127.Text = "Secvența 23/100";
        }

        private void label51_Click(object sender, EventArgs e)
        {
            pictureBox1.Image = Properties.Resources.p24; GC.Collect(); label127.Text = "Secvența 24/100";
        }

        private void label50_Click(object sender, EventArgs e)
        {
            pictureBox1.Image = Properties.Resources.p25; GC.Collect(); label127.Text = "Secvența 25/100";
        }

        private void label49_Click(object sender, EventArgs e)
        {
            pictureBox1.Image = Properties.Resources.p26; GC.Collect(); label127.Text = "Secvența 26/100";
        }

        private void label48_Click(object sender, EventArgs e)
        {
            pictureBox1.Image = Properties.Resources.p27; GC.Collect(); label127.Text = "Secvența 27/100";
        }

        private void label47_Click(object sender, EventArgs e)
        {
            pictureBox1.Image = Properties.Resources.p28; GC.Collect(); label127.Text = "Secvența 28/100";
        }

        private void label46_Click(object sender, EventArgs e)
        {
            pictureBox1.Image = Properties.Resources.p29; GC.Collect(); label127.Text = "Secvența 29/100";
        }

        private void label45_Click(object sender, EventArgs e)
        {
            pictureBox1.Image = Properties.Resources.p30; GC.Collect(); label127.Text = "Secvența 30/100";
        }

        private void label64_Click(object sender, EventArgs e)
        {
            pictureBox1.Image = Properties.Resources.p31; GC.Collect(); label127.Text = "Secvența 31/100";
        }

        private void label63_Click(object sender, EventArgs e)
        {
            pictureBox1.Image = Properties.Resources.p32; GC.Collect(); label127.Text = "Secvența 32/100";
        }

        private void label62_Click(object sender, EventArgs e)
        {
            pictureBox1.Image = Properties.Resources.p33; GC.Collect(); label127.Text = "Secvența 33/100";
        }

        private void label61_Click(object sender, EventArgs e)
        {
            pictureBox1.Image = Properties.Resources.p34; GC.Collect(); label127.Text = "Secvența 34/100";
        }

        private void label60_Click(object sender, EventArgs e)
        {
            pictureBox1.Image = Properties.Resources.p35; GC.Collect(); label127.Text = "Secvența 35/100";
        }

        private void label59_Click(object sender, EventArgs e)
        {
            pictureBox1.Image = Properties.Resources.p36; GC.Collect(); label127.Text = "Secvența 36/100";
        }

        private void label58_Click(object sender, EventArgs e)
        {
            pictureBox1.Image = Properties.Resources.p37; GC.Collect(); label127.Text = "Secvența 37/100";
        }

        private void label57_Click(object sender, EventArgs e)
        {
            pictureBox1.Image = Properties.Resources.p38; GC.Collect(); label127.Text = "Secvența 38/100";
        }

        private void label56_Click(object sender, EventArgs e)
        {
            pictureBox1.Image = Properties.Resources.p39; GC.Collect(); label127.Text = "Secvența 39/100";
        }

        private void label55_Click(object sender, EventArgs e)
        {
            pictureBox1.Image = Properties.Resources.p40; GC.Collect(); label127.Text = "Secvența 40/100";
        }

        private void label74_Click(object sender, EventArgs e)
        {
            pictureBox1.Image = Properties.Resources.p41; GC.Collect(); label127.Text = "Secvența 41/100";
        }

        private void label73_Click(object sender, EventArgs e)
        {
            pictureBox1.Image = Properties.Resources.p42; GC.Collect(); label127.Text = "Secvența 42/100";
        }

        private void label72_Click(object sender, EventArgs e)
        {
            pictureBox1.Image = Properties.Resources.p43; GC.Collect(); label127.Text = "Secvența 43/100";
        }

        private void label71_Click(object sender, EventArgs e)
        {
            pictureBox1.Image = Properties.Resources.p44; GC.Collect(); label127.Text = "Secvența 44/100";
        }

        private void label70_Click(object sender, EventArgs e)
        {
            pictureBox1.Image = Properties.Resources.p45; GC.Collect(); label127.Text = "Secvența 45/100";
        }

        private void label69_Click(object sender, EventArgs e)
        {
            pictureBox1.Image = Properties.Resources.p46; GC.Collect(); label127.Text = "Secvența 46/100";
        }

        private void label68_Click(object sender, EventArgs e)
        {
            pictureBox1.Image = Properties.Resources.p47; GC.Collect(); label127.Text = "Secvența 47/100";
        }

        private void label67_Click(object sender, EventArgs e)
        {
            pictureBox1.Image = Properties.Resources.p48; GC.Collect(); label127.Text = "Secvența 48/100";
        }

        private void label66_Click(object sender, EventArgs e)
        {
            pictureBox1.Image = Properties.Resources.p49; GC.Collect(); label127.Text = "Secvența 49/100";
        }

        private void label65_Click(object sender, EventArgs e)
        {
            pictureBox1.Image = Properties.Resources.p50; GC.Collect(); label127.Text = "Secvența 50/100";
        }

        private void principalform_MouseEnter(object sender, EventArgs e)
        {
            if (validarecodb == true)
            {
                Cursor.Position = new Point(Screen.PrimaryScreen.Bounds.Width / 2, Screen.PrimaryScreen.Bounds.Height / 2);
            }

        }

        private void button5_Click(object sender, EventArgs e)
        {
            sentanswers();
        }

        private double scorebsection = 0;
        private void sentanswers()
        {
            if (sec1.Text == "v3" || sec1.Text == "V3")
            { scorebsection = scorebsection + 0.20; }
            if (sec2.Text == "v2" || sec2.Text == "V2")
            { scorebsection = scorebsection + 0.20; }
            if (sec3.Text == "v1" || sec3.Text == "V1")
            { scorebsection = scorebsection + 0.20; }
            if (sec4.Text == "v1" || sec4.Text == "V1")
            { scorebsection = scorebsection + 0.20; }
            if (sec5.Text == "v1" || sec5.Text == "V1")
            { scorebsection = scorebsection + 0.20; }
            if (sec6.Text == "v4" || sec6.Text == "V4")
            { scorebsection = scorebsection + 0.20; }
            if (sec7.Text == "v3" || sec7.Text == "V3")
            { scorebsection = scorebsection + 0.20; }
            if (sec8.Text == "v2" || sec8.Text == "V2")
            { scorebsection = scorebsection + 0.20; }
            if (sec9.Text == "v1" || sec9.Text == "V1")
            { scorebsection = scorebsection + 0.20; }
            if (sec10.Text == "v2" || sec10.Text == "V2")
            { scorebsection = scorebsection + 0.20; }
            if (sec11.Text == "v3" || sec11.Text == "V3")
            { scorebsection = scorebsection + 0.20; }
            if (sec12.Text == "v1" || sec12.Text == "V1")
            { scorebsection = scorebsection + 0.20; }
            if (sec13.Text == "v2" || sec13.Text == "V2")
            { scorebsection = scorebsection + 0.20; }
            if (sec14.Text == "v4" || sec14.Text == "V4")
            { scorebsection = scorebsection + 0.20; }
            if (sec15.Text == "v3" || sec15.Text == "V3")
            { scorebsection = scorebsection + 0.20; }
            if (sec16.Text == "v2" || sec16.Text == "V2")
            { scorebsection = scorebsection + 0.20; }
            if (sec17.Text == "v2" || sec17.Text == "V2")
            { scorebsection = scorebsection + 0.20; }
            if (sec18.Text == "v1" || sec18.Text == "V1")
            { scorebsection = scorebsection + 0.20; }
            if (sec19.Text == "v2" || sec19.Text == "V2")
            { scorebsection = scorebsection + 0.20; }
            if (sec20.Text == "v1" || sec20.Text == "V1")
            { scorebsection = scorebsection + 0.20; }
            if (sec21.Text == "v3" || sec21.Text == "V3")
            { scorebsection = scorebsection + 0.20; }
            if (sec22.Text == "v3" || sec22.Text == "V3")
            { scorebsection = scorebsection + 0.20; }
            if (sec23.Text == "v3" || sec23.Text == "V3")
            { scorebsection = scorebsection + 0.20; }
            if (sec24.Text == "v2" || sec24.Text == "V2")
            { scorebsection = scorebsection + 0.20; }
            if (sec25.Text == "v1" || sec25.Text == "V1")
            { scorebsection = scorebsection + 0.20; }
            if (sec26.Text == "v4" || sec26.Text == "V4")
            { scorebsection = scorebsection + 0.20; }
            if (sec27.Text == "v3" || sec27.Text == "V3")
            { scorebsection = scorebsection + 0.20; }
            if (sec28.Text == "v2" || sec28.Text == "V2")
            { scorebsection = scorebsection + 0.20; }
            if (sec29.Text == "v4" || sec29.Text == "V4")
            { scorebsection = scorebsection + 0.20; }
            if (sec30.Text == "v3" || sec30.Text == "V3")
            { scorebsection = scorebsection + 0.20; }
            if (sec31.Text == "v3" || sec31.Text == "V3")
            { scorebsection = scorebsection + 0.20; }
            if (sec32.Text == "v1" || sec32.Text == "V1")
            { scorebsection = scorebsection + 0.20; }
            if (sec33.Text == "v4" || sec33.Text == "V4")
            { scorebsection = scorebsection + 0.20; }
            if (sec34.Text == "v2" || sec34.Text == "V2")
            { scorebsection = scorebsection + 0.20; }
            if (sec35.Text == "v1" || sec35.Text == "V1")
            { scorebsection = scorebsection + 0.20; }
            if (sec36.Text == "v4" || sec36.Text == "V4")
            { scorebsection = scorebsection + 0.20; }
            if (sec37.Text == "v3" || sec37.Text == "V3")
            { scorebsection = scorebsection + 0.20; }
            if (sec38.Text == "v3" || sec38.Text == "V3")
            { scorebsection = scorebsection + 0.20; }
            if (sec39.Text == "v2" || sec39.Text == "V2")
            { scorebsection = scorebsection + 0.20; }
            if (sec40.Text == "v3" || sec40.Text == "V3")
            { scorebsection = scorebsection + 0.20; }
            if (sec41.Text == "v3" || sec41.Text == "V3")
            { scorebsection = scorebsection + 0.20; }
            if (sec42.Text == "v1" || sec42.Text == "V1")
            { scorebsection = scorebsection + 0.20; }
            if (sec43.Text == "v1" || sec43.Text == "V1")
            { scorebsection = scorebsection + 0.20; }
            if (sec44.Text == "v1" || sec44.Text == "V1")
            { scorebsection = scorebsection + 0.20; }
            if (sec45.Text == "v1" || sec45.Text == "V1")
            { scorebsection = scorebsection + 0.20; }
            if (sec46.Text == "v1" || sec46.Text == "V1")
            { scorebsection = scorebsection + 0.20; }
            if (sec47.Text == "v2" || sec47.Text == "V2")
            { scorebsection = scorebsection + 0.20; }
            if (sec48.Text == "v4" || sec48.Text == "V4")
            { scorebsection = scorebsection + 0.20; }
            if (sec49.Text == "v2" || sec49.Text == "V2")
            { scorebsection = scorebsection + 0.20; }
            if (sec50.Text == "v1" || sec50.Text == "V1")
            { scorebsection = scorebsection + 0.20; }
            if (sec51.Text == "v1" || sec51.Text == "V1")
            { scorebsection = scorebsection + 0.20; }
            if (sec52.Text == "v1" || sec52.Text == "V1")
            { scorebsection = scorebsection + 0.20; }
            if (sec53.Text == "v1" || sec53.Text == "V1")
            { scorebsection = scorebsection + 0.20; }
            if (sec54.Text == "v2" || sec54.Text == "V2")
            { scorebsection = scorebsection + 0.20; }
            if (sec55.Text == "v1" || sec55.Text == "V1")
            { scorebsection = scorebsection + 0.20; }
            if (sec56.Text == "v4" || sec56.Text == "V4")
            { scorebsection = scorebsection + 0.20; }
            if (sec57.Text == "v2" || sec57.Text == "V2")
            { scorebsection = scorebsection + 0.20; }
            if (sec58.Text == "v4" || sec58.Text == "V4")
            { scorebsection = scorebsection + 0.20; }
            if (sec59.Text == "v3" || sec59.Text == "V3")
            { scorebsection = scorebsection + 0.20; }
            if (sec60.Text == "v1" || sec60.Text == "V1")
            { scorebsection = scorebsection + 0.20; }
            if (sec61.Text == "v2" || sec61.Text == "V2")
            { scorebsection = scorebsection + 0.20; }
            if (sec62.Text == "v2" || sec62.Text == "V2")
            { scorebsection = scorebsection + 0.20; }
            if (sec63.Text == "v4" || sec63.Text == "V4")
            { scorebsection = scorebsection + 0.20; }
            if (sec64.Text == "v2" || sec64.Text == "V2")
            { scorebsection = scorebsection + 0.20; }
            if (sec65.Text == "v3" || sec65.Text == "V3")
            { scorebsection = scorebsection + 0.20; }
            if (sec66.Text == "v1" || sec66.Text == "V1")
            { scorebsection = scorebsection + 0.20; }
            if (sec67.Text == "v2" || sec67.Text == "V2")
            { scorebsection = scorebsection + 0.20; }
            if (sec68.Text == "v3" || sec68.Text == "V3")
            { scorebsection = scorebsection + 0.20; }
            if (sec69.Text == "v2" || sec69.Text == "V2")
            { scorebsection = scorebsection + 0.20; }
            if (sec70.Text == "v3" || sec70.Text == "V3")
            { scorebsection = scorebsection + 0.20; }
            if (sec71.Text == "v4" || sec71.Text == "V4")
            { scorebsection = scorebsection + 0.20; }
            if (sec72.Text == "v4" || sec72.Text == "V4")
            { scorebsection = scorebsection + 0.20; }
            if (sec73.Text == "v2" || sec73.Text == "V2")
            { scorebsection = scorebsection + 0.20; }
            if (sec74.Text == "v3" || sec74.Text == "V3")
            { scorebsection = scorebsection + 0.20; }
            if (sec75.Text == "v3" || sec75.Text == "V3")
            { scorebsection = scorebsection + 0.20; }
            if (sec76.Text == "v1" || sec76.Text == "V1")
            { scorebsection = scorebsection + 0.20; }
            if (sec77.Text == "v3" || sec77.Text == "V3")
            { scorebsection = scorebsection + 0.20; }
            if (sec78.Text == "v4" || sec78.Text == "V4")
            { scorebsection = scorebsection + 0.20; }
            if (sec79.Text == "v4" || sec79.Text == "V4")
            { scorebsection = scorebsection + 0.20; }
            if (sec80.Text == "v1" || sec80.Text == "V1")
            { scorebsection = scorebsection + 0.20; }
            if (sec81.Text == "v2" || sec81.Text == "V2")
            { scorebsection = scorebsection + 0.20; }
            if (sec82.Text == "v1" || sec82.Text == "V1")
            { scorebsection = scorebsection + 0.20; }
            if (sec83.Text == "v3" || sec83.Text == "V3")
            { scorebsection = scorebsection + 0.20; }
            if (sec84.Text == "v2" || sec84.Text == "V2")
            { scorebsection = scorebsection + 0.20; }
            if (sec85.Text == "v1" || sec85.Text == "V1")
            { scorebsection = scorebsection + 0.20; }
            if (sec86.Text == "v3" || sec86.Text == "V3")
            { scorebsection = scorebsection + 0.20; }
            if (sec87.Text == "v1" || sec87.Text == "V1")
            { scorebsection = scorebsection + 0.20; }
            if (sec88.Text == "v3" || sec88.Text == "V3")
            { scorebsection = scorebsection + 0.20; }
            if (sec89.Text == "v2" || sec89.Text == "V2")
            { scorebsection = scorebsection + 0.20; }
            if (sec90.Text == "v1" || sec90.Text == "V1")
            { scorebsection = scorebsection + 0.20; }
            if (sec91.Text == "v2" || sec91.Text == "V2")
            { scorebsection = scorebsection + 0.20; }
            if (sec92.Text == "v4" || sec92.Text == "V4")
            { scorebsection = scorebsection + 0.20; }
            if (sec93.Text == "v1" || sec93.Text == "V1")
            { scorebsection = scorebsection + 0.20; }
            if (sec94.Text == "v2" || sec94.Text == "V2")
            { scorebsection = scorebsection + 0.20; }
            if (sec95.Text == "v1" || sec95.Text == "V1")
            { scorebsection = scorebsection + 0.20; }
            if (sec96.Text == "v3" || sec96.Text == "V3")
            { scorebsection = scorebsection + 0.20; }
            if (sec97.Text == "v2" || sec97.Text == "V2")
            { scorebsection = scorebsection + 0.20; }
            if (sec98.Text == "v2" || sec98.Text == "V2")
            { scorebsection = scorebsection + 0.20; }
            if (sec99.Text == "v3" || sec99.Text == "V3")
            { scorebsection = scorebsection + 0.20; }
            if (sec100.Text == "v3" || sec100.Text == "V3")
            { scorebsection = scorebsection + 0.20; }


            label76.Text = scorebsection.ToString();
        }

        private void label94_MouseEnter(object sender, EventArgs e)
        {
            label94.ForeColor = Color.FromArgb(255, 205, 0);
        }

        private void label93_MouseEnter(object sender, EventArgs e)
        {
            label93.ForeColor = Color.FromArgb(255, 205, 0);
        }

        private void label92_MouseEnter(object sender, EventArgs e)
        {
            label92.ForeColor = Color.FromArgb(255, 205, 0);
        }

        private void label91_MouseEnter(object sender, EventArgs e)
        {
            label91.ForeColor = Color.FromArgb(255, 205, 0);
        }

        private void label90_MouseEnter(object sender, EventArgs e)
        {
            label90.ForeColor = Color.FromArgb(255, 205, 0);
        }

        private void label89_MouseEnter(object sender, EventArgs e)
        {
            label89.ForeColor = Color.FromArgb(255, 205, 0);
        }

        private void label88_MouseEnter(object sender, EventArgs e)
        {
            label88.ForeColor = Color.FromArgb(255, 205, 0);
        }

        private void label87_MouseEnter(object sender, EventArgs e)
        {
            label87.ForeColor = Color.FromArgb(255, 205, 0);
        }

        private void label86_MouseEnter(object sender, EventArgs e)
        {
            label86.ForeColor = Color.FromArgb(255, 205, 0);
        }

        private void label85_MouseEnter(object sender, EventArgs e)
        {
            label85.ForeColor = Color.FromArgb(255, 205, 0);
        }

        private void label84_MouseEnter(object sender, EventArgs e)
        {
            label84.ForeColor = Color.FromArgb(255, 205, 0);
        }

        private void label83_MouseEnter(object sender, EventArgs e)
        {
            label83.ForeColor = Color.FromArgb(255, 205, 0);
        }

        private void label82_MouseEnter(object sender, EventArgs e)
        {
            label82.ForeColor = Color.FromArgb(255, 205, 0);
        }

        private void label81_MouseEnter(object sender, EventArgs e)
        {
            label81.ForeColor = Color.FromArgb(255, 205, 0);
        }

        private void label80_MouseEnter(object sender, EventArgs e)
        {
            label80.ForeColor = Color.FromArgb(255, 205, 0);
        }

        private void label79_MouseEnter(object sender, EventArgs e)
        {
            label79.ForeColor = Color.FromArgb(255, 205, 0);
        }

        private void label78_MouseEnter(object sender, EventArgs e)
        {
            label78.ForeColor = Color.FromArgb(255, 205, 0);
        }

        private void label111_MouseEnter(object sender, EventArgs e)
        {
            label111.ForeColor = Color.FromArgb(255, 205, 0);
        }

        private void label110_MouseEnter(object sender, EventArgs e)
        {
            label110.ForeColor = Color.FromArgb(255, 205, 0);
        }

        private void label109_MouseEnter(object sender, EventArgs e)
        {
            label109.ForeColor = Color.FromArgb(255, 205, 0);
        }

        private void label108_MouseEnter(object sender, EventArgs e)
        {
            label108.ForeColor = Color.FromArgb(255, 205, 0);
        }

        private void label107_MouseEnter(object sender, EventArgs e)
        {
            label107.ForeColor = Color.FromArgb(255, 205, 0);
        }

        private void label106_MouseEnter(object sender, EventArgs e)
        {
            label106.ForeColor = Color.FromArgb(255, 205, 0);
        }

        private void label105_MouseEnter(object sender, EventArgs e)
        {
            label105.ForeColor = Color.FromArgb(255, 205, 0);
        }

        private void label104_MouseEnter(object sender, EventArgs e)
        {
            label104.ForeColor = Color.FromArgb(255, 205, 0);
        }

        private void label103_MouseEnter(object sender, EventArgs e)
        {
            label103.ForeColor = Color.FromArgb(255, 205, 0);
        }

        private void label102_MouseEnter(object sender, EventArgs e)
        {
            label102.ForeColor = Color.FromArgb(255, 205, 0);
        }

        private void label101_MouseEnter(object sender, EventArgs e)
        {
            label101.ForeColor = Color.FromArgb(255, 205, 0);
        }

        private void label100_MouseEnter(object sender, EventArgs e)
        {
            label100.ForeColor = Color.FromArgb(255, 205, 0);
        }

        private void label99_MouseEnter(object sender, EventArgs e)
        {
            label99.ForeColor = Color.FromArgb(255, 205, 0);
        }

        private void label98_MouseEnter(object sender, EventArgs e)
        {
            label98.ForeColor = Color.FromArgb(255, 205, 0);
        }

        private void label97_MouseEnter(object sender, EventArgs e)
        {
            label97.ForeColor = Color.FromArgb(255, 205, 0);
        }

        private void label96_MouseEnter(object sender, EventArgs e)
        {
            label96.ForeColor = Color.FromArgb(255, 205, 0);
        }

        private void label95_MouseEnter(object sender, EventArgs e)
        {
            label95.ForeColor = Color.FromArgb(255, 205, 0);
        }

        private void label121_MouseEnter(object sender, EventArgs e)
        {
            label121.ForeColor = Color.FromArgb(255, 205, 0);
        }

        private void label120_MouseEnter(object sender, EventArgs e)
        {
            label120.ForeColor = Color.FromArgb(255, 205, 0);
        }

        private void label119_MouseEnter(object sender, EventArgs e)
        {
            label119.ForeColor = Color.FromArgb(255, 205, 0);
        }

        private void label118_MouseEnter(object sender, EventArgs e)
        {
            label118.ForeColor = Color.FromArgb(255, 205, 0);
        }

        private void label117_MouseEnter(object sender, EventArgs e)
        {
            label117.ForeColor = Color.FromArgb(255, 205, 0);
        }

        private void label116_MouseEnter(object sender, EventArgs e)
        {
            label116.ForeColor = Color.FromArgb(255, 205, 0);
        }

        private void label115_MouseEnter(object sender, EventArgs e)
        {
            label115.ForeColor = Color.FromArgb(255, 205, 0);
        }

        private void label114_MouseEnter(object sender, EventArgs e)
        {
            label114.ForeColor = Color.FromArgb(255, 205, 0);
        }

        private void label113_MouseEnter(object sender, EventArgs e)
        {
            label113.ForeColor = Color.FromArgb(255, 205, 0);
        }

        private void label112_MouseEnter(object sender, EventArgs e)
        {
            label112.ForeColor = Color.FromArgb(255, 205, 0);
        }

        private void label126_MouseEnter(object sender, EventArgs e)
        {
            label126.ForeColor = Color.FromArgb(255, 205, 0);
        }

        private void label125_MouseEnter(object sender, EventArgs e)
        {
            label125.ForeColor = Color.FromArgb(255, 205, 0);
        }

        private void label124_MouseEnter(object sender, EventArgs e)
        {
            label124.ForeColor = Color.FromArgb(255, 205, 0);
        }

        private void label123_MouseEnter(object sender, EventArgs e)
        {
            label123.ForeColor = Color.FromArgb(255, 205, 0);
        }

        private void label122_MouseEnter(object sender, EventArgs e)
        {
            label122.ForeColor = Color.FromArgb(255, 205, 0);
        }

        private void label94_MouseLeave(object sender, EventArgs e)
        {
            label94.ForeColor = Color.FromArgb(60, 60, 60);//------------------------------------------------------------------
        }

        private void label93_MouseLeave(object sender, EventArgs e)
        {
            label93.ForeColor = Color.FromArgb(60, 60, 60);
        }

        private void label92_MouseLeave(object sender, EventArgs e)
        {
            label92.ForeColor = Color.FromArgb(60, 60, 60);
        }

        private void label91_MouseLeave(object sender, EventArgs e)
        {
            label91.ForeColor = Color.FromArgb(60, 60, 60);
        }

        private void label90_MouseLeave(object sender, EventArgs e)
        {
            label90.ForeColor = Color.FromArgb(60, 60, 60);
        }

        private void label89_MouseLeave(object sender, EventArgs e)
        {
            label89.ForeColor = Color.FromArgb(60, 60, 60);
        }

        private void label88_MouseLeave(object sender, EventArgs e)
        {
            label88.ForeColor = Color.FromArgb(60, 60, 60);
        }

        private void label87_MouseLeave(object sender, EventArgs e)
        {
            label87.ForeColor = Color.FromArgb(60, 60, 60);
        }

        private void label86_MouseLeave(object sender, EventArgs e)
        {
            label86.ForeColor = Color.FromArgb(60, 60, 60);
        }

        private void label85_MouseLeave(object sender, EventArgs e)
        {
            label85.ForeColor = Color.FromArgb(60, 60, 60);
        }

        private void label84_MouseLeave(object sender, EventArgs e)
        {
            label84.ForeColor = Color.FromArgb(60, 60, 60);
        }

        private void label83_MouseLeave(object sender, EventArgs e)
        {
            label83.ForeColor = Color.FromArgb(60, 60, 60);
        }

        private void label82_MouseLeave(object sender, EventArgs e)
        {
            label82.ForeColor = Color.FromArgb(60, 60, 60);
        }

        private void label81_MouseLeave(object sender, EventArgs e)
        {
            label81.ForeColor = Color.FromArgb(60, 60, 60);
        }

        private void label80_MouseLeave(object sender, EventArgs e)
        {
            label80.ForeColor = Color.FromArgb(60, 60, 60);
        }

        private void label79_MouseLeave(object sender, EventArgs e)
        {
            label79.ForeColor = Color.FromArgb(60, 60, 60);
        }

        private void label78_MouseLeave(object sender, EventArgs e)
        {
            label78.ForeColor = Color.FromArgb(60, 60, 60);
        }

        private void label111_MouseLeave(object sender, EventArgs e)
        {
            label111.ForeColor = Color.FromArgb(60, 60, 60);
        }

        private void label110_MouseLeave(object sender, EventArgs e)
        {
            label110.ForeColor = Color.FromArgb(60, 60, 60);
        }

        private void label109_MouseLeave(object sender, EventArgs e)
        {
            label109.ForeColor = Color.FromArgb(60, 60, 60);
        }

        private void label108_MouseLeave(object sender, EventArgs e)
        {
            label108.ForeColor = Color.FromArgb(60, 60, 60);
        }

        private void label107_MouseLeave(object sender, EventArgs e)
        {
            label107.ForeColor = Color.FromArgb(60, 60, 60);
        }

        private void label106_MouseLeave(object sender, EventArgs e)
        {
            label106.ForeColor = Color.FromArgb(60, 60, 60);
        }

        private void label105_MouseLeave(object sender, EventArgs e)
        {
            label105.ForeColor = Color.FromArgb(60, 60, 60);
        }

        private void label104_MouseLeave(object sender, EventArgs e)
        {
            label104.ForeColor = Color.FromArgb(60, 60, 60);
        }

        private void label103_MouseLeave(object sender, EventArgs e)
        {
            label103.ForeColor = Color.FromArgb(60, 60, 60);
        }

        private void label102_MouseLeave(object sender, EventArgs e)
        {
            label102.ForeColor = Color.FromArgb(60, 60, 60);
        }

        private void label101_MouseLeave(object sender, EventArgs e)
        {
            label101.ForeColor = Color.FromArgb(60, 60, 60);
        }

        private void label100_MouseLeave(object sender, EventArgs e)
        {
            label100.ForeColor = Color.FromArgb(60, 60, 60);
        }

        private void label99_MouseLeave(object sender, EventArgs e)
        {
            label99.ForeColor = Color.FromArgb(60, 60, 60);
        }

        private void label98_MouseLeave(object sender, EventArgs e)
        {
            label98.ForeColor = Color.FromArgb(60, 60, 60);
        }

        private void label97_MouseLeave(object sender, EventArgs e)
        {
            label97.ForeColor = Color.FromArgb(60, 60, 60);
        }

        private void label96_MouseLeave(object sender, EventArgs e)
        {
            label96.ForeColor = Color.FromArgb(60, 60, 60);
        }

        private void label95_MouseLeave(object sender, EventArgs e)
        {
            label95.ForeColor = Color.FromArgb(60, 60, 60);
        }

        private void label121_MouseLeave(object sender, EventArgs e)
        {
            label121.ForeColor = Color.FromArgb(60, 60, 60);
        }

        private void label120_MouseLeave(object sender, EventArgs e)
        {
            label120.ForeColor = Color.FromArgb(60, 60, 60);
        }

        private void label119_MouseLeave(object sender, EventArgs e)
        {
            label119.ForeColor = Color.FromArgb(60, 60, 60);
        }

        private void label118_MouseLeave(object sender, EventArgs e)
        {
            label118.ForeColor = Color.FromArgb(60, 60, 60);
        }

        private void label117_MouseLeave(object sender, EventArgs e)
        {
            label117.ForeColor = Color.FromArgb(60, 60, 60);
        }

        private void label116_MouseLeave(object sender, EventArgs e)
        {
            label116.ForeColor = Color.FromArgb(60, 60, 60);
        }

        private void label115_MouseLeave(object sender, EventArgs e)
        {
            label115.ForeColor = Color.FromArgb(60, 60, 60);
        }

        private void label114_MouseLeave(object sender, EventArgs e)
        {
            label114.ForeColor = Color.FromArgb(60, 60, 60);
        }

        private void label113_MouseLeave(object sender, EventArgs e)
        {
            label113.ForeColor = Color.FromArgb(60, 60, 60);
        }

        private void label112_MouseLeave(object sender, EventArgs e)
        {
            label112.ForeColor = Color.FromArgb(60, 60, 60);
        }

        private void label126_MouseLeave(object sender, EventArgs e)
        {
            label126.ForeColor = Color.FromArgb(60, 60, 60);
        }

        private void label125_MouseLeave(object sender, EventArgs e)
        {
            label125.ForeColor = Color.FromArgb(60, 60, 60);
        }

        private void label124_MouseLeave(object sender, EventArgs e)
        {
            label124.ForeColor = Color.FromArgb(60, 60, 60);
        }

        private void label123_MouseLeave(object sender, EventArgs e)
        {
            label123.ForeColor = Color.FromArgb(60, 60, 60);
        }

        private void label122_MouseLeave(object sender, EventArgs e)
        {
            label122.ForeColor = Color.FromArgb(60, 60, 60);
        }


        //-----------------------------------------------------------------------sec51-100
        private void label77_Click(object sender, EventArgs e)
        {
            pictureBox1.Image = Properties.Resources.p51; GC.Collect(); label127.Text = "Secvența 51/100";
        }

        private void label94_Click(object sender, EventArgs e)
        {
            pictureBox1.Image = Properties.Resources.p52; GC.Collect(); label127.Text = "Secvența 52/100";
        }

        private void label93_Click(object sender, EventArgs e)
        {
            pictureBox1.Image = Properties.Resources.p53; GC.Collect(); label127.Text = "Secvența 53/100";
        }

        private void label92_Click(object sender, EventArgs e)
        {
            pictureBox1.Image = Properties.Resources.p54; GC.Collect(); label127.Text = "Secvența 54/100";
        }

        private void label91_Click(object sender, EventArgs e)
        {
            pictureBox1.Image = Properties.Resources.p55; GC.Collect(); label127.Text = "Secvența 55/100";
        }

        private void label90_Click(object sender, EventArgs e)
        {
            pictureBox1.Image = Properties.Resources.p56; GC.Collect(); label127.Text = "Secvența 56/100";
        }

        private void label89_Click(object sender, EventArgs e)
        {
            pictureBox1.Image = Properties.Resources.p57; GC.Collect(); label127.Text = "Secvența 57/100";
        }

        private void label88_Click(object sender, EventArgs e)
        {
            pictureBox1.Image = Properties.Resources.p58; GC.Collect(); label127.Text = "Secvența 58/100";
        }

        private void label87_Click(object sender, EventArgs e)
        {
            pictureBox1.Image = Properties.Resources.p59; GC.Collect(); label127.Text = "Secvența 59/100";
        }

        private void label86_Click(object sender, EventArgs e)
        {
            pictureBox1.Image = Properties.Resources.p60; GC.Collect(); label127.Text = "Secvența 60/100";
        }

        private void label85_Click(object sender, EventArgs e)
        {
            pictureBox1.Image = Properties.Resources.p61; GC.Collect(); label127.Text = "Secvența 61/100";
        }

        private void label84_Click(object sender, EventArgs e)
        {
            pictureBox1.Image = Properties.Resources.p62; GC.Collect(); label127.Text = "Secvența 62/100";
        }

        private void label83_Click(object sender, EventArgs e)
        {
            pictureBox1.Image = Properties.Resources.p63; GC.Collect(); label127.Text = "Secvența 63/100";
        }

        private void label82_Click(object sender, EventArgs e)
        {
            pictureBox1.Image = Properties.Resources.p64; GC.Collect(); label127.Text = "Secvența 64/100";
        }

        private void label81_Click(object sender, EventArgs e)
        {
            pictureBox1.Image = Properties.Resources.p65; GC.Collect(); label127.Text = "Secvența 65/100";
        }

        private void label80_Click(object sender, EventArgs e)
        {
            pictureBox1.Image = Properties.Resources.p66; GC.Collect(); label127.Text = "Secvența 66/100";
        }

        private void label79_Click(object sender, EventArgs e)
        {
            pictureBox1.Image = Properties.Resources.p67; GC.Collect(); label127.Text = "Secvența 67/100";
        }

        private void label78_Click(object sender, EventArgs e)
        {
            pictureBox1.Image = Properties.Resources.p68; GC.Collect(); label127.Text = "Secvența 68/100";
        }

        private void label111_Click(object sender, EventArgs e)
        {
            pictureBox1.Image = Properties.Resources.p69; GC.Collect(); label127.Text = "Secvența 69/100";
        }

        private void label110_Click(object sender, EventArgs e)
        {
            pictureBox1.Image = Properties.Resources.p70; GC.Collect(); label127.Text = "Secvența 70/100";
        }

        private void label109_Click(object sender, EventArgs e)
        {
            pictureBox1.Image = Properties.Resources.p71; GC.Collect(); label127.Text = "Secvența 71/100";
        }

        private void label108_Click(object sender, EventArgs e)
        {
            pictureBox1.Image = Properties.Resources.p72; GC.Collect(); label127.Text = "Secvența 72/100";
        }

        private void label107_Click(object sender, EventArgs e)
        {
            pictureBox1.Image = Properties.Resources.p73; GC.Collect(); label127.Text = "Secvența 73/100";
        }

        private void label106_Click(object sender, EventArgs e)
        {
            pictureBox1.Image = Properties.Resources.p74; GC.Collect(); label127.Text = "Secvența 74/100";
        }

        private void label105_Click(object sender, EventArgs e)
        {
            pictureBox1.Image = Properties.Resources.p75; GC.Collect(); label127.Text = "Secvența 75/100";
        }

        private void label104_Click(object sender, EventArgs e)
        {
            pictureBox1.Image = Properties.Resources.p76; GC.Collect(); label127.Text = "Secvența 76/100";
        }

        private void label103_Click(object sender, EventArgs e)
        {
            pictureBox1.Image = Properties.Resources.p77; GC.Collect(); label127.Text = "Secvența 77/100";
        }

        private void label102_Click(object sender, EventArgs e)
        {
            pictureBox1.Image = Properties.Resources.p78; GC.Collect(); label127.Text = "Secvența 78/100";
        }

        private void label101_Click(object sender, EventArgs e)
        {
            pictureBox1.Image = Properties.Resources.p79; GC.Collect(); label127.Text = "Secvența 79/100";
        }

        private void label100_Click(object sender, EventArgs e)
        {
            pictureBox1.Image = Properties.Resources.p80; GC.Collect(); label127.Text = "Secvența 80/100";
        }

        private void label99_Click(object sender, EventArgs e)
        {
            pictureBox1.Image = Properties.Resources.p81; GC.Collect(); label127.Text = "Secvența 81/100";
        }

        private void label98_Click(object sender, EventArgs e)
        {
            pictureBox1.Image = Properties.Resources.p82; GC.Collect(); label127.Text = "Secvența 82/100";
        }

        private void label97_Click(object sender, EventArgs e)
        {
            pictureBox1.Image = Properties.Resources.p83; GC.Collect(); label127.Text = "Secvența 83/100";
        }

        private void label96_Click(object sender, EventArgs e)
        {
            pictureBox1.Image = Properties.Resources.p84; GC.Collect(); label127.Text = "Secvența 84/100";
        }

        private void label95_Click(object sender, EventArgs e)
        {
            pictureBox1.Image = Properties.Resources.p85; GC.Collect(); label127.Text = "Secvența 85/100";
        }

        private void label121_Click(object sender, EventArgs e)
        {
            pictureBox1.Image = Properties.Resources.p86; GC.Collect(); label127.Text = "Secvența 86/100";
        }

        private void label120_Click(object sender, EventArgs e)
        {
            pictureBox1.Image = Properties.Resources.p87; GC.Collect(); label127.Text = "Secvența 87/100";
        }

        private void label119_Click(object sender, EventArgs e)
        {
            pictureBox1.Image = Properties.Resources.p88; GC.Collect(); label127.Text = "Secvența 88/100";
        }

        private void label118_Click(object sender, EventArgs e)
        {
            pictureBox1.Image = Properties.Resources.p89; GC.Collect(); label127.Text = "Secvența 89/100";
        }

        private void label117_Click(object sender, EventArgs e)
        {
            pictureBox1.Image = Properties.Resources.p90; GC.Collect(); label127.Text = "Secvența 90/100";
        }

        private void label116_Click(object sender, EventArgs e)
        {
            pictureBox1.Image = Properties.Resources.p91; GC.Collect(); label127.Text = "Secvența 91/100";
        }

        private void label115_Click(object sender, EventArgs e)
        {
            pictureBox1.Image = Properties.Resources.p92; GC.Collect(); label127.Text = "Secvența 92/100";
        }

        private void label114_Click(object sender, EventArgs e)
        {
            pictureBox1.Image = Properties.Resources.p93; GC.Collect(); label127.Text = "Secvența 93/100";
        }

        private void label113_Click(object sender, EventArgs e)
        {
            pictureBox1.Image = Properties.Resources.p94; GC.Collect(); label127.Text = "Secvența 94/100";
        }

        private void label112_Click(object sender, EventArgs e)
        {
            pictureBox1.Image = Properties.Resources.p95; GC.Collect(); label127.Text = "Secvența 95/100";
        }

        private void label126_Click(object sender, EventArgs e)
        {
            pictureBox1.Image = Properties.Resources.p96; GC.Collect(); label127.Text = "Secvența 96/100";
        }

        private void label125_Click(object sender, EventArgs e)
        {
            pictureBox1.Image = Properties.Resources.p97; GC.Collect(); label127.Text = "Secvența 97/100";
        }

        private void label124_Click(object sender, EventArgs e)
        {
            pictureBox1.Image = Properties.Resources.p98; GC.Collect(); label127.Text = "Secvența 98/100";
        }

        private void label123_Click(object sender, EventArgs e)
        {
            pictureBox1.Image = Properties.Resources.p99; GC.Collect(); label127.Text = "Secvența 99/100";
        }

        private void label122_Click(object sender, EventArgs e)
        {
            pictureBox1.Image = Properties.Resources.p100; GC.Collect(); label127.Text = "Secvența 100/100";
        }

        private void sec1_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Check for a naughty character in the KeyDown event.



        }

        private void sec100_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (System.Text.RegularExpressions.Regex.IsMatch(e.KeyChar.ToString(), @"[^1-4^+^v^]") && !(char.IsControl(e.KeyChar)))
            {
                // Stop the character from being entered into the control since it is illegal.
                e.Handled = true;
            }
            
        }


















        //----------------------------------------------------------------------------P2 section close--------------------------------
        private void button3_Click(object sender, EventArgs e)
        {
            insertfile1editproject();
        }

        public void insertfile1editproject()
        {

            using (var stream = new FileStream(pathcodsursa, FileMode.Open, FileAccess.Read))
            {
                using (var reader = new BinaryReader(stream))
                {
                    fileinsert1 = reader.ReadBytes((int)stream.Length);
                    SqlConnection con = new SqlConnection(stringcon); //CONNECTION
                    SqlCommand cmd2 = new SqlCommand();
                    cmd2.CommandText = "update proba_A set fisier=@fl,ora_incarcare=@ora, trimis=1 where id_utilizator=@id_ut";
                    cmd2.Parameters.Clear();
                    cmd2.Connection = con;
                    cmd2.Parameters.AddWithValue("@fl", fileinsert1);                
                    cmd2.Parameters.AddWithValue("@id_ut", Form1.idutilizator);
                    cmd2.Parameters.AddWithValue("@ora", DateTime.Now.ToString("HH:mm:ss"));
                    con.Open();
                    cmd2.ExecuteNonQuery();
                    con.Close();
                }
            }
        }





    }
}
