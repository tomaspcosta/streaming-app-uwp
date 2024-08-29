using StreamingApp.Domain.Models;
using StreamingApp.UWP.ViewModels;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace StreaminApp1.UWP.Views.Quiz
{
    public sealed partial class TypeSeriesQuizPage : Page
    {


        public SeriesViewModel SeriesViewModel { get; set; }
        public TypeSeriesQuizPage()
        {
            InitializeComponent();
            SeriesViewModel = new SeriesViewModel();
            LoadCategories();
        }

        private async void LoadCategories()
        {
           
            await SeriesViewModel.LoadAllCategoriesAsync();
        }

        private void categoryComboBox_SelectionChanged(
           object sender, SelectionChangedEventArgs e)
        {
            // Set the SelectedCategory property when the selection changes
            if (sender is ComboBox comboBox)
            {
                SeriesViewModel.SelectedCategory = comboBox.SelectedItem as Category;
            }
        }

        private void ContinueButton_Click(object sender, RoutedEventArgs e)
        {

            var selectedMovieCategory = SeriesViewModel.SelectedCategory;

            // Check if a category is selected before navigating
            if (selectedMovieCategory != null)
            {
                Frame.Navigate(typeof(DurationSeriesQuizPage), selectedMovieCategory);
            }
            else
            {
              
            }
        }
    }
}
