using System;
using System.Windows.Forms;
using LibraryClient.Models;

namespace LibraryClient.Forms
{
    public partial class AuthorCreate : Form
    {
        private readonly ApiClient _client;

        /// <summary>
        /// Shows information about existing author
        /// </summary>
        /// <param name="client">Web api client</param>
        /// <param name="authorId">Author id</param>
        public AuthorCreate(ApiClient client)
        {
            _client = client;
            InitializeComponent();
        }

        
        private async void btnSave_Click(object sender, EventArgs e)
        {
            btnSave.Enabled = false;
            btnCancel.Enabled = false;
            saveProgress.Visible = true;

            // Gathers information from form
            var authorPost = new AuthorPost
            {
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
            await _client.CreateAuthor(authorPost);
        }
    }
}
