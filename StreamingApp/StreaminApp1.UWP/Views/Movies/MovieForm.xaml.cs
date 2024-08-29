using StreamingApp.Domain.Models;
using StreamingApp.UWP.Converters;
using StreamingApp.UWP.ViewModels;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;


namespace StreamingApp.UWP.Views.Movies
{
    public sealed partial class MovieForm : Page
    {
        public MovieViewModel MovieViewModel { get; set; }

        static ByteToImageConverter Converter { get; set; }
        public MovieForm()
        {
            this.InitializeComponent();
            MovieViewModel = new MovieViewModel();
            LoadCategories();
            Converter = new ByteToImageConverter();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            if (e.Parameter != null)
            {
                MovieViewModel = e.Parameter as MovieViewModel;
            }
            base.OnNavigatedFrom(e);
        }

        private async void LoadCategories()
        {
            // Assuming MovieViewModel has a method LoadAllCategoriesAsync to load
            // categories
            await MovieViewModel.LoadAllCategoriesAsync();
        }
        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            Frame.GoBack();
        }

        private async void btnSave_Click(object sender, RoutedEventArgs e)
        {
            if (await MovieViewModel.CreateOrUpdateMovieAsync())
            {
                Frame.GoBack();
            }
        }
        private void categoryComboBox_SelectionChanged(
            object sender, SelectionChangedEventArgs e)
        {
            if (sender is ComboBox comboBox)
            {
                MovieViewModel.SelectedCategory = comboBox.SelectedItem as Category;
            }
        }

        private void rating_TextChanged(object sender, TextChangedEventArgs e) { }

        private void description_TextChanged(object sender,
                                             TextChangedEventArgs e)
        { }

        private void cast_TextChanged(object sender, TextChangedEventArgs e) { }

        private void country_TextChanged(object sender, TextChangedEventArgs e) { }

        private void name_TextChanged(object sender, TextChangedEventArgs e) { }
    }
}
