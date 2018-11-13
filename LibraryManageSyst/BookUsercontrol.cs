using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace LibraryManageSyst
{ 
    public partial class BookUsercontrol : UserControl
    {
        private static BookUsercontrol _instance;

        public static BookUsercontrol theInstance
        {
            get
            {
                if(_instance == null)
                {
                    _instance = new BookUsercontrol();
                }
                return _instance;
            }
        }

        public BookUsercontrol()
        {
            InitializeComponent();
        }


        SqlConnection con = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=\"|DataDirectory|\\Database.mdf\";Integrated Security=True");


        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            addNew addNew = new addNew();

            string insertBookDetails = "INSERT INTO book_details(isbn,name, author,publisher) VALUES(@isbn,@name,@author,@publisher)";
            string insertIntoDepartment = "INSERT INTO book(department_id) VALUES(@department)";


            addNew.ShowDialog();
            if (addNew.DialogResult == DialogResult.OK)
            {
                try
                {

                    con.Open();
                    SqlCommand cmd = new SqlCommand(insertBookDetails, con);

                    cmd.Parameters.AddWithValue("@isbn", addNew.isbn);
                    cmd.Parameters.AddWithValue("@name", addNew.name);
                    cmd.Parameters.AddWithValue("@author", addNew.author);
                    cmd.Parameters.AddWithValue("@publisher", addNew.publisher);
                   
                    cmd.ExecuteNonQuery();

                    con.Close();

                    insertBook(addNew.department);

                    insertAuthor(addNew.author);

                    refreshTheDataGrid();


                }
                catch (Exception ex)
                {
                    MessageBox.Show("Invalid Sql operation" + ex);
                    con.Close();
                }

               
            }
           
        }

        private void insertAuthor(string author)
        {
            string insertAuthor = "INSERT INTO author(name) VALUES (@name)";

            SqlCommand cmd = new SqlCommand(insertAuthor, con);

            try
            {
                con.Open();

                cmd.Parameters.AddWithValue("@name", author);

                cmd.ExecuteNonQuery();

                con.Close();

            }
            catch (Exception ex)
            {
                MessageBox.Show("Invalid Sql operation" + ex);
                con.Close();
            }
            
        }



        private void insertBook(int department)
        {
            string insertBookDepartment = "INSERT INTO books(department_Id) VALUES(@department_Id)";
            //string selectLast = "SELECT IDENT_CURRENT(books_details)";

            SqlCommand cmd = new SqlCommand(insertBookDepartment, con);
            
            try
            {
                con.Open();

                cmd.Parameters.AddWithValue("@department_Id", department);

                cmd.ExecuteNonQuery();

                con.Close();

            }
            catch (Exception ex)
            {
                MessageBox.Show("Invalid Sql operation" + ex);
                con.Close();
            }
            

        }

        public void refreshTheDataGrid()
        {
            try
            {
                string list = "SELECT * FROM book_details";
                string temp = "SELECT department_name FROM department d, book b WHERE d.department_id = b.department_id";
                SqlCommand cmd = new SqlCommand(list, con);
                // SqlCommand cmd1 = new SqlCommand(temp, con);

                con.Open();
                cmd.ExecuteNonQuery();

                SqlDataAdapter dataAdapt = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                DataSet dataSet = new DataSet();
                dataAdapt.Fill(dataSet);

              
                dataGridView1.DataSource = dataSet.Tables[0];
                this.dataGridView1.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
                this.dataGridView1.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                this.dataGridView1.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                this.dataGridView1.Columns[3].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                //this.dataGridView1.Columns[4].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                //this.dataGridView1.Columns[5].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

                con.Close();

            }
            catch (Exception ex)
            {
                MessageBox.Show("" + ex);
                
            }
        }

        private void BookUsercontrol_Load(object sender, EventArgs e)
        {
            refreshTheDataGrid();
        }
    }
}
