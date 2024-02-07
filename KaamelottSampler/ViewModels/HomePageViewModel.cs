using KaamelottSampler.Models;
using KaamelottSampler.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace KaamelottSampler.ViewModels
{
    public class HomePageViewModel
    {
        private readonly KaamelottDataService _dataService;

        public HomePageViewModel(KaamelottDataService dataService)
        {
            _dataService = dataService;
        }

        public List<Saample> Saamples { get; set; }

        public ICommand LoadSamplesCommand => new Command(async () => await LoadSamplesAsync());

        private async Task LoadSamplesAsync()
        {
            Saamples = await _dataService.GetSaamplesFromJson();
        }
    }
}
