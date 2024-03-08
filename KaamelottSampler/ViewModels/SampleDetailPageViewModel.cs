using CommunityToolkit.Mvvm.ComponentModel;
using KaamelottSampler.Models;
using KaamelottSampler.Services;
using Plugin.Maui.Audio;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
//using Share = Microsoft.Maui.ApplicationModel.DataTransfer.Share;

namespace KaamelottSampler.ViewModels
{
    public class SampleDetailPageViewModel : ReactiveObject, IQueryAttributable
    {
        private readonly IAudioManager _audioManager;

        public SampleDetailPageViewModel()
        {
            _audioManager = App.Current.Services.GetService<IAudioManager>();
        }

        #region BindableProperties

        [Reactive]
        public Saample CurrentSample { get; set; }

        #endregion

        #region Commands

        public ICommand PlayMP3Command => new Command(async () => await PlayMP3());

        private async Task PlayMP3()
        {
            //Joue le MP3
            try
            {
                //Récupéré le fichier MP3 depuis nos "raw assets" (embedded)
                using var stream = await FileSystem.OpenAppPackageFileAsync(CurrentSample.File);
                //Jouer le mp3
                var player = _audioManager.CreatePlayer(stream);
                player.Play();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public ICommand PlayTTSCommand => new Command(async () => await PlayTTS());
        private async Task PlayTTS()
        {
            //Joue le TTS
            await TextToSpeech.Default.SpeakAsync(CurrentSample.Title);
        }

        public ICommand ShareCommand => new Command(async () => await Share());
        private async Task Share()
        {
            await Microsoft.Maui.ApplicationModel.DataTransfer.Share.Default.RequestAsync(new ShareTextRequest
            {
                Uri = "https://github.com/2ec0b4/kaamelott-soundboard/tree/master/sounds/" + CurrentSample.File,
                Title = CurrentSample.Title
            });
        }

        #endregion

        #region Get Navigation parameters

        public void ApplyQueryAttributes(IDictionary<string, object> query)
        {
            CurrentSample = query["saample"] as Saample;
        }

        #endregion


    }
}
