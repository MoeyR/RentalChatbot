using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;

namespace RentalChatbot
{
    public partial class Program
    {
        public static string CmdReader(string instruction)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write(instruction);
            Console.ForegroundColor = ConsoleColor.White;
            string cmd = Console.ReadLine();
            return cmd;
        }

        static void Main(string[] args)
        {
            ServiceCollection collection = new ServiceCollection();

            collection.AddScoped<IUser, User>();
            collection.AddScoped<IMenu, Menu>();
            collection.AddScoped<IRCController, RCController>();

            var serviceProvider = collection.BuildServiceProvider();

            var rcController = serviceProvider.GetRequiredService<IRCController>();

            rcController.Start();

            Console.Read();
            
        }
    }
}
