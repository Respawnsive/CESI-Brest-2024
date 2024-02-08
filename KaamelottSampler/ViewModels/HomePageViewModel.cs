using CommunityToolkit.Mvvm.ComponentModel;
using KaamelottSampler.Models;
using KaamelottSampler.Services;
using Plugin.Maui.Audio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace KaamelottSampler.ViewModels
{
    public class HomePageViewModel : ObservableObject
    {
        private readonly KaamelottDataService _dataService;
        private readonly IAudioManager _audioManager;
        private List<Saample> _AllSamples;

        public HomePageViewModel()
        {
            _dataService = App.Current.Services.GetService<KaamelottDataService>();
            _audioManager = App.Current.Services.GetService<IAudioManager>();
        }

        #region BindableProperties

        private List<Saample> _saamples;
        public List<Saample> Saamples 
        { 
            get
            {
                return _saamples;
            }
            set
            {
                SetProperty(ref _saamples, value);
            }
        }

        private Saample _selectedSaample;
        public Saample SelectedSaample
        {
            get
            {
                return _selectedSaample;
            }
            set
            {
                SetProperty(ref _selectedSaample, value);
            }
        }

        private string _filterText;
        public string FilterText
        {
            get 
            { 
                return _filterText; 
            }
            set 
            {
                SetProperty(ref _filterText, value); 
            }
        }

        private string _filterSelectedCharacter;
        public string FilterSelectedCharacter
        {
            get
            {
                return _filterSelectedCharacter;
            }
            set
            {
                SetProperty(ref _filterSelectedCharacter, value);
            }
        }

        private List<string> _filterCharacterList;
        public List<string> FilterCharacterList
        {
            get
            {
                return _filterCharacterList;
            }
            set
            {
                SetProperty(ref _filterCharacterList, value);
            }
        }


        #endregion

        public ICommand LoadSamplesCommand => new Command(async () => await LoadSamplesAsync());

        public ICommand SelectedSaampleCommand => new Command<Saample>(async (saample) => await SelectSaampleAsync(saample));

        public ICommand FilterCommand => new Command(async () => await FilterSamplesAsync());

        private async Task FilterSamplesAsync()
        {
            //Pas de filtres
            if (FilterText == null && FilterSelectedCharacter == null)
            {
                Saamples = _AllSamples;
                return;
            }

            //Filtre sur le texte sans characters
            if (FilterText != null && FilterSelectedCharacter == null)
            {
                Saamples = _AllSamples.Where(s => s.Title.ToLower().Contains(FilterText.ToLower())).ToList();
                return;
            }

            //Filtre sur le character sans texte
            if (FilterText == null && FilterSelectedCharacter != null)
            {
                Saamples = _AllSamples.Where(s => s.Character == FilterSelectedCharacter).ToList();
                return;
            }

            //Filtre sur le character et le texte
            if (FilterText != null && FilterSelectedCharacter != null)
            {
                Saamples = _AllSamples.Where(s => s.Character == FilterSelectedCharacter && s.Title.ToLower().Contains(FilterText.ToLower())).ToList();
                return;
            }
        }

        private async Task SelectSaampleAsync(Saample saample)
        {
            try
            {
                //Récupéré le fichier MP3 depuis nos "raw assets" (embedded)
                using var stream = await FileSystem.OpenAppPackageFileAsync(saample.File);
                //Jouer le mp3
                var player = _audioManager.CreatePlayer(stream);
                player.Play();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private async Task LoadSamplesAsync()
        {
            _AllSamples = await _dataService.GetSaamplesFromJson();
            FilterCharacterList = _AllSamples.Select(s => s.Character).Distinct().OrderBy(x => x).ToList();
            await FilterSamplesAsync();
        }
    }
}
