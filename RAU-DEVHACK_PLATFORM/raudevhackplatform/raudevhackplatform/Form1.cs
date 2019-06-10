using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Configuration;
using System.IO;
namespace raudevhackplatform
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void label9_Click(object sender, EventArgs e)
        {
            label8.Visible = false;
            label13.Visible = false;
            label14.Visible = false;
            label2.Visible = false;
            label4.Visible = false;
            //label8.BackColor = Color.Black;
            //label13.BackColor = Color.Red;
            //label14.BackColor = Color.Yellow;
            //label15.BackColor = Color.Blue;
            label8.Left = ((Control)sender).Left;
            label8.Width = ((Control)sender).Width;
            bunifuTransition1.ShowSync(label8);

            panel2.Visible = true;
            panel3.Visible = false;
            panel4.Visible = false;
            panel5.Visible = false;
            panel6.Visible = false;
        }

        private void label10_Click(object sender, EventArgs e)
        {
            label8.Visible = false;
            label13.Visible = false;
            label14.Visible = false;
            label2.Visible = false;
            label4.Visible = false;
            //label8.BackColor = Color.Black;
            //label13.BackColor = Color.Red;
            //label14.BackColor = Color.Yellow;
            //label15.BackColor = Color.Blue;
            label13.Left = ((Control)sender).Left;
            label13.Width = ((Control)sender).Width;
            bunifuTransition1.ShowSync(label13);

            panel2.Visible = false;
            panel3.Visible = true;
            panel4.Visible = false;
            panel5.Visible = false;
            panel6.Visible = false;
        }

        private void label11_Click(object sender, EventArgs e)
        {
            label8.Visible = false;
            label13.Visible = false;
            label14.Visible = false;
            label2.Visible = false;
            label4.Visible = false;
            //label8.BackColor = Color.Black;
            //label13.BackColor = Color.Red;
            //label14.BackColor = Color.Yellow;
            //label15.BackColor = Color.Blue;
            label14.Left = ((Control)sender).Left;
            label14.Width = ((Control)sender).Width;
            bunifuTransition1.ShowSync(label14);

            panel2.Visible = false;
            panel3.Visible = false;
            panel4.Visible = true;
            panel5.Visible = false;
            panel6.Visible = false;
        }

        private void label3_Click(object sender, EventArgs e)
        {
            label8.Visible = false;
            label13.Visible = false;
            label14.Visible = false;
            label2.Visible = false;
            label4.Visible = false;
            //label8.BackColor = Color.Black;
            //label13.BackColor = Color.Red;
            //label14.BackColor = Color.Yellow;
            //label15.BackColor = Color.Blue;
            label2.Left = ((Control)sender).Left;
            label2.Width = ((Control)sender).Width;
            bunifuTransition1.ShowSync(label2);

            panel2.Visible = false;
            panel3.Visible = false;
            panel4.Visible = false;
            panel5.Visible = true;
            panel6.Visible = false;
        }

        private void label5_Click(object sender, EventArgs e)
        {
            label8.Visible = false;
            label13.Visible = false;
            label14.Visible = false;
            label2.Visible = false;
            label4.Visible = false;
            //label8.BackColor = Color.Black;
            //label13.BackColor = Color.Red;
            //label14.BackColor = Color.Yellow;
            //label15.BackColor = Color.Blue;
            label4.Left = ((Control)sender).Left;
            label4.Width = ((Control)sender).Width;
            bunifuTransition1.ShowSync(label4);

            panel2.Visible = false;
            panel3.Visible = false;
            panel4.Visible = false;
            panel5.Visible = false;
            panel6.Visible = true;
        }

        private void closebutton_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void materialFlatButton1_Click(object sender, EventArgs e)
        {

          Login();

        }
        string stringcon =  System.Configuration.ConfigurationManager.ConnectionStrings["rauhack"].ConnectionString;
        public static int idutilizator;
        public static string codutilizator="";
        private void Login()
        {
            codutilizator = materialSingleLineTextField1.Text;
            string parola = materialSingleLineTextField2.Text;
            int i = 0;
           SqlConnection con = new SqlConnection(stringcon) ;
           SqlCommand cmd = new SqlCommand();
            cmd.Parameters.Clear();
            cmd.Connection = con;
            cmd.CommandText = "SELECT * FROM utilizator where codutilizator=@cod and parola=@parola";
            cmd.Parameters.AddWithValue("@cod", materialSingleLineTextField1.Text);
            cmd.Parameters.AddWithValue("@parola", materialSingleLineTextField2.Text);

            SqlDataAdapter dt = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            DataTable dt1 = new DataTable();

            dt.Fill(dt1);
            dt.Fill(ds);

            int count = ds.Tables[0].Rows.Count;
            if(count == 1)
            {
                if (codutilizator == ds.Tables[i].Rows[i]["codutilizator"].ToString() && parola == ds.Tables[i].Rows[i]["parola"].ToString())
                {
                    foreach (DataRow dr in dt1.Rows)
                    {
                        int a = Convert.ToInt32(dr["tip_utilizator"].ToString());
                        if (a == 0)
                        {
                            pictureBox7.Image = null; pictureBox5.Image = null; pictureBox6.Image = null; pictureBox31.Image = null; pictureBox28.Image = null; pictureBox33.Image = null;
                            pictureBox26.Image = null; pictureBox32.Image = null; pictureBox27.Image = null; pictureBox29.Image = null; pictureBox30.Image = null; pictureBox16.Image = null;
                            pictureBox19.Image = null; pictureBox17.Image = null; pictureBox23.Image = null; pictureBox18.Image = null; pictureBox20.Image = null; pictureBox21.Image = null;
                            pictureBox25.Image = null; pictureBox22.Image = null; pictureBox24.Image = null; pictureBox13.Image = null; pictureBox14.Image = null; pictureBox15.Image = null;
                            pictureBox8.Image = null; pictureBox10.Image = null; pictureBox11.Image = null; pictureBox9.Image = null; pictureBox12.Image = null; pictureBox34.Image = null;
                            pictureBox35.Image = null; pictureBox36.Image = null; pictureBox37.Image = null; pictureBox38.Image = null; pictureBox39.Image = null;
                            GC.Collect();
                            principalform pf = new principalform();
                            this.Hide();
                            pf.Show();
                            idutilizator = Convert.ToInt32(dr["id"].ToString());
                        }
                        else
                        {
                            pictureBox7.Image = null; pictureBox5.Image = null; pictureBox6.Image = null; pictureBox31.Image = null; pictureBox28.Image = null; pictureBox33.Image = null;
                            pictureBox26.Image = null; pictureBox32.Image = null; pictureBox27.Image = null; pictureBox29.Image = null; pictureBox30.Image = null; pictureBox16.Image = null;
                            pictureBox19.Image = null; pictureBox17.Image = null; pictureBox23.Image = null; pictureBox18.Image = null; pictureBox20.Image = null; pictureBox21.Image = null;
                            pictureBox25.Image = null; pictureBox22.Image = null; pictureBox24.Image = null; pictureBox13.Image = null; pictureBox14.Image = null; pictureBox15.Image = null;
                            pictureBox8.Image = null; pictureBox10.Image = null; pictureBox11.Image = null; pictureBox9.Image = null; pictureBox12.Image = null; pictureBox34.Image = null;
                            pictureBox35.Image = null; pictureBox36.Image = null; pictureBox37.Image = null; pictureBox38.Image = null; pictureBox39.Image = null;
                            GC.Collect();
                            admin cc = new admin();
                            this.Hide();
                            cc.Show();
                            idutilizator = Convert.ToInt32(dr["id"].ToString());

                        }
                    }
                }

            }
            else
            {
                label17.Visible = true;
                timer1.Start();
            }

        }
        private void materialSingleLineTextField2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {


                Login();



            }
        }
        public int countererrorseconds = 0;//count for seconds
        private void timer1_Tick(object sender, EventArgs e)
        {
            countererrorseconds = countererrorseconds + 100;
            if (countererrorseconds == 4000)
            {
                label17.Visible = false;
                timer1.Stop();
                countererrorseconds = 0;

            }
            else
                return;
        }

        private void materialSingleLineTextField1_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyData == Keys.Tab)
            {
                materialSingleLineTextField2.Focus();
            }
        }
    }
}
