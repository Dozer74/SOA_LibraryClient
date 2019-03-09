using System;
using System.Collections.Generic;
using System.Windows.Forms;
using LibraryClient.Models;

namespace LibraryClient.Forms
{
    public partial class AuthorDetail : Form
    {
        private readonly ApiClient _client;
        private readonly int _authorId;
        private Author _author;

        /// <summary>
        /// Shows information about existing author
        /// </summary>
        /// <param name="client">Web api client</param>
        /// <param name="authorId">Author id</param>
        public AuthorDetail(ApiClient client, int authorId)
        {
            _client = client;
            _authorId = authorId;
            InitializeComponent();
        }

        private async void AuthorDetail_Load(object sender, EventArgs e)
        {
            _author = await _client.GetAuthor(_authorId);

            tbFirstName.Text = _author.FirstName;
            tbLastName.Text = _author.LastName;
            tbYearBirth.Text = _author.YearBirth.ToString();
            tbYearDeath.Text = _author.YearDeath?.ToString();
            tbBio.Text = _author.Biography;

            var items = new List<ListViewItem>(_author.Books.Count);
            foreach (var book in _author.Books)
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
        
        private async void button2_Click(object sender, EventArgs e)
        {
            // Gathers information from form
            var authorPost = new AuthorPost
            {
                Id = _authorId,
                FirstName = tbFirstName.Text,
                LastName = tbLastName.Text,
                Biography = tbBio.Text,
            };

            if (int.TryParse(tbYearBirth.Text, out var yearBirth))
            {
                authorPost.YearBirth = yearBirth;
            }

            if (int.TryParse(tbYearDeath.Text, out var yearDeath))
            {
                authorPost.YearDeath = yearDeath;
            }

            // Updates existing author
            await _client.UpdateAuthor(authorPost);
        }
    }
}
