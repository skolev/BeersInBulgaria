using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace BeerStatsClient.Data
{
    class HttpRequester
    {
        public async static Task<T> Get<T>(string url, IDictionary<string, string> headers = null)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, url);

            if (headers != null)
            {
                foreach (var header in headers)
                {
                    request.Headers.Add(header.Key, header.Value);
                }
            }

            bool isNullResponse = false;
            try
            {
                var client = new HttpClient();
                var response = await client.SendAsync(request);
                var content = await response.Content.ReadAsStringAsync();
                T responseData = JsonConvert.DeserializeObject<T>(content);
                if (responseData.GetType().FullName ==
                    "System.Collections.Generic.List`1[[BeerStatsClient.Models.BeerTypesModel, BeerStatsClient, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]]")
                {
                    var localFolder = Windows.Storage.ApplicationData.Current.LocalFolder;
                    var beerFile = await localFolder.CreateFileAsync("beers.txt", Windows.Storage.CreationCollisionOption.ReplaceExisting);
                    await Windows.Storage.FileIO.WriteTextAsync(beerFile, content);
                }
                return responseData;
            }

            catch (HttpRequestException ex)
            {
                isNullResponse = true;
            }

            if (isNullResponse)
            {
                return await LoadInfo<T>();
            }

            return await LoadInfo<T>();
        }
  
        private async static Task<T> LoadInfo<T>()
        {
            var localFolder = Windows.Storage.ApplicationData.Current.LocalFolder;
            Windows.Storage.StorageFile fileToUse = await localFolder.GetFileAsync("beers.txt");
            var content = await Windows.Storage.FileIO.ReadTextAsync(fileToUse);
            T responseData = JsonConvert.DeserializeObject<T>(content);
            return responseData;
        }
    }
}