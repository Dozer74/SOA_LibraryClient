using System.Collections.Generic;
using Newtonsoft.Json;

namespace LibraryClient.Models
{
    public class Author
    {
        public int Id { get; set; }

        [JsonProperty(PropertyName = "first_name")]
        public string FirstName { get; set; }

        [JsonProperty(PropertyName = "last_name")]
        public string LastName { get; set; }

        [JsonProperty(PropertyName = "biography")]
        public string Biography { get; set; }

        [JsonProperty(PropertyName = "year_birth")]
        public int YearBirth { get; set; }

        [JsonProperty(PropertyName = "year_death")]
        public int? YearDeath { get; set; }

        [JsonProperty(PropertyName = "books_count")]
        public int BooksCount { get; set; }

        [JsonProperty(PropertyName = "books")]
        public List<Book> Books { get; set; }
    }

    public class AuthorPost
    {
        public int Id { get; set; }

        [JsonProperty(PropertyName = "first_name")]
        public string FirstName { get; set; }

        [JsonProperty(PropertyName = "last_name")]
        public string LastName { get; set; }

        [JsonProperty(PropertyName = "biography")]
        public string Biography { get; set; }

        [JsonProperty(PropertyName = "year_birth")]
        public int YearBirth { get; set; }

        [JsonProperty(PropertyName = "year_death")]
        public int? YearDeath { get; set; }

    }
}