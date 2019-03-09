using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using LibraryClient.Forms;
using LibraryClient.Models;

namespace LibraryClient.Controls
{
    public partial class BooksList : UserControl, IReloadable
    {
        private readonly ApiClient _client;
        private List<Book> _books;

        public BooksList(ApiClient client)
        {
            _client = client;
            InitializeComponent();
        }

        public async void Reload()
        {
            _books = await _client.GetBooks();

            var items = new List<ListViewItem>(_books.Count);
            foreach (var book in _books.Where(b => b.AuthorsNames != null))
            {
                items.Add(new ListViewItem(new[]
                {
                    book.Title,
                    book.AuthorsNames,
                    book.Genre,
                    book.Year.ToString()
                }));
            }

            listView.Items.Clear();
            listView.Items.AddRange(items.ToArray());
        }

        private void ShowBooks()
        {
            var filter = textBox1.Text.ToLower();
            var items = new List<ListViewItem>(_books.Count);
            foreach (var book in _books)
            {
                if (string.IsNullOrEmpty(book.AuthorsNames) ||
                    !string.IsNullOrWhiteSpace(filter) &&
                    !book.Title.ToLower().Contains(filter))
                {
                    continue;
                }

                items.Add(new ListViewItem(new[]
                {
                    book.Title,
                    book.AuthorsNames,
                    book.Genre,
                    book.Year.ToString()
                }));
            }

            listView.Items.Clear();
            listView.Items.AddRange(items.ToArray());
        }

        private void BooksList_Load(object sender, EventArgs e)
        {
            Reload();
        }

        private void listView_DoubleClick(object sender, EventArgs e)
        {
            if (listView.SelectedIndices.Count != 1)
            {
                return;
            }

            var index = listView.SelectedIndices[0];
            var bookId = _books[index].Id;

            var detailForm = new BookDetail(_client, bookId);
            if (detailForm.ShowDialog(this) == DialogResult.OK)
            {
                Reload();
            }
        }

        private void textBox1_TextChanged(Object sender, EventArgs e)
        {
            ShowBooks();
        }
    }
}
