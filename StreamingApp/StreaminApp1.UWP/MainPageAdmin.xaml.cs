using StreaminApp1.UWP.Views.Favorites;
using StreaminApp1.UWP.Views.Quiz;
using StreaminApp1.UWP.Views.User;
using StreamingApp.UWP.ViewModels;
using StreamingApp.UWP.Views.Categories;
using StreamingApp.UWP.Views.Home;
using StreamingApp.UWP.Views.Movies;
using StreamingApp.UWP.Views.Serie;
using StreamingApp.UWP.Views.Users;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Navigation;

namespace StreamingApp.UWP
{
    public sealed partial class MainPageAdmin : Page
    {
        public MovieViewModel MovieViewModel { get; set; }

        public MainPageAdmin()
        {
            this.InitializeComponent();
            this.Loaded += MainPageAdmin_Loaded;
            MovieViewModel = new MovieViewModel();
            nvMain.BackRequested += nvMain_BackRequested;
        }

        private void nvMain_BackRequested(NavigationView sender, NavigationViewBackRequestedEventArgs args)
        {
            if (frmMainAdmin.CanGoBack)
            {
                frmMainAdmin.GoBack();
            }
        }

        private void MainPageAdmin_Loaded(object sender, Windows.UI.Xaml.RoutedEventArgs e)
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
            frmMainAdmin.Navigate(typeof(UserArea));
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
            frmMainAdmin.Navigate(typeof(HomePage));
        }

        private void nvMain_ItemInvoked(NavigationView sender,
                                        NavigationViewItemInvokedEventArgs args)
        {
            var selectedItem = args.InvokedItemContainer as NavigationViewItem;
            if (selectedItem != null)
            {
                switch (selectedItem.Tag)
                {
                    case "inicio":
                        frmMainAdmin.Navigate(typeof(HomePage));
                        break;
                    case "categorias admin":
                        frmMainAdmin.Navigate(typeof(ManageCategoriesPage));
                        break;
                    case "series admin":
                        frmMainAdmin.Navigate(typeof(ManageSeriesPage));
                        break;
                    case "filmes admin":
                        frmMainAdmin.Navigate(typeof(MovieManage));
                        break;
                    case "users admin":
                        frmMainAdmin.Navigate(typeof(UserManagePage));
                        break;
                    case "Perfil":
                        frmMainAdmin.Navigate(typeof(UserArea));
                        break;
                    case "filmes":
                        frmMainAdmin.Navigate(typeof(MoviesPage));
                        break;
                    case "series":
                        frmMainAdmin.Navigate(typeof(SeriesPage));
                        break;
                    case "contact":
                        System.Diagnostics.Debug.WriteLine("Contact item clicked!");
                        frmMainAdmin.Navigate(typeof(UserArea));
                        break;
                    default:
                        break;
                }
            }
        }

        private void ScrollViewer_ViewChanged(object sender,
                                              ScrollViewerViewChangedEventArgs e)
        {
            // Handle ScrollViewer view changed event if needed
        }

        private void frmMainAdmin_Navigated(
            object sender, Windows.UI.Xaml.Navigation.NavigationEventArgs e)
        { }

    }
}