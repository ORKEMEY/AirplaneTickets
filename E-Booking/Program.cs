using System;
using System.Collections.Generic;

namespace E_Booking
{
    class Program
    {
        static void Main(string[] args)
        {

            Console.WriteLine(" < E-Bookong, Artem Poshukailo, IS-82, V-2 > ");

			FlightsManager Manager = FlightsManager.GetSource();

			Program.LogIn();

			WriteColorLine(" < Process comletion > ", ConsoleColor.Magenta);
			Console.WriteLine(" < Enter any key > \n>");
			Console.ReadKey();
        }

		public static void MainMenu()
		{
			Console.WriteLine(" < Menu > ");

			Console.Write(" < Enter \""); WriteColor("o", ConsoleColor.DarkCyan); Console.WriteLine("\" to output menu > "); 
			Console.Write(" < Enter \""); WriteColor("r", ConsoleColor.DarkCyan); Console.WriteLine("\" to register > "); 
			Console.Write(" < Enter \""); WriteColor("q", ConsoleColor.DarkCyan); Console.WriteLine("\" to quit > ");
			Console.WriteLine();
		}


		public static void LogIn()
		{
			FlightsManager Manager = FlightsManager.GetSource();
			Customer CurCust = null;
			char mode = ' ';

			MainMenu();

			do
			{

				try
				{
					Console.Write(" < Enter mode > \n>");
					mode = char.Parse(Console.ReadLine());
				}catch(FormatException)
				{
					WriteColorLine(" < Invalid argument > ", ConsoleColor.Red);
				}


				switch (mode)
				{

					case 'o':
					case 'O':
						MainMenu();
						break;

					case 'r':
					case 'R':
						CurCust = new Customer();
						Manager.CustomersBase.AddToBase(CurCust);
						Console.WriteLine(" < Your account has been successfuly created > \n");	
						break;

					case'q':
					case 'Q':
						break;


					default:
						WriteColorLine(" < Wrong mode > ", ConsoleColor.Red);
						break;
				}
				
			} while (mode != 'q' && mode != 'Q' );

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
