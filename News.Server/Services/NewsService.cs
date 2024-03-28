using News.Server.Services.Contracts;
using News.Server.Entity;
using System.Net.Http;

namespace News.Server.Services
{
    public class NewsService : INewsService
    {
        private readonly HttpClient httpClient;

        public NewsService(HttpClient httpClient) 
        {
            this.httpClient = httpClient;

        }
        public async Task<NewsEntity> GetItem(int id)
        {
            try
            {
                var response = await httpClient.GetAsync($"api/News/{id}");

                if (response.IsSuccessStatusCode)
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.NoContent)
                    {
                        return default(NewsEntity);
                    }

                    return await response.Content.ReadFromJsonAsync<NewsEntity>();
                }
                else
                {
                    var message = await response.Content.ReadAsStringAsync();
                    throw new Exception($"Http status code: {response.StatusCode} message: {message}");
                }
            }
            catch (Exception)
            {
                //Log exception
                throw;
            }
        }

        public async Task<NewsEntity> AddItem(NewsEntity newsEntity)
        {
            try
            {
                var response = await httpClient.PostAsJsonAsync<NewsEntity>("api/ShoppingCart", newsEntity);

                if (response.IsSuccessStatusCode)
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.NoContent)
                    {
                        return default(NewsEntity);
                    }

                    return await response.Content.ReadFromJsonAsync<NewsEntity>();

                }
                else
                {
                    var message = await response.Content.ReadAsStringAsync();
                    throw new Exception($"Http status:{response.StatusCode} Message -{message}");
                }

            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
