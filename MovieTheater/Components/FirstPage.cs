using Core.DTOs;
using Core.Models;
using MovieTheater.BeautifyUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieTheater.Components
{
    public class FirstPage
    {
        private const string YES = "YES";
        private const string NO = "NO";
        private UserHomePage homePage = new UserHomePage();
        public bool AskToLogInOrSignUp()
        {
            Console.Clear();
            BeautifyUserInput beautifyUserInput = new BeautifyUserInput();
            beautifyUserInput.AskIfAccountAlreadyExists();

            var answer = Console.ReadLine();

            switch (answer)
            {
                case YES:
                    return true;
                case NO:
                    return false;
            }

            Console.WriteLine("Try again");
            Thread.Sleep(1000);
            return AskToLogInOrSignUp();
        }

        public void RedirectToHomePageIfLoggedIn(User user, 
            Func<UserLoginDTO> func)
        {
            if (user == null)
            {
                Console.WriteLine("Something went wrong. Try again");
                Thread.Sleep(1000);
                func();
                return;
            }

            homePage.ShowHomePage(user);
        }

        public void RedirectToHomePageIfSignedUp(User user,
            Func<UserRegisterDTO> func)
        {
            if (user == null)
            {
                Console.WriteLine("Something went wrong. Try again");
                Thread.Sleep(1000);
                func();
                return;
            }

            homePage.ShowHomePage(user);
        }
    }
}
