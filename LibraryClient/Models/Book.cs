using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace LibraryClient.Models
{
    public class Book
    {
        public int Id { get; set; }

        [JsonProperty(PropertyName = "title")]
        public string Title { get; set; }

        [JsonProperty(PropertyName = "description")]
        public string Description { get; set; }

        [JsonProperty(PropertyName = "authors_names")]
        public string AuthorsNames { get; set; }

        [JsonProperty(PropertyName = "authors")]
        public List<Author> Authors { get; set; }

        [JsonProperty(PropertyName = "genre")]
        public string Genre { get; set; }

        [JsonProperty(PropertyName = "year")]
        public int Year { get; set; }

        [JsonProperty(PropertyName = "cover")]
        private Byte[] CoverBytes { get; set; }

        public Image Cover => Helpers.ByteArrayToImage(CoverBytes);
    }

    public class BookPost
    {
        public int Id { get; set; }

        [JsonProperty(PropertyName = "title")]
        public string Title { get; set; }

        [JsonProperty(PropertyName = "description")]
        public string Description { get; set; }
        
        [JsonProperty(PropertyName = "authors_ids")]
        public List<int> AuthorsIds { get; set; }

        [JsonProperty(PropertyName = "genre")]
        public string Genre { get; set; }

        [JsonProperty(PropertyName = "year")]
        public int Year { get; set; }

        [JsonProperty(PropertyName = "cover")]
        public Byte[] CoverBytes { get; set; }
    }
}
