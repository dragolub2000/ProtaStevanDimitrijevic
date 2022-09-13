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
using System.Windows.Markup;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.Reflection;

namespace SecondProgram
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //DESKTOP-JN7AEUR
            this.LoadView();
        }
        private void LoadView()
        {
            string ComputerName = Environment.MachineName;
            string ConString = @"Data Source=" + ComputerName + ";Initial Catalog=testdatabase;Integrated Security=True";
            SqlConnection con = new SqlConnection(ConString);
            string querystring = "SELECT * FROM Radnik2";
            con.Open();
            SqlCommand cmd = new SqlCommand(querystring, con);
            SqlDataReader reader = cmd.ExecuteReader();
            int i = 0;
            this.listStudents.Items.Clear();
            while (reader.Read())
            {
                this.listStudents.Items.Add(reader["id"].ToString());

                this.listStudents.Items[i].SubItems.Add(reader["Ime"].ToString());
                this.listStudents.Items[i].SubItems.Add(reader["Prezime"].ToString());
                i++;
            }
            con.Close();
        }
        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                string ComputerName = Environment.MachineName;
                string ConString = @"Data Source=" + ComputerName + ";Initial Catalog=testdatabase;Integrated Security=True";
                SqlConnection con = new SqlConnection(ConString);
                string Ime = txtIme.Text;
                String Prezime = txtPrezime.Text;
                string querystring = "INSERT INTO Radnik2 (Ime,Prezime) "
                    + "VALUES (@Ime,@Prezime)";

                con.Open();
                SqlCommand cmd = new SqlCommand(querystring, con);
                cmd.Parameters.AddWithValue("@Ime",Ime);
                cmd.Parameters.AddWithValue("@Prezime", Prezime);
                // izvrsi sql upit
                cmd.ExecuteNonQuery();
                con.Close();
                // prikazi list view opet
                this.LoadView();
                MessageBox.Show("Radnik uspesno unet u bazu podataka!");
            }
            catch (Exception ex) {
                MessageBox.Show("Postoji problem: " + ex.Message); ;
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.LoadView();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.listStudents.SelectedItems.Count == 0)
                    return;

                string id = this.listStudents.SelectedItems[0].Text;
                //MessageBox.Show(id);

                string ComputerName = Environment.MachineName;
                string ConString = @"Data Source=" + ComputerName + ";Initial Catalog=testdatabase;Integrated Security=True";
                SqlConnection con = new SqlConnection(ConString);
                string Ime = txtIme.Text;
                String Prezime = txtPrezime.Text;
                string querystring = "UPDATE Radnik2 SET Ime=@Ime,Prezime=@Prezime WHERE  ID=@id";

                con.Open();
                SqlCommand cmd = new SqlCommand(querystring, con);
                cmd.Parameters.AddWithValue("@Ime", Ime);
                cmd.Parameters.AddWithValue("@Prezime", Prezime);
                cmd.Parameters.AddWithValue("@id", id);
                // izvrsi sql upit
                cmd.ExecuteNonQuery();
                con.Close();
                // prikazi list view opet
                this.LoadView();
                MessageBox.Show("Podaci o radniku uspesno promenjeni u bazi podataka u bazu podataka!");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Postoji problem: " + ex.Message); ;
            }
        }

        private void listStudents_Click(object sender, EventArgs e)
        {
            var item = this.listStudents.SelectedItems[0];
            string Ime = item.SubItems[1].Text;
            string Prezime = item.SubItems[2].Text;
            this.txtIme.Text = Ime;
            this.txtPrezime.Text = Prezime;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.listStudents.SelectedItems.Count == 0)
                    return;

                string id = this.listStudents.SelectedItems[0].Text;

                string ComputerName = Environment.MachineName;
                string ConString = @"Data Source=" + ComputerName + ";Initial Catalog=testdatabase;Integrated Security=True";
                SqlConnection con = new SqlConnection(ConString);
                string querystring = "DELETE FROM Radnik2  WHERE  ID=@id";

                con.Open();
                SqlCommand cmd = new SqlCommand(querystring, con);
                cmd.Parameters.AddWithValue("@id", id);
                // izvrsi sql upit
                cmd.ExecuteNonQuery();
                con.Close();
                // prikazi list view opet
                this.LoadView();
                MessageBox.Show("Podaci o radniku uspesno izbrisani u bazi podataka u bazu podataka!");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Postoji problem: " + ex.Message); ;
            }
        }
    }
}
