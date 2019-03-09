using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using LibraryClient.Models;

namespace LibraryClient.Forms
{
    public partial class BookCreate : Form
    {
        private readonly ApiClient _client;
        private bool _imageChanged;
        private List<int> _authorsIds;
        private List<Genre> _genres;

        public BookCreate(ApiClient client)
        {
            _client = client;
            _authorsIds = new List<int>();
            InitializeComponent();
        }

        private string FormatAuthorNames(IEnumerable<Author> authors)
        {
            var authorsNames = authors.Select(a => $"{a.FirstName[0]}. {a.LastName}");
            return string.Join(", ", authorsNames);
        }
        

        private void btnUpdateAuthors_Click(object sender, EventArgs e)
        {
            var form = new SelectAuthorsForm(_client, _authorsIds.ToArray());
            if (form.ShowDialog(this) != DialogResult.OK)
                return;

            var authors = form.SelectedAuthors.ToArray();
            _authorsIds = authors.Select(a => a.Id).ToList();
            lbAuthors.Text = FormatAuthorNames(authors);
        }

        private async void btnSave_Click(Object sender, EventArgs e)
        {
            var book = new BookPost
            {
                AuthorsIds = _authorsIds.ToList(),
                Description = tbDesc.Text,
                Genre = _genres[cbGenre.SelectedIndex].Id,
                Title = tbTitle.Text,
            };

            // If book has not default cover picture
            if (_imageChanged)
            {
                book.CoverBytes = Helpers.ImageToBytes(pbCover.Image);
            }

            if (int.TryParse(tbYear.Text, out var year))
            {
                book.Year = year;
            }

            await _client.CreateBook(book);
        }

        private void pbCover_Click(Object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog(this) == DialogResult.OK)
            {
                pbCover.Image = Image.FromFile(openFileDialog1.FileName);
                _imageChanged = true;
            }
        }

        private async void BookCreate_Load(Object sender, EventArgs e)
        {
            _genres =  await _client.GetGenres();
            cbGenre.Items.AddRange(_genres.Select(g => g.Name).ToArray());
        }
    }
}
