using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using LibraryClient.Models;

namespace LibraryClient.Forms
{
    public partial class SelectAuthorsForm : Form
    {
        private readonly ApiClient _client;
        private readonly int[] _selectedIds;
        private List<Author> _authors;

        public IEnumerable<Author> SelectedAuthors { get; private set; }
    
        public SelectAuthorsForm(ApiClient client, int[] selectedIds)
        {
            _client = client;
            _selectedIds = selectedIds;
            InitializeComponent();
        }

        private async void SelectAuthorsForm_Load(object sender, EventArgs e)
        {
            _authors = await _client.GetAuthors();

            for (int i = 0; i < _authors.Count; i++)
            {
                var author = _authors[i];
                listAuthors.Items.Add($"{author.FirstName} {author.LastName}");
                if (_selectedIds.Contains(author.Id))
                {
                    listAuthors.SetItemChecked(i, true);
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            SelectedAuthors = _authors
                .Where((author, idx) => listAuthors.CheckedIndices.Contains(idx));
        }
    }
}
