using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using LibraryClient.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace LibraryClient
{
    public class ApiClient : HttpClient
    {
        public ApiClient(Uri baseUri)
        {
            BaseAddress = baseUri;

            DefaultRequestHeaders.Accept.Clear();
            DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
        }

        private StringContent BuildContent(object obj)
        {
            var json = JsonConvert.SerializeObject(obj);
            return new StringContent(json, Encoding.UTF8, "application/json");
        }

        private async Task<T> GetObject<T>(string url)
        {
            var response = await GetAsync(url);
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadAsAsync<T>();
            }

            return default(T);
        }

        #region Authors

        public async Task<List<Author>> GetAuthors()
        {
            return await GetObject<List<Author>>("authors");
        }

        public async Task<Author> GetAuthor(int id)
        {
            return await GetObject<Author>($"authors/{id}");
        }

        public async Task<bool> UpdateAuthor(AuthorPost author)
        {
            var content = BuildContent(author);
            var response = await PutAsync($"authors/{author.Id}/", content);

            return response.IsSuccessStatusCode;
        }

        public async Task<bool> CreateAuthor(AuthorPost author)
        {
            var content = BuildContent(author);
            var response = await PostAsync("authors/", content);

            return response.IsSuccessStatusCode;
        }

        #endregion


        #region Books

        public async Task<List<Book>> GetBooks()
        {
            var response = await GetAsync("books");
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadAsAsync<List<Book>>();
            }

            return null;
        }

        public async Task<Book> GetBook(int id)
        {
            var response = await GetAsync($"books/{id}");
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadAsAsync<Book>();
            }

            return null;
        }

        public async Task<bool> UpdateBook(BookPost book)
        {
            var content = BuildContent(book);
            var response = await PutAsync($"books/{book.Id}/", content);

            return response.IsSuccessStatusCode;
        }

        public async Task<bool> RemoveBook(int bookId)
        {
            var response = await DeleteAsync($"books/{bookId}/");
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> CreateBook(BookPost book)
        {
            var content = BuildContent(book);
            var response = await PostAsync("books/", content);
            return response.IsSuccessStatusCode;
        }

        #endregion

        #region Genres

        public async Task<List<Genre>> GetGenres()
        {
            var response = await GetAsync("genres");
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadAsAsync<List<Genre>>();
            }

            return null;
        }

        #endregion


        
    }
}
