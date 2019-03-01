using System;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Rtl.Data.TvMaze.Proxy
{
    public class TvMazeProxy : ITvMazeProxy
    {
        private readonly string _baseUrl;

        public TvMazeProxy(string baseUrl)
        {
            _baseUrl = baseUrl;
        }

        public async Task<Announcement[]> GetShows(string country)
        {
            return await ExecuteHttpRequest<Announcement[]>($"{_baseUrl}/schedule?country={country}");
        }

        public async Task<Character[]> GetCast(int showId)
        {
            try
            {
                return await ExecuteHttpRequest<Character[]>($"{_baseUrl}/shows/{showId}/cast");
            }
            catch (TooManyRequestsException)
            {
                Console.WriteLine($"retrying {showId}");
                Thread.Sleep(1000);
                return await GetCast(showId);
            }
        }

        private static async Task<T> ExecuteHttpRequest<T>(string url)
        {
            using (var client = new HttpClient())
            {
                using (var response = await client.GetAsync(new Uri(url)))
                {
                    if (response.StatusCode == HttpStatusCode.TooManyRequests)
                    {
                        throw new TooManyRequestsException();
                    }

                    return await response.Content.ReadAsAsync<T>();
                }
            }
        }
    }
}