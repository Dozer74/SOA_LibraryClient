using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using LibraryClient.Models;

namespace LibraryClient.Controls
{
    public partial class BooksList : UserControl
    {
        private readonly ApiClient _client;
        private List<Book> _books;

        public BooksList(ApiClient client)
        {
            _client = client;
            InitializeComponent();
        }

        private async void LoadBooks()
        {
            _books = await _client.GetBooks();

            var items = new List<ListViewItem>(_books.Count);
            foreach (var book in _books)
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

        private void BooksList_Load(object sender, EventArgs e)
        {
            LoadBooks();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            LoadBooks();
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
            detailForm.ShowDialog(this);
        }
    }
}
