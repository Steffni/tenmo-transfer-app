using System;
using System.Collections.Generic;
using TenmoClient.Data;

namespace TenmoClient
{
    public class ConsoleService
    {
        /// <summary>
        /// Prompts for transfer ID to view, approve, or reject
        /// </summary>
        /// <param name="action">String to print in prompt. Expected values are "Approve" or "Reject" or "View"</param>
        /// <returns>ID of transfers to view, approve, or reject</returns>
        /// 

        public int PromptForTransferID(string action)
        {
            Console.WriteLine("");
            Console.Write("Please enter transfer ID to " + action + " (0 to cancel): ");
            if (!int.TryParse(Console.ReadLine(), out int transferId))
            {
                Console.WriteLine("Invalid input. Only input a number.");
                return 0;
            }
            else
            {
                return transferId;
            }
        }


        public Transfer PromptForTransferData()
        {
            Transfer transfer = new Transfer();
            transfer.AccountFrom = UserService.GetUserId();
            transfer.StatusId = 1;
            transfer.TypeId = 2;

            int accountTo = -1;
            decimal amount = -1;
            bool isGoodInput = false;

            while (!isGoodInput || accountTo < 0)
            {
                Console.Write("Enter User ID you wish to transfer to: ");
                isGoodInput = int.TryParse(Console.ReadLine(), out accountTo);

                if (isGoodInput && accountTo > 0)
                {
                    transfer.AccountTo = accountTo;
                }
                else
                {
                    Console.WriteLine("Invalid input. Only input an existing User ID number.");
                }
            }

            while (!isGoodInput || amount < 0)
            {
                Console.Write("Enter the amount you wish to transfer to " + accountTo + ": ");
                isGoodInput = decimal.TryParse(Console.ReadLine(), out amount);

                if (isGoodInput && amount > 0)
                {
                    transfer.Amount = amount;
                }
                else
                {
                    Console.WriteLine("Invalid input. Only input a valid dollar amount.");
                }
            }
            return transfer;
        }



        public LoginUser PromptForLogin()
        {
            Console.Write("Username: ");
            string username = Console.ReadLine();
            string password = GetPasswordFromConsole("Password: ");

            LoginUser loginUser = new LoginUser
            {
                Username = username,
                Password = password
            };
            return loginUser;
        }


        private string GetPasswordFromConsole(string displayMessage)
        {
            string pass = "";
            Console.Write(displayMessage);
            ConsoleKeyInfo key;

            do
            {
                key = Console.ReadKey(true);

                // Backspace Should Not Work
                if (!char.IsControl(key.KeyChar))
                {
                    pass += key.KeyChar;
                    Console.Write("*");
                }
                else
                {
                    if (key.Key == ConsoleKey.Backspace && pass.Length > 0)
                    {
                        pass = pass.Remove(pass.Length - 1);
                        Console.Write("\b \b");
                    }
                }
            }
            // Stops Receving Keys Once Enter is Pressed
            while (key.Key != ConsoleKey.Enter);
            Console.WriteLine("");
            return pass;
        }
    }
}
