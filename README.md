# Projeto DA - StreamingApp

## - Introduction

StreamingApp is a UWP application that allows users to watch movies and series. This README provides essential information about the application and its features.

## - IMPORTANT

 - In order to visualize the movies/series in the MoviesPage/SeriesPage/HomePage the admin must add images to the respective movies/series.
 - To do that you must follow the following steps: login as an admin -> ManageMovies/ManageSeries -> Edit a movie/serie -> Click "Choose Image"
 - Theres already some images for the movies/series, that are located in this path: C:...\ad-project-group-11\StreamingApp\StreaminApp1.UWP\Assets\MovieThumb 

## - Default Admin Credentials

Upon the first run, the data base its created, and the application will automatically populate de data base with some movies, series, categories and it will also create one admin account:

    Admin Credentials:
        Username: admin
        Password: admin

## - Features

- **Manage Movies, Series and Categories [ADMIN]:**
  - Add/edit/remove movies.
  - Add/edit/remove series.
  - Add/edit/remove categories.
  - Add/edit/remove seasons to/from series.
  - Add/edit/remove episodes to/from seasons.

- **Manage Users [ADMIN]:**
  - Create/remove regular users.
  - Update regular users information/credentials (username, phone number, email address).
  - Create/remove admin users.
  - Update admin users information/credentials (username, phone number, email address).
  
- **User Authentication:**
  - Log in as an admin or a regular user.
  - Register (regular users only, admin users can only be created by an admin).

- **Update Regular User Information:**
  - Each user can update their own information/credentials (username, password, phone number, email address).

- **Favorites:**
  - Users can add/remove movies or series to/from their favorites list.
  - Users can see their favorite list.

- **Quiz:**
  - Users can answer a quiz and based on their answers it will sugest a movie or serie to watch.

- **See all movies/series available:**
  - Users have two different pages to display all movies/series available for them, and can see them by category (for example only display comedy movies).
