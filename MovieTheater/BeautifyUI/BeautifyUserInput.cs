using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieTheater.BeautifyUI
{
    public class BeautifyUserInput
    {
        private char symbol = ' ';
        public void AskIfAccountAlreadyExists()
        {
            Console.Write(new string(symbol, 27));

            for (int i = 0; i < 32; i++)
            {
                Console.Write("ALREADY HAVE AN ACCOUNT?(YES/NO)"[i]);
                Thread.Sleep(100);
            }

            Console.WriteLine(new string(symbol, 26));
        }

        public void SignInLogin()
        {
            Console.Clear();
            Console.Write(new string(symbol, 36));

            for (int i = 0; i < 11; i++)
            {
                Console.Write("LOG IN HERE"[i]);
                Thread.Sleep(100);
            }

            Console.WriteLine(new string(symbol, 36));

            Console.Write(new string(symbol, 29));

            for (int i = 0; i < 25; i++)
            {
                Console.Write("ENTER YOUR LOGIN OR EMAIL"[i]);
                Thread.Sleep(100);
            }

            Console.WriteLine(new string(symbol, 29));
        }

        public void SignInPassword()
        {
            Console.Write(new string(symbol, 32));

            for (int i = 0; i < 19; i++)
            {
                Console.Write("ENTER YOUR PASSWORD"[i]);
                Thread.Sleep(100);
            }

            Console.WriteLine(new string(symbol, 32));
        }

        public void SignUpInput()
        {
            Console.Clear();
            Console.Write(new string(symbol, 36));

            for (int i = 0; i < 12; i++)
            {
                Console.Write("SIGN UP HERE"[i]);
                Thread.Sleep(100);
            }

            Console.WriteLine(new string(symbol, 35));
            Console.WriteLine();
        }

        public void SignUpName()
        {
            Console.Write(new string('-', 34));

            for (int i = 0; i < 15; i++)
            {
                Console.Write("ENTER YOUR NAME"[i]);
                Thread.Sleep(100);
            }

            Console.WriteLine(new string(symbol, 34));
        }

        public void SignUpAge()
        {
            Console.Write(new string(symbol, 35));

            for (int i = 0; i < 14; i++)
            {
                Console.Write("ENTER YOUR AGE"[i]);
                Thread.Sleep(100);
            }

            Console.WriteLine(new string(symbol, 33));
        }

        public void SignUpLogin()
        {
            Console.Write(new string(symbol, 35));

            for (int i = 0; i < 14; i++)
            {
                Console.Write("CREATE A LOGIN"[i]);
                Thread.Sleep(100);
            }

            Console.WriteLine(new string(symbol, 34));
        }

        public void SignUpEmail()
        {
            Console.Write(new string(symbol, 34));

            for (int i = 0; i < 16; i++)
            {
                Console.Write("ENTER YOUR EMAIL"[i]);
                Thread.Sleep(100);
            }

            Console.WriteLine(new string(symbol, 33));
        }

        public void SignUpPassword()
        {
            Console.Write(new string(symbol, 33));

            for (int i = 0; i < 17; i++)
            {
                Console.Write("CREATE A PASSWORD"[i]);
                Thread.Sleep(100);
            }

            Console.WriteLine(new string(symbol, 33));
        }

        public void PasswordRestrictions()
        {
            Console.Write(new string(symbol, 33));

            for (int i = 0; i < 17; i++)
            {
                Console.Write("RESTRICTIONS HERE"[i]);
                Thread.Sleep(100);
            }

            Console.WriteLine(new string(symbol, 33));
        }

        public void SignUpConfirmPassword()
        {
            Console.Write(new string(symbol, 32));

            for (int i = 0; i < 20; i++)
            {
                Console.Write("CONFIRM THE PASSWORD"[i]);
                Thread.Sleep(100);
            }

            Console.WriteLine(new string(symbol, 31));
        }
    }
}
