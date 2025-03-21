
using StreaminApp1.UWP.Views.Movies;
using StreamingApp.Domain.Models;
using StreamingApp.UWP.Converters;
using StreamingApp.UWP.ViewModels;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace StreamingApp.UWP.Views.Movies
{
    public sealed partial class MoviesPage : Page
    {
        public MovieViewModel MovieViewModel { get; set; }
        static ByteToImageConverter Converter { get; set; }
        public UserViewModel UserViewModel { get; set; }

        public MoviesPage()
        {
            this.InitializeComponent();
            MovieViewModel = new MovieViewModel();
            Converter = new ByteToImageConverter();
            UserViewModel = App.UserViewModel;
            LoadCategories();
        }
        protected async override void OnNavigatedTo(NavigationEventArgs e)
        {
            await MovieViewModel.LoadAllAsync();
            base.OnNavigatedTo(e);
        }

        private async void LoadCategories()
        {
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
        private async void categoryComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Set the SelectedCategory property when the selection changes
            if (sender is ComboBox comboBox)
            {
                MovieViewModel.SelectedCategory = comboBox.SelectedItem as Category;

                // Check if the SelectedCategory is not null
                if (MovieViewModel.SelectedCategory != null)
                {
                    await MovieViewModel.LoadAllAsyncByCategory(MovieViewModel.SelectedCategory);
                }
            }
        }
        private void gridCategories_Tapped(object sender, Windows.UI.Xaml.Input.TappedRoutedEventArgs e)
        {
            if (sender is FrameworkElement fe && fe.DataContext is Movie m)
            {
                MovieViewModel.Movie = m;

                if (UserViewModel.IsAdmin)
                {
                    Frame.Navigate(typeof(MovieInfoPageAdmin), MovieViewModel);

                }
                else
                {
                    Frame.Navigate(typeof(MovieInfoPage), MovieViewModel);
                }
               
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
