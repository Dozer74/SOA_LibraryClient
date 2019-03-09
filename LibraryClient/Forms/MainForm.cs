using System;
using System.Windows.Forms;
using LibraryClient.Controls;

namespace LibraryClient.Forms
{
    public partial class MainForm : Form
    {
        private readonly ApiClient _client;
        private readonly AuthorList _authorListControl;
        private readonly BooksList _booksListControl;
        private IReloadable _currentControl;

        public MainForm()
        {
            InitializeComponent();
            _client = new ApiClient(new Uri("http://localhost:8000"));
            _authorListControl = new AuthorList(_client);
            _booksListControl = new BooksList(_client);
        }

        private bool SetControl(Control control)
        {
            if (_currentControl == control)
            {
                return false;
            }
            panelPlaceholder.Controls.Clear();
            panelPlaceholder.Controls.Add(control);
            control.Dock = DockStyle.Fill;
            _currentControl = control as IReloadable;

            Text = control == _authorListControl ? "Список авторов" : "Список книг";
            return true;
        }

        private void btnAuthors_Click(object sender, EventArgs e)
        {
            SetControl(_authorListControl);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            SetControl(_booksListControl);
        }

        private void btnBooks_Click(object sender, EventArgs e)
        {
            SetControl(_booksListControl);
        }

        private void btnCreateAuthor_Click(object sender, EventArgs e)
        {
            var authorDetails = new AuthorCreate(_client);
            if (authorDetails.ShowDialog(this) == DialogResult.OK)
            {
                if (!SetControl(_authorListControl)) // authors list is active already
                {
                    _currentControl.Reload();
                }
            }
        }

        private void btnReload_Click(Object sender, EventArgs e)
        {
            _currentControl?.Reload();
        }

        private void btnCreateBook_Click(Object sender, EventArgs e)
        {
            var form = new BookCreate(_client);
            if (form.ShowDialog(this) == DialogResult.OK)
            {
                if (!SetControl(_booksListControl)) // books list is active already
                {
                    _currentControl.Reload();
                }
            }
        }
    }
}
