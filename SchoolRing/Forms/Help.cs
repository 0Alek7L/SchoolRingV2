using SchoolRing.Help_info;
using System;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Media;

namespace SchoolRing
{
    public partial class Help : Form
    {
        Timer timer;
        public Help()
        {
            InitializeComponent();
            timer = new Timer();
            timer.Interval = 200;
            timer.Tick += Timer_Tick;
            timer.Start();
            Program.ShowTheCurrentIcon(pictureBox3);
        }
        private void Timer_Tick(object sender, EventArgs e)
        {
            TimeForClockAndText time = new TimeForClockAndText();
            labelDayOfWeek.Text = time.PrintDay();
            Program.ShowTheCurrentIcon(pictureBox3);
        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {
            Program.LastForms.Pop().Show();
            this.Hide();
        }

        private void dEFAULTToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Program.ChangeCustomIcon(pictureBox3, false);
            Program.customIconPath = null;
        }

        private void cUSTOMToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Program.ChoosePathForCustomIcon(contextMenuStrip1);
            Program.ChangeCustomIcon(pictureBox3, true);
        }

        private void Help_Load(object sender, EventArgs e)
        {
            //richTextBox1.Text =  HelpInfo.ChangeClassLength;
            //richTextBox1.Text =  HelpInfo.ChooseUser;
            //richTextBox1.Text =  HelpInfo.SchoolProgram;
            //richTextBox1.Text = HelpInfo.MergeClasses;
            //richTextBox1.Text = HelpInfo.Vacations;
            //richTextBox1.Text = HelpInfo.Melody;
            //richTextBox1.Text = HelpInfo.Printer;
            //richTextBox1.Text = HelpInfo.Search;
            //richTextBox1.Text = HelpInfo.CustomizeClassName;
            //richTextBox1.Text = HelpInfo.Notes;
            //richTextBox1.Text = HelpInfo.MainMenu;
        }

        private void pictureBoxCloseMenu_Click(object sender, EventArgs e)
        {
            panel1.Show();
        }

        private void label10_MouseEnter(object sender, EventArgs e)
        {
            Label a = (Label)sender;
            a.ForeColor = System.Drawing.Color.FromArgb(34, 146, 164);
            a.BackColor = System.Drawing.Color.White;
        }

        private void label10_MouseLeave(object sender, EventArgs e)
        {
            Label a = (Label)sender;
            a.BackColor = System.Drawing.Color.FromArgb(34, 146, 164);
            a.ForeColor = System.Drawing.Color.White;
        }

        private void label1_Click(object sender, EventArgs e)
        {
            richTextBox1.Clear();
            richTextBox1.Text = HelpInfo.ChangeClassLength;
            panel1.Hide();
        }

        private void label2_Click(object sender, EventArgs e)
        {
            richTextBox1.Clear();
            richTextBox1.Text = HelpInfo.ChooseUser;
            panel1.Hide();
        }

        private void label3_Click(object sender, EventArgs e)
        {
            richTextBox1.Clear();
            richTextBox1.Text = HelpInfo.SchoolProgram;
            panel1.Hide();
        }

        private void label11_Click(object sender, EventArgs e)
        {
            richTextBox1.Clear();
            richTextBox1.Text = HelpInfo.MainMenu;
            panel1.Hide();
        }

        private void label5_Click(object sender, EventArgs e)
        {
            richTextBox1.Clear();
            richTextBox1.Text = HelpInfo.Vacations;
            panel1.Hide();
        }

        private void label6_Click(object sender, EventArgs e)
        {
            richTextBox1.Clear();
            richTextBox1.Text = HelpInfo.Melody;
            panel1.Hide();
        }

        private void label4_Click(object sender, EventArgs e)
        {
            richTextBox1.Clear();
            richTextBox1.Text = HelpInfo.MergeClasses;
            panel1.Hide();
        }

        private void label7_Click(object sender, EventArgs e)
        {
            richTextBox1.Clear();
            richTextBox1.Text = HelpInfo.Printer;
            panel1.Hide();
        }

        private void label8_Click(object sender, EventArgs e)
        {
            richTextBox1.Clear();
            richTextBox1.Text = HelpInfo.Search;
            panel1.Hide();
        }

        private void label9_Click(object sender, EventArgs e)
        {
            richTextBox1.Clear();
            richTextBox1.Text = HelpInfo.CustomizeClassName;
            panel1.Hide();
        }

        private void label10_Click(object sender, EventArgs e)
        {
            richTextBox1.Clear();
            richTextBox1.Text = HelpInfo.Notes;
            panel1.Hide();
        }
    }
}
