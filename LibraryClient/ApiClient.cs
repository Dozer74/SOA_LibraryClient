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

        #region Authors

        public async Task<List<Author>> GetAuthors()
        {
            var response = await GetAsync("authors");
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadAsAsync<List<Author>>();
            }

            return null;
        }

        public async Task<Author> GetAuthor(int id)
        {
            var response = await GetAsync($"authors/{id}");
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadAsAsync<Author>();
            }

            return null;
        }

        public async Task<bool> UpdateAuthor(AuthorPost author)
        {
            var json = JsonConvert.SerializeObject(author);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await PutAsync($"authors/{author.Id}/", content);

            return response.IsSuccessStatusCode;
        }

        public async Task<bool> CreateAuthor(AuthorPost author)
        {
            var json = JsonConvert.SerializeObject(author);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await PostAsync($"authors/{author.Id}/", content);

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
