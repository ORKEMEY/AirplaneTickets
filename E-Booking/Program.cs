using System;
using System.Collections.Generic;

namespace E_Booking
{
	class Program
	{
		static void Main(string[] args)
		{

			Console.WriteLine(" < E-Bookong, Artem Poshukailo, IS-82, V-2 > ");

			var Manager = FlightsManager.GetSource();

			Program.LogIn();

			Serializer.Serialize<FlightsBase>(Manager.FlightsBase, "FlightsBase.dat");

			WriteColorLine(" < Process comletion > ", ConsoleColor.Magenta);
			Console.WriteLine(" < Enter any key > \n>");
			Console.ReadKey();
		}

		public static void MainMenu()
		{
			Console.WriteLine(" < Menu > ");

			Console.Write(" < Enter \""); WriteColor("o", ConsoleColor.DarkCyan); Console.WriteLine("\" to output menu > ");
			Console.Write(" < Enter \""); WriteColor("r", ConsoleColor.DarkCyan); Console.WriteLine("\" to register > ");
			Console.Write(" < Enter \""); WriteColor("m", ConsoleColor.DarkCyan); Console.WriteLine("\" to log in as manager > ");
			Console.Write(" < Enter \""); WriteColor("q", ConsoleColor.DarkCyan); Console.WriteLine("\" to quit > ");
			Console.WriteLine();
		}


		public static void LogIn()
		{
			FlightsManager Manager = FlightsManager.GetSource();
			//Customer CurCust = null;
			char mode = ' ';
			MainMenu();

			do
			{
				mode = Program.EnterMode();
				switch (mode)
				{

					case 'o':
					case 'O':
						MainMenu();
						break;

					case 'r':
					case 'R':
						Manager.CustomersBase.RegistrateCustomer();
						Console.WriteLine(" < Your account has been successfuly created > \n");
						break;

					case 'm':
					case 'M':

						if (Manager.EnterPassword())
						{
							Manager.Menu();
							MainMenu();
						}

						break;

					case 'q':
					case 'Q':
						break;


					default:
						WriteColorLine(" < Wrong mode > ", ConsoleColor.Red);
						break;
				}

			} while (mode != 'q' && mode != 'Q');

		}

		public static void WriteColorLine(string String, ConsoleColor Color)
		{
			Console.ForegroundColor = Color;
			Console.WriteLine(String);
			Console.ResetColor();
		}

		public static void WriteColor(string String, ConsoleColor Color)
		{
			Console.ForegroundColor = Color;
			Console.Write(String);
			Console.ResetColor();
		}

		public static char EnterMode()
		{
			char mode = ' ';
			try
			{
				Console.Write(" < Enter mode > \n>");
				mode = char.Parse(Console.ReadLine());
			}
			catch (FormatException)
			{
			}

			return mode;
		}

	}

	public static class ListExtension
	{
		internal static void RefreshInd(this List<Flight> CurList)
		{

			for (int count = 0; count < CurList.Count; count++)
			{
				CurList[count].IDFlight = count;

			}
		}

		internal static void RefreshInd(this List<Customer> CurList)
		{
			for (int count = 0; count < CurList.Count; count++)
			{
				CurList[count].IDCustomer = count;

			}
		}

		internal static void RefreshInd(this List<Booking> CurList)
		{
			for (int count = 0; count < CurList.Count; count++)
			{
				CurList[count].IDBooking = count;

			}
		}
	}

}
