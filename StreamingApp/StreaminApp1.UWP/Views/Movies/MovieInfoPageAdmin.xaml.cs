using StreamingApp.Domain.Models;
using StreamingApp.UWP;
using StreamingApp.UWP.Converters;
using StreamingApp.UWP.ViewModels;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Navigation;

namespace StreaminApp1.UWP.Views.Movies
{
    public sealed partial class MovieInfoPageAdmin : Page
    {
        public MovieViewModel MovieViewModel { get; set; }
   

        static ByteToImageConverter Converter { get; set; }

        public MovieInfoPageAdmin()
        {
            this.InitializeComponent();
            MovieViewModel = new MovieViewModel();
          
            Converter = new ByteToImageConverter();

        }
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            if (e.Parameter != null)
            {

               
                MovieViewModel = e.Parameter as MovieViewModel;

            }

            base.OnNavigatedTo(e);
        }

     




    }
}
