using StreamingApp.Domain.SeedWork;
using System;
using System.Collections.Generic;
using System.Text;

namespace StreamingApp.Domain.Models
{
    public class Favourites : Entity
    {
        public Favourites()
        {
            FavoriteMovies = new List<Movie>();
            FavoriteSeries = new List<SeriesClass>();
        }
        public List<Movie> FavoriteMovies { get; set; }

        public List<SeriesClass> FavoriteSeries { get; set; }

        public User User { get; set; }
        public int UserId { get; set; }

    }
}
