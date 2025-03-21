using StreamingApp.Domain.Models;
using StreamingApp.UWP.ViewModels;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;


namespace StreaminApp1.UWP.Views.Quiz
{
    public sealed partial class TypeMovieQuizPage : Page
    {

        public MovieViewModel MovieViewModel { get; set; }
        public TypeMovieQuizPage()
        {
            InitializeComponent();
            MovieViewModel = new MovieViewModel();
            LoadCategories();
        }

        private async void LoadCategories()
        {
           
            
            await MovieViewModel.LoadAllCategoriesAsync();
        }

        private void categoryComboBox_SelectionChanged(
           object sender, SelectionChangedEventArgs e)
        {
            // Set the SelectedCategory property when the selection changes
            if (sender is ComboBox comboBox)
            {
                MovieViewModel.SelectedCategory = comboBox.SelectedItem as Category;
            }
        }

        private void ContinueButton_Click(object sender, RoutedEventArgs e)
        {

            var selectedMovieCategory = MovieViewModel.SelectedCategory;

            // Check if a category is selected before navigating
            if (selectedMovieCategory != null)
            {
                Frame.Navigate(typeof(DurationMovieQuizPage), selectedMovieCategory);
            }
            else
            {
              
            }
        }

        private void TextBlock_SelectionChanged(object sender, RoutedEventArgs e)
        {

        }
    }
}
