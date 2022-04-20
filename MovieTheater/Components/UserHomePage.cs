using Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieTheater.Components
{
    public class UserHomePage
    {
        private static User LoggedUser;
        private char symbol = ' ';
        public void ShowHomePage(User user)
        {
            LoggedUser = user;
            Console.Clear();
            Console.Write(new string(symbol, 34));

            for (int i = 0; i < 8 + user.Name.Length; i++)
            {
                Console.Write($"WELCOME {user.Name}"[i]);
                Thread.Sleep(100);
            }
            Console.WriteLine();

            Console.Write(new string(symbol, 26));

            for (int i = 0; i < 30; i++)
            {
                Console.Write("HERE IS THE LIST OF THE MOVIES"[i]);
                Thread.Sleep(100);
            }

            Console.WriteLine(new string(symbol, 26));
        }
    }
}
