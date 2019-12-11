using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Booking
{
    class Account : Serializer
	{
		protected string Password;

		public void SetPassword()
		{
			string newPassword;

			do
			{
				newPassword = null;

				Console.Write(" < Enter your password > \n>");
				newPassword = ReadPassword();

				if (newPassword.Length <= 4)
				{
					Program.WriteColorLine(" < Password must to contain more than 4 letters > ", ConsoleColor.Red);
					newPassword = null;
				}
				else
				{
					Password = newPassword;
				}

			} while (newPassword == null);
			
		}

		protected bool VerifyPassword(string newPassword)
		{
			return Password.Equals(newPassword);
		}

		public static string ReadPassword()
		{
			string password = string.Empty;
			ConsoleKeyInfo key;
			do
			{
				key = Console.ReadKey(true);
				if (key.Key == ConsoleKey.Enter) break;
				if (key.Key == ConsoleKey.Backspace)
				{
					if (password.Length != 0)
					{
						password = password.Remove(password.Length - 1);
						Console.Write("\b \b");
					}
				}
				else
				{
					password += key.KeyChar;
					Console.Write("*");
				}
			}
			while (true);

			Console.WriteLine();
			return password;
		}

    }
}
