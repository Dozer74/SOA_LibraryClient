using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using LibraryClient.Forms;
using LibraryClient.Models;

namespace LibraryClient.Controls
{
    public partial class AuthorList : UserControl, IReloadable
    {
        private readonly ApiClient _client;
        private List<Author> _authors;

        public AuthorList(ApiClient client)
        {
            _client = client;
            InitializeComponent();
        }

        public async void Reload()
        {
            // Reloads authors
            _authors = await _client.GetAuthors();
            ShowAuthors();
        }

        private void ShowAuthors()
        {
            // Updates listView
            var items = new List<ListViewItem>(_authors.Count);
            var filter = textBox1.Text.ToLower();
            foreach (var author in _authors)
            {

                if (!string.IsNullOrWhiteSpace(filter) &&
                    !author.FullName.ToLower().Contains(filter))
                {
                    continue;
                }

                var lifeYears = author.YearBirth.ToString();
                if (author.YearDeath != null)
                {
                    lifeYears += " - " + author.YearDeath;
                }

                items.Add(new ListViewItem(new[]
                {
                    $"{author.FirstName} {author.LastName}",
                    lifeYears,
                    author.BooksCount.ToString()
                }));
            }

            listView.Items.Clear();
            listView.Items.AddRange(items.ToArray());
        }

        private void AuthorList_Load(object sender, EventArgs e)
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
            var authorId = _authors[index].Id;

            var detailForm = new AuthorDetail(_client, authorId);
            if (detailForm.ShowDialog(this) == DialogResult.OK)
            {
                Reload();
            }
        }

        private void textBox1_TextChanged(Object sender, EventArgs e)
        {
            ShowAuthors();
        }
    }
}
