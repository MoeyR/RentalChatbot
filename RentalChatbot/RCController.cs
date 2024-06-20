using RentalChatbot.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentalChatbot
{
    public partial class Program
    {
        public class RCController : IRCController
        {
            private readonly IUser _user;
            private readonly IMenu _menu;

            public RCController(IUser user, IMenu menu) 
            {
                _user = user;
                _menu = menu;
            }

            public void Start()
            {
                //User Login
                do 
                {
                    _user.Login();

                } while (!_user.IsUserLogin);

                //Show Menu
                _menu.ShowMedu();
            }









        }
    }

    
}
