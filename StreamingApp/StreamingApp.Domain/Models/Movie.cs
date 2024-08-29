using StreamingApp.Domain.SeedWork;
using System;

namespace StreamingApp.Domain.Models
{
    public class Movie : Entity
    {
        public string Name { get; set; }
        public byte[] Thumb { get; set; }
        public bool IsFavourite { set; get; }
        public int CategoryId { set; get; }
        public Category Category { set; get; }
        public int Rating { set; get; }
        public string Description { set; get; }
        public DateTimeOffset Released { set; get; }
        public string Cast { set; get; }
        public TimeSpan Duration { set; get; }
        public string Country { set; get; }

        public int GetCategoryId()
        {
            return CategoryId;
        }
        public override string ToString()
        {
            return Name;
        }

    }
}
