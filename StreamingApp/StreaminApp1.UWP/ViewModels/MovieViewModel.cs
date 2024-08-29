using GalaSoft.MvvmLight.Command;
using StreamingApp.Domain;
using StreamingApp.Domain.Models;
using StreamingApp.InfraStructure;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.Storage.Pickers;
using Windows.Storage.Streams;
using Windows.UI.Xaml.Media.Imaging;

namespace StreamingApp.UWP.ViewModels
{
    public class MovieViewModel : BindableBase
    {
        private IUnitOfWork _uow;

        public MovieViewModel()
        {
            _uow = new UnitOfWork();
            Movies = new ObservableCollection<Movie>();
            Movie = new Movie();
            Categories = new ObservableCollection<Category>();
            MysteryMovies = new ObservableCollection<Movie>();
            ComedyMovies = new ObservableCollection<Movie>();
            FilteredMovies = new ObservableCollection<Movie>();
            FavoritesMovies = new ObservableCollection<Movie>();
        }
        public ObservableCollection<Movie> Movies { get; set; }
        public ObservableCollection<Category> Categories { get; set; }
        public ObservableCollection<Movie> MysteryMovies { get; set; }
        public ObservableCollection<Movie> ComedyMovies { get; set; }
        public ObservableCollection<Movie> FavoritesMovies { get; set; }


        private int _categoryId;
        public int CategoryId
        {
            get { return Movie.GetCategoryId(); }
            set { Set(ref _categoryId, value); }
        }

        private Category _selectedCategory;

        public Category SelectedCategory
        {
            get { return _selectedCategory; }
            set
            {
                Set(ref _selectedCategory, value);
                Movie.CategoryId = value?.Id ?? 0; // Set Movie.CategoryId based on SelectedCategory
            }
        }

        private bool _isFavourite;

        public bool IsFavourite
        {
            get { return _isFavourite; }
            set
            {
                Set(ref _isFavourite, value);
                OnPropertyChanged(nameof(Valid));
                OnPropertyChanged(nameof(Invalid));
            }
        }


        private int _movieRating;

        public int MovieRating
        {
            get { return _movieRating; }
            set
            {
                Set(ref _movieRating, value);
                OnPropertyChanged(nameof(Valid));
                OnPropertyChanged(nameof(Invalid));
            }
        }

        private string _movieDescription;

        public string MovieDescription
        {
            get { return _movieDescription; }
            set
            {
                Set(ref _movieDescription, value);
                OnPropertyChanged(nameof(Valid));
                OnPropertyChanged(nameof(Invalid));
            }
        }

        private string _movieName;

        public string MovieName
        {
            get { return _movieName; }
            set
            {
                Set(ref _movieName, value);
                OnPropertyChanged(nameof(Valid));
                OnPropertyChanged(nameof(Invalid));
            }
        }
        private DateTimeOffset _movieReleased;

        public DateTimeOffset MovieReleased
        {
            get { return _movieReleased; }
            set
            {
                Set(ref _movieReleased, value);
                OnPropertyChanged(nameof(Valid));
                OnPropertyChanged(nameof(Invalid));
            }
        }

        private string _movieCast;

        public string MovieCast
        {
            get { return _movieCast; }
            set
            {
                Set(ref _movieCast, value);
                OnPropertyChanged(nameof(Valid));
                OnPropertyChanged(nameof(Invalid));
            }
        }

        private TimeSpan _movieDuration;

        public TimeSpan MovieDuration
        {
            get { return _movieDuration; }
            set
            {
                Set(ref _movieDuration, value);
                OnPropertyChanged(nameof(Valid));
                OnPropertyChanged(nameof(Invalid));
            }
        }

        private string _movieCountry;

        public string MovieCountry
        {
            get { return _movieCountry; }
            set
            {
                Set(ref _movieCountry, value);
                OnPropertyChanged(nameof(Valid));
                OnPropertyChanged(nameof(Invalid));
            }
        }

        private ObservableCollection<Movie> _filteredMovies;

        public ObservableCollection<Movie> FilteredMovies
        {
            get { return _filteredMovies; }
            set { Set(ref _filteredMovies, value); }
        }


        internal async void DeleteAsync()
        {
            _uow.MovieRepository.Delete(Movie);
            Movies.Remove(Movie);
            await _uow.SaveAsync();
        }

        public async Task LoadAllByCategoryAndDurationAsync(Category categorySelected, TimeSpan duration)
        {
            try
            {
                CategoryId = categorySelected.Id;
                _movieDuration = duration;

                var FilteredMovies = await _uow.MovieRepository.FindAllByCategoryAndTime(categorySelected.Id, _movieDuration);

                Movies.Clear();
                foreach (var movie in FilteredMovies)
                {
                    Movies.Add(movie);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error loading movies by category and duration: {ex.Message}");
            }
        }


        public async Task LoadAllAsyncByCategory(Category categorySelected)
        {
            try
            {
                CategoryId = categorySelected.Id;
                var movies = await _uow.MovieRepository.FindAllByCategory(CategoryId);
                Movies.Clear();
                foreach (var movie in movies)
                {
                    Movies.Add(movie);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error loading movies and categories: {ex.Message}");
            }
        }

        public async Task LoadAllAsyncBySpecificCategory()
        {
            try
            {
                CategoryId = 8;
                var movies = await _uow.MovieRepository.FindAllByCategory(CategoryId);
                Movies.Clear();
                foreach (var movie in movies)
                {
                    Movies.Add(movie);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error loading movies and categories: {ex.Message}");
            }
        }

        public async Task LoadAllAsync()
        {
            try
            {
                var movies = await _uow.MovieRepository.FindAllAsync();
                Movies.Clear();

                foreach (var movie in movies)
                {
                    Movies.Add(movie);
                }

                var isFav = await _uow.MovieRepository.FindAllByIsFavourite();
                FavoritesMovies.Clear();

                foreach (var isfavourite in isFav)
                {
                    FavoritesMovies.Add(isfavourite);
                }

                // Load movies for specific categories
                var actionMovies = await _uow.MovieRepository.FindAllByCategory(8); // Replace 1 with the ID for the Action category
                MysteryMovies.Clear();
                foreach (var movie in actionMovies)
                {
                    MysteryMovies.Add(movie);
                }

                var comedyMovies = await _uow.MovieRepository.FindAllByCategory(6); // Replace 2 with the ID for the Comedy category
                ComedyMovies.Clear();
                foreach (var movie in comedyMovies)
                {
                    ComedyMovies.Add(movie);
                }


                // Load categories
                await LoadAllCategoriesAsync();
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error loading movies and categories: {ex.Message}");
            }
        }

        public async Task LoadAllCategoriesAsync()
        {
            try
            {
                var categories = await _uow.CategoryRepository.FindAllAsync();
                Categories.Clear();

                foreach (var category in categories)
                {
                    Categories.Add(category);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error loading categories: {ex.Message}");
            }
        }



        public Movie _movie;
        public Movie Movie
        {
            get { return _movie; }
            set
            {
                _movie = value;
                MovieName = _movie?.Name;
                MovieRating = _movie?.Rating ?? 0;
                MovieDescription = _movie?.Description ?? string.Empty;
                MovieReleased = _movie?.Released ?? DateTimeOffset.Now;
                MovieCast = _movie?.Cast ?? string.Empty;
                MovieDuration = _movie?.Duration ?? TimeSpan.Zero;
                MovieCountry = _movie?.Country ?? string.Empty;
                MoviePoster = _movie?.Thumb;
                SelectedCategory = _movie?.Category;
                IsFavourite = _movie?.IsFavourite ?? false;

            }
        }
        //  public string PageTile { get { return Category.Id == 0 ? "New Category :
        //  Edit Category" + CategoryName; } }
        public string PageTitle
        {
            get
            {
                return Movie.Id == 0 ? "New Category " : "Edit Category: " + Movie.Name;
            }
        }
        public bool Valid
        {
            get { return !string.IsNullOrWhiteSpace(MovieName); }
        }

        public bool Invalid
        {
            get { return !Valid; }
        }

        private byte[] _moviePoster;

        public byte[] MoviePoster
        {
            get { return _moviePoster; }
            set
            {
                Set(ref _moviePoster, value);
                OnPropertyChanged(nameof(MoviePoster));  // Notify for the Image control
            }
        }

        public BitmapImage MoviePosterImage
        {
            get
            {
                if (MoviePoster != null)
                {
                    BitmapImage image = new BitmapImage();
                    InMemoryRandomAccessStream stream = new InMemoryRandomAccessStream();
                    stream.AsStreamForWrite().Write(MoviePoster, 0, MoviePoster.Length);
                    stream.Seek(0);

                    image.SetSource(stream);
                    return image;
                }
                return null;
            }
        }

        private RelayCommand _chooseImageCommand;

        public RelayCommand ChooseImageCommand
        {
            get
            {
                return _chooseImageCommand ??
                       (_chooseImageCommand =
                            new RelayCommand(async () => await ChooseImageAsync()));
            }
        }

        private async Task ChooseImageAsync()
        {
            try
            {
                // Open a file picker to choose an image
                FileOpenPicker filePicker = new FileOpenPicker();
                filePicker.ViewMode = PickerViewMode.Thumbnail;
                filePicker.SuggestedStartLocation = PickerLocationId.PicturesLibrary;
                filePicker.FileTypeFilter.Add(".jpg");
                filePicker.FileTypeFilter.Add(".jpeg");
                filePicker.FileTypeFilter.Add(".png");

                StorageFile file = await filePicker.PickSingleFileAsync();
                if (file != null)
                {
                    // Convert the selected image file into a byte array
                    using (IRandomAccessStreamWithContentType stream =
                               await file.OpenReadAsync())
                    {
                        var reader = new DataReader(stream.GetInputStreamAt(0));
                        var bytes = new byte[stream.Size];
                        await reader.LoadAsync((uint)stream.Size);
                        reader.ReadBytes(bytes);

                        // Update MovieViewModel's MoviePoster property with the byte array
                        // representing the image
                        MoviePoster = bytes;
                    }
                }
            }
            catch (Exception ex)
            {
                // Log the exception or display an error message
                Debug.WriteLine($"Error choosing image: {ex.Message}");
            }
        }

        public void AddToFavorites()
        {
            // Add the current movie to FavoritesMovies
            if (!FavoritesMovies.Contains(Movie))
            {
                Movie.IsFavourite = true;
                FavoritesMovies.Add(Movie);

                // Update the database to reflect the change
                _uow.MovieRepository.Update(Movie);
                _uow.SaveAsync();
            }
        }

        public void RemoveFromFavorites()
        {
            // Remove the current movie from FavoritesMovies
            if (FavoritesMovies.Contains(Movie))
            {
                Movie.IsFavourite = false;
                FavoritesMovies.Remove(Movie);

                // Update the database to reflect the change
                _uow.MovieRepository.Update(Movie);
                _uow.SaveAsync();
            }
        }

        public async Task<bool> CreateOrUpdateMovieAsync()
        {
            // check if already exists
            var existingMovie = Movies.FirstOrDefault(x => x.Name == MovieName);
            {
                if (Movie.Id == 0)
                {
                    // create new movie
                    var movie = new Movie
                    {
                        Name = MovieName,
                        Category = SelectedCategory,
                        Rating = MovieRating,
                        Description = MovieDescription,
                        Released = MovieReleased,
                        Cast = MovieCast,
                        Duration = MovieDuration,
                        Country = MovieCountry,
                        Thumb = MoviePoster,
                        IsFavourite = IsFavourite

                    };
                    _uow.MovieRepository.Create(movie);
                    Movies.Add(movie);
                }
                else
                {
                    // edit movie
                    Movie.Name = MovieName;
                    Movie.Category = SelectedCategory;
                    Movie.Rating = MovieRating;
                    Movie.Description = MovieDescription;
                    Movie.Released = MovieReleased;
                    Movie.Cast = MovieCast;
                    Movie.Duration = MovieDuration;
                    Movie.Country = MovieCountry;
                    Movie.Thumb = MoviePoster;
                    Movie.IsFavourite = IsFavourite;
                    _uow.MovieRepository.Update(Movie);
                }
                await _uow.SaveAsync();


                return true;

            }
        }


    }
}