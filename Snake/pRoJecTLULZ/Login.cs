using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pRoJecTLULZ
{
    public class Login
    {
        Connection connection = Connection.getInstance();

        public async Task<bool> Logins()
        {
            Console.Write("Enter username: ");
            string username = Console.ReadLine();

            Console.Write("Enter password: ");
            string password = Console.ReadLine();

            string result = await connection.Login(username, password);
            Console.WriteLine(result);
            if(result.Count(t => t == '-') == 4)
            {
                // result == sessionToken, spara för att kunna lägga til    l score senare
                Console.WriteLine("Login successful. Welcome, " + username + "!");
                return true;
            }
            else
            {
                Console.WriteLine("Invalid username or password. Please try again.");
                return false;
            }
        }

        public async Task<bool> Register()
        {
            Console.Write("Enter username: ");
            string username = Console.ReadLine();

            Console.Write("Enter password: ");
            string password = Console.ReadLine();

            string result = await connection.Register(username, password);

            if (result.Equals("User created"))
            {
                Console.WriteLine("You are now registered {0}", username);
                return true;
            }
            else
            {
                Console.WriteLine("Username already exists, choose another one");
                return false;
            }
        }

        public async Task<bool> thing() 
        { 
            bool isRunning = true;

            while (isRunning)
            {
                Console.WriteLine("1. Register");
                Console.WriteLine("2. Login");
                Console.WriteLine("3. Exit");
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        while(!await Register())
                        {
                        }
                        break;
                    case "2":
                        if(await Logins())
                        {
                            return true;
                        }
                        break;
                    case "3":
                        isRunning = false;
                        return false;
                    default:
                        Console.WriteLine("Error, please try again.");
                        break;
                }

                //Console.WriteLine();
            }

            return true;
        }

    }
}
