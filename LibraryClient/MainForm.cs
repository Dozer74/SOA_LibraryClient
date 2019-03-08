using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using LibraryClient.Controls;
using LibraryClient.Models;

namespace LibraryClient
{
    public partial class MainForm : Form
    {
        private readonly ApiClient _client;
        private readonly AuthorList _authorListControl;
        private readonly BooksList _booksListControl;

        public MainForm()
        {
            InitializeComponent();
            _client = new ApiClient(new Uri("http://localhost:8000"));
            _authorListControl = new AuthorList(_client);
            _booksListControl = new BooksList(_client);
        }

        private void SetControl(Control control)
        {
            panelPlaceholder.Controls.Clear();
            panelPlaceholder.Controls.Add(control);
            control.Dock = DockStyle.Fill;

            Text = control == _authorListControl ? "Список авторов" : "Список книг";
        }

        private void btnAuthors_Click(object sender, EventArgs e)
        {
            SetControl(_authorListControl);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            SetControl(_booksListControl);
        }

        private void btnBooks_Click_1(object sender, EventArgs e)
        {
            SetControl(_booksListControl);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var authorDetails = new AuthorDetail(_client);
            if (authorDetails.ShowDialog(this) == DialogResult.OK)
            {
                SetControl(_authorListControl);
            }
        }
    }
}
