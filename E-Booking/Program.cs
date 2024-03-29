﻿using System;
using System.Collections.Generic;

namespace E_Booking
{
	class Program
	{
		static void Main(string[] args)
		{
			Console.WriteLine(" < E-Bookong, Artem Poshukailo, IS-82, V-8 > ");

			var CustBase = CustomersBase.GetSource();
			var Manager = FlightsManager.GetSource();

			Program.LogIn();

			Serializer.Serialize<FlightsBase>(Manager.Flights, "FlightsBase.dat");
			Serializer.Serialize<CustomersBase>(CustBase, "CustomersBase.dat");

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
			Console.Write(" < Enter \""); WriteColor("c", ConsoleColor.DarkCyan); Console.WriteLine("\" to log in as customer > ");
			Console.Write(" < Enter \""); WriteColor("q", ConsoleColor.DarkCyan); Console.WriteLine("\" to quit > ");
			Console.WriteLine();
		}

		public static void LogIn()
		{
			FlightsManager Manager = FlightsManager.GetSource();
			CustomersBase CustBase = CustomersBase.GetSource();
			Customer CurCust = null;
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

						Manager.Customers.RegistrateCustomer();
						Program.WriteColorLine(" < Your account has been successfuly created > \n", ConsoleColor.Green);
						break;

					case 'm':
					case 'M':

						if (Manager.EnterPassword())
						{
							Manager.Menu();
							MainMenu();
						}

						break;

					case 'c':
					case 'C':

						CurCust = CustBase.FindByLogin();
						if (CurCust != null )
						{
							if (CurCust.EnterPassword())
							{
								CurCust.CustomersMenu();
								MainMenu();
							}
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

	delegate bool CheckLogin(string CurLogin);

	enum FlightsTime { departure, arrival }

	[Serializable]
	struct Baggage
	{
		public bool HandLuggage;
		public bool Suitcase;
		public Baggage(bool handLuggage, bool suitcase)
		{
			HandLuggage = handLuggage;
			Suitcase = suitcase;
		}

	}
}
