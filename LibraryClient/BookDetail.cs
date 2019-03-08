using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using LibraryClient.Models;

namespace LibraryClient
{
    public partial class BookDetail : Form
    {
        private readonly ApiClient _client;
        private List<Genre> _genres;
        private readonly int _bookId;

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
            pbCover.Image = _book.Cover;
            
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
    }
}
