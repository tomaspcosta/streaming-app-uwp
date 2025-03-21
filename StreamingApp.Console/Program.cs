using StreamingApp.Domain.Models;
using StreamingApp.InfraStructure;
using System.Globalization;
using System.Security.Cryptography;
using System.Text;

// Seed Initial Data
PopulateDb();
await SeedInitialData();

#region Menu Functions
// HomePage
User? currentUser = null;
var exit = false;

while (!exit)
{
    if (currentUser != null)
    {
        if (currentUser.IsAdmin)
        {
            currentUser = AdminUserMenu(currentUser);
        }
        else
        {
            currentUser = NormalUserMenu(currentUser);
        }
    }
    else
    {
        Console.Clear();
        HomePageMenu();
        var preLoginOption = Console.ReadLine();
        Console.Clear();

        switch (preLoginOption)
        {
            case "1":
                Console.Clear();
                currentUser = PerformLogin();
                break;
            case "2":
                Console.Clear();
                await PerformRegisterAsync();
                break;
            case "0":
                Environment.Exit(0);
                break;
            default:
                Console.WriteLine("Choose a valid option.");
                WaitByClick();
                break;
        }
    }
}


void HomePageMenu()
{
    Console.Clear();
    Console.WriteLine("╔════════════════════════════════════════════════════════════╗");
    Console.WriteLine("║                         - WELCOME -                        ║");
    Console.WriteLine("╚════════════════════════════════════════════════════════════╝");
    Console.WriteLine("╔════════════════════════════════════════════════════════════╗");
    Console.WriteLine("║  [1]  - Login                                              ║");
    Console.WriteLine("║  [2]  - Register                                           ║");
    Console.WriteLine("║  [0]  - Exit                                               ║");
    Console.WriteLine("╚════════════════════════════════════════════════════════════╝");
    Console.WriteLine("");
    Console.Write("Select option: ");
}

User NormalUserMenu(User currentUser)
{
    Console.WriteLine("╔══════════════════════════════════════════════════════════════════╗");
    Console.WriteLine("║                      - STREAMING APP MENU -                      ║");
    Console.WriteLine("╚══════════════════════════════════════════════════════════════════╝");
    Console.WriteLine("╔══════════════════════════════════════════════════════════════════╗");
    Console.WriteLine("║  \u001b[34m[1]\u001b[37m - Show all movies/series available                          ║");
    Console.WriteLine("║  \u001b[34m[2]\u001b[37m - Search movie or series                                    ║");
    Console.WriteLine("║  \u001b[34m[3]\u001b[37m - Search movie or series and show specific details          ║");
    Console.WriteLine("║  \u001b[34m[4]\u001b[37m - Quiz                                                      ║");
    Console.WriteLine("║  \u001b[34m[5]\u001b[37m - Add movie to favouries                                    ║");
    Console.WriteLine("║  \u001b[34m[6]\u001b[37m - Add series to favouries                                   ║");
    Console.WriteLine("║  \u001b[34m[7]\u001b[37m - Show favorites                                            ║");
    Console.WriteLine("║  \u001b[31m[L]\u001b[37m - Logout                                                    ║");
    Console.WriteLine("║  \u001b[31m[0]\u001b[37m - Exit                                                      ║");
    Console.WriteLine("╚══════════════════════════════════════════════════════════════════╝");
    Console.WriteLine("");
    Console.Write("Select option: ");
    var option = Console.ReadLine()!.ToUpper();
    Console.Clear();

    switch (option)
    {
        case "1":
            PrintCategoriesAsync().Wait();
            WaitByClick();
            Console.Clear();
            break;
        case "2":
            SearchMovies();
            WaitByClick();
            Console.Clear();
            break;
        case "3":
            SearchAndDisplayDetailsAsync().Wait();
            WaitByClick();
            Console.Clear();
            break;
        case "4":
            Quiz();
            WaitByClick();
            Console.Clear();
            break;
        case "5":
            AddMovieToFavorites(currentUser).Wait();
            WaitByClick();
            Console.Clear();
            break;
        case "6":
            AddSeriesToFavorites(currentUser).Wait();
            WaitByClick();
            Console.Clear();
            break;
        case "7":
            PrintUserFavorites(currentUser).Wait();
            WaitByClick();
            Console.Clear();
            break;
        case "0":
            exit = true;
            break;
        case "L":
            currentUser = PerformLogin()!;
            break;
        default:
            Console.WriteLine("Choose a valid option.");
            WaitByClick();
            break;
    }
    return currentUser!;
}

User AdminUserMenu(User currentUser)
{
    Console.WriteLine("╔═══════════════════════════════════════════════════════════════════╗");
    Console.WriteLine("║                    - STREAMING APP ADMIN MENU -                   ║");
    Console.WriteLine("╚═══════════════════════════════════════════════════════════════════╝");
    Console.WriteLine("╔═══════════════════════════════════════════════════════════════════╗");
    Console.WriteLine("║  \u001b[34m[1]\u001b[37m  - Show all movies/series available                          ║");
    Console.WriteLine("║  \u001b[34m[2]\u001b[37m  - Search movie or series                                    ║");
    Console.WriteLine("║  \u001b[34m[3]\u001b[37m  - Search movie or series and show specific details          ║");
    Console.WriteLine("║  \u001b[34m[4]\u001b[37m  - Create Movies/Series/Seasons/Episodes/Categories          ║");
    Console.WriteLine("║  \u001b[34m[5]\u001b[37m  - Create Admin                                              ║");
    Console.WriteLine("║  \u001b[34m[6]\u001b[37m  - Create User                                               ║");
    Console.WriteLine("║  \u001b[34m[7]\u001b[37m  - Update User                                               ║");
    Console.WriteLine("║  \u001b[34m[8]\u001b[37m  - Remove User                                               ║");
    Console.WriteLine("║  \u001b[34m[9]\u001b[37m  - Print Users                                               ║");
    Console.WriteLine("║  \u001b[34m[10]\u001b[37m - Print DbPath                                              ║");
    Console.WriteLine("║  \u001b[31m[L]\u001b[37m  - Logout                                                    ║");
    Console.WriteLine("║  \u001b[31m[0]\u001b[37m  - Exit                                                      ║");
    Console.WriteLine("╚═══════════════════════════════════════════════════════════════════╝");
    Console.WriteLine("");
    Console.Write("Select option: ");
    var option = Console.ReadLine()!.ToUpper();
    Console.Clear();

    switch (option)
    {
        case "1":
            PrintCategoriesAsync().Wait();
            WaitByClick();
            Console.Clear();
            break;
        case "2":
            SearchMovies();
            WaitByClick();
            Console.Clear();
            break;
        case "3":
            SearchAndDisplayDetailsAsync().Wait();
            WaitByClick();
            Console.Clear();
            break;
        case "4":
            CreateCategoriesAsyncDinamically();
            Console.Clear();
            break;
        case "5":
            CreateAdminAsync().Wait();
            Console.Clear();
            break;
        case "6":
            CreateUserAsync().Wait();
            break;
        case "7":
            UpdateUser();
            WaitByClick();
            Console.Clear();
            break;
        case "8":
            RemoveUser();
            WaitByClick();
            Console.Clear();
            break;
        case "9":
            PrintUsersAsync();
            WaitByClick();
            Console.Clear();
            break;
        case "10":
            PrintDbPath();
            WaitByClick();
            Console.Clear();
            break;
        case "0":
            exit = true;
            break;
        case "L":
            currentUser = PerformLogin()!;
            break;
        default:
            Console.WriteLine("Choose a valid option.");
            WaitByClick();
            Console.Clear();
            break;
    }
    return currentUser;
}

static void WelcomUserScreen(User currentUser)
{
    Console.WriteLine("╔═════════════════════════════════════╗");
    Console.Write("           Welcome, ");
    Console.ForegroundColor = ConsoleColor.Blue;
    Console.Write($"{currentUser.Username}");
    Console.ResetColor();
    Console.WriteLine("!    ");
    Console.WriteLine("╚═════════════════════════════════════╝");
    WaitByClick();
    Console.Clear();
}


#endregion

#region Register

async Task<User?> PerformRegisterAsync()
{
    string newUsername;
    string newPassword = "";

    using (var uow = new UnitOfWork())
    {
        var userRepository = uow.UserRepository;

        while (true)
        {
            Console.WriteLine("╔════════════════════════════════════════════════════════╗");
            Console.WriteLine("║                     - REGISTER -                       ║");
            Console.WriteLine("╚════════════════════════════════════════════════════════╝");
            Console.Write("Enter a username for the new account: ");
            newUsername = Console.ReadLine()!;

            if (!string.IsNullOrEmpty(newUsername) && newUsername.Length >= 5)
            {
                var existingUser = await userRepository.FindByUsernameAsync(newUsername);
                if (existingUser == null)
                {
                    break;
                }
                else
                {
                    Console.WriteLine("Username already exists. Choose a different username.");
                    WaitByClick();
                    Console.Clear();
                }
            }
            else
            {
                Console.WriteLine("Username must have a minimum of 5 characters.");
                WaitByClick();
                Console.Clear();
            }
        }

        ConsoleKeyInfo key;

        while (true)
        {
            Console.Write("Enter a password for the new account: ");
            int digitCount = 0;

            do
            {
                key = Console.ReadKey(true);

                if (char.IsLetterOrDigit(key.KeyChar))
                {
                    newPassword += key.KeyChar;

                    if (char.IsDigit(key.KeyChar))
                    {
                        digitCount++;
                    }

                    Console.Write("*");
                }
                else if (key.Key == ConsoleKey.Backspace && newPassword.Length > 0)
                {
                    newPassword = newPassword.Substring(0, newPassword.Length - 1);
                    Console.Write("\b \b");

                    if (digitCount > 0 && char.IsDigit(newPassword[newPassword.Length - 1]))
                    {
                        digitCount--;
                    }
                }
            } while (key.Key != ConsoleKey.Enter);

            Console.WriteLine();

            // Check password requirements
            if (newPassword.Length >= 8 && newPassword.Any(char.IsUpper) && digitCount >= 2)
            {
                break;
            }
            else
            {
                Console.WriteLine("Password must have a minimum length of 8 characters, at least one upper case character, and at least two numbers.");
                newPassword = "";
            }
        }

        // Confirm password input
        Console.Write("Confirm password: ");
        string confirmedPassword = ReadPassword();

        if (newPassword == confirmedPassword)
        {
            // Create a new user and save it in the db
            await userRepository.CreateNormalUserAsync(newUsername, newPassword);

            await uow.SaveAsync();

            currentUser = await userRepository.FindByUsernameAsync(newUsername);

            currentUser.UserFavourites = new List<Favourites>(); // Initialize the list if it's null

            currentUser.UserFavourites.ForEach(favorite => favorite.UserId = currentUser.Id);

            Console.WriteLine("Account registered successfully!");
            WaitByClick();
            Console.Clear();
            WelcomUserScreen(currentUser);
            Console.Clear();
        }
        else
        {
            Console.WriteLine("Passwords do not match. Please try again.");
            WaitByClick();
            Console.Clear();
        }
        return currentUser;
    }
}
#endregion

#region Login

User? PerformLogin()
{
    User? currentUser = null;

    while (true)
    {
        Console.WriteLine("╔════════════════════════════════════════════════════════╗");
        Console.WriteLine("║                       - LOGIN -                        ║");
        Console.WriteLine("╚════════════════════════════════════════════════════════╝");

        Console.Write("Enter your username (or enter 'x' and press enter to return to the main menu): ");
        string input = Console.ReadLine()!;

        if (input != null && input.ToLower() == "x")
        {
            Console.Clear();
            break;
        }

        string username = input!;

        Console.Write("Enter your password: ");
        string password = ReadPassword();

        currentUser = GetUserByUsername(username);

        if (currentUser != null && ValidatePassword(password, currentUser.Password))
        {
            Console.Clear();
            WelcomUserScreen(currentUser);
            break;
        }
        else
        {
            Console.WriteLine("Invalid username or password. Please try again.");
            WaitByClick();
            Console.Clear();
        }
    }

    return currentUser;
}

string HashPassword(string password)
{
    var data = Encoding.UTF8.GetBytes(password);

    using (var sha1 = SHA1.Create())
    {
        var hashedData = sha1.ComputeHash(data);
        return BitConverter.ToString(hashedData).Replace("-", "").ToUpper();
    }
}

User GetUserByUsername(string username)
{
    using (var unitOfWork = new UnitOfWork())
    {
        return unitOfWork.UserRepository.FindByUsernameAsync(username).Result;
    }
}

string ReadPassword()
{
    string password = "";
    ConsoleKeyInfo key;
    do
    {
        key = Console.ReadKey(true);

        // Ignore any key that isn't a number or letter
        if (char.IsLetterOrDigit(key.KeyChar))
        {
            password += key.KeyChar;
            Console.Write("*");
        }
        // Remove the last character from the password if backspace is pressed
        else if (key.Key == ConsoleKey.Backspace && password.Length > 0)
        {
            password = password.Substring(0, password.Length - 1);
            Console.Write("\b \b");
        }
    } while (key.Key != ConsoleKey.Enter);

    Console.WriteLine();
    return password;
}

bool ValidatePassword(string enteredPassword, string storedPassword)
{
    var hashedEnteredPassword = HashPassword(enteredPassword);

    return string.Equals(hashedEnteredPassword, storedPassword);
}
#endregion

#region Features
static void WaitByClick()
{
    Console.Write("Press any key to proceed.");
    Console.ReadLine();
}

//CRIAR CATEGORIAS DINAMICAMENTE
static async void CreateCategoriesAsyncDinamically()
{
    var exit = false;

    while (!exit)
    {
        using (var uow = new UnitOfWork())
        {
            Console.WriteLine("╔═══════════════ CREATE ═══════════════╗");
            Console.WriteLine("║ What do you want to create?          ║");
            Console.WriteLine("║ [1] - Movies                         ║");
            Console.WriteLine("║ [2] - Series                         ║");
            Console.WriteLine("║ [3] - Seasons                        ║");
            Console.WriteLine("║ [4] - Episodes                       ║");
            Console.WriteLine("║ [5] - Categories                     ║");
            Console.WriteLine("║ [0] - Exit                           ║");
            Console.WriteLine("╚══════════════════════════════════════╝");
            Console.WriteLine("");
            Console.Write("Option: ");
            var option = Console.ReadLine();

            switch (option)
            {
                case "1":
                    var movie = new Movie();
                    Console.WriteLine("What will be the name of the movie?");
                    Console.Write("Name: ");
                    movie.Name = Console.ReadLine();

                    Console.WriteLine("");
                    Console.Write("Duration (hh:mm:ss): ");
                    if (TimeSpan.TryParse(Console.ReadLine(), out TimeSpan duration))
                    {
                        movie.Duration = duration;

                        Console.WriteLine("Choose the movie category:");

                        // Lista as Categorias
                        var categories = await uow.CategoryRepository.FindAllWithDependenciesAsync();
                        foreach (var category in categories)
                        {
                            Console.WriteLine($"{category.Id}. {category.Name}");
                        }

                        // Pede ao user para escolher uma categoria
                        Console.Write("Choose the category number: ");
                        if (int.TryParse(Console.ReadLine(), out int categoryId))
                        {
                            var selectedCategory = categories.FirstOrDefault(c => c.Id == categoryId);
                            if (selectedCategory != null)
                            {
                                movie.CategoryId = selectedCategory.Id;

                                Console.Write("Choose the movie rating (Rating from 1 to 10): ");
                                if (int.TryParse(Console.ReadLine(), out int rating) && rating >= 1 && rating <= 10)
                                {
                                    movie.Rating = rating;

                                    Console.Write("Enter the movie release date (dd/MM/yyyy): ");
                                    if (DateTime.TryParseExact(Console.ReadLine(), "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime releaseDate))
                                    {
                                        movie.Released = releaseDate;

                                        Console.Write("Enter the movie cast: ");
                                        movie.Cast = Console.ReadLine();

                                        Console.Write("Enter the movie's country of origin: ");
                                        movie.Country = Console.ReadLine();

                                        Console.Write("Enter the movie description: ");
                                        movie.Description = Console.ReadLine();

                                        var existingMovie = await uow.MovieRepository.FindByNameAsync(movie.Name);

                                        if (existingMovie != null)
                                        {
                                            Console.WriteLine("Movie already exists!");
                                            break;
                                        }
                                        else
                                        {

                                            // --------------*Adiciona uma categoria a um movie*---------------
                                            var selectedCat = await uow.CategoryRepository.GetByIdAsync(movie.CategoryId);
                                            selectedCat.Movies.Add(movie);

                                            // ---------------*Adiciona a categoria ao UOW*--------------------
                                            uow.CategoryRepository.Update(selectedCat);
                                            await uow.SaveAsync();

                                            Console.WriteLine("Movie created successfully!");

                                        }

                                    }
                                    else
                                    {
                                        Console.WriteLine("Invalid format for release date. Please enter in 'dd/MM/yyyy' format.");
                                    }
                                }
                                else
                                {
                                    Console.WriteLine("Invalid rating. The rating should be a number between 1 and 10.");
                                }


                            }
                            else
                            {
                                Console.WriteLine("Invalid category.");
                            }
                        }
                        else
                        {
                            Console.WriteLine("Invalid input for category number.");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Invalid format for duration. Please enter in 'hh:mm:ss' format.");
                    }


                    break;
                case "2":
                    var series = new SeriesClass();
                    Console.WriteLine("What will be the name of the series?");
                    Console.Write("Name: ");
                    series.Name = Console.ReadLine();

                    Console.Write("");
                    Console.WriteLine("What would be the category for the series?");

                    // Lista as Categorias
                    var categoriesForSeries = await uow.CategoryRepository.FindAllWithDependenciesAsync();
                    foreach (var categoryForSeries in categoriesForSeries)
                    {
                        Console.WriteLine($"{categoryForSeries.Id}. {categoryForSeries.Name}");
                    }

                    // Pede ao user para escolher uma categoria para as series
                    Console.Write("Choose the category number: ");
                    if (int.TryParse(Console.ReadLine(), out int seriesCategoryId))
                    {
                        var selectedSeriesCategory = categoriesForSeries.FirstOrDefault(c => c.Id == seriesCategoryId);
                        if (selectedSeriesCategory != null)
                        {
                            series.CategoryId = selectedSeriesCategory.Id;

                            Console.Write("Choose the series rating (Rating from 1 to 10): ");
                            if (int.TryParse(Console.ReadLine(), out int seriesRating) && seriesRating >= 1 && seriesRating <= 10)
                            {
                                series.Rating = seriesRating;

                                Console.Write("Enter the series release date (dd/MM/yyyy): ");
                                if (DateTime.TryParseExact(Console.ReadLine(), "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime seriesReleaseDate))
                                {
                                    series.Released = seriesReleaseDate;

                                    Console.Write("Enter the series cast: ");
                                    series.Cast = Console.ReadLine();

                                    Console.Write("Enter the series' country of origin: ");
                                    series.Country = Console.ReadLine();

                                    Console.Write("Enter the series description: ");
                                    series.Description = Console.ReadLine();


                                    var existingSeries = await uow.SeriesRepository.FindByNameAsync(series.Name);


                                    if (existingSeries != null)
                                    {
                                        Console.WriteLine("Serie already exists!");
                                        break;
                                    }
                                    else
                                    {

                                        // -----------*Adicioa as series a um determinada categoria*------------
                                        var selectedSeriesCategoryToUpdate = await uow.CategoryRepository.GetByIdAsync(series.CategoryId);
                                        selectedSeriesCategoryToUpdate.Series.Add(series);

                                        // ------------*Adiciona a categoria ao UOW*-----------
                                        uow.CategoryRepository.Update(selectedSeriesCategoryToUpdate);
                                        await uow.SaveAsync();

                                        Console.WriteLine("Series created successfully!");


                                    }

                                }
                                else
                                {
                                    Console.WriteLine("Invalid format for release date. Please enter in 'dd/MM/yyyy' format.");
                                }
                            }
                            else
                            {
                                Console.WriteLine("Invalid rating. The rating should be a number between 1 and 10.");
                            }
                        }
                        else
                        {
                            Console.WriteLine("Invalid category.");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Invalid input for category number.");
                    }

                    break;
                case "3":
                    Console.WriteLine("Choose the series for which you want to manage seasons:");

                    // Lista as series
                    var seriesForSeasons = await uow.SeriesRepository.FindAllAsync();
                    foreach (var serieForSeasons in seriesForSeasons)
                    {
                        Console.WriteLine($"{serieForSeasons.Id}. {serieForSeasons.Name}");
                    }

                    // Pede ao user para escolher uma serie
                    Console.Write("Choose the series number: ");
                    if (int.TryParse(Console.ReadLine(), out int selectedSeriesForSeasonsId))
                    {
                        var selectedSeriesForSeasons = seriesForSeasons.FirstOrDefault(s => s.Id == selectedSeriesForSeasonsId);
                        if (selectedSeriesForSeasons != null)
                        {
                            Console.WriteLine($"Gerenciar Temporadas para a Série: {selectedSeriesForSeasons.Name}");

                            Console.WriteLine("1. Create New Season");
                            Console.WriteLine("2. Add Existing Season");
                            Console.Write("Choose an option: ");

                            var seasonOption = Console.ReadLine();

                            switch (seasonOption)
                            {
                                case "1":
                                    // -----------------*CRIA uma nova season*---------------------
                                    var newSeason = new Season();
                                    Console.WriteLine("Enter the number of the new season: ");
                                    if (int.TryParse(Console.ReadLine(), out int newSeasonNumber))
                                    {
                                        newSeason.Number = newSeasonNumber;


                                        selectedSeriesForSeasons.Seasons.Add(newSeason);

                                        // ------------*Adiciona as series ao UOW*-----------
                                        uow.SeriesRepository.Update(selectedSeriesForSeasons);
                                        await uow.SaveAsync();

                                        Console.WriteLine("New season created with success!");
                                    }
                                    else
                                    {
                                        Console.WriteLine("Season number invalid.");
                                    }
                                    break;

                                case "2":
                                    // ------------------*Lista seasons existentes*------------------

                                    var SeasonList = await uow.SeasonRepository.FindAllWithDependenciesAsync();

                                    Console.WriteLine("Existing seasons:");
                                    foreach (var existingSeason in SeasonList)
                                    {
                                        Console.WriteLine($"{existingSeason.Id}. Temporada {existingSeason.Number}");
                                    }

                                    // Pede ao user para escolher uma season existente
                                    Console.Write("Choose the number of the existing season: ");
                                    if (int.TryParse(Console.ReadLine(), out int existingSeasonId))
                                    {
                                        var selectedExistingSeason = SeasonList.FirstOrDefault(s => s.Id == existingSeasonId);
                                        if (selectedExistingSeason != null)
                                        {
                                            // Adiciona uma season existente á série selecionada
                                            selectedSeriesForSeasons.Seasons.Add(selectedExistingSeason);

                                            // ------------*Adiciona as series ao UOW*-----------
                                            uow.SeriesRepository.Update(selectedSeriesForSeasons);
                                            await uow.SaveAsync();

                                            Console.WriteLine("Existing season added successfully!");
                                        }
                                        else
                                        {
                                            Console.WriteLine("Invalid existing season.");
                                        }
                                    }
                                    else
                                    {
                                        Console.WriteLine("Invalid input for the existing season number.");
                                    }
                                    break;

                                default:
                                    Console.WriteLine("Invalid option.");
                                    break;
                            }
                        }
                        else
                        {
                            Console.WriteLine("Invalid series.");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Invalid input for the series number.");
                    }
                    break;
                case "4":
                    // List the available series
                    Console.WriteLine("Choose the series to which you want to add an episode:");
                    var seriesForEpisodes = await uow.SeriesRepository.FindAllWithDependenciesAsync();
                    foreach (var seriesForEp in seriesForEpisodes)
                    {
                        Console.WriteLine($"{seriesForEp.Id}. {seriesForEp.Name}");
                    }

                    // Prompt the user to choose a series
                    Console.Write("Choose the series number: ");
                    if (int.TryParse(Console.ReadLine(), out int selectedSeriesForEpisodesId))
                    {
                        // Explicitly load the seasons for the selected series
                        var selectedSeriesForEpisodes = seriesForEpisodes.FirstOrDefault(s => s.Id == selectedSeriesForEpisodesId);
                        if (selectedSeriesForEpisodes != null)
                        {
                            // List seasons for the selected series
                            Console.WriteLine($"Available Seasons for Series '{selectedSeriesForEpisodes.Name}':");
                            foreach (var seasonForEpisodes in selectedSeriesForEpisodes.Seasons)
                            {
                                Console.WriteLine($"{seasonForEpisodes.Id}. Season {seasonForEpisodes.Number}");
                            }

                            // Prompt the user to choose a season
                            Console.Write("Choose the season number: ");
                            if (int.TryParse(Console.ReadLine(), out int selectedSeasonForEpisodesId))
                            {
                                var selectedSeasonForEpisodes = selectedSeriesForEpisodes.Seasons.FirstOrDefault(s => s.Id == selectedSeasonForEpisodesId);
                                if (selectedSeasonForEpisodes != null)
                                {
                                    // Create a new episode
                                    var newEpisode = new Episodes();
                                    Console.Write("Enter the name of the episode: ");
                                    newEpisode.Name = Console.ReadLine();

                                    // Add the episode to the selected season
                                    selectedSeasonForEpisodes.Episodes.Add(newEpisode);

                                    // Update the series in the UOW
                                    uow.SeriesRepository.Update(selectedSeriesForEpisodes);
                                    await uow.SaveAsync();

                                    Console.WriteLine("Episode added successfully!");
                                }
                                else
                                {
                                    Console.WriteLine("Invalid season.");
                                }
                            }
                            else
                            {
                                Console.WriteLine("Invalid input for the season number.");
                            }
                        }
                        else
                        {
                            Console.WriteLine("Invalid series.");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Invalid input for the series number.");
                    }
                    break;


                case "5":
                    var Categories = new Category();
                    Console.Write("Enter the category name: ");
                    Categories.Name = Console.ReadLine();


                    var existingCategory = await uow.CategoryRepository.FindByNameAsync(Categories.Name);

                    if (existingCategory != null)
                    {
                        Console.WriteLine("Category with the same name already exists. Choose a different name or update the existing category.");
                    }
                    else
                    {
                        // If the category does not exist, add it to the Uow
                        uow.CategoryRepository.Create(Categories);
                        await uow.SaveAsync();
                        Console.WriteLine("Category created successfully!");
                    }
                    break;

                case "0":
                    exit = true;
                    break;
                default:
                    Console.WriteLine("Choose a valid option.");
                    WaitByClick();
                    break;
            }
        }

    }
}

//PRINT FILMES
static async Task PrintMoviesAsync()
{
    using (var uow = new UnitOfWork())
    {

        var listOfCategoriesMovies = await uow.CategoryRepository.FindAllWithDependenciesAsync();

        Console.WriteLine("╔══════════ MOVIES AVAILABLE ══════════╗");

        foreach (var categoria in listOfCategoriesMovies)
        {
            foreach (var movie in categoria.Movies)
            {
                Console.WriteLine($"- {movie.Name}");
            }
        }
        Console.WriteLine("╚══════════════════════════════════════╝");
    }
}

//PRINT SERIES
static async Task PrintSeriesAsync()
{
    using (var uow = new UnitOfWork())
    {

        Console.WriteLine("╔══════════ SERIES AVAILABLE ══════════╗");
        var listOfCategoriesSeries = await uow.CategoryRepository.FindAllWithDependenciesAsync();
        foreach (var categoria in listOfCategoriesSeries)
        {
            foreach (var series in categoria.Series)
            {
                Console.WriteLine($" - {series.Name}");
            }
        }
        Console.WriteLine("╚══════════════════════════════════════╝");
    }
}

//PRINT CATEGORIAS / SEASONS / EPISODIOS / FILMES / SERIES
static async Task PrintCategoriesAsync()
{
    using (var uow = new UnitOfWork())
    {

        var listOfCategoriesMovies = await uow.CategoryRepository.FindAllWithDependenciesAsync();

        Console.WriteLine("╔═════════════════════════════════════╗");
        Console.WriteLine("║             - MOVIES -              ║");
        Console.WriteLine("╚═════════════════════════════════════╝");
        foreach (var categoria in listOfCategoriesMovies)
        {
            foreach (var movie in categoria.Movies)
            {
                Console.WriteLine($"");
                Console.WriteLine($" - Title: {movie.Name}");
                Console.WriteLine($" Category: {movie.Category}");
                Console.WriteLine($" Cast : {movie.Cast}");
                Console.WriteLine($" Discription: {movie.Description}");
                Console.WriteLine($" Rating: {movie.Rating}");
            }
        }

        Console.WriteLine($"");
        Console.WriteLine("╔═════════════════════════════════════╗");
        Console.WriteLine("║             - SERIES -              ║");
        Console.WriteLine("╚═════════════════════════════════════╝");
        var listOfCategoriesSeries = await uow.CategoryRepository.FindAllWithDependenciesAsync();
        foreach (var categoria in listOfCategoriesSeries)
        {
            foreach (var series in categoria.Series)
            {
                Console.WriteLine($"");
                Console.WriteLine($" - Title: {series.Name}");
                Console.WriteLine($"Category:{series.Category}");
                Console.WriteLine($"Cast : {series.Cast}");
                Console.WriteLine($"Discription: {series.Description}");
                Console.WriteLine($"Rating: {series.Rating}");
            }
        }

        Console.WriteLine($"");
        Console.WriteLine("╔═════════════════════════════════════╗");
        Console.WriteLine("║         - SERIES SEASONS -          ║");
        Console.WriteLine("╚═════════════════════════════════════╝");
        var listOfSeriesSeasons = await uow.SeriesRepository.FindAllWithDependenciesAsync();
        Console.WriteLine($"");

        foreach (var series in listOfSeriesSeasons)
        {
            Console.WriteLine($" - {series}");

            foreach (var season in series.Seasons)
            {
                int episodeCount = await uow.SeasonRepository.CountNumberAsync(season.Id);
                Console.WriteLine($"   -- Season {season.Number} - Number of Episodes: {episodeCount}");
            }
        }
    }
}

//CREATE ADMIN
static async Task CreateAdminAsync()
{
    Console.WriteLine("╔═════════════════════════════════════╗");
    Console.WriteLine("║          - CREATE ADMIN -           ║");
    Console.WriteLine("╚═════════════════════════════════════╝");

    Console.WriteLine("");

    while (true)
    {
        Console.Write("Enter admin username (or enter 'x' and press enter to return to the admin menu): ");
        var usernameInput = Console.ReadLine();

        if (usernameInput?.ToLower() == "x")
        {
            Console.Clear();
            return;
        }

        var username = usernameInput;

        if (username.Length < 5)
        {
            Console.WriteLine("Username must have a minimum length of 5 characters. Please try again.");
            continue;
        }

        using (var uow = new UnitOfWork())
        {
            var existingUser = await uow.UserRepository.FindByUsernameAsync(username);
            if (existingUser == null)
            {
                string password = "";
                string confirmPassword = "";

                while (true)
                {
                    int digitCount = 0;

                    Console.Write("Enter admin password: ");
                    ConsoleKeyInfo key;

                    do
                    {
                        key = Console.ReadKey(true);

                        if (char.IsLetterOrDigit(key.KeyChar))
                        {
                            password += key.KeyChar;

                            if (char.IsDigit(key.KeyChar))
                            {
                                digitCount++;
                            }

                            Console.Write("*");
                        }
                        else if (key.Key == ConsoleKey.Backspace && password.Length > 0)
                        {
                            password = password.Substring(0, password.Length - 1);
                            Console.Write("\b \b");

                            if (digitCount > 0 && char.IsDigit(password[password.Length - 1]))
                            {
                                digitCount--;
                            }
                        }
                    } while (key.Key != ConsoleKey.Enter);

                    Console.WriteLine();

                    if (password.Length >= 8 && password.Any(char.IsUpper) && digitCount >= 2)
                    {
                        while (true)
                        {
                            Console.Write("Confirm admin password: ");
                            ConsoleKeyInfo confirmKey;

                            do
                            {
                                confirmKey = Console.ReadKey(true);

                                if (char.IsLetterOrDigit(confirmKey.KeyChar))
                                {
                                    confirmPassword += confirmKey.KeyChar;
                                    Console.Write("*");
                                }
                                else if (confirmKey.Key == ConsoleKey.Backspace && confirmPassword.Length > 0)
                                {
                                    confirmPassword = confirmPassword.Substring(0, confirmPassword.Length - 1);
                                    Console.Write("\b \b");
                                }
                            } while (confirmKey.Key != ConsoleKey.Enter);

                            Console.WriteLine();

                            if (confirmPassword == password)
                            {
                                var admin = new User { Username = username, Password = password, IsAdmin = true };
                                uow.UserRepository.Create(admin);
                                await uow.SaveAsync();
                                Console.WriteLine("Admin created successfully!");
                                WaitByClick();
                                Console.Clear();
                                return;
                            }
                            else
                            {
                                Console.WriteLine("Passwords do not match. Please try again.");
                                confirmPassword = ""; // Reset confirmPassword for re-entry
                            }
                        }
                    }
                    else
                    {
                        Console.WriteLine("Password must have a minimum length of 8 characters, at least one upper case character, and at least two numbers.");
                        password = "";
                    }
                }
            }
            else
            {
                Console.WriteLine("Admin with the same username already exists.");
                WaitByClick();
                Console.Clear();
            }
        }
    }
}

//CREATE USER
static async Task CreateUserAsync()
{
    string newUsername;
    string newPassword = "";
    string confirmPassword = "";

    using (var uow = new UnitOfWork())
    {
        var userRepository = uow.UserRepository;

        while (true)
        {
            Console.WriteLine("╔═════════════════════════════════════╗");
            Console.WriteLine("║           - CREATE USER -           ║");
            Console.WriteLine("╚═════════════════════════════════════╝");
            Console.WriteLine("");

            Console.Write("Enter username for the new user (or enter 'x' and press enter to return to the main menu): ");
            newUsername = Console.ReadLine()!;

            if (newUsername?.ToLower() == "x")
            {
                Console.Clear();
                return;
            }

            if (newUsername != null && newUsername.Length >= 5)
            {
                var existingUser = await userRepository.FindByUsernameAsync(newUsername);
                if (existingUser == null)
                {
                    break;
                }
                else
                {
                    Console.WriteLine("Username already exists. Choose a different username.");
                    WaitByClick();
                    Console.Clear();
                }
            }
            else
            {
                Console.WriteLine("Username must have a minimum of 5 characters.");
                WaitByClick();
                Console.Clear();
                continue;
            }
        }

        ConsoleKeyInfo key;

        while (true)
        {
            Console.Write("Enter password for the new user: ");
            int digitCount = 0;

            do
            {
                key = Console.ReadKey(true);

                if (char.IsLetterOrDigit(key.KeyChar))
                {
                    newPassword += key.KeyChar;

                    if (char.IsDigit(key.KeyChar))
                    {
                        digitCount++;
                    }

                    Console.Write("*");
                }
                else if (key.Key == ConsoleKey.Backspace && newPassword.Length > 0)
                {
                    newPassword = newPassword.Substring(0, newPassword.Length - 1);
                    Console.Write("\b \b");

                    if (digitCount > 0 && char.IsDigit(newPassword[newPassword.Length - 1]))
                    {
                        digitCount--;
                    }
                }
            } while (key.Key != ConsoleKey.Enter);

            Console.WriteLine();

            if (newPassword.Length >= 8 && newPassword.Any(char.IsUpper) && digitCount >= 2)
            {
                break;
            }
            else
            {
                Console.WriteLine("Password must have a minimum length of 8 characters, at least one upper case character, and at least two numbers.");
                newPassword = "";
            }
        }

        while (true)
        {
            Console.Write("Confirm password: ");
            StringBuilder confirmPasswordBuilder = new StringBuilder();

            do
            {
                key = Console.ReadKey(true);

                if (char.IsLetterOrDigit(key.KeyChar))
                {
                    confirmPasswordBuilder.Append(key.KeyChar);
                    Console.Write("*");
                }
                else if (key.Key == ConsoleKey.Backspace && confirmPasswordBuilder.Length > 0)
                {
                    confirmPasswordBuilder.Remove(confirmPasswordBuilder.Length - 1, 1);
                    Console.Write("\b \b");
                }
            } while (key.Key != ConsoleKey.Enter);

            confirmPassword = confirmPasswordBuilder.ToString();
            Console.WriteLine();

            if (confirmPassword == newPassword)
            {
                break;
            }
            else
            {
                Console.WriteLine("Passwords do not match. Please try again.");
                WaitByClick();
                Console.Clear();
            }
        }

        await userRepository.CreateNormalUserAsync(newUsername, newPassword);
        await uow.SaveAsync();

        Console.WriteLine("Normal user created!");
        WaitByClick();
        Console.Clear();
    }
}

//REMOVE USER
static void RemoveUser()
{
    Console.WriteLine("╔═════════════════════════════════════╗");
    Console.WriteLine("║           - REMOVE USER -           ║");
    Console.WriteLine("╚═════════════════════════════════════╝");

    Console.WriteLine("");

    Console.Write("Do you want to go back? (Y/N): ");
    string goBackChoice = Console.ReadLine()!.ToUpper();

    if (goBackChoice == "Y")
    {
        return;
    }
    else if (goBackChoice == "N")
    {
        using (var uow = new UnitOfWork())
        {
            PrintUsersAsync();

            Console.Write("Enter username to remove: ");
            var usernameToRemove = Console.ReadLine();

            var userToRemove = uow.UserRepository.FindByUsernameAsync(usernameToRemove).Result;

            if (userToRemove != null)
            {
                if (userToRemove.IsAdmin)
                {
                    while (true)
                    {
                        Console.Write($"This user is an admin, are you sure you want to delete user '{usernameToRemove}' (y/n) ?");
                        var confirmDeletion = Console.ReadLine();

                        if (confirmDeletion?.ToLower() == "y")
                        {
                            Console.Write($"Confirm deletion of admin '{usernameToRemove}' by entering the username: ");
                            var confirmUsername = Console.ReadLine();

                            if (confirmUsername == usernameToRemove)
                            {
                                uow.UserRepository.RemoveUserAsync(usernameToRemove).Wait();
                                Console.WriteLine($"User '{usernameToRemove}' removed successfully.");
                                break;
                            }
                            else
                            {
                                Console.WriteLine("Entered username does not match. Deletion canceled.");
                                break;
                            }
                        }
                        else if (confirmDeletion?.ToLower() == "n")
                        {
                            Console.WriteLine("Deletion canceled.");
                            break;
                        }
                        else
                        {
                            Console.Write("Please insert a valid option (y/n)\n");
                        }
                    }
                }
                else
                {
                    Console.Write($"Are you sure you want to delete user '{usernameToRemove}'? (y/n): ");
                    var confirmDeletion = Console.ReadLine();

                    if (confirmDeletion?.ToLower() == "y")
                    {
                        uow.UserRepository.RemoveUserAsync(usernameToRemove).Wait();
                        Console.WriteLine($"User '{usernameToRemove}' removed successfully.");
                    }
                    else if (confirmDeletion?.ToLower() == "n")
                    {
                        Console.WriteLine("Deletion canceled.");
                    }
                    else
                    {
                        Console.Write("Please insert a valid option (y/n)\n");
                    }
                }
            }
            else
            {
                Console.WriteLine($"User with username '{usernameToRemove}' not found.");
            }
        }
    }
    else
    {
        Console.WriteLine("Invalid choice. Please enter 'Y' or 'N'.");
    }
}

//UPDATE USER
static void UpdateUser()
{
    Console.WriteLine("╔═════════════════════════════════════╗");
    Console.WriteLine("║           - UPDATE USER -           ║");
    Console.WriteLine("╚═════════════════════════════════════╝");

    Console.WriteLine("");

    Console.Write("Do you want to go back? (Y/N): ");
    string goBackChoice = Console.ReadLine()!.ToUpper();

    if (goBackChoice == "Y")
    {
        return;
    }
    else if (goBackChoice == "N")
    {
        using (var uow = new UnitOfWork())
        {
            PrintUsersAsync();

            Console.Write("Enter the username to update: ");
            var usernameToUpdate = Console.ReadLine();

            var userToUpdate = uow.UserRepository.FindByUsernameAsync(usernameToUpdate).Result;

            if (userToUpdate != null)
            {
                string newUsername = "";
                string newPassword = "";

                while (true)
                {
                    Console.Write("Enter the new username: ");
                    newUsername = Console.ReadLine()!;

                    if (newUsername?.Length >= 5)
                    {
                        var existingUser = uow.UserRepository.FindByUsernameAsync(newUsername).Result;
                        if (existingUser == null || existingUser.Username == usernameToUpdate)
                        {
                            break;
                        }
                        else
                        {
                            Console.WriteLine("Username already exists. Choose a different username.");
                            WaitByClick();
                            Console.Clear();
                        }
                    }
                    else
                    {
                        Console.WriteLine("Username must have a minimum of 5 characters.");
                    }
                }

                while (true)
                {
                    Console.Write("Enter the new password: ");
                    StringBuilder newPasswordBuilder = new StringBuilder();
                    int digitCount = 0;

                    ConsoleKeyInfo key;
                    do
                    {
                        key = Console.ReadKey(true);

                        if (char.IsLetterOrDigit(key.KeyChar))
                        {
                            newPasswordBuilder.Append(key.KeyChar);

                            if (char.IsDigit(key.KeyChar))
                            {
                                digitCount++;
                            }

                            Console.Write("*");
                        }
                        else if (key.Key == ConsoleKey.Backspace && newPasswordBuilder.Length > 0)
                        {
                            newPasswordBuilder.Remove(newPasswordBuilder.Length - 1, 1);
                            Console.Write("\b \b");

                            if (digitCount > 0 && char.IsDigit(newPasswordBuilder[newPasswordBuilder.Length - 1]))
                            {
                                digitCount--;
                            }
                        }
                    } while (key.Key != ConsoleKey.Enter);

                    newPassword = newPasswordBuilder.ToString();
                    Console.WriteLine();

                    if (newPassword.Length >= 8 && newPassword.Any(char.IsUpper) && digitCount >= 2)
                    {
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Password must have a minimum length of 8 characters, at least one upper case character, and at least two numbers.");
                    }
                }

                uow.UserRepository.UpdateUserAsync(usernameToUpdate, newUsername, newPassword).Wait();
                uow.SaveAsync().Wait();
                Console.WriteLine($"User '{usernameToUpdate}' updated successfully.");
            }
            else
            {
                Console.WriteLine($"User with username '{usernameToUpdate}' not found.");
            }
        }
    }
    else
    {
        Console.WriteLine("Invalid choice. Please enter 'Y' or 'N'.");
    }
}

//PRINTAR TODOS OS USERS REGISTADOS
static async void PrintUsersAsync()
{
    using (var uow = new UnitOfWork())
    {
        var userRepository = uow.UserRepository;
        var users = await userRepository.FindAllAsync();

        Console.WriteLine("╔═══════════════ Users ════════════════");

        foreach (var user in users)
        {
            Console.WriteLine($" Username: {user.Username,-10}, Admin: {user.IsAdmin}");
        }

        Console.WriteLine("╚══════════════════════════════════════");
    }
}

//QUIZ
static async void Quiz()
{
    Console.WriteLine(" ════════════════ WELCOME ════════════════ ");
    Console.WriteLine("Let's help you choose what you want to watch.");
    Console.WriteLine("Through a few questions, we will select");
    Console.WriteLine("        the best movies or series    ");
    Console.WriteLine("           for you to watch.        ");
    Console.WriteLine("");
    Console.Write("         ||Press Enter to continue||       ");
    Console.ReadLine();

    Console.WriteLine("What type of format do you want to watch?");

    Console.WriteLine("1. Movies");
    Console.WriteLine("2. Series");

    Console.Write("Choose an option (1 or 2): ");
    var userChoice = Console.ReadLine();

    switch (userChoice)
    {
        case "1":
            using (var uow = new UnitOfWork())
            {
                Console.WriteLine("What would be the ideal duration?");
                Console.Write("Enter the duration [hh:mm]: ");
                var durationInput = Console.ReadLine();

                if (TimeSpan.TryParseExact(durationInput, "hh\\:mm", CultureInfo.InvariantCulture, out TimeSpan duration))
                {
                    Console.WriteLine("What would be the mininal rating?");
                    Console.Write("Enter the rating: ");
                    var movieRatingInput = Console.ReadLine();

                    if (int.TryParse(movieRatingInput, out int movieRating))
                    {
                        var listOfRatingMovies = uow.MovieRepository.FindAllByRatingAsync(movieRating);

                        Console.WriteLine("What would be the movie category?");

                        var listOfCatMovies = await uow.CategoryRepository.FindAllWithDependenciesAsync();

                        foreach (var categoria in listOfCatMovies)
                        {

                            Console.WriteLine($" --{categoria.Name}");
                        }
                        Console.Write("Enter the category name: ");
                        var movieType = Console.ReadLine();
                        var listOfCategoriesMovies = uow.CategoryRepository.FindAllByNameStartedWithAsyncMovieSeries(movieType).Result;

                        foreach (var movieCtg in listOfCategoriesMovies)
                        {
                            Console.WriteLine("════════════════ MOVIES ════════════════");
                            foreach (var movie in movieCtg.Movies)
                            {
                                if (movie.Duration <= duration && listOfRatingMovies != null)
                                {
                                    Console.WriteLine($"| --- {movie.Name}, Rating: {movie.Rating}");
                                }
                            }
                        }
                    }
                    else
                    {
                        Console.WriteLine("Invalid format for rating. Please enter a valid integer.");
                    }
                }
                else
                {
                    Console.WriteLine("Invalid format for duration. Please enter in 'hh:mm' format.");
                }
            }
            break;

        case "2":
            using (var uow = new UnitOfWork())
            {
                Console.WriteLine("What would be the ideal duration (hh:mm)?");
                Console.Write("Enter the duration: ");
                var durationInput = Console.ReadLine();

                if (TimeSpan.TryParseExact(durationInput, "hh\\:mm", CultureInfo.InvariantCulture, out TimeSpan duration))
                {

                    Console.WriteLine("What would be the mininal rating?");
                    Console.Write("Enter the rating: ");
                    var serieRatingInput = Console.ReadLine();

                    if (int.TryParse(serieRatingInput, out int serieRating))
                    {
                        var listOfRatingSeries = uow.SeriesRepository.FindAllByRatingAsync(serieRating);

                        Console.WriteLine("What would be the series category?");

                        var listOfCatSeries = await uow.CategoryRepository.FindAllWithDependenciesAsync();

                        foreach (var categoria in listOfCatSeries)
                        {

                            Console.WriteLine($" --{categoria.Name}");
                        }
                        Console.Write("Enter the category name: ");
                        var seriesType = Console.ReadLine();
                        var listOfCategoriesSeries = uow.CategoryRepository.FindAllByNameStartedWithAsyncMovieSeries(seriesType).Result;

                        foreach (var seriesCtg in listOfCategoriesSeries)
                        {
                            Console.WriteLine("════════════════ SERIES ════════════════");
                            foreach (var series in seriesCtg.Series)
                            {
                                if (series.Duration <= duration && listOfRatingSeries != null)
                                {
                                    Console.WriteLine($"| --- {series.Name}, Rating: {series.Rating}");
                                }
                            }
                        }

                    }
                    else
                    {
                        Console.WriteLine("Invalid format for rating. Please enter a valid integer.");
                    }
                }
                else
                {
                    Console.WriteLine("Invalid format for duration. Please enter in 'hh:mm' format.");
                }
            }
            break;

        default:
            Console.WriteLine("Invalid option. Please choose 1 for Movies or 2 for Series.");
            break;
    }
}


// PESQUISA POR FILMES E MOSTRA OS DETALHES
static async Task SearchAndDisplayDetailsAsync()
{
    Console.Write("Enter the movie/series name to search(in lower case): ");

    var searchTerm = Console.ReadLine();
    if (!string.IsNullOrEmpty(searchTerm))
    {
        // Convert the first character to uppercase
        searchTerm = char.ToUpper(searchTerm[0]) + searchTerm.Substring(1);

        Console.WriteLine("Transformed string: " + searchTerm);
    }
    else
    {
        Console.WriteLine("The input string is empty.");
    }

    using (var uow = new UnitOfWork())
    {
        var movies = await uow.MovieRepository.FindAllByNameStartedWithAsync(searchTerm);
        var series = await uow.SeriesRepository.FindAllByNameStartedWithAsync(searchTerm);

        Console.WriteLine("════════════════ Search Results ════════════════");

        if (movies.Any() || series.Any())
        {
            Console.WriteLine("Movies:");

            foreach (var movie in movies)
            {
                Console.WriteLine($"  {movie.Name}");
            }

            Console.WriteLine("\nSeries:");

            foreach (var serie in series)
            {
                Console.WriteLine($"  {serie.Name}");
            }

            Console.Write("Enter the name of the movie/series to view details: ");
            var selectedName = Console.ReadLine();

            var selectedMovie = movies.FirstOrDefault(m => m.Name.Equals(selectedName, StringComparison.OrdinalIgnoreCase));
            var selectedSerie = series.FirstOrDefault(s => s.Name.Equals(selectedName, StringComparison.OrdinalIgnoreCase));

            if (selectedMovie != null)
            {
                DisplayMovieDetails(selectedMovie);
            }
            else if (selectedSerie != null)
            {
                DisplaySerieDetails(selectedSerie);
            }
            else
            {
                Console.WriteLine($"No movie or series found with the name '{selectedName}'.");
            }
        }
        else
        {
            Console.WriteLine($"No results found for '{searchTerm}'.");
        }

        Console.WriteLine("══════════════════════════════════════════════════");
    }
}

static async void DisplaySerieDetails(SeriesClass series)
{
    using (var uow = new UnitOfWork())

    {

        var category = await uow.CategoryRepository.FindByIdAsync(series.CategoryId);


        Console.WriteLine($"\nDetails for Series: {series.Name}");
        Console.WriteLine($"Category: {category.Name}");
        Console.WriteLine($"Rating: {series.Rating}/10");
        Console.WriteLine($"Cast: {series.Cast}");
        Console.WriteLine($"Description: {series.Description}");
        Console.WriteLine($"Released: {series.Released.ToShortDateString()}");
        Console.WriteLine($"Duration: {FormatTimeSpan(series.Duration)}");

        var seriesList = await uow.SeriesRepository.FindAllByNameStartedWithAsync(series.Name);

        foreach (var s in seriesList)
        {
            foreach (var season in s.Seasons)
            {
                int episodeCount = await uow.SeasonRepository.CountNumberAsync(season.Id);
                Console.WriteLine($"- Season {season.Number} - Number of Episodes: {episodeCount}");
            }
        }
    }
}

static async void DisplayMovieDetails(Movie movie)
{
    using (var uow = new UnitOfWork())

    {
        var category = await uow.CategoryRepository.FindByIdAsync(movie.CategoryId);

        Console.WriteLine($"\nDetails for Movie: {movie.Name}");
        Console.WriteLine($"Category: {category.Name}");
        Console.WriteLine($"Rating: {movie.Rating}/10");
        Console.WriteLine($"Cast: {movie.Cast}");
        Console.WriteLine($"Description: {movie.Description}");
        Console.WriteLine($"Released: {movie.Released.ToShortDateString()}");
        Console.WriteLine($"Duration: {FormatTimeSpan(movie.Duration)}");
    }
}
static string FormatTimeSpan(TimeSpan timeSpan)
{
    return $"{(int)timeSpan.TotalHours:D2}:{timeSpan.Minutes:D2}:{timeSpan.Seconds:D2}";
}

//----------------------------------------------------------------------------------------------------------------

//PESQUISAR FILMES / SERIES
static void SearchMovies()
{
    Console.WriteLine("╔═════════════════════════════════════╗");
    Console.WriteLine("║     - SEARCH SERIES / MOVIES -      ║");
    Console.WriteLine("╚═════════════════════════════════════╝");
    Console.WriteLine("");
    Console.Write("Enter the movie/serie name to search in (lower case): ");
    Console.WriteLine("");
    var searchTerm = Console.ReadLine();
    if (!string.IsNullOrEmpty(searchTerm))
    {
        // Convert the first character to uppercase
        searchTerm = char.ToUpper(searchTerm[0]) + searchTerm.Substring(1);

        Console.WriteLine("Transformed string: " + searchTerm);
    }
    else
    {
        Console.WriteLine("The input string is empty.");
    }

    using (var uow = new UnitOfWork())
    {
        var movies = uow.MovieRepository.FindAllByNameStartedWithAsync(searchTerm).Result;

        Console.WriteLine("╔═══════════════ Search Results ═══════════════╗");

        foreach (var movie in movies)
        {
            Console.WriteLine($" Movie: {movie.Name}");
        }
        Console.Write("\n");
        var series = uow.SeriesRepository.FindAllByNameStartedWithAsync(searchTerm).Result;

        foreach (var serie in series)
        {
            Console.WriteLine($" Serie: {serie.Name}");
        }

        Console.WriteLine("╚══════════════════════════════════════════════╝");
    }
}

//PRINT DB PATH
static void PrintDbPath()
{
    Console.WriteLine("╔═════════════════════════════════════╗");
    Console.WriteLine("║              - DB PATH -            ║");
    Console.WriteLine("╚═════════════════════════════════════╝");

    using (var uow = new UnitOfWork())
    {
        Console.WriteLine($"Path to Database: {uow._dbContext.DbPath}");
        Console.WriteLine("");
    }
}

//CRIAR ADMIN E USER AUTOMATICAMENTE
async Task SeedInitialData()
{
    using (var uow = new UnitOfWork())
    {
        // Creating admin user
        var admin = await uow.UserRepository.CreateNormalUserAsync("admin01", "Xyz1234567");
        admin.IsAdmin = true;

        // Creating normal user
        var user = await uow.UserRepository.CreateNormalUserAsync("user01", "Xyz123456");

        await uow.SaveAsync();
    }
}

static async void PopulateDb()
{
    using (var uow = new UnitOfWork())
    {
        // Check if the movie "Inception" already exists, because it's the first one to be added
        if (await MovieExists(uow, "Inception"))
        {
            return;
        }

        // Create Movies
        var movie1 = new Movie { Name = "Inception", Duration = new TimeSpan(2, 28, 0), CategoryId = 1, Rating = 9, Released = DateTime.Now.AddYears(-3), Cast = "Leonardo DiCaprio, Joseph Gordon-Levitt", Country = "USA", Description = "A mind-bending thriller about dreams within dreams." };
        var movie2 = new Movie { Name = "The Shawshank Redemption", Duration = new TimeSpan(2, 22, 0), CategoryId = 1, Rating = 10, Released = DateTime.Now.AddYears(-7), Cast = "Tim Robbins, Morgan Freeman", Country = "USA", Description = "Two imprisoned men bond over a number of years, finding solace and eventual redemption through acts of common decency." };
        var movie3 = new Movie { Name = "The Dark Knight", Duration = new TimeSpan(2, 32, 0), CategoryId = 2, Rating = 9, Released = DateTime.Now.AddYears(-10), Cast = "Christian Bale, Heath Ledger", Country = "USA", Description = "When the menace known as The Joker emerges, Batman must confront the greatest psychological and physical tests of his ability to fight injustice." };
        var movie4 = new Movie { Name = "The Matrix", Duration = new TimeSpan(2, 16, 0), CategoryId = 2, Rating = 8, Released = DateTime.Now.AddYears(-22), Cast = "Keanu Reeves, Laurence Fishburne", Country = "USA", Description = "A computer hacker learns from mysterious rebels about the true nature of his reality and his role in the war against its controllers." };
        var movie5 = new Movie { Name = "Pulp Fiction", Duration = new TimeSpan(2, 34, 0), CategoryId = 3, Rating = 9, Released = DateTime.Now.AddYears(-27), Cast = "John Travolta, Uma Thurman", Country = "USA", Description = "The lives of two mob hitmen, a boxer, a gangster and his wife, and a pair of diner bandits intertwine in four tales of violence and redemption." };
        var movie6 = new Movie { Name = "Forrest Gump", Duration = new TimeSpan(2, 22, 0), CategoryId = 1, Rating = 8, Released = DateTime.Now.AddYears(-28), Cast = "Tom Hanks, Robin Wright", Country = "USA", Description = "The presidencies of Kennedy and Johnson, the events of Vietnam, Watergate, and other history unfold through the perspective of an Alabama man with an IQ of 75." };
        var movie7 = new Movie { Name = "The Godfather", Duration = new TimeSpan(2, 55, 0), CategoryId = 3, Rating = 10, Released = DateTime.Now.AddYears(-50), Cast = "Marlon Brando, Al Pacino", Country = "USA", Description = "An organized crime dynasty's aging patriarch transfers control of his clandestine empire to his reluctant son." };
        var movie8 = new Movie { Name = "Schindler's List", Duration = new TimeSpan(3, 15, 0), CategoryId = 1, Rating = 10, Released = DateTime.Now.AddYears(-28), Cast = "Liam Neeson, Ben Kingsley", Country = "USA", Description = "In German-occupied Poland during World War II, industrialist Oskar Schindler gradually becomes concerned for his Jewish workforce after witnessing their persecution by the Nazis." };
        var movie9 = new Movie { Name = "Inglourious Basterds", Duration = new TimeSpan(2, 33, 0), CategoryId = 3, Rating = 8, Released = DateTime.Now.AddYears(-12), Cast = "Brad Pitt, Christoph Waltz", Country = "USA", Description = "In Nazi-occupied France during World War II, a plan to assassinate Nazi leaders by a group of Jewish U.S. soldiers coincides with a theatre owner's vengeful plans for the same." };
        var movie10 = new Movie { Name = "The Silence of the Lambs", Duration = new TimeSpan(1, 58, 0), CategoryId = 3, Rating = 9, Released = DateTime.Now.AddYears(-30), Cast = "Jodie Foster, Anthony Hopkins", Country = "USA", Description = "A young FBI cadet must receive the help of an incarcerated and manipulative cannibal killer to help catch another serial killer, a madman who skins his victims." };

        // Create Series
        var series1 = new SeriesClass { Name = "Breaking Bad", CategoryId = 3, Rating = 9, Released = DateTime.Now.AddYears(-14), Cast = "Bryan Cranston, Aaron Paul", Country = "USA", Description = "A high school chemistry teacher turned methamphetamine manufacturer partners with a former student to secure his family's financial future." };
        var series2 = new SeriesClass { Name = "Game of Thrones", CategoryId = 4, Rating = 8, Released = DateTime.Now.AddYears(-10), Cast = "Emilia Clarke, Kit Harington", Country = "USA", Description = "Nine noble families fight for control over the lands of Westeros, while an ancient enemy returns after being dormant for millennia." };
        var series3 = new SeriesClass { Name = "Stranger Things", CategoryId = 4, Rating = 8, Released = DateTime.Now.AddYears(-5), Cast = "Millie Bobby Brown, Finn Wolfhard", Country = "USA", Description = "A group of kids in a small town uncover a series of supernatural mysteries and government conspiracies when a young girl with psychokinetic abilities is found." };
        var series4 = new SeriesClass { Name = "The Witcher", CategoryId = 4, Rating = 7, Released = DateTime.Now.AddYears(-2), Cast = "Henry Cavill, Freya Allan", Country = "USA", Description = "Geralt of Rivia, a solitary monster hunter, struggles to find his place in a world where people often prove more wicked than beasts." };
        var series5 = new SeriesClass { Name = "Money Heist", CategoryId = 5, Rating = 8, Released = DateTime.Now.AddYears(-4), Cast = "Úrsula Corberó, Álvaro Morte", Country = "Spain", Description = "A criminal mastermind, known as The Professor, recruits eight criminals with unique skills to carry out an ambitious plan to rob the Royal Mint of Spain." };
        var series6 = new SeriesClass { Name = "Stranger Things 2", CategoryId = 4, Rating = 9, Released = DateTime.Now.AddYears(-4), Cast = "Millie Bobby Brown, Finn Wolfhard", Country = "USA", Description = "The gang continues to face supernatural threats as they uncover more secrets about the alternate dimension known as the Upside Down." };
        var series7 = new SeriesClass { Name = "The Mandalorian", CategoryId = 5, Rating = 8, Released = DateTime.Now.AddYears(-3), Cast = "Pedro Pascal, Gina Carano", Country = "USA", Description = "A lone gunfighter makes his way through the outer reaches of the galaxy, far from the authority of the New Republic." };
        var series8 = new SeriesClass { Name = "Black Mirror", CategoryId = 7, Rating = 8, Released = DateTime.Now.AddYears(-12), Cast = "Various", Country = "UK", Description = "An anthology series exploring the dark, often dystopian sides of technological advancements and their impact on society." };
        var series9 = new SeriesClass { Name = "Narcos", CategoryId = 5, Rating = 8, Released = DateTime.Now.AddYears(-7), Cast = "Pedro Pascal, Wagner Moura", Country = "USA", Description = "A chronicled look at the criminal exploits of Colombian drug lord Pablo Escobar, as well as the many other drug kingpins who plagued the country through the years." };
        var series10 = new SeriesClass { Name = "Westworld", CategoryId = 7, Rating = 8, Released = DateTime.Now.AddYears(-5), Cast = "Evan Rachel Wood, Thandie Newton", Country = "USA", Description = "An amusement park for rich vacationers, the park is looked after by robotic hosts who believe they are human." };

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
            c1.Movies.AddRange(new List<Movie> { movie1, movie2, movie6, movie7, movie8 });
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

// check if a movie with the given name already exists
static async Task<bool> MovieExists(UnitOfWork uow, string movieName)
{
    return await uow.MovieRepository.FindByNameAsync(movieName) != null;
}

// check if a category with the given name already exists
static async Task<bool> CategoryExists(UnitOfWork uow, string categoryName)
{
    return await uow.CategoryRepository.FindByNameAsync(categoryName) != null;
}
//add favourites 
async Task AddMovieToFavorites(User currentUser)
{
    await PrintMoviesAsync();

    Console.WriteLine("╔═════════════════════════════════════╗");
    Console.WriteLine("║        - ADD TO FAVORITES -         ║");
    Console.WriteLine("╚═════════════════════════════════════╝");

    Console.Write("Enter the name of the movie you want to add to favorites: ");
    var movieName = Console.ReadLine();

    using var uow = new UnitOfWork();
    var movie = await uow.MovieRepository.FindByNameAsync(movieName);

    if (movie != null)
    {
        var favouritesRepository = uow.FavouritesRepository;

        // Find or create user's favorites
        var userFavourites = await favouritesRepository.FindByUserIdAsync(currentUser.Id);

        if (userFavourites == null)
        {
            // If user's favorites do not exist, create them
            userFavourites = new Favourites
            {
                UserId = currentUser.Id
            };
            favouritesRepository.Add(userFavourites);
        }

        // Check if the movie is already in the user's favorites to avoid duplicates
        if (!userFavourites.FavoriteMovies.Any(m => m.Id == movie.Id))
        {
            userFavourites.FavoriteMovies.Add(movie);
            await uow.SaveAsync();
            Console.WriteLine($"{movie.Name} added to favorites.");
        }
        else
        {
            Console.WriteLine($"{movie.Name} is already in favorites.");
        }
    }
    else
    {
        Console.WriteLine($"Movie with name {movieName} not found.");
    }
}


async Task AddSeriesToFavorites(User currentUser)
{
    await PrintSeriesAsync();

    Console.WriteLine("╔═════════════════════════════════════╗");
    Console.WriteLine("║        - ADD TO FAVORITES -         ║");
    Console.WriteLine("╚═════════════════════════════════════╝");

    Console.Write("Enter the name of the series you want to add to favorites: ");
    var seriesName = Console.ReadLine();

    using var uow = new UnitOfWork();
    var series = await uow.SeriesRepository.FindByNameAsync(seriesName);

    if (series != null)
    {
        var favouritesRepository = uow.FavouritesRepository;

        // Find or create user's favorites
        var userFavourites = await favouritesRepository.FindByUserIdAsync(currentUser.Id);

        if (userFavourites == null)
        {
            // If user's favorites do not exist, create them
            userFavourites = new Favourites
            {
                UserId = currentUser.Id
            };
            favouritesRepository.Add(userFavourites);
        }

        // Check if the series is already in the user's favorites to avoid duplicates
        if (!userFavourites.FavoriteSeries.Any(s => s.Id == series.Id))
        {
            userFavourites.FavoriteSeries.Add(series);
            await uow.SaveAsync();
            Console.WriteLine($"{series.Name} added to favorites.");
        }
        else
        {
            Console.WriteLine($"{series.Name} is already in favorites.");
        }
    }
    else
    {
        Console.WriteLine($"Series with name {seriesName} not found.");
    }
}



async Task PrintUserFavorites(User currentUser)
{
    Console.WriteLine("╔═════════════════════════════════════╗");
    Console.WriteLine("║          - YOUR FAVORITES -         ║");
    Console.WriteLine("╚═════════════════════════════════════╝");

    using var uow = new UnitOfWork();
    var user = await uow.FavouritesRepository.FindByUserIdAsync(currentUser.Id);

    if (user != null)
    {
        if (user.FavoriteMovies != null && user.FavoriteMovies.Any())
        {
            Console.WriteLine("Favorite Movies:");
            foreach (var favoriteMovie in user.FavoriteMovies)
            {
                Console.WriteLine($" - {favoriteMovie.Name}");
                // Add other movie properties as needed
            }
        }
        else
        {
            Console.WriteLine("No favorite movies.");
        }

        Console.WriteLine();

        if (user.FavoriteSeries != null && user.FavoriteSeries.Any())
        {
            Console.WriteLine("Favorite Series:");
            foreach (var favoriteSeries in user.FavoriteSeries)
            {
                Console.WriteLine($" - {favoriteSeries.Name}");
                // Add other series properties as needed
            }
        }
        else
        {
            Console.WriteLine("No favorite series.");
        }
    }
    else
    {
        Console.WriteLine("User not found.");
    }
}

#endregion