using StreamingApp.UWP.ViewModels;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Navigation;

namespace StreamingApp.UWP.Views.Categories
{
    public sealed partial class CategoryFormPage : Page
    {
        public CategoryViewModel CategoryViewModel { get; set; }
        public CategoryFormPage()
        {
            this.InitializeComponent();
            CategoryViewModel = new CategoryViewModel();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            if (e.Parameter != null)
            {
                CategoryViewModel = e.Parameter as CategoryViewModel;
            }
            base.OnNavigatedFrom(e);
        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            Frame.GoBack();
        }


        private async void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            if (await CategoryViewModel.CreateOrUpdateCategoryAsync())
            { Frame.GoBack(); }
            else
            { FlyoutBase.ShowAttachedFlyout(btnSave_Click); }
        }
    }
}
