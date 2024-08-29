using StreamingApp.Domain.SeedWork;
using System.Collections.Generic;


namespace StreamingApp.Domain.Models
{
    public class Category : Entity
    {
        public Category()
        {
            Movies = new List<Movie>();
            Series = new List<SeriesClass>();

        }
        public string Name { set; get; }
        public List<Movie> Movies { get; set; }

        public List<SeriesClass> Series { get; set; }
        public override string ToString()
        {
            return Name;
        }
    }
}
