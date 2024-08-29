using Microsoft.EntityFrameworkCore;
using StreamingApp.Domain.Models;
using System;

namespace StreamingApp.Infrastructure
{
    public class MovieListDbContext : DbContext
    {
        public MovieListDbContext()
        {
            var folder = Environment.SpecialFolder.LocalApplicationData;
            var path = Environment.GetFolderPath(folder);
            DbPath = System.IO.Path.Combine(path, "StreamingAppDbUWP.db");
        }
        public string DbPath { get; private set; }
        public DbSet<Movie> Movies { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Episodes> Episodes { get; set; }
        public DbSet<SeriesClass> Series { get; set; }
        public DbSet<Season> Seasons { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Favourites> Favourites { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite($"Data Source={DbPath}");

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Movie>().HasIndex(x => x.Name).IsUnique();
            modelBuilder.Entity<Movie>().Property(x => x.Name).IsRequired().HasMaxLength(256);

            modelBuilder.Entity<Category>().HasIndex(x => x.Name).IsUnique();
            modelBuilder.Entity<Category>().Property(x => x.Name).IsRequired().HasMaxLength(256);

            modelBuilder.Entity<Episodes>().HasIndex(x => x.Number).IsUnique();
            modelBuilder.Entity<Episodes>().Property(x => x.Number).IsRequired().HasMaxLength(256);

            modelBuilder.Entity<SeriesClass>().HasIndex(x => x.Name).IsUnique();
            modelBuilder.Entity<SeriesClass>().Property(x => x.Name).IsRequired().HasMaxLength(256);

            modelBuilder.Entity<Season>().HasIndex(x => x.Id).IsUnique();
            modelBuilder.Entity<Season>().Property(x => x.Id).IsRequired().HasMaxLength(256);

            modelBuilder.Entity<User>().HasIndex(x => x.Username).IsUnique();
            modelBuilder.Entity<User>().Property(x => x.Username).IsRequired().HasMaxLength(256);

            modelBuilder.Entity<Favourites>().HasIndex(x => x.Id).IsUnique();
            modelBuilder.Entity<Favourites>().Property(x => x.Id).IsRequired().HasMaxLength(256);
        }
    }
}