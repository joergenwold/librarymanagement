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

namespace LibraryManageSyst
{
    public partial class Form1 : Form
    {
        string userName = "";
        string password = "";


        public Form1()
        {
            InitializeComponent();
        }

        SqlConnection connection = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=\"|DataDirectory|\\Database.mdf\";Integrated Security=True");
        SqlCommand cmd;
        SqlDataReader DataRead;

        private string getUserName()
        {
            //fetches data from the database
            connection.Open();
            string syntax = "SELECT value FROM users WHERE property = 'username' ";
            cmd = new SqlCommand(syntax, connection);
            DataRead = cmd.ExecuteReader();
            DataRead.Read();
            string temp = DataRead[0].ToString();
            connection.Close();
            return temp;

        }

        private string getPassword()
        {
            //fetches data from the database
            connection.Open();
            string query = "SELECT value FROM users WHERE property = 'password' ";
            cmd = new SqlCommand(query, connection);
            DataRead = cmd.ExecuteReader();
            DataRead.Read();
            string temp = DataRead[0].ToString();
            connection.Close();
            return temp;

        }



        private void button1_Click(object sender, EventArgs e)
        {
            
            userName = getUserName();
            password = getPassword();
            string name = textBox1.Text;
            string pass = textBox2.Text;

            if(name.Equals(userName) && pass.Equals(password))
            {
                AppUI appUI = new AppUI();
                this.Hide();
                appUI.Show();
                fillWithDummyData();
            }
            else
            {
                MessageBox.Show("Wrong name or password!");
            }
       
        }


        private void fillWithDummyData()
        {
            //BOOK1
            insertBookDepart(0);
            insertBookDetails(9788202459772, "Harry Potter og de vises stein", "JK ROWLING", "CAPPELEN DAMM");

            //BOOK2
            insertBookDepart(0);
            insertBookDetails(9788202597721, "Harry Potter og mysteriekammeret", "JK ROWLING", "CAPPELEN DAMM");

            //BOOK3
            insertBookDepart(1);
            insertBookDetails(9788202476007, "Harry Potter og fangen fra Azkaban", "JK ROWLING", "CAPPELEN DAMM");


        }

        private void insertBookDetails(Int64 isbn, string name, string author, string publisher)
        {
            string insertBookDetails = "INSERT INTO book_details(isbn,name, author,publisher) VALUES(@isbn,@name,@author,@publisher)";

            insertAuthor(author);
            SqlCommand cmd = new SqlCommand(insertBookDetails, connection);


            try
            {
                connection.Open();
                

                cmd.Parameters.AddWithValue("@isbn", isbn);
                cmd.Parameters.AddWithValue("@name", name);
                cmd.Parameters.AddWithValue("@author",author);
                cmd.Parameters.AddWithValue("@publisher", publisher);
                cmd.ExecuteNonQuery();

                connection.Close();

            }
            catch (Exception ex)
            {
                MessageBox.Show("Invalid Sql operation" + ex);
                connection.Close();
            }
        }

        private void insertBookDepart(int department)
        {
            string insertBookDepartment = "INSERT INTO books(department_Id) VALUES(@department_Id)";

            SqlCommand cmd = new SqlCommand(insertBookDepartment, connection);

            try
            {
                connection.Open();

                cmd.Parameters.AddWithValue("@department_Id", department);

                cmd.ExecuteNonQuery();

                connection.Close();

            }
            catch (Exception ex)
            {
                MessageBox.Show("Invalid Sql operation" + ex);
                connection.Close();
            }
        }

        private void insertAuthor(string name)
        {
            string insertAuthor = "INSERT INTO author(name) VALUES (@name)";

            SqlCommand cmd = new SqlCommand(insertAuthor, connection);

            try
            {
                connection.Open();

                cmd.Parameters.AddWithValue("@name", name);

                cmd.ExecuteNonQuery();

                connection.Close();

            }
            catch (Exception ex)
            {
                MessageBox.Show("Invalid Sql operation" + ex);
                connection.Close();
            }
        }


    }
}
