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
using MySql.Data.MySqlClient;
using System.Threading;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.IO;

namespace raudevhackplatform
{
    public partial class admin : Form
    {
        public admin()
        {
            InitializeComponent();
            AdminCh();
            //this.bunifuCustomDataGrid1.ScrollBars = ScrollBars.None;
            //this.bunifuCustomDataGrid1.MouseWheel += new MouseEventHandler(bunifuCustomGrid1_mousewheel);
            GridView();
           





            Date();
            SwitchButt();
            
        }

        

        //private void bunifuCustomGrid1_mousewheel(object sender, MouseEventArgs e)
        //{
        //    if (e.Delta > 0 && bunifuCustomDataGrid1.FirstDisplayedScrollingRowIndex > 0)
        //    {
        //        bunifuCustomDataGrid1.FirstDisplayedScrollingRowIndex--;
        //    }
        //    else if (e.Delta < 0)
        //    {
        //        bunifuCustomDataGrid1.FirstDisplayedScrollingRowIndex++;
        //    }
        //}

        private void closebutton_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void bunifuiOSSwitch10_OnValueChange(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(stringcon);
            SqlCommand cmd = new SqlCommand();


            if (bunifuiOSSwitch10.Value==false)
            {
                cmd.Connection = con;
                cmd.CommandText = "update optiuni set validare_sec_a = 0";
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
            else
            {
                cmd.Connection = con;
                cmd.CommandText = "update optiuni set validare_sec_a = 1";
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }

        private void bunifuiOSSwitch1_OnValueChange(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(stringcon);
            SqlCommand cmd = new SqlCommand();

            if (bunifuiOSSwitch1.Value == false)
            {
                cmd.Connection = con;
                cmd.CommandText = "update optiuni set validare_sec_b = 0";
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();

                
            }
            else
            {
                cmd.Connection = con;
                cmd.CommandText = "update optiuni set validare_sec_b = 1";
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }
        string stringcon = System.Configuration.ConfigurationManager.ConnectionStrings["rauhack"].ConnectionString;

        private void SwitchButt()
        {
            SqlConnection con = new SqlConnection(stringcon);
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.Parameters.Clear();
            con.Open();

            cmd.CommandText = "SELECT * FROM optiuni";
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
                int valoare = Convert.ToInt32(dr["validare_sec_a"].ToString());
                int valoareb = Convert.ToInt32(dr["validare_sec_b"].ToString());
                if (valoare == 1)
                {
                    bunifuiOSSwitch10.Value = true;
                }
                else { bunifuiOSSwitch10.Value = false; }

                if (valoareb == 1)
                {
                    bunifuiOSSwitch1.Value = true;
                }
                else { bunifuiOSSwitch1.Value = false; }

            }
            con.Close();
        }




        private void Date()
        {

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
                
            }
            con.Close();

        }

        private void GridView()
        {
            SqlConnection con = new SqlConnection(stringcon);
            
            con.Open();
            SqlDataAdapter MyDA = new SqlDataAdapter();
            string sqlSelectAll = "select proba_a.id,utilizator.id AS ID_CON,utilizator.numecomplet,utilizator.liceu,utilizator.limbaj_programare,proba_a.ora_incarcare from proba_a inner join utilizator on utilizator.id = proba_a.id_utilizator where proba_a.trimis = 1 and proba_a.verificat = 0 ";
            MyDA.SelectCommand = new SqlCommand(sqlSelectAll, con);

            DataTable table = new DataTable();
            MyDA.Fill(table);

            BindingSource bSource = new BindingSource();
            bSource.DataSource = table;


            bunifuCustomDataGrid1.DataSource = bSource;
            con.Close();

           

            //--------------------------------------

            //store autosized widths
            //int colw = bunifuCustomDataGrid1.Columns[0].Width;
            //remove autosizing
            bunifuCustomDataGrid1.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            //set width to calculated by autosize
            bunifuCustomDataGrid1.Columns[0].Width = 50;
            bunifuCustomDataGrid1.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            //set width to calculated by autosize
            bunifuCustomDataGrid1.Columns[1].Width = 100;
            bunifuCustomDataGrid1.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            //set width to calculated by autosize
            bunifuCustomDataGrid1.Columns[2].Width = 365;
            bunifuCustomDataGrid1.Columns[3].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            //set width to calculated by autosize
            bunifuCustomDataGrid1.Columns[3].Width = 365;
            bunifuCustomDataGrid1.Columns[4].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            //set width to calculated by autosize
            bunifuCustomDataGrid1.Columns[4].Width = 60;
            bunifuCustomDataGrid1.Columns[5].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            //set width to calculated by autosize
            bunifuCustomDataGrid1.Columns[5].Width = 120;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            GridView();

            //bunifuCustomDataGrid1.Refresh();
            //bunifuCustomDataGrid1.Update();

        }

        private void bunifuCustomDataGrid1_MouseHover(object sender, EventArgs e)
        {
            bunifuCustomDataGrid1.Focus();
        }

        private void bunifuCustomDataGrid1_MouseMove(object sender, MouseEventArgs e)
        {
            DataGridView.HitTestInfo hit = bunifuCustomDataGrid1.HitTest(e.X, e.Y);
            if (hit.Type == DataGridViewHitTestType.Cell)
            {
                bunifuCustomDataGrid1.Rows[hit.RowIndex].Selected = true;
            }
        }
        public static int id;

        private void bunifuCustomDataGrid1_CellClick(object sender, DataGridViewCellEventArgs e)
        {


            id = Convert.ToInt32(bunifuCustomDataGrid1.Rows[e.RowIndex].Cells["ID_CON"].Value.ToString());

            SqlConnection con = new SqlConnection(stringcon);
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.Parameters.Clear();
            con.Open();

            cmd.CommandText = "update proba_a set verificat=1 where id_utilizator=" + id + "";

            
            cmd.ExecuteNonQuery();
            con.Close();


            verificare vf = new verificare();
            vf.ShowDialog();
        }
        private void AdminCh()
        {

            SqlConnection con = new SqlConnection(stringcon);
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.Parameters.Clear();
            con.Open();

            cmd.CommandText = "SELECT * FROM utilizator where codutilizator=@cod";
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
                string count = dr["codutilizator"].ToString();

                if (count == "aiuli" || count == "afishi")
                {
                    label344.Visible = true;
                    bunifuiOSSwitch10.Visible = true;
                    label2.Visible = true;

                    label4.Visible = true;
                    bunifuiOSSwitch1.Visible = true;
                    label3.Visible = true;
                }
                else
                {
                    label344.Visible = false;
                    bunifuiOSSwitch10.Visible = false;
                    label2.Visible = false;

                    label4.Visible = false;
                    bunifuiOSSwitch1.Visible = false;
                    label3.Visible = false;
                }
            }
            con.Close();
        }
        private void GridViewPDF()
        {
            SqlConnection con = new SqlConnection(stringcon);

            con.Open();
            SqlDataAdapter MyDA = new SqlDataAdapter();
            string sqlSelectAll = "select utilizator.numecomplet as [NUME CONCURENT],utilizator.codutilizator as [COD CONCURENT],utilizator.liceu as [PROVENIENŢĂ LICEU], proba_a.punctaj1 as [PUNCTAJ PROBLEMĂ 1],proba_a.punctaj2 as [PUNCTAJ PROBLEMĂ 2],proba_a.punctaj3 as [PUNCTAJ PROBLEMĂ 3],proba_a.punctaj4 as [PUNCTAJ PROBLEMĂ 4],proba_b.punctaj as [PUNCTAJ GRILĂ], proba_a.punctaj1+proba_a.punctaj2+proba_a.punctaj3+proba_a.punctaj4+proba_b.punctaj  as [PUNCTAJ TOTAL], proba_a.mentiuni as MENŢIUNE from proba_a inner join utilizator on utilizator.id = proba_a.id_utilizator inner join proba_b on proba_b.id_utilizator = utilizator.id order by [PUNCTAJ TOTAL] desc";
            MyDA.SelectCommand = new SqlCommand(sqlSelectAll, con);

            DataTable table = new DataTable();
            MyDA.Fill(table);

            BindingSource bSource = new BindingSource();
            bSource.DataSource = table;


            bunifuCustomDataGrid2.DataSource = bSource;
            con.Close();

        }

        private void GridViewPDFVerificator()
        {
            SqlConnection con = new SqlConnection(stringcon);

            con.Open();
            SqlDataAdapter MyDA = new SqlDataAdapter();
            string sqlSelectAll = "select  utilizator.numecomplet [NUME PARTICIPANT], utilizator.codutilizator as [COD PARTICIPANT], proba_a.nume_verificator AS [NUME CORECTOR],proba_a.mentiuni [MENŢIUNIE] from proba_a inner join utilizator on proba_a.id_utilizator = utilizator.id";
            MyDA.SelectCommand = new SqlCommand(sqlSelectAll, con);

            DataTable table = new DataTable();
            MyDA.Fill(table);

            BindingSource bSource = new BindingSource();
            bSource.DataSource = table;


            bunifuCustomDataGrid2.DataSource = bSource;
            con.Close();

        }

        public void exportgridpdf(DataGridView dd,string filename)
        {

            Paragraph PDate = new Paragraph(DateTime.UtcNow.ToShortDateString(), FontFactory.GetFont(FontFactory.TIMES, 10, iTextSharp.text.Font.NORMAL));

            //adding image (logo)

            string imageURL = "C\\Users\\Iuli\\Desktop\\logo.jpg";//HttpContext.Current.Server.MapPath("~/Content/images.jpg");
            iTextSharp.text.Image jpg = iTextSharp.text.Image.GetInstance(imageURL);
            //Resize image depend upon your need
            jpg.ScaleToFit(70f, 120f);
            //Give space before image
            //jpg.SpacingBefore = 10f;
            //Give some space after the image
            jpg.SpacingAfter = 1f;
            jpg.Alignment = Element.ALIGN_TOP;


            PDate.Alignment = Element.ALIGN_TOP;

            PdfPTable headerTbl = new PdfPTable(3);

            headerTbl.TotalWidth = 600;

            headerTbl.HorizontalAlignment = Element.ALIGN_TOP;

            PdfPCell cell3 = new PdfPCell(PDate);
            PdfPCell cell2 = new PdfPCell(jpg);
            cell2.Border = 0;
            cell3.Border = 0;

            cell3.PaddingLeft = 10;
            cell2.PaddingLeft = 10;
            headerTbl.AddCell(cell3);
            headerTbl.AddCell(cell2);






            BaseFont bf = BaseFont.CreateFont(BaseFont.TIMES_ROMAN, BaseFont.CP1250,BaseFont.EMBEDDED);
            PdfPTable pdftable = new PdfPTable(dd.Columns.Count);
            pdftable.DefaultCell.Padding = 3;
            pdftable.WidthPercentage = 100;
            pdftable.HorizontalAlignment = Element.ALIGN_LEFT;
            pdftable.DefaultCell.BorderWidth = 1;

            //header
            iTextSharp.text.Font text = new iTextSharp.text.Font(bf, 10, iTextSharp.text.Font.NORMAL);
            foreach(DataGridViewColumn col in dd.Columns)
            {
                PdfPCell cell = new PdfPCell(new Phrase(col.HeaderText, text));
                cell.BackgroundColor = new iTextSharp.text.BaseColor(240, 240, 240);
                pdftable.AddCell(cell);


            }
            foreach(DataGridViewRow row in dd.Rows)
            {
                foreach(DataGridViewCell cell in row.Cells)
                {
                    pdftable.AddCell(new Phrase(cell.Value.ToString(), text));
                }
            }

            var savefiledialog = new SaveFileDialog();
            savefiledialog.FileName = filename;
            savefiledialog.DefaultExt = ".pdf";
            if (savefiledialog.ShowDialog() == DialogResult.OK)
            {
                using (FileStream stream = new FileStream(savefiledialog.FileName,FileMode.Create))
                {
                    Document pdfdoc = new Document(PageSize.A3,10f,10f,10f,0f);
                    PdfWriter.GetInstance(pdfdoc, stream);
                    pdfdoc.Open();
                    pdfdoc.Add(pdftable);
                    pdfdoc.Close();
                    stream.Close();

                }
            }

        }
        private void materialFlatButton1_Click(object sender, EventArgs e)
        {
            GridViewPDF();
            exportgridpdf(bunifuCustomDataGrid2, "CLASAMENT_JUNIOR_RAU-DEVHACK");
        }

        private void materialRaisedButton1_Click(object sender, EventArgs e)
        {
            GridViewPDF();
            exportgridpdf(bunifuCustomDataGrid2, "CLASAMENT_JUNIOR_RAU-DEVHACK");
        }

        private void materialRaisedButton2_Click(object sender, EventArgs e)
        {
            GridViewPDFVerificator();
            exportgridpdf(bunifuCustomDataGrid2, "CORECTORI_JUNIOR_RAU-DEVHACK");
        }
    }
}
