using StreamingApp.Domain.Models;
using StreamingApp.InfraStructure;
using StreamingApp.UWP.ViewModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace StreamingApp.UWP.Views.Users
{
    public sealed partial class LoginPage : Page
    {
        public UserViewModel UserViewModel { get; set; }

        public LoginPage()
        {
            this.InitializeComponent();
            this.Loaded += MainPageAdmin_Loaded;
            UserViewModel = App.UserViewModel;
            UserViewModel.User = new User();
            UserViewModel.ShowError = false;
        }

        private void MainPageAdmin_Loaded(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            PopulateDb();
        }

        private async void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;

            if (button != null)
            {
                button.IsEnabled = false;

                bool loginSuccess = await UserViewModel.DoLoginAsync();

                if (loginSuccess)
                {
                    if (UserViewModel.IsAdmin)
                    {
                        Frame.Navigate(typeof(MainPageAdmin), "HomePage");
                    }
                    else
                    {
                        Frame.Navigate(typeof(MainPageUser), "HomePage");
                    }
                }
                else
                {
                    ErrorMessageTextBlock.Text = "Invalid username or password.";
                }

                button.IsEnabled = true;
            }
        }

        private void PasswordBox_KeyDown(
            object sender, Windows.UI.Xaml.Input.KeyRoutedEventArgs e)
        {
            // Check if the Enter key is pressed
            if (e.Key == Windows.System.VirtualKey.Enter)
            {
                // Perform the login action
                LoginButton_Click(sender, e);
            }
        }

        private void UsernameTextBox_Loaded(object sender, RoutedEventArgs e)
        {
            // Set focus to the UsernameTextBox when it is loaded
            UsernameTextBox.Focus(FocusState.Programmatic);
        }

        private void OnPageKeyDown(object sender,
                                   Windows.UI.Xaml.Input.KeyRoutedEventArgs e)
        {
            // Check if the Enter key is pressed
            if (e.Key == Windows.System.VirtualKey.Enter)
            {
                // Perform the login action
                LoginButton_Click(LoginButton, e);
            }
        }

        private void UsernameTextBox_KeyDown(
            object sender, Windows.UI.Xaml.Input.KeyRoutedEventArgs e)
        {
            // Check if the Enter key is pressed
            if (e.Key == Windows.System.VirtualKey.Enter)
            {
                // Perform the login action
                LoginButton_Click(LoginButton, e);
            }
        }

        private void CreateAccountTapped(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(RegisterPage));
        }

        static async Task<bool> MovieExists(UnitOfWork uow, string movieName)
        {
            return await uow.MovieRepository.FindByNameAsync(movieName) != null;
        }

        // check if a category with the given name already exists
        static async Task<bool> CategoryExists(UnitOfWork uow,
                                               string categoryName)
        {
            return await uow.CategoryRepository.FindByNameAsync(categoryName) != null;
        }
        static async void PopulateDb()
        {
            using (var uow = new UnitOfWork())
            {
                var adminExists = await uow.UserRepository.FindByUsernameAsync("admin");

                if (adminExists == null)
                {
                    // Creating admin user
                    var admin = await uow.UserRepository.CreateNormalUserAsync(
                        "admin", "admin", "admi1@gmail.com", "987654321");
                    admin.IsAdmin = true;
                    await uow.SaveAsync();
                }
            }
            using (var uow = new UnitOfWork())
            {
                // Check if the movie "Inception" already exists, because it's the first one to be added
                if (await MovieExists(uow, "Inception"))
                {
                    return;
                }

                // Create Movies
                var movie1 = new Movie
                {
                    Name = "Inception",
                    Duration = new TimeSpan(2, 28, 0),
                    CategoryId = 1,
                    Rating = 9,
                    Released = DateTime.Now.AddYears(-3),
                    Cast = "Leonardo DiCaprio, Joseph Gordon-Levitt",
                    Country = "USA",
                    Description = "A mind-bending thriller about dreams within dreams."
                };
                var movie2 = new Movie
                {
                    Name = "The Shawshank Redemption",
                    Duration = new TimeSpan(2, 22, 0),
                    CategoryId = 1,
                    Rating = 10,
                    Released = DateTime.Now.AddYears(-7),
                    Cast = "Tim Robbins, Morgan Freeman",
                    Country = "USA",
                    Description =
                      "Two imprisoned men bond over a number of years, finding solace and eventual redemption through acts of common decency."
                };
                var movie3 = new Movie
                {
                    Name = "The Dark Knight",
                    Duration = new TimeSpan(2, 32, 0),
                    CategoryId = 2,
                    Rating = 9,
                    Released = DateTime.Now.AddYears(-10),
                    Cast = "Christian Bale, Heath Ledger",
                    Country = "USA",
                    Description =
                      "When the menace known as The Joker emerges, Batman must confront the greatest psychological and physical tests of his ability to fight injustice."
                };
                var movie4 = new Movie
                {
                    Name = "The Matrix",
                    Duration = new TimeSpan(2, 16, 0),
                    CategoryId = 2,
                    Rating = 8,
                    Released = DateTime.Now.AddYears(-22),
                    Cast = "Keanu Reeves, Laurence Fishburne",
                    Country = "USA",
                    Description =
                      "A computer hacker learns from mysterious rebels about the true nature of his reality and his role in the war against its controllers."
                };
                var movie5 = new Movie
                {
                    Name = "Pulp Fiction",
                    Duration = new TimeSpan(2, 34, 0),
                    CategoryId = 3,
                    Rating = 9,
                    Released = DateTime.Now.AddYears(-27),
                    Cast = "John Travolta, Uma Thurman",
                    Country = "USA",
                    Description =
                      "The lives of two mob hitmen, a boxer, a gangster and his wife, and a pair of diner bandits intertwine in four tales of violence and redemption."
                };
                var movie6 = new Movie
                {
                    Name = "Forrest Gump",
                    Duration = new TimeSpan(2, 22, 0),
                    CategoryId = 1,
                    Rating = 8,
                    Released = DateTime.Now.AddYears(-28),
                    Cast = "Tom Hanks, Robin Wright",
                    Country = "USA",
                    Description =
                      "The presidencies of Kennedy and Johnson, the events of Vietnam, Watergate, and other history unfold through the perspective of an Alabama man with an IQ of 75."
                };
                var movie7 = new Movie
                {
                    Name = "The Godfather",
                    Duration = new TimeSpan(2, 55, 0),
                    CategoryId = 3,
                    Rating = 10,
                    Released = DateTime.Now.AddYears(-50),
                    Cast = "Marlon Brando, Al Pacino",
                    Country = "USA",
                    Description =
                      "An organized crime dynasty's aging patriarch transfers control of his clandestine empire to his reluctant son."
                };
                var movie8 = new Movie
                {
                    Name = "Schindler's List",
                    Duration = new TimeSpan(3, 15, 0),
                    CategoryId = 1,
                    Rating = 10,
                    Released = DateTime.Now.AddYears(-28),
                    Cast = "Liam Neeson, Ben Kingsley",
                    Country = "USA",
                    Description =
                      "In German-occupied Poland during World War II, industrialist Oskar Schindler gradually becomes concerned for his Jewish workforce after witnessing their persecution by the Nazis."
                };
                var movie9 = new Movie
                {
                    Name = "Inglourious Basterds",
                    Duration = new TimeSpan(2, 33, 0),
                    CategoryId = 3,
                    Rating = 8,
                    Released = DateTime.Now.AddYears(-12),
                    Cast = "Brad Pitt, Christoph Waltz",
                    Country = "USA",
                    Description =
                      "In Nazi-occupied France during World War II, a plan to assassinate Nazi leaders by a group of Jewish U.S. soldiers coincides with a theatre owner's vengeful plans for the same."
                };
                var movie10 = new Movie
                {
                    Name = "The Silence of the Lambs",
                    Duration = new TimeSpan(1, 58, 0),
                    CategoryId = 3,
                    Rating = 9,
                    Released = DateTime.Now.AddYears(-30),
                    Cast = "Jodie Foster, Anthony Hopkins",
                    Country = "USA",
                    Description =
                      "A young FBI cadet must receive the help of an incarcerated and manipulative cannibal killer to help catch another serial killer, a madman who skins his victims."
                };

                // Create Series
                var series1 = new SeriesClass
                {
                    Name = "Breaking Bad",
                    CategoryId = 3,
                    Rating = 9,
                    Released = DateTime.Now.AddYears(-14),
                    Cast = "Bryan Cranston, Aaron Paul",
                    Country = "USA",
                    Description =
                      "A high school chemistry teacher turned methamphetamine manufacturer partners with a former student to secure his family's financial future."
                };
                var series2 = new SeriesClass
                {
                    Name = "Game of Thrones",
                    CategoryId = 4,
                    Rating = 8,
                    Released = DateTime.Now.AddYears(-10),
                    Cast = "Emilia Clarke, Kit Harington",
                    Country = "USA",
                    Description =
                      "Nine noble families fight for control over the lands of Westeros, while an ancient enemy returns after being dormant for millennia."
                };
                var series3 = new SeriesClass
                {
                    Name = "Stranger Things",
                    CategoryId = 4,
                    Rating = 8,
                    Released = DateTime.Now.AddYears(-5),
                    Cast = "Millie Bobby Brown, Finn Wolfhard",
                    Country = "USA",
                    Description =
                      "A group of kids in a small town uncover a series of supernatural mysteries and government conspiracies when a young girl with psychokinetic abilities is found."
                };
                var series4 = new SeriesClass
                {
                    Name = "The Witcher",
                    CategoryId = 4,
                    Rating = 7,
                    Released = DateTime.Now.AddYears(-2),
                    Cast = "Henry Cavill, Freya Allan",
                    Country = "USA",
                    Description =
                      "Geralt of Rivia, a solitary monster hunter, struggles to find his place in a world where people often prove more wicked than beasts."
                };
                var series5 = new SeriesClass
                {
                    Name = "Money Heist",
                    CategoryId = 5,
                    Rating = 8,
                    Released = DateTime.Now.AddYears(-4),
                    Cast = "Úrsula Corberó, Álvaro Morte",
                    Country = "Spain",
                    Description =
                      "A criminal mastermind, known as The Professor, recruits eight criminals with unique skills to carry out an ambitious plan to rob the Royal Mint of Spain."
                };
                var series6 = new SeriesClass
                {
                    Name = "Stranger Things 2",
                    CategoryId = 4,
                    Rating = 9,
                    Released = DateTime.Now.AddYears(-4),
                    Cast = "Millie Bobby Brown, Finn Wolfhard",
                    Country = "USA",
                    Description =
                      "The gang continues to face supernatural threats as they uncover more secrets about the alternate dimension known as the Upside Down."
                };
                var series7 = new SeriesClass
                {
                    Name = "The Mandalorian",
                    CategoryId = 5,
                    Rating = 8,
                    Released = DateTime.Now.AddYears(-3),
                    Cast = "Pedro Pascal, Gina Carano",
                    Country = "USA",
                    Description =
                      "A lone gunfighter makes his way through the outer reaches of the galaxy, far from the authority of the New Republic."
                };
                var series8 = new SeriesClass
                {
                    Name = "Black Mirror",
                    CategoryId = 7,
                    Rating = 8,
                    Released = DateTime.Now.AddYears(-12),
                    Cast = "Various",
                    Country = "UK",
                    Description =
                      "An anthology series exploring the dark, often dystopian sides of technological advancements and their impact on society."
                };
                var series9 = new SeriesClass
                {
                    Name = "Narcos",
                    CategoryId = 5,
                    Rating = 8,
                    Released = DateTime.Now.AddYears(-7),
                    Cast = "Pedro Pascal, Wagner Moura",
                    Country = "USA",
                    Description =
                      "A chronicled look at the criminal exploits of Colombian drug lord Pablo Escobar, as well as the many other drug kingpins who plagued the country through the years."
                };
                var series10 = new SeriesClass
                {
                    Name = "Westworld",
                    CategoryId = 7,
                    Rating = 8,
                    Released = DateTime.Now.AddYears(-5),
                    Cast = "Evan Rachel Wood, Thandie Newton",
                    Country = "USA",
                    Description =
                      "An amusement park for rich vacationers, the park is looked after by robotic hosts who believe they are human."
                };

                // Create Seasons
                var se1 = new Season { Number = 1 };
                var se2 = new Season { Number = 2 };

                // Create Episodes
                var ep1 = new Episodes { Number = 1 };
                var ep2 = new Episodes { Number = 2 };
                var ep3 = new Episodes { Number = 3 };
                var ep4 = new Episodes { Number = 4 };
                var ep5 = new Episodes { Number = 5 };

                // Add episodes to seasons
                se1.Episodes.AddRange(new List<Episodes> { ep1, ep2, ep3 });
                se2.Episodes.AddRange(new List<Episodes> { ep1, ep2 });

                // Add seasons to series
                series5.Seasons.Add(se2);
                series5.Seasons.Add(se1);

                if (!await CategoryExists(uow, "Drama"))
                {
                    // Create Categories
                    var c1 = new Category { Name = "Drama" };
                    var c2 = new Category { Name = "Action" };
                    var c3 = new Category { Name = "Thriller" };
                    var c4 = new Category { Name = "Fantasy" };
                    var c5 = new Category { Name = "Crime" };
                    var c6 = new Category { Name = "Comedy" };
                    var c7 = new Category { Name = "Science Fiction" };
                    var c8 = new Category { Name = "Mystery" };
                    var c9 = new Category { Name = "Romance" };
                    var c10 = new Category { Name = "Adventure" };

                    // Add movies and series to their respective categories
                    c1.Movies.AddRange(
                        new List<Movie> { movie1, movie2, movie6, movie7, movie8 });
                    c2.Movies.AddRange(new List<Movie> { movie3, movie4 });
                    c3.Movies.AddRange(new List<Movie> { movie5, movie9, movie10 });
                    c3.Series.AddRange(new List<SeriesClass> { series1 });
                    c4.Series.AddRange(new List<SeriesClass> { series2, series3, series4, series6 });
                    c5.Series.AddRange(new List<SeriesClass> { series5, series9, series10, series7 });
                    c6.Movies.AddRange(new List<Movie> { movie7, movie10 });
                    c7.Series.AddRange(new List<SeriesClass> { series8, series10 });
                    c8.Movies.AddRange(new List<Movie> { movie1, movie5 });
                    c9.Series.AddRange(new List<SeriesClass> { series2, series8 });
                    c10.Movies.AddRange(new List<Movie> { movie4, movie9 });

                    // Add categories to the unit of work and save changes
                    uow.CategoryRepository.Create(c1);
                    uow.CategoryRepository.Create(c2);
                    uow.CategoryRepository.Create(c3);
                    uow.CategoryRepository.Create(c4);
                    uow.CategoryRepository.Create(c5);
                    uow.CategoryRepository.Create(c6);
                    uow.CategoryRepository.Create(c7);
                    uow.CategoryRepository.Create(c8);
                    uow.CategoryRepository.Create(c9);
                    uow.CategoryRepository.Create(c10);
                }

                // Save changes
                await uow.SaveAsync();
            }
        }
    }
}