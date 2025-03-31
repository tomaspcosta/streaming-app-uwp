#   StreamingApp Project

##   Table of Contents

1.  [Project Description](#project-description)
2.  [Technologies Used](#technologies-used)
3.  [Features](#features)
4.  [Important Notes](#important-notes)
5.  [Default Admin Credentials](#default-admin-credentials)

##   1. Project Description

StreamingApp is a UWP application that allows users to watch movies and series. This project was developed as part of an academic exercise to build a functional media streaming application. It demonstrates the implementation of user authentication, content management, and media browsing features.

This application aims to provide a platform where users can stream movies and series, while administrators have tools to manage the content and user base.

##   2. Technologies Used

* UWP
* C# (.NET Framework)
* SQLite
* MySQL/SQL Server

##   3. Features

This application includes the following features:

###   Admin Functionalities:

* **Manage Movies, Series, and Categories:**
    * Add, edit, and remove movies.
    * Add, edit, and remove series.
    * Add, edit, and remove categories.
    * Add, edit, and remove seasons to/from series.
    * Add, edit, and remove episodes to/from seasons.
* **Manage Users:**
    * Create and remove regular users.
    * Update regular users' information/credentials (username, phone number, email address).
    * Create and remove admin users.
    * Update admin users' information/credentials (username, phone number, email address).

###   User Functionalities:

* **User Authentication:**
    * Log in as an admin or a regular user.
    * Register as a regular user (admin users are created by an admin).
* **Update Regular User Information:**
    * Users can update their own information/credentials (username, password, phone number, email address).
* **Favorites:**
    * Users can add/remove movies or series to/from their favorites list.
    * Users can view their favorites list.
* **Quiz:**
    * Users can answer a quiz to receive movie or series recommendations.
* **See all movies/series available:**
    * Users can browse all available movies/series and filter them by category.

##   4. Important Notes

* To visualize the movies/series in the MoviesPage/SeriesPage/HomePage, the admin must add images to the respective movies/series.
* To add images, follow these steps: log in as an admin -> ManageMovies/ManageSeries -> Edit a movie/serie -> Click "Choose Image."
* Some default images for movies/series are located in this path: `C:\...\ad-project-group-11\StreamingApp\StreaminApp1.UWP\Assets\MovieThumb`

##   5. Default Admin Credentials

Upon the first run, the database is created, and the application will automatically populate it with some movies, series, categories, and one admin account.
Admin Credentials:

    Username: admin
    Password: admin
