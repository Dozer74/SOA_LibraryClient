using System;
using System.Collections.Generic;
using System.Windows.Forms;
using LibraryClient.Models;

namespace LibraryClient.Controls
{
    public partial class AuthorList : UserControl
    {
        private readonly ApiClient _client;
        private List<Author> _authors;

        public AuthorList(ApiClient client)
        {
            _client = client;
            InitializeComponent();
        }

        private async void ReloadAuthors()
        {
            // Reloads authors
            _authors = await _client.GetAuthors();

            // Updates listView
            var items = new List<ListViewItem>(_authors.Count);
            foreach (var author in _authors)
            {
                var lifeYears = author.YearBirth.ToString();
                if (author.YearDeath != null)
                {
                    lifeYears += " - "+author.YearDeath;
                }

                items.Add(new ListViewItem(new []
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
            ReloadAuthors();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ReloadAuthors();
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
                ReloadAuthors();
            }
        }
    }
}
