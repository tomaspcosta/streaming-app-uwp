using StreamingApp.UWP.Converters;
using StreamingApp.UWP.ViewModels;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Navigation;

// The User Control item template is documented at https://go.microsoft.com/fwlink/?LinkId=234236

namespace StreaminApp1.UWP.Views.SeriesFolder
{
    public sealed partial class SeriesInfoPage : Page
    {

        public SeriesViewModel SeriesViewModel { get; set; }

        static ByteToImageConverter Converter { get; set; }
        public SeriesInfoPage()
        {
            this.InitializeComponent();
            SeriesViewModel = new SeriesViewModel();

            Converter = new ByteToImageConverter();
        }


        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            if (e.Parameter != null)
            {
                SeriesViewModel = e.Parameter as SeriesViewModel;

                if (SeriesViewModel.SeriesClass.IsFavourite == true)
                {
                    ToggleButtonText.Text = "Remove";
                }
                else
                {
                    ToggleButtonText.Text = "Add";
                }
            }

            base.OnNavigatedTo(e);
        }

        private void ToggleButton_Click(object sender, RoutedEventArgs e)
        {
            var toggleButton = sender as ToggleButton;
            if (toggleButton != null)
            {
                bool isFavorite = toggleButton.IsChecked == true;

                if (isFavorite)
                {
                    SeriesViewModel.AddToFavorites();
                    SetToggleButtonText("Remove");
                }
                else
                {
                    SeriesViewModel.RemoveFromFavorites();
                    SetToggleButtonText("Add");
                }
            }
        }

        private void SetToggleButtonText(string text)
        {
            ToggleButtonText.Text = text;
        }


    }
}
