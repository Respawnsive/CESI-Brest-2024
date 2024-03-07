using KaamelottSampler.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace KaamelottSampler.Services
{
    public class KaamelottDataService
    {
        public async Task<List<Saample>> GetSaamplesAsync()
        {
            //Logique de synchro online/offline
            NetworkAccess accessType = Connectivity.Current.NetworkAccess;
            if (accessType == NetworkAccess.Internet
                || accessType == NetworkAccess.Unknown)
            {
                return await GetSaamplesFromApi();
            }
            else
            {
                return await GetSaamplesFromJson();
            }
        }

        private async Task<List<Saample>> GetSaamplesFromJson()
        {
            using var stream = await FileSystem.OpenAppPackageFileAsync("sounds.json");
            using var reader = new StreamReader(stream);
            var contents = await reader.ReadToEndAsync();
            var saamples = JsonSerializer.Deserialize<List<Saample>>(contents);
            return saamples;
        }

        private async Task<List<Saample>> GetSaamplesFromApi()
        {
            HttpClient client = new HttpClient();
            var response = await client.GetStringAsync("https://storagekaamelot.blob.core.windows.net/kaamlotcontainer/datas.json");
            if (response != null)
                return JsonSerializer.Deserialize<List<Saample>>(response);
            return await GetSaamplesFromJson();
        }
    }
}
