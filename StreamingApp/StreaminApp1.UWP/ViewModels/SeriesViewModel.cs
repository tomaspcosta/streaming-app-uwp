using GalaSoft.MvvmLight.Command;
using StreamingApp.Domain;
using StreamingApp.Domain.Models;
using StreamingApp.InfraStructure;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Windows.Storage.Pickers;
using Windows.Storage;
using Windows.Storage.Streams;
using Windows.UI.Xaml.Media.Imaging;
using StreamingApp.UWP.Views.Serie;
using System.IO;

namespace StreamingApp.UWP.ViewModels
{
    public class SeriesViewModel : BindableBase
    {
        private IUnitOfWork _uow;

        public SeriesViewModel()
        {
            _uow = new UnitOfWork();
            Series = new ObservableCollection<SeriesClass>();
            SeriesClass = new SeriesClass();
            SelectedSeries = new SeriesClass();
            SelectedSeason = new Season();
            Categories = new ObservableCollection<Category>();
            FilteredSeries = new ObservableCollection<SeriesClass>();
            CrimeSeries = new ObservableCollection<SeriesClass>();
            ThrillerSeries = new ObservableCollection<SeriesClass>();
            FavouriteSeries = new ObservableCollection<SeriesClass>();

        }
        public ObservableCollection<SeriesClass> Series { get; set; }

        public ObservableCollection<Category> Categories { get; set; }

        public ObservableCollection<SeriesClass> CrimeSeries { get; set; }

        public ObservableCollection<SeriesClass> ThrillerSeries { get; set; }

        public ObservableCollection<SeriesClass> FavouriteSeries { get; set; }

        private Category _selectedCategory;

        public Category SelectedCategory
        {
            get { return _selectedCategory; }
            set { Set(ref _selectedCategory, value); }
        }

        private int _categoryId;
        public int CategoryId
        {
            get { return SeriesClass.GetCategoryId(); }
            set { Set(ref _categoryId, value); }
        }

        private string _seriesCast;

        public string SeriesCast
        {
            get { return _seriesCast; }
            set
            {
                Set(ref _seriesCast, value);
                OnPropertyChanged(nameof(Valid));
                OnPropertyChanged(nameof(Invalid));
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

        private string _seriesCountry;

        public string SeriesCountry
        {
            get { return _seriesCountry; }
            set
            {
                Set(ref _seriesCountry, value);
                OnPropertyChanged(nameof(Valid));
                OnPropertyChanged(nameof(Invalid));
            }
        }
        private string _seriesDescription;

        public string SeriesDescription
        {
            get { return _seriesDescription; }
            set
            {
                Set(ref _seriesDescription, value);
                OnPropertyChanged(nameof(Valid));
                OnPropertyChanged(nameof(Invalid));
            }
        }

        private TimeSpan _seriesDuration;
        public TimeSpan SeriesDuration
        {
            get { return _seriesDuration; }
            set
            {
                Set(ref _seriesDuration, value);
                OnPropertyChanged(nameof(Valid));
                OnPropertyChanged(nameof(Invalid));
            }
        }
        private DateTimeOffset _seriesReleased;

        public DateTimeOffset SeriesReleased
        {
            get { return _seriesReleased; }
            set
            {
                Set(ref _seriesReleased, value);
                OnPropertyChanged(nameof(Valid));
                OnPropertyChanged(nameof(Invalid));
            }
        }

        private string _seriesName;

        public string SeriesName
        {
            get { return _seriesName; }
            set
            {
                Set(ref _seriesName, value);
                OnPropertyChanged(nameof(Valid));
                OnPropertyChanged(nameof(Invalid));
            }
        }
        private int _seasonNumber;

        public int SeasonNumber
        {
            get { return _seasonNumber; }
            set
            {
                Set(ref _seasonNumber, value);
                OnPropertyChanged(nameof(Valid));
                OnPropertyChanged(nameof(Invalid));
            }
        }
        private int _seriesRating;

        public int SeriesRating
        {
            get { return _seriesRating; }
            set
            {
                Set(ref _seriesRating, value);
                OnPropertyChanged(nameof(Valid));
                OnPropertyChanged(nameof(Invalid));
            }
        }


        private ObservableCollection<SeriesClass> _filteredSeries;

        public ObservableCollection<SeriesClass> FilteredSeries
        {
            get { return _filteredSeries; }
            set { Set(ref _filteredSeries, value); }
        }

        public SeriesClass _series;
        public SeriesClass SeriesClass
        {
            get { return _series; }
            set
            {
                _series = value;
                SeriesName = _series?.Name;
                SeriesRating = _series?.Rating ?? 0;
                SelectedCategory = _series?.Category;
                SeriesDescription = _series?.Description;
                SeriesCountry = _series?.Country;
                SeriesCast = _series?.Cast;
                SeriesDuration = _series?.Duration ?? TimeSpan.Zero;
                SeriesReleased = _series?.Released ?? DateTimeOffset.Now;
                SeriesPoster = _series?.Thumb;
                IsFavourite = _series?.IsFavourite ?? false;
            }
        }
        public bool Valid { get { return !string.IsNullOrWhiteSpace(SeriesName); } }

        public bool Invalid { get { return !Valid; } }





        public async Task LoadAllByCategoryAndDurationAsync(Category categorySelected, TimeSpan duration)
        {
            try
            {
                CategoryId = categorySelected.Id;
                _seriesDuration = duration;

                var FilteredSeries = await _uow.SeriesRepository.FindAllByCategoryAndTime(categorySelected.Id, _seriesDuration);

                Series.Clear();
                foreach (var serie in FilteredSeries)
                {
                    Series.Add(serie);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error loading series by category and duration: {ex.Message}");
            }
        }




        public async Task LoadAllAsyncByCategory(Category categorySelected)
        {
            try
            {
                CategoryId = categorySelected.Id;
                var series = await _uow.SeriesRepository.FindAllByCategory(categorySelected.Id);
                Series.Clear();
                foreach (var serie in series)
                {
                    Series.Add(serie);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error loading series and categories: {ex.Message}");
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
        public async Task LoadAllAsync()
        {
            try
            {
                var series = await _uow.SeriesRepository.FindAllAsync();
                Series.Clear();

                foreach (var serie in series)
                {
                    Series.Add(serie);
                }

                var isFav = await _uow.SeriesRepository.FindAllByIsFavourite();
                FavouriteSeries.Clear();

                foreach (var isfavourite in isFav)
                {
                    FavouriteSeries.Add(isfavourite);
                }

                var crimeSeries = await _uow.SeriesRepository.FindAllByCategory(5);
                CrimeSeries.Clear();


                foreach (var serie in crimeSeries)
                {
                    CrimeSeries.Add(serie);
                }

                var thrillerSeries = await _uow.SeriesRepository.FindAllByCategory(3);
                ThrillerSeries.Clear();

                foreach (var serie in thrillerSeries)
                {
                    ThrillerSeries.Add(serie);
                }

                // Load categories
                await LoadAllCategoriesAsync();
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error loading series and categories: {ex.Message}");
            }
        }

        private Season _selectedSeason;

        public Season SelectedSeason
        {
            get { return _selectedSeason; }
            set { Set(ref _selectedSeason, value); }
        }
        public int Id
        {
            get { return SeriesClass.GetSeriesId(); }
        }


        private SeriesClass _selectedSeries;

        public SeriesClass SelectedSeries
        {
            get { return _selectedSeries; }
            set
            {
                if (Set(ref _selectedSeries, value))
                {
                    OnPropertyChanged(nameof(SelectedSeries));
                    OnPropertyChanged(nameof(IsSeriesSelected));
                    OnPropertyChanged(nameof(Seasons));
                }
            }
        }

        public bool IsSeriesSelected => SelectedSeries != null;

        public List<Season> Seasons => SelectedSeries?.Seasons;


        public void ApplyFilters(Category selectedCategory, TimeSpan selectedDuration)
        {


            var filteredSeries = Series.Where(series =>
                (selectedCategory == null || series.Category == selectedCategory) &&
                (selectedDuration == TimeSpan.Zero || series.Duration <= selectedDuration)
            ).ToList();

            // Update the Series collection with the filtered series
            Series.Clear();
            foreach (var series in filteredSeries)
            {
                Series.Add(series);
            }
        }

        public async Task LoadSeasonsAsync(SeriesClass series)
        {
            if (series != null)
            {
                var seasons = await _uow.SeasonRepository.GetSeasonsForSeriesAsync(series.Id);

                // Clear and add the seasons to the list
                SelectedSeries.Seasons.Clear();
                foreach (var season in seasons)
                {
                    SelectedSeries.Seasons.Add(season);
                }
            }
        }



        private byte[] _seriesPoster;

        public byte[] SeriesPoster
        {
            get { return _seriesPoster; }
            set
            {
                Set(ref _seriesPoster, value);
                OnPropertyChanged(nameof(SeriesPoster));  // Notify for the Image control
            }
        }

        public BitmapImage SeriesPosterImage
        {
            get
            {
                if (SeriesPoster != null)
                {
                    BitmapImage image = new BitmapImage();
                    InMemoryRandomAccessStream stream = new InMemoryRandomAccessStream();
                    stream.AsStreamForWrite().Write(SeriesPoster, 0, SeriesPoster.Length);
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


                        SeriesPoster = bytes;
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
            if (!FavouriteSeries.Contains(SeriesClass))
            {
                SeriesClass.IsFavourite = true;
                FavouriteSeries.Add(SeriesClass);

                // Update the database to reflect the change
                _uow.SeriesRepository.Update(SeriesClass);
                _uow.SaveAsync();
            }
        }

        public void RemoveFromFavorites()
        {
            // Remove the current series from FavouriteSeries
            if (FavouriteSeries.Contains(SeriesClass))
            {
                SeriesClass.IsFavourite = false;
                FavouriteSeries.Remove(SeriesClass);

                // Update the database to reflect the change
                _uow.SeriesRepository.Update(SeriesClass);
                _uow.SaveAsync();
            }
        }
        public async Task<bool> CreateOrUpdateSerieAsync()
        {
            // check if already exists
            var existingSerie = Series.FirstOrDefault(x => x.Name == SeriesName);
            {
                if (SeriesClass.Id == 0)
                {

                    var serie = new SeriesClass
                    {
                        Name = SeriesName,
                        Category = SelectedCategory,
                        Cast = SeriesCast,
                        Rating = SeriesRating,
                        Country = SeriesCountry,
                        Description = SeriesDescription,
                        Duration = SeriesDuration,
                        Released = SeriesReleased,
                        Thumb = SeriesPoster,
                        IsFavourite = IsFavourite

                    };
                    _uow.SeriesRepository.Create(serie);
                    Series.Add(serie);
                }
                else
                {

                    //edit series
                    SeriesClass.Name = SeriesName;
                    SeriesClass.Category = SelectedCategory;
                    SeriesClass.Cast = SeriesCast;
                    SeriesClass.Rating = SeriesRating;
                    SeriesClass.Country = SeriesCountry;
                    SeriesClass.Description = SeriesDescription;
                    SeriesClass.Duration = SeriesDuration;
                    SeriesClass.Released = SeriesReleased;
                    SeriesClass.Thumb = SeriesPoster;
                    SeriesClass.IsFavourite = IsFavourite;

                    _uow.SeriesRepository.Update(SeriesClass);
                }
                await _uow.SaveAsync();
                return true;
            }
        }
    }
}



