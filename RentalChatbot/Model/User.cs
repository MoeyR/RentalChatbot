using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentalChatbot
{
    public partial class Program
    {
        public class User : IUser
        {
            public bool IsUserLogin { get; set; }

            public void Login()
            {
                string username;
                string password;

                username = CmdReader("Please login: \nusername(Hint: alex): ");

                if (username != "alex")
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("Sorry, no such user. \n----------\n");
                    return;
                }

                password = CmdReader("password(Hint: 1234): ");

                if (password != "1234")
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("Incorrect password. \n----------\n");
                    return;
                }

                IsUserLogin = true;
            }
        }
    }
    
}
