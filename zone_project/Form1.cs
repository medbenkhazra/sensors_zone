using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace zone_project
{
    public partial class Form1 : Form
    {
        public String statut;
        public int yAxis;
        public String parametres = "SERVER=127.0.0.1; DATABASE=zone_project; UID=root; PASSWORD=";
        public int idZone;
        public int currRowIndex;
        public int selectedId;
        public int yAxis1;
        public int yAxis2;
        public int yAxis3;
        public int yAxis4;
        public Form1()
        {
            InitializeComponent();
            loadZones();
            load();
            loadStatut();
            loadCapteurs();
            colorRows();
            appel3abiit();
          /*  DataGridViewButtonColumn col = new DataGridViewButtonColumn();
            col.UseColumnTextForButtonValue = true;
            col.Text = "ADD";
            col.Name = "statut";
            dataGridView1.Columns.Add(col);*/
        }
        protected override void OnLoad(EventArgs e)
        {
            // do stuff before Load-event is raised
            base.OnLoad(e);
            colorRows();
            // do stuff after Load-event was raised
        }
        public void appel3abiit()
        {
            colorRows();
        }

        public void colorRows()
        {

            /*  string a = dataGridView1.Rows[0].Cells[2].Value.ToString();
              foreach (DataGridViewRow Myrow in dataGridView1.Rows)
              {
                  if (a == "Luzon")//"))//Columns.Contains("SOLD"))//Columns.Contains("SOLD"))
                  {
                      dataGridView1.DefaultCellStyle.BackColor = Color.Red;

                  }
              }*/




            Console.WriteLine(this.dataGridView1.Rows.Count);
          for (int i=0;i< this.dataGridView1.Rows.Count-1;i++)
              {
                //  DataGridViewRow row = this.dataGridView1.Rows[i];
                //  Console.WriteLine(row.Cells["statut2"].Value.ToString());
                Console.WriteLine(i);
                Console.WriteLine(dataGridView1.Rows[i].Cells[3].Value.ToString());
                  if (dataGridView1.Rows[i].Cells[3].Value.ToString() == "deconnecte")
                  {
                    Console.WriteLine("d5ul l decoonne");
                    dataGridView1.Rows[i].Cells[3].Style.BackColor = Color.Red;
                  }
                  else
                  {
                    dataGridView1.Rows[i].Cells[3].Style.BackColor = Color.Green;
                  }

                
              /*  Console.WriteLine(dataGridView1.Rows[i].Cells[4].Value.ToString());
                if (dataGridView1.Rows[i].Cells[4].Value.ToString() == "eteindre")
                {
                    Console.WriteLine("d5ul l eteindre");
                    dataGridView1.Rows[i].Cells[4].Style.BackColor = Color.Red;
                }
                else
                {
                    dataGridView1.Rows[i].Cells[4].Style.BackColor = Color.Green;
                }*/

            }


        }

        public void loadCapteurs()
        {
            MySqlConnection connection = new MySqlConnection(parametres);
            connection.Open();
            MySqlCommand cmd = connection.CreateCommand();
            cmd.CommandText = "SELECT * FROM capteurs INNER JOIN zone ON capteurs.idZone=zone.id;";
            MySqlDataReader reader;
            reader = cmd.ExecuteReader();
            //  List<String> capteurs = new List<string>();
            Dictionary<String, String> capteurs = new Dictionary<string, string>();
            while (reader.Read())
            {
                String nom = reader.GetString("nom");
                String zone = reader.GetString(7);

                capteurs.Add(nom,zone);

            }
            reader.Close();

            connection.Close();

            foreach (KeyValuePair<String,String> dic in capteurs)
            {
                MySqlConnection connection2 = new MySqlConnection(parametres);
                connection2.Open();
                MySqlCommand cmd2 = connection2.CreateCommand();
                cmd2.CommandText = "SELECT * FROM capteurs where nom=@nom";
                cmd2.Parameters.AddWithValue("@nom",dic.Key);
                MySqlDataReader reader2;
                reader2 = cmd2.ExecuteReader();
                
                while (reader2.Read())
                {
                    statut = reader2.GetString("statut");
                    Console.WriteLine("****haa statut ****");
                    Console.WriteLine(statut);

                    

                }



                Button b = new Button();
                b.Width = 30;
                b.Height = 20;
                yAxis += 15;
                b.Location = new Point(0, yAxis);
                if (statut == "deconnecte")
                {
                    Console.WriteLine("d5ul l etei");
                    b.BackColor = Color.Red;
                }
                else
                {
                    Console.WriteLine("d5ul l allumer");
                    b.BackColor = Color.Green;
                }

                

                ControlExtension.Draggable(b, true);


                if (dic.Value=="zone 1")
                {
                    zone1.Controls.Add(b);

                }else if (dic.Value=="zone 2")
                {
                    zone2.Controls.Add(b);
                }
                else if (dic.Value == "zone 3")
                {
                    zone3.Controls.Add(b);
                }
                else if (dic.Value == "zone 4")
                {
                    zone4.Controls.Add(b);
                }
                else
                {
                    Console.WriteLine("pas de capteurs");
                }
            }

            reader.Close();
            

        }

        public void loadStatut()
        {
            comboBox2.Items.Add("deconnecte");
            comboBox2.Items.Add("connecte");


            comboBox3.Items.Add("eteindre");
            comboBox3.Items.Add("allumer");
        }
        public void loadZones()
        {
            MySqlConnection connection = new MySqlConnection(parametres);
            connection.Open();
            MySqlCommand cmd = connection.CreateCommand();
            cmd.CommandText = "select * from zone";
            MySqlDataReader reader;
            reader = cmd.ExecuteReader();
            List<String> zones = new List<string>();
            while (reader.Read())
            {
                String zone = reader.GetString("nom");
               
                zones.Add(zone);

            }
            foreach (String z in zones)
            {
                comboBox1.Items.Add(z);
            }
            
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }
        public void load()
        {
            DataTable data = new DataTable();

            MySqlConnection connection = new MySqlConnection(parametres);
            connection.Open();

            String request = "select * from capteurs";
            MySqlCommand cmd = new MySqlCommand(request, connection);

            MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);

            adapter.Fill(data);

            dataGridView1.DataSource = data;
            connection.Close();
/*
            MySqlCommand cmd1 = new MySqlCommand(request, connection);
            MySqlDataReader reader;
            reader = cmd1.ExecuteReader();
            // int idZone;
            while (reader.Read())
            {
                String statut = reader.GetString("statut");
                String statut2 = reader.GetString("statut2");
            }
            reader.Close();
            */

        }

        private void button2_Click(object sender, EventArgs e)
        {


            String nom = textBox1.Text;
            String description = textBox2.Text;
            String zone = comboBox1.SelectedItem.ToString();
           

            MySqlConnection connection = new MySqlConnection(parametres);
            connection.Open();

            MySqlCommand cmd1 = connection.CreateCommand();
            cmd1.CommandText = "select * from zone where nom=@zone ";
            Console.WriteLine(zone);
            cmd1.Parameters.AddWithValue("@zone", zone);
            
            MySqlDataReader reader;
            reader = cmd1.ExecuteReader();
           // int idZone;
            while (reader.Read())
            {
                idZone = reader.GetInt32("id");
            }
            reader.Close();

            MySqlCommand cmd2 = connection.CreateCommand();
            cmd2.CommandText = "insert into capteurs values(null, @nom, @description,@statut,@statut2,@idZone)";
            cmd2.Parameters.AddWithValue("@nom", nom);
            cmd2.Parameters.AddWithValue("@description", description);
            cmd2.Parameters.AddWithValue("@statut", comboBox2.SelectedItem.ToString());
            cmd2.Parameters.AddWithValue("@statut2",comboBox3.SelectedItem.ToString());
            cmd2.Parameters.AddWithValue("@idZone", idZone);
            cmd2.ExecuteNonQuery();
            connection.Close();
            textBox1.Clear();
            textBox2.Clear();
         
            load();
            Button b = new Button();
            b.Width = 30;
            b.Height = 20;
            if (comboBox2.SelectedItem=="deconnecte")
            {
                b.BackColor = Color.Red;
            }
            else
            {
                b.BackColor = Color.Green;
            }
            
            ControlExtension.Draggable(b,true);
            Console.WriteLine("********");
            Console.WriteLine(comboBox1.SelectedItem);
           
            //   zone1.Controls.Add(b);
            if (comboBox1.SelectedItem.ToString()=="zone 1")
            {
                Console.WriteLine("d5uuul l 1");
                
                yAxis1 += 11;
                b.Location = new Point(0, yAxis1);
                zone1.Controls.Add(b);
            }else if(comboBox1.SelectedItem.ToString() == "zone 2")
            {
                Console.WriteLine("d5uuul l 2");
                
                yAxis2 += 11;
                b.Location = new Point(0, yAxis2);
                zone2.Controls.Add(b);
            }
            else if (comboBox1.SelectedItem.ToString() == "zone 3")
            {
                
                yAxis3 += 11;
                b.Location = new Point(0, yAxis3);
                Console.WriteLine("d5uuul l 3");
                zone3.Controls.Add(b);
            }
            else if (comboBox1.SelectedItem.ToString() == "zone 4")
            {
                Console.WriteLine("d5uuul l 4");
                
                yAxis4 += 11;
                b.Location = new Point(0, yAxis4);
                zone4.Controls.Add(b);
            }
            else
            {
                Console.WriteLine("maaakaynch !!!!");
                MessageBox.Show("cette zone n'existe pas ");
            }

            colorRows();
        }

        private void zone1_Enter(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewRow row = this.dataGridView1.Rows[e.RowIndex];
            currRowIndex = Convert.ToInt32(row.Cells[0].Value);
            selectedId = int.Parse(row.Cells[0].Value.ToString());
            textBox1.Text = row.Cells["nom"].Value.ToString();
            textBox2.Text = row.Cells["description"].Value.ToString();
          //  comboBox1.SelectedItem = row.Cells["statut"].Value.ToString();
            comboBox2.SelectedItem= row.Cells["statut"].Value.ToString();
            comboBox3.SelectedItem = row.Cells["statut2"].Value.ToString();

            Console.WriteLine("*********");
            Console.WriteLine(row.Cells["statut"].Value.ToString());
            Console.WriteLine(row.Cells["statut2"].Value.ToString());
            if (row.Cells["idZone"].Value.ToString()== "1")
            {
                Console.WriteLine("d5ul l 1");
                comboBox1.SelectedItem = "zone 1";
            }else if (row.Cells["idZone"].Value.ToString() == "2")
            {
                comboBox1.SelectedItem = "zone 2";
            }
            else if (row.Cells["idZone"].Value.ToString() == "3")
            {
                comboBox1.SelectedItem = "zone 3";
            }
            else if (row.Cells["idZone"].Value.ToString() == "4")
            {
                comboBox1.SelectedItem = "zone 4";
            }
            else
            {
                comboBox1.SelectedItem = "zone 5";
            }





            //  textBox3.Text = row.Cells["statut"].Value.ToString();
            //  textBox4.Text = row.Cells["statut2"].Value.ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            String nom = textBox1.Text;
            String description = textBox2.Text;
            String statut = comboBox2.SelectedItem.ToString();
            String statut2 = comboBox3.SelectedItem.ToString();
            String idZone;
            if (comboBox1.SelectedItem.ToString()=="zone 1")
            {
                idZone = "1";
            }else if(comboBox1.SelectedItem.ToString() == "zone 2")
            {
                idZone = "2";
            }
            else if (comboBox1.SelectedItem.ToString() == "zone 3")
            {
                idZone = "3";
            }
            else if (comboBox1.SelectedItem.ToString() == "zone 4")
            {
                idZone = "4";
            }
            else 
            {
                idZone = "5";
            }


            MySqlConnection connection = new MySqlConnection(parametres);
            connection.Open();
            MySqlCommand cmd = connection.CreateCommand();
            cmd.CommandText = "update capteurs set nom=@nom,description=@description,statut=@statut,statut2=@statut2,idZone=@idZone WHERE id=@id";
            cmd.Parameters.AddWithValue("@nom", nom);
            cmd.Parameters.AddWithValue("@description", description);
            cmd.Parameters.AddWithValue("@statut", statut);
            cmd.Parameters.AddWithValue("@statut2", statut2);
            cmd.Parameters.AddWithValue("@idZone", idZone);
            cmd.Parameters.AddWithValue("@id", selectedId);

            cmd.ExecuteNonQuery();

            connection.Close();
            textBox1.Clear();
            textBox2.Clear();
            load();
            /*
                        Console.WriteLine("****avant***");
                        Console.WriteLine(zone1.Controls.Count);
                        Console.WriteLine(zone2.Controls.Count);
                        Console.WriteLine(zone3.Controls.Count);
                        Console.WriteLine(zone4.Controls.Count);

                        Console.WriteLine("****apres***");
                        foreach (Control c in zone1.Controls) zone1.Controls.Remove(c);
                        Console.WriteLine(zone1.Controls.Count);

                        foreach (Control c in zone1.Controls) zone1.Controls.Remove(c);
                        Console.WriteLine(zone1.Controls.Count);
                        foreach (Control c in zone2.Controls) zone2.Controls.Remove(c);
                        Console.WriteLine(zone2.Controls.Count);
                        foreach (Control c in zone3.Controls) zone3.Controls.Remove(c);
                        Console.WriteLine(zone3.Controls.Count);
                        foreach (Control c in zone4.Controls) zone4.Controls.Remove(c);
                        Console.WriteLine(zone4.Controls.Count);*/
            /*    for (int i=0;i<zone1.Controls.Count-1;i++)
                {
        zone1.Controls.Remove(zone1.Controls[i]);
                }
    for (int i = 0; i < zone2.Controls.Count - 1; i++)
    {
        zone2.Controls.Remove(zone2.Controls[i]);
    }
    for (int i = 0; i < zone3.Controls.Count - 1; i++)
    {
        zone3.Controls.Remove(zone3.Controls[i]);
    }
    for (int i = 0; i < zone4.Controls.Count - 1; i++)
    {
        zone4.Controls.Remove(zone4.Controls[i]);
    }*/
          //  zone1.Controls.Clear();
            //zone2.Controls.Clear();
            //zone3.Controls.Clear();
            //zone4.Controls.Clear();

            loadCapteurs();
            colorRows();
            
            this.Invalidate();
          //  this.Refresh();

        }

        private void button3_Click(object sender, EventArgs e)
        {
            int rowIndex = dataGridView1.CurrentCell.RowIndex;

            DialogResult dialogDelete = MessageBox.Show("voulez-vous vraiment supprimer ce capteur", "Supprimer un client", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
            if (dialogDelete == DialogResult.OK)
            {
                dataGridView1.Rows.RemoveAt(rowIndex);

                MySqlConnection connection = new MySqlConnection(parametres);
                connection.Open();
                MySqlCommand cmd = connection.CreateCommand();
                cmd.CommandText = "DELETE FROM capteurs WHERE id=" + currRowIndex;
                cmd.ExecuteNonQuery();
                connection.Close();
                MessageBox.Show("Capteur supprimé");
                textBox1.Clear();
                textBox2.Clear();
                
                load();
                /*
                Console.WriteLine("****avant***");
                Console.WriteLine(zone1.Controls.Count);
                Console.WriteLine(zone2.Controls.Count);
                Console.WriteLine(zone3.Controls.Count);
                Console.WriteLine(zone4.Controls.Count);

                Console.WriteLine("****apres***");
                foreach (Control c in zone1.Controls) zone1.Controls.Remove(c);
                Console.WriteLine(zone1.Controls.Count);

                foreach (Control c in zone1.Controls) zone1.Controls.Remove(c);
                Console.WriteLine(zone1.Controls.Count);
                foreach (Control c in zone2.Controls) zone2.Controls.Remove(c);
                Console.WriteLine(zone2.Controls.Count);
                foreach (Control c in zone3.Controls) zone3.Controls.Remove(c);
                Console.WriteLine(zone3.Controls.Count);
                foreach (Control c in zone4.Controls) zone4.Controls.Remove(c);
                Console.WriteLine(zone4.Controls.Count);
                */
                loadCapteurs();

            }
            colorRows();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            loadCapteurs();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
