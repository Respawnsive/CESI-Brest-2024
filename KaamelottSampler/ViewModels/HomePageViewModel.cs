using CommunityToolkit.Mvvm.ComponentModel;
using KaamelottSampler.Models;
using KaamelottSampler.Services;
using Plugin.Maui.Audio;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace KaamelottSampler.ViewModels
{
    public class HomePageViewModel : ReactiveObject
    {
        private readonly KaamelottDataService _dataService;

        private List<Saample> _AllSamples = new List<Saample>();

        public HomePageViewModel()
        {
            _dataService = App.Current.Services.GetService<KaamelottDataService>();
            Task.Run(async () => await LoadSamplesAsync());

            //Définir nos "réactions"
            this.WhenAnyValue(x => x.FilterText, x => x.FilterSelectedCharacter)
                .Skip(1)
                .Throttle(new TimeSpan(0, 0, 0, 0, 500))
                .Subscribe(async _ => await FilterSamplesAsync());
        }

        #region BindableProperties

        [Reactive]
        public ObservableCollection<Saample> Saamples { get; set; }

        [Reactive]
        public Saample SelectedSaample { get; set; }

        [Reactive]
        public string FilterText { get; set; }

        [Reactive]
        public string FilterSelectedCharacter { get; set; }

        [Reactive]
        public List<string> FilterCharacterList { get; set; }

        [Reactive]
        public bool IsRefreshing { get; set; }

        #endregion

        public ICommand LoadSamplesCommand => new Command(async() => await LoadSamplesAsync());

        public ICommand SelectedSaampleCommand => new Command<Saample>(async (saample) => await SelectSaampleAsync(saample));

        public ICommand FilterCommand => new Command(async () => await FilterSamplesAsync());

        #region ClearCommand
        public ReactiveCommand<Unit, Unit> ClearCommand => ReactiveCommand.CreateFromTask(ClearFilterAsync, CanClearFilter);

        private async Task ClearFilterAsync()
        {
            FilterText = "";
            FilterSelectedCharacter = "";
            await FilterSamplesAsync();
        }

        public IObservable<bool>? CanClearFilter => 
            this.WhenAnyValue(x => x.FilterText, x => x.FilterSelectedCharacter,
             (text, character) => !string.IsNullOrEmpty(text) || !string.IsNullOrEmpty(character));

        #endregion

        private async Task FilterSamplesAsync()
        {
            //Pas de filtres
            if (FilterText == null && FilterSelectedCharacter == null)
            {
                Saamples = new ObservableCollection<Saample>(_AllSamples);
                return;
            }

            //Filtre sur le texte sans characters
            if (FilterText != null && FilterSelectedCharacter == null)
            {
                Saamples = new ObservableCollection<Saample>(_AllSamples.Where(s => s.Title.ToLower().Contains(FilterText.ToLower())).ToList());
                return;
            }

            //Filtre sur le character sans texte
            if (FilterText == null && FilterSelectedCharacter != null)
            {
                Saamples = new ObservableCollection<Saample>(_AllSamples.Where(s => s.Character == FilterSelectedCharacter).ToList());
                return;
            }

            //Filtre sur le character et le texte
            if (FilterText != null && FilterSelectedCharacter != null)
            {
                Saamples = new ObservableCollection<Saample>(_AllSamples.Where(s => s.Character == FilterSelectedCharacter && s.Title.ToLower().Contains(FilterText.ToLower())).ToList());
                return;
            }
        }

        private async Task SelectSaampleAsync(Saample saample)
        {
            var param = new ShellNavigationQueryParameters()
            {
                { "saample", saample }
            };
            await Shell.Current.GoToAsync("SampleDetailPage", param);
        }

        private async Task LoadSamplesAsync()
        {
            IsRefreshing = true;
            _AllSamples = await _dataService.GetSaamplesAsync();
            FilterCharacterList = _AllSamples.Select(s => s.Character).Distinct().OrderBy(x => x).ToList();
            await FilterSamplesAsync();
            IsRefreshing = false;
        }
    }
}
