using RentalChatbot.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentalChatbot
{
    public partial class Program
    {
        public partial class Menu : IMenu
        {

            List<Car> currentCars = new List<Car>
            {
                new Car { CarID = 1, Make = "Tesla", Type = "Model S", Year = 2023, RentPerDay = 140 },
                new Car { CarID = 2, Make = "BMW", Type = "i7", Year = 2024, RentPerDay = 180 },
                new Car { CarID = 3, Make = "Porsche", Type = "Taycan", Year = 2023, RentPerDay = 260 }
            };

            List<Truck> currentTrucks = new List<Truck>
            {
                new Truck { CarID = 4, Make = "Toyota", Type = "Box Truck",  Payload="Up to 6700 lbs", RentPerDay = 160 },
                new Truck { CarID = 5, Make = "GMC",  Type = "Flat Deck", Payload = "Up to 7000 lbs", RentPerDay = 200 },
                new Truck { CarID = 6, Make = "Ford", Type = "Flat Deck",  Payload = "Up to 4000 lbs", RentPerDay = 190 }
            };

            List<Car> purchasedCarsList = new List<Car>();
            List<Truck> purchasedTrucksList = new List<Truck>();

            List<Car> cancelledCarsList = new List<Car>();
            List<Truck> cancelledTrucksList = new List<Truck>();


            public void ShowMedu()
            {
                bool exit = false;

                while (!exit) 
                {
                    string selection = CmdReader("\n************\nMain Menu\n1. Rent\n2. MyCart\n3. Logout\nPlease enter your option(Hint: type 1): ");

                    switch(selection)
                    {
                        case "1":
                            displayCurrentCars();
                            //ProceedToCheckout -> processPayment
                            processPayment();
                            break;
                        case "2":
                            Console.WriteLine("MyCart\n");
                            //DisplayMyCart: ProceedToCheckout -> processPayment
                            break;
                        case "3":
                            Console.WriteLine("You've been logged out.\n");
                            exit = true;
                            break;
                        default:
                            Console.ForegroundColor = ConsoleColor.Yellow;
                            Console.WriteLine("Error. Please enter number 1-3\n");
                            break;
                    }
                }
            }



            /// <summary>
            /// Display a car list and a truck list for user to choose to see details
            /// </summary>
            /// <returns> Car ID </returns>
            public void displayCurrentCars()
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("\n------Current Cars:-------\n");
                foreach (var car in currentCars)
                {
                    Console.WriteLine($"ID: {car.CarID}. Make: {car.Make}, Daily Rent Price: {car.RentPerDay}");
                }

                Console.WriteLine("\n------Current Trucks:-------\n");
                foreach (var truck in currentTrucks)
                {
                    Console.WriteLine($"ID: {truck.CarID}. Make: {truck.Make}, Daily Rent Price: {truck.RentPerDay}");
                }

                bool continueLoop = true;
                
                while (continueLoop)
                {
                    var userInput = CmdReader("\n ->Please type: 'more info', or 'return' \n");

                    if (userInput.Equals("more info"))
                    {
                        this.moreInfo();
                        continueLoop = false;
                    }
                    else if (userInput.Equals("return"))
                    {
                        continueLoop = false;
                        break;
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine("Error. I don't quite understand.");
                        continueLoop = true;
                    }

                }
            }



            /// <summary>
            /// Method for user to see item details by entering ID number
            /// </summary>
            /// <returns></returns>
            public void moreInfo()
            {
                bool continueLoop = true;
                int numSelected = 0;
                while (continueLoop)
                {
                    string inputId = CmdReader("\n ->Key in ID number to see Car details. (Hint: Type '3' to see details about the 3rd Car) \n");
                    int userNum = Convert.ToInt32(inputId);
                    Console.ForegroundColor = ConsoleColor.Green;

                    if (userNum < 1) 
                    {
                        Console.WriteLine("Error! Please type a number between 1 - 6");
                        continueLoop = true;
                    }
                    else if (userNum < 4) 
                    {
                        var output = currentCars.Find(Car => Car.CarID == userNum);
                        Console.WriteLine(output.vehicleDetail());
                        numSelected += userNum;
                        continueLoop = false;
                    }
                    else if (userNum < 7)
                    {
                        var output = currentTrucks.Find(Truck => Truck.CarID == userNum);
                        Console.WriteLine(output.vehicleDetail());
                        numSelected += userNum;
                        continueLoop = false;
                    }
                    else
                    {
                        Console.WriteLine("Error! Please type a number between 1 - 6");
                        continueLoop = true;
                    }
                }
                
                string userDecision = CmdReader("\n ->Rent and add to cart? or Return to main menu? (Hint: type 'rent' or 'return')\n");
                
                
                if (userDecision == "rent")
                {
                    addToShoppingCart(numSelected);
                }else if (userDecision == "return")
                {
                    return;
                }
                //return numSelected;
            }


            /// <summary>
            /// Method of adding selected item to the car list or truck list based on item ID
            /// </summary>
            /// <param name="carId"></param>
            public void addToShoppingCart(int carId)
            {
                if(carId > 0 && carId <= 3)
                {
                    var car = currentCars.Find(Car => Car.CarID == carId);
                    purchasedCarsList.Add(car);
                }
                else
                {
                    var truck = currentTrucks.Find(Truck => Truck.CarID == carId);
                    purchasedTrucksList.Add(truck);
                }
            }


            /// <summary>
            /// Enum of PaymentMethod
            /// </summary>
            enum PaymentMethod
            {
                Visa, MaterCard, PayPal, ApplePay
            }


            /// <summary>
            /// Method for payment processing, and print Purchase Receipt
            /// </summary>
            /// <param name="payTotal"></param>
            public void processPayment()
            {
                bool correctInput = false;
                string userReply;
                do {
                    userReply = CmdReader("\n->Checkout now ? (Hint: type 'checkout' or 'cancel')\n");
                    correctInput = userReply == "checkout" || userReply == "cancel";
                } while (!correctInput);

                if (userReply == "checkout")
                {
                    var payMethod = CmdReader($"\n ->How would you like to pay? (Hint: type {PaymentMethod.Visa}, or {PaymentMethod.MaterCard} or {PaymentMethod.PayPal}, or {PaymentMethod.ApplePay})\n");

                    //Display purchased list and prompt for user confirmation:
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("\nSure! Here's your car to rent: \n");

                    int paymentTotal = 0;
                    ArrayList items = new ArrayList();
                    if (purchasedCarsList.Count > 0)
                    {
                        var rentedCars = from car in purchasedCarsList
                                         select car;
                        foreach (var c in rentedCars)
                        {
                            paymentTotal += c.finalTotal();
                            items.Add(c.Make);
                            Console.WriteLine($"{c.vehicleDetail()} \n CarID# {c.CarID} Total: ${c.finalTotal()}");
                        }

                    }

                    if (purchasedTrucksList.Count > 0)
                    {
                        var rentedTrucks = from truck in purchasedTrucksList
                                           select truck;
                        foreach (var t in rentedTrucks)
                        {
                            paymentTotal += t.finalTotal();
                            items.Add(t.Make);
                            Console.WriteLine($"{t.vehicleDetail()} \n TruckID# {t.CarID} Total: ${t.finalTotal()}");
                        }
                    }

                    string[] itemMakes = items.ToArray(typeof(string)) as string[];

                    Console.WriteLine("\n--------------------\n");
                    
                    var userComfirm = CmdReader("\n ->Confirm payment? (Hint: 'yes' or 'cancel')\n");
                    if (userComfirm == "yes")
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("\n*************************************\n");
                        Console.WriteLine("\nTransaction Success! Here's your receipt: \n");
                        ConfirmationReceipt userReceipt = new ConfirmationReceipt("Alex", payMethod, itemMakes, paymentTotal, DateTime.Now.AddDays(2).AddHours(2), 29378001, DateTime.Now);
                        Console.WriteLine(userReceipt);
                        Console.WriteLine("\n Thank you for your purchase! Have a great day!\n");
                        Console.WriteLine("\n*************************************\n");
                        purchasedCarsList.Clear();
                        purchasedTrucksList.Clear();
                    }
                    else
                    {
                        return;
                    }

                }
                else
                {
                    return;
                }
            }



        }
    }
    
}
