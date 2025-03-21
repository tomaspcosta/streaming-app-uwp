using StreamingApp.Domain.SeedWork;

namespace StreamingApp.Domain.Models
{
    public class Episodes : Entity
    {

        // Static variable to keep track of the last assigned episode number
        private static int lastEpisodeNumber = 0;

        public string Name { get; set; }

        // Increment the episode number for each new episode
        public int Number { get; set; } = ++lastEpisodeNumber;

        public int Rating { get; set; }
        public string Description { get; set; }
        public int SeasonId { set; get; }
        public Season Season { set; get; }

        public override string ToString()
        {
            return Name;
        }
    }
}
