using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using LibraryClient.Models;

namespace LibraryClient.Forms
{
    public partial class BookDetail : Form
    {
        private readonly ApiClient _client;
        private List<Genre> _genres;
        private readonly int _bookId;
        private bool _imageChanged;

        private Book _book;

        public BookDetail(ApiClient client, int bookId)
        {
            _client = client;
            _bookId = bookId;
            InitializeComponent();
        }

        private string FormatAuthorNames(IEnumerable<Author> authors)
        {
            var authorsNames = _book.Authors.Select(a => $"{a.FirstName[0]}. {a.LastName}");
            return string.Join(", ", authorsNames);
        }

        private async void BookDetail_Load(object sender, EventArgs e)
        {
            _book = await _client.GetBook(_bookId);

            // Set up genres
            _genres = await _client.GetGenres();
            cbGenre.Items.AddRange(_genres.Select(g => g.Name).ToArray());
            cbGenre.SelectedIndex = _genres.FindIndex(g => g.Id == _book.Genre);

            tbDesc.Text = _book.Description;
            tbTitle.Text = _book.Title;
            tbYear.Text = _book.Year.ToString();
            cbGenre.SelectedItem = _book.Genre;

            if (_book.Cover != null)
            {
                pbCover.Image = _book.Cover;
            }

            lbAuthors.Text = FormatAuthorNames(_book.Authors);
        }

        private void btnUpdateAuthors_Click(object sender, EventArgs e)
        {
            var ids = _book.Authors.Select(a => a.Id).ToArray();
            var selectAuthors = new SelectAuthorsForm(_client, ids);

            if (selectAuthors.ShowDialog(this) == DialogResult.OK)
            {
                _book.Authors = selectAuthors.SelectedAuthors.ToList();
                lbAuthors.Text = FormatAuthorNames(_book.Authors);
            }
        }

        private async void btnSave_Click(Object sender, EventArgs e)
        {
            var book = new BookPost
            {
                Id = _bookId,
                AuthorsIds = _book.Authors.Select(a => a.Id).ToList(),
                Description = tbDesc.Text,
                Genre = _genres[cbGenre.SelectedIndex].Id,
                Title = tbTitle.Text,
            };

            // If book has not default cover picture
            if (_book.Cover != null && _imageChanged)
            {
                book.CoverBytes = Helpers.ImageToBytes(pbCover.Image);
            }

            if (int.TryParse(tbYear.Text, out var year))
            {
                book.Year = year;
            }

            await _client.UpdateBook(book);
        }

        private void pbCover_Click(Object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog(this) == DialogResult.OK)
            {
                pbCover.Image = Image.FromFile(openFileDialog1.FileName);
                _imageChanged = true;
            }
        }

        private async void btnRemove_Click(Object sender, EventArgs e)
        {
            if (MessageBox.Show(
                    $"Удалить книгу \"{_book.Title}\"?\nИнформация будет удалена безвозвратно",
                    "Подтвердите удаление", 
                    MessageBoxButtons.YesNoCancel, 
                    MessageBoxIcon.Asterisk) == DialogResult.Yes)
            {
                await _client.RemoveBook(_bookId);
                await Task.Delay(200);
            }
        }
    }
}
