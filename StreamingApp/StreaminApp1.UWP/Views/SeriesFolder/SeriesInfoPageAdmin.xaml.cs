using StreamingApp.UWP.Converters;
using StreamingApp.UWP.ViewModels;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Navigation;

// The User Control item template is documented at https://go.microsoft.com/fwlink/?LinkId=234236

namespace StreaminApp1.UWP.Views.SeriesFolder
{
    public sealed partial class SeriesInfoPageAdmin : Page
    {

        public SeriesViewModel SeriesViewModel { get; set; }

        static ByteToImageConverter Converter { get; set; }
        public SeriesInfoPageAdmin()
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
            }

            base.OnNavigatedTo(e);
        }



    }
}
