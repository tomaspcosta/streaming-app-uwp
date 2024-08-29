using StreamingApp.Domain.SeedWork;
using System;
using System.Collections.Generic;

namespace StreamingApp.Domain.Models
{
    public class Season : Entity
    {
        public Season()
        {
            Episodes = new List<Episodes>();
        }
        public int Number { get; set; }
        public string Description { get; set; }
        public int Rating { get; set; }
        public DateTimeOffset Released { set; get; }
        public string Cast { set; get; }
        public int SeriesId { set; get; }
        public SeriesClass Series { set; get; }

        public List<Episodes> Episodes { get; set; }
        public int GetSeasonId()
        {
            return Id;
        }
        public override string ToString()
        {
            return Description;
        }
    }
}
