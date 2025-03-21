using StreaminApp1.UWP.Views.Favorites;
using StreaminApp1.UWP.Views.Quiz;
using StreamingApp.UWP.ViewModels;
using StreamingApp.UWP.Views.Home;
using StreamingApp.UWP.Views.Movies;
using StreamingApp.UWP.Views.Serie;
using StreamingApp.UWP.Views.Users;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Navigation;

namespace StreamingApp.UWP
{
    public sealed partial class MainPageUser : Page
    {
        public MovieViewModel MovieViewModel { get; set; }

        public MainPageUser()
        {
            this.InitializeComponent();
            this.Loaded += MainPageUser_Loaded;
            MovieViewModel = new MovieViewModel();
            nvMain.BackRequested += nvMain_BackRequested;
        }

        private void nvMain_BackRequested(NavigationView sender, NavigationViewBackRequestedEventArgs args)
        {
            if (frmMainUser.CanGoBack)
            {
                frmMainUser.GoBack();
            }
        }

        private void MainPageUser_Loaded(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            LoadMovies();
            CheckLoginParameter();
        }

        private async void LoadMovies()
        {
            await MovieViewModel.LoadAllAsync();
        }

        private void Contact_ItemClick(object sender, TappedRoutedEventArgs e)
        {
            frmMainUser.Navigate(typeof(UserArea));
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            if (e.Parameter != null && e.Parameter is string destination)
            {
                switch (destination)
                {
                    case "HomePage":
                        NavigateToHomePage();
                        break;
                    default:
                        break;
                }
            }
        }

        private void CheckLoginParameter()
        {
            if (this.Frame.BackStackDepth == 0)
            {
                NavigateToHomePage();
            }
        }

        private void NavigateToHomePage()
        {
            frmMainUser.Navigate(typeof(HomePage));
        }

        private void nvMain_ItemInvoked(NavigationView sender, NavigationViewItemInvokedEventArgs args)
        {
            var selectedItem = args.InvokedItemContainer as NavigationViewItem;
            if (selectedItem != null)
            {
                switch (selectedItem.Tag)
                {
                    case "inicio":
                        frmMainUser.Navigate(typeof(HomePage));
                        break;
                    case "series":
                        frmMainUser.Navigate(typeof(SeriesPage));
                        break;
                    case "filmes":
                        frmMainUser.Navigate(typeof(MoviesPage));
                        break;
                    case "Perfil":
                        frmMainUser.Navigate(typeof(UserArea));
                        break;
                    case "favoritos":
                        frmMainUser.Navigate(typeof(FavouritesPage));
                        break;
                    case "quiz":
                        frmMainUser.Navigate(typeof(IntroductionQuizPage));
                        break;
                    case "contact":
                        System.Diagnostics.Debug.WriteLine("Contact item clicked!");
                        frmMainUser.Navigate(typeof(UserArea));
                        break;
                    default:
                        break;
                }
            }
        }

        private void ScrollViewer_ViewChanged(object sender, ScrollViewerViewChangedEventArgs e)
        {
            // Handle ScrollViewer view changed event if needed
        }

        private void frmMainUser_Navigated(object sender, Windows.UI.Xaml.Navigation.NavigationEventArgs e)
        {

        }

    }
}