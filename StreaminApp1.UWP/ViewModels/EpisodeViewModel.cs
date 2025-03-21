using StreamingApp.Domain;
using StreamingApp.Domain.Models;
using StreamingApp.InfraStructure;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace StreamingApp.UWP.ViewModels
{
    public class EpisodeViewModel : BindableBase
    {
        private IUnitOfWork _uow;

        public EpisodeViewModel()
        {
            _uow = new UnitOfWork();
            Episodes = new ObservableCollection<Episodes>();
            Episode = new Episodes();
        }

        public ObservableCollection<Episodes> Episodes { get; set; }

        public Episodes Episode { get; set; }

        public async Task LoadAllAsync()
        {
            var list = await _uow.EpisodesRepository.FindAllAsync();
            Episodes.Clear();

            foreach (var episode in list)
            {
                Episodes.Add(episode);
            }
        }

        public bool Valid
        {
            get { return !string.IsNullOrWhiteSpace(Name); }
        }

        public bool Invalid
        {
            get { return !Valid; }
        }

        private string _name;
        private int _rating;
        private string _description;
        private int _seasonId;
        private int _episodeNumber;

        public string Name
        {
            get { return _name; }
            set
            {
                Set(ref _name, value);
                OnPropertyChanged(nameof(Valid));
                OnPropertyChanged(nameof(Invalid));
            }
        }
        private int _selectedSeasonId;

        public int SelectedSeasonId
        {
            get { return _selectedSeasonId; }
            set { Set(ref _selectedSeasonId, value); }
        }
        private Season _selectedSeason;

        public Season SelectedSeason
        {
            get { return _selectedSeason; }
            set { Set(ref _selectedSeason, value); }
        }
        public int Rating
        {
            get { return _rating; }
            set
            {
                Set(ref _rating, value);
                OnPropertyChanged(nameof(Valid));
                OnPropertyChanged(nameof(Invalid));
            }
        }

        public string Description
        {
            get { return _description; }
            set
            {
                Set(ref _description, value);
                OnPropertyChanged(nameof(Valid));
                OnPropertyChanged(nameof(Invalid));
            }
        }

        private int _currentSeasonId;
        public int SeasonId
        {
            get { return _seasonId; }
            set
            {
                Set(ref _seasonId, value);
                OnPropertyChanged(nameof(Valid));
                OnPropertyChanged(nameof(Invalid));
            }
        }
        public int EpisodeNumber
        {
            get { return _episodeNumber; }
            set
            {
                Set(ref _episodeNumber, value);
                OnPropertyChanged(nameof(Valid));
                OnPropertyChanged(nameof(Invalid));
            }
        }

        public async Task<bool> CreateOrUpdateEpisodeAsync()
        {
            // Check if episode already exists
            var existingEpisode = Episodes.FirstOrDefault(
                x => x.Name == Name && x.SeasonId == _currentSeasonId);
            if (existingEpisode != null)
            {
                // Already exists
                // TODO: alert user
                return false;
            }
            else
            {
                // Create new episode
                var newEpisode =
                    new Episodes
                    {
                        Name = Name,
                        Number = EpisodeNumber,
                        Rating = Rating,
                        Description = Description,
                        SeasonId = SelectedSeasonId
                    };

                _uow.EpisodesRepository.Create(newEpisode);
                Episodes.Add(newEpisode);

                await _uow.SaveAsync();
                return true;
            }
        }
    }

}
