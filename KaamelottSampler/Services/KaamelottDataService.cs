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
        public async Task<List<Saample>> GetSaamplesFromJson()
        {
            using var stream = await FileSystem.OpenAppPackageFileAsync("sounds.json");
            using var reader = new StreamReader(stream);
            var contents = await reader.ReadToEndAsync();
            var saamples = JsonSerializer.Deserialize<List<Saample>>(contents);
            return saamples;
        }

        public async Task<List<Saample>> GetSaamplesFromApi()
        {
            return new List<Saample>();
        }
    }
}
