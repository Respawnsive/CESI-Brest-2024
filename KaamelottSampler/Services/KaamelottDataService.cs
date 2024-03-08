using Controls.UserDialogs.Maui;
using KaamelottSampler.Models;
using Microsoft.AppCenter.Crashes;
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
        private readonly IUserDialogs _userDialogs;

        public KaamelottDataService(IUserDialogs userDialogs)
        {
            _userDialogs = userDialogs;
        }

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
                _userDialogs.Alert("Vous êtes hors ligne, les données locales seront utilisées", "Hors ligne", "OK");
                return await GetSaamplesFromJson();
            }
        }

        private async Task<List<Saample>> GetSaamplesFromJson()
        {
            List<Saample> result = new List<Saample>();
            try
            {
                using var stream = await FileSystem.OpenAppPackageFileAsync("sounds.json");
                using var reader = new StreamReader(stream);
                var contents = await reader.ReadToEndAsync();
                result = JsonSerializer.Deserialize<List<Saample>>(contents);
            }
            catch (Exception ex)
            {
                Crashes.TrackError(ex);
                _userDialogs.Alert($"Une erreur est survenue lors de la récupération des données locales ({ex?.Message})", "Erreur", "OK");
            }
            return result;
        }

        private async Task<List<Saample>> GetSaamplesFromApi()
        {
            List<Saample> result = new List<Saample>();
            try
            {
                HttpClient client = new HttpClient();
                var response = await client.GetStringAsync("https://storagekaamelot.blob.core.windows.net/kaamlotcontainer/datas.json");
                if (response != null)
                    result = JsonSerializer.Deserialize<List<Saample>>(response);
                else
                    result = await GetSaamplesFromJson();
            }
            catch (Exception ex)
            {
                Crashes.TrackError(ex);
                _userDialogs.Alert($"Une erreur est survenue lors de la récupération des données depuis l'API ({ex?.Message})", "Erreur", "OK");
            }
            return result;
        }
    }
}
