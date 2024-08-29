using StreamingApp.Domain;
using StreamingApp.Domain.Models;
using StreamingApp.InfraStructure;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace StreamingApp.UWP.ViewModels
{
    public class SeasonViewModel : BindableBase
    {
        private IUnitOfWork _uow;

        public SeasonViewModel()
        {
            _uow = new UnitOfWork();
            Seasons = new ObservableCollection<Season>();
            Season = new Season();

        }
        public int Id
        {
            get { return Season.GetSeasonId(); }
        }

        public ObservableCollection<Season> Seasons { get; set; }

        private Season _selectedSeason;

        public Season SelectedSeason
        {
            get { return _selectedSeason; }
            set { Set(ref _selectedSeason, value); }
        }
        private int _selectedSeriesId;

        public int SelectedSeriesId
        {
            get { return _selectedSeriesId; }
            set { Set(ref _selectedSeriesId, value); }
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

        private string _seasonDescription;

        public string SeasonDescription
        {
            get { return _seasonDescription; }
            set
            {
                Set(ref _seasonDescription, value);
                OnPropertyChanged(nameof(Valid));
                OnPropertyChanged(nameof(Invalid));
            }
        }

        private int _seasonRating;

        public int SeasonRating
        {
            get { return _seasonRating; }
            set
            {
                Set(ref _seasonRating, value);
                OnPropertyChanged(nameof(Valid));
                OnPropertyChanged(nameof(Invalid));
            }
        }

        private DateTimeOffset _seasonReleased;

        public DateTimeOffset SeasonReleased
        {
            get { return _seasonReleased; }
            set
            {
                Set(ref _seasonReleased, value);
                OnPropertyChanged(nameof(Valid));
                OnPropertyChanged(nameof(Invalid));
            }
        }

        private string _seasonCast;

        public string SeasonCast
        {
            get { return _seasonCast; }
            set
            {
                Set(ref _seasonCast, value);
                OnPropertyChanged(nameof(Valid));
                OnPropertyChanged(nameof(Invalid));
            }
        }
        private Season _season;

        public Season Season
        {
            get { return _season; }
            set
            {
                _season = value;
                SeasonNumber = _season?.Number ?? 0;
                SeasonDescription = _season?.Description ?? string.Empty;
                SeasonRating = _season?.Rating ?? 0;
                SeasonReleased = _season?.Released ?? DateTimeOffset.Now;
                SeasonCast = _season?.Cast ?? string.Empty;
                SelectedSeriesId = _season?.SeriesId ?? 0;
            }
        }
        public List<Episodes> Episodes => SelectedSeason?.Episodes;

        public bool Valid { get { return SeasonNumber != 0; } }

        public bool Invalid { get { return !Valid; } }

        public async Task LoadAllSeasonsAsync()
        {
            try
            {
                var categories = await _uow.SeasonRepository.GetSeasonsForSeriesAsync(SelectedSeriesId);
                Seasons.Clear();

                foreach (var category in categories)
                {
                    Seasons.Add(category);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error loading categories: {ex.Message}");
            }
        }

        public async Task<bool> CreateOrUpdateSeasonAsync()
        {
            // Check if a season already exists with the provided number in the selected series
            var existingSeason = Seasons.FirstOrDefault(s => s.Number == SeasonNumber && s.SeriesId == SelectedSeriesId);
            if (existingSeason != null)
            {
                // Season already exists
                // TODO: Alert user
                return false;
            }
            else
            {
                // Create or edit the season
                if (Season.Id == 0)
                {
                    // Create new season
                    var season = new Season
                    {
                        Number = SeasonNumber,
                        Description = SeasonDescription,
                        Rating = SeasonRating,
                        Released = SeasonReleased,
                        Cast = SeasonCast,
                        SeriesId = SelectedSeriesId // Set the SeriesId for the season                                                          
                    };
                    _uow.SeasonRepository.Create(season);
                    SelectedSeries?.Seasons.Add(season);
                }
                else
                {
                    // Edit existing season
                    Season.Number = SeasonNumber;
                    Season.Description = SeasonDescription;
                    Season.Rating = SeasonRating;
                    Season.Released = SeasonReleased;
                    Season.Cast = SeasonCast;
                    Season.SeriesId = SelectedSeriesId; // Update the SeriesId for the season                                                         
                    _uow.SeasonRepository.Update(Season);
                }

                await _uow.SaveAsync();
                //await LoadSeasonsAsync(SelectedSeries); // Reload seasons after update
                return true;
            }
        }

    }
}

