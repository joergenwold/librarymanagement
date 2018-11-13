using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LibraryManageSyst
{
    public partial class addNew : Form
    {
       
        public int department;
        public string name = "";
        public string publisher = "";
        public Int64 isbn;
        public string author = "";
        SqlConnection con = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=\"|DataDirectory|\\Database.mdf\";Integrated Security=True");


        public addNew()
        {
            InitializeComponent();
            populateComboBox();
            foreach(var item in listOfDepartments)
            {
                comboBox1.Items.Add(item);
            }
           
        }

        List<string> listOfDepartments = new List<string>();

        private void populateComboBox()
        {
           
            string getDepartments = "SELECT dep_Name FROM Department";
            SqlCommand cmd = new SqlCommand(getDepartments, con);
            con.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            while(reader.Read())
            {
                listOfDepartments.Add(reader.GetString(0));
            }
            reader.Close();
          
        }

        private void button2_Click(object sender, EventArgs e)
        {
            
            if(textBox1.Text != "" && textBox2.Text != "" && numericUpDown1.Value != 0 && textBox4.Text != "" && comboBox1.SelectedItem != null)
            {
                name = textBox1.Text;
                publisher = textBox2.Text;
                isbn = Convert.ToInt64(numericUpDown1.Value);
                author = textBox4.Text;
                department = comboBox1.SelectedIndex;
            }
            this.Close();
        }

        private void label11_Click(object sender, EventArgs e)
        {

        }

        private void addNew_Load(object sender, EventArgs e)
        {

        }
    }
}
