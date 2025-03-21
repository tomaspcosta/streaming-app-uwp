using StreamingApp.Domain.SeedWork;
using System;
using System.Collections.Generic;

namespace StreamingApp.Domain.Models
{
    public class SeriesClass : Entity
    {

        public SeriesClass()
        {
            Seasons = new List<Season>();
        }

        public bool IsFavourite { set; get; }
        public string Name { get; set; }
        public byte[] Thumb { get; set; }
        public int CategoryId { set; get; }
        public Category Category { set; get; }
        public int Rating { get; set; }
        public string Description { set; get; }

        public TimeSpan Duration { set; get; }
        public DateTimeOffset Released { set; get; }
        public int GetSeriesId()
        {
            return Id;
        }
        public int GetCategoryId()
        {
            return CategoryId;
        }
        public string Cast { set; get; }
        public string Country { set; get; }
        public override string ToString()
        {
            return Name;
        }
        public List<Season> Seasons { get; set; }
    }
}
