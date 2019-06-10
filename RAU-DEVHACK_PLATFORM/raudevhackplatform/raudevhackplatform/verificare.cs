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
using System.IO;

namespace raudevhackplatform
{
    public partial class verificare : Form
    {
        public verificare()
        {
            InitializeComponent();
            LoadData();
        }
        protected override void OnPaintBackground(PaintEventArgs e)
        {
            using (SolidBrush brush = new SolidBrush(Color.FromArgb(80, 0, 0, 0)))
            {
                e.Graphics.FillRectangle(brush, e.ClipRectangle);
            }
            GC.Collect();
        }

        private void materialSingleLineTextField1_KeyPress(object sender, KeyPressEventArgs e)
        {
            char notaobtinuta = e.KeyChar;
            if (!Char.IsDigit(notaobtinuta) && notaobtinuta != 8 && notaobtinuta != 46)
            { e.Handled = true; }
        }

        private void materialFlatButton2_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(stringcon);
            con.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = "update proba_a set punctaj1=@punctaj1, punctaj2=@punctaj2, punctaj3=@punctaj3, punctaj4=@punctaj4, mentiuni=@mentiune, trimis='1',id_verificator=@idd,nume_verificator=(select numecomplet from utilizator where id=@idd) where id_utilizator=@id";
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("@punctaj1", materialSingleLineTextField1.Text);
            cmd.Parameters.AddWithValue("@punctaj2", materialSingleLineTextField2.Text);
            cmd.Parameters.AddWithValue("@punctaj3", materialSingleLineTextField3.Text);
            cmd.Parameters.AddWithValue("@punctaj4", materialSingleLineTextField4.Text);
            cmd.Parameters.AddWithValue("@mentiune", richTextBox1.Text);
            cmd.Parameters.AddWithValue("@idd", Form1.idutilizator);
            cmd.Parameters.AddWithValue("@id", admin.id);
            cmd.ExecuteNonQuery();




            this.Close();
        }
        string stringcon = System.Configuration.ConfigurationManager.ConnectionStrings["rauhack"].ConnectionString;

        private void LoadData()
        {
            SqlConnection con = new SqlConnection(stringcon);
            con.Open();

            SqlCommand cmd = new SqlCommand();

            cmd.Connection = con;
            cmd.CommandText = "select proba_a.id,utilizator.id,utilizator.codutilizator,utilizator.numecomplet,utilizator.liceu,utilizator.limbaj_programare,proba_a.ora_incarcare from proba_a inner join utilizator on utilizator.id = proba_a.id_utilizator where utilizator.id = @id";
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("@id", admin.id);
            cmd.ExecuteNonQuery();

            DataTable dt = new DataTable();
            DataTable dt1 = new DataTable();
            SqlDataAdapter da1 = new SqlDataAdapter(cmd);

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da1.Fill(ds);
            da.Fill(dt);



            foreach (DataRow dr in dt.Rows)
            {


                label5.Text = dr["codutilizator"].ToString();
                label6.Text = dr["numecomplet"].ToString();
                label7.Text = dr["liceu"].ToString();

                label8.Text = dr["limbaj_programare"].ToString();
                label9.Text = Convert.ToString(dr["ora_incarcare"]).ToString();
              

            }
        }

        private void materialFlatButton1_Click(object sender, EventArgs e)
        {
            

                SaveFileDialog saveFileDialog1 = new SaveFileDialog();
                saveFileDialog1.Filter = "RAR (*.rar*)|*.rar*";
                saveFileDialog1.FileName = label6.Text+ ".rar";
                saveFileDialog1.Title = "Salvare fisier..";
                saveFileDialog1.ShowDialog();

                string cnStr = stringcon;
                string sql = "select fisier from proba_A where id_utilizator="+admin.id+"";
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
    }
}
