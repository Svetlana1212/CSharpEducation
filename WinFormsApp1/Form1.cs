namespace WinFormsApp1
{
    public partial class newForm : Form
    {
        public newForm()
        {
            InitializeComponent();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            textBox1.Text = "Привет, C#!";
        }
    }
}
