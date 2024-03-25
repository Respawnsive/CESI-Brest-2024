using CommunityToolkit.Mvvm.ComponentModel;
using Microsoft.AppCenter.Crashes;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using System.Windows.Input;

namespace KaamelottSampler.ViewModels
{
    public class AboutPageViewModel : ReactiveObject
    {

        public AboutPageViewModel()
        {
            GithubURL = "https://github.com/2ec0b4/kaamelott-soundboard";
            CesiURL = "https://brest.cesi.fr/";
        }

        #region BindableProperties

        [Reactive]
        public string GithubURL { get; set; }
        [Reactive]
        public string CesiURL { get; set; }
        [Reactive]
        public ImageSource PhotoSource { get; set; }
        [Reactive]
        public string CurrentLocation { get; set; }


        public ICommand OpenGithubCommand => new Command(async () => await OpenUrl(GithubURL));

        public ICommand OpenCesiCommand => new Command(async () => await OpenUrl(CesiURL));

        public ICommand TakePhotoCommand => new Command(async () => await TakePhoto());

        public ICommand GetLocationCommand => new Command(async () => await GetLocation());

        private async Task GetLocation()
        {
            try
            {
                var location = await Geolocation.Default.GetLocationAsync();
                if (location != null)
                {
                    CurrentLocation = $"Latitude: {location.Latitude}, Longitude: {location.Longitude}, Altitude: {location.Altitude}, Précision: {location.Accuracy}";
                }
            }
            catch (FeatureNotSupportedException fnsEx)
            {
                // Handle not supported on device exception
            }
            catch (FeatureNotEnabledException fneEx)
            {
                // Handle not enabled on device exception
            }
            catch (PermissionException pEx)
            {
                // Handle permission exception
            }
            catch (Exception ex)
            {
                // Unable to get location
            }
        }

        private async Task TakePhoto()
        {
            try
            {
                //Prendre une photo
                if (MediaPicker.IsCaptureSupported)
                {
                    //Demande d'autorisation 
                    if (await HasCameraPermission())
                    {
                        var result = await MediaPicker.CapturePhotoAsync();
                        if (result != null)
                        {
                            if (File.Exists(result.FullPath))
                            {
                                PhotoSource = ImageSource.FromFile(result.FullPath);
                            }

                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Crashes.TrackError(ex);
            }
        }

        private async Task OpenUrl(string url)
        {
            await Launcher.OpenAsync(url);
        }

        private async Task<bool> HasCameraPermission()
        {
            PermissionStatus status = await Permissions.CheckStatusAsync<Permissions.Camera>();

            if (status != PermissionStatus.Granted)
            {
                status = await Permissions.RequestAsync<Permissions.Camera>();
            }

            return status == PermissionStatus.Granted;
        }

        #endregion


    }
}
