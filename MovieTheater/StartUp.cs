
using BLL.Services;
using Core.DTOs;
using Core.Models;
using Microsoft.Extensions.Configuration;
using MovieTheater.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieTheater
{
    public class StartUp
    {
        private readonly MovieService _service;
        private readonly UserService _userService;
        //private readonly MovieLists _movieLists;
        private readonly IConfigurationRoot _config;

        public StartUp(MovieService service,
            UserService userService,
            //MovieLists movieLists,
            IConfigurationRoot config)
        {
            _service = service;
            _config = config;
            _userService = userService;
           // _movieLists = movieLists;
        }

        public async Task Do()
        {

            var movies = await _service.GetAllMoviesAsync();
            /*FirstPage firstPage = new FirstPage();
            // log in and sign up functionality
            bool alreadySignedUp = firstPage.AskToLogInOrSignUp();

            if (alreadySignedUp)
            {
                var user = UserCredentials.LogIn();
                var foundUser = _userService.LogIn(user);

                firstPage.RedirectToHomePageIfLoggedIn(foundUser, UserCredentials.LogIn);
            }
            else
            {
                var user = UserCredentials.SignUp();

                var foundUser = await _userService.SignUp(user);

                firstPage.RedirectToHomePageIfSignedUp(foundUser, UserCredentials.SignUp);
            }*/

            // getting the list of all movies

           // _movieLists.ShowAllAvailableMovies(0);
            /*Movie movie = new Movie() { Title = "Saw2", YearReleased = 2007, Genre = "Thriller" };
            Movie movie2 = new Movie() { Title = "Collectioner2", YearReleased = 2010, Genre = "Thriller" };
            Movie movie3 = new Movie() { Title = "Collectioner3", YearReleased = 2015, Genre = "Thriller" };


            await _service.AddMovie(movie);
            *//*await _service.AddMovie(movie2);
            await _service.AddMovie(movie3);*//*
            
            MainWindow();*/
            //var result = _userService.LogIn(user);
        
        }
    }
}
