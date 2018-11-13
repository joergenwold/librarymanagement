using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace LibraryManageSyst
{
    public partial class AppUI : Form
    {
        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;

        [DllImportAttribute("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd,int Msg, int wParam, int lParam);
        [DllImportAttribute("user32.dll")]
        public static extern bool ReleaseCapture();


        public AppUI()
        {
            InitializeComponent();
        }

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }

        }

        //Log out button
        private void button1_Click(object sender, EventArgs e)
        {
            Form1 form = new Form1();
            this.Hide();
            form.Show();
        }

        //minimize button
        private void button2_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        //exit button
        private void button3_Click(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }
        
        //Transaction button
        private void button4_Click(object sender, EventArgs e)
        {
            SidePanel.Height = button4.Height;
            SidePanel.Top = button4.Top;
        }

        //Books button
        private void button5_Click(object sender, EventArgs e)
        {
            SidePanel.Height = button5.Height;
            SidePanel.Top = button5.Top;
            if(!ContentPanel.Controls.Contains(BookUsercontrol.theInstance))
            {
                ContentPanel.Controls.Add(BookUsercontrol.theInstance);
                BookUsercontrol.theInstance.Dock = DockStyle.Fill;
                BookUsercontrol.theInstance.BringToFront();
            }
            else
            {
                BookUsercontrol.theInstance.BringToFront();
            }
        }

        //Borrowers button
        private void button6_Click(object sender, EventArgs e)
        {
            SidePanel.Height = button6.Height;
            SidePanel.Top = button6.Top;
        }

        //Settings
        private void button7_Click(object sender, EventArgs e)
        {
            SidePanel.Height = button7.Height;
            SidePanel.Top = button7.Top;
        }

        //About us button
        private void button8_Click(object sender, EventArgs e)
        {
            SidePanel.Height = button8.Height;
            SidePanel.Top = button8.Top;
        }
    }
}
