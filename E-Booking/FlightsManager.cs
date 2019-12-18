using System;
using System.Collections.Generic;

namespace E_Booking
{
	class FlightsManager : Account
	{
		public CustomersBase Customers { get; set; }
		public FlightsBase Flights { get; set; }

		private static FlightsManager Source;
		public static FlightsManager GetSource()
		{
			if (Source == null)
				Source = new FlightsManager();
			return Source;
		}

		private FlightsManager()
		{
			Password = "12345";
			Customers = CustomersBase.GetSource();
			Flights = Deserialize<FlightsBase>("FlightsBase.dat");
			if(Flights == default(FlightsBase)) Flights = new FlightsBase();
		}

		private static void OutputManagersMenu()
		{
			Console.WriteLine(" < Menu > ");

			Console.Write(" < Enter \""); Program.WriteColor("o", ConsoleColor.DarkCyan); Console.WriteLine("\" to output menu > ");
			Console.Write(" < Enter \""); Program.WriteColor("с", ConsoleColor.DarkCyan); Console.WriteLine("\" to switch to customers menu > ");
			Console.Write(" < Enter \""); Program.WriteColor("f", ConsoleColor.DarkCyan); Console.WriteLine("\" to switch to flights menu > ");
			Console.Write(" < Enter \""); Program.WriteColor("q", ConsoleColor.DarkCyan); Console.WriteLine("\" to quit > ");
			Console.WriteLine();
		}

		public void Menu()
		{
			char mode = ' ';

			Console.Clear();
			OutputManagersMenu();

			do
			{
				mode = Program.EnterMode();

				switch (mode)
				{

					case 'o':
					case 'O':
						OutputManagersMenu();
						break;

					case 'c':
					case 'C':
						CustomerMenu();
						OutputManagersMenu();
						break;

					case 'f':
					case 'F':
						FlightMenu();
						OutputManagersMenu();
						break;

					case 'q':
					case 'Q':
						Console.Clear();
						break;

					default:
						Program.WriteColorLine(" < Wrong mode > ", ConsoleColor.Red);
						break;
				}

			} while (mode != 'q' && mode != 'Q');

		}

		private static void OutputFlightMenu()
		{
			Console.WriteLine(" < Menu > ");
			Console.Write(" < Enter \""); Program.WriteColor("o", ConsoleColor.DarkCyan); Console.WriteLine("\" to output menu > ");
			Console.Write(" < Enter \""); Program.WriteColor("r", ConsoleColor.DarkCyan); Console.WriteLine("\" to register new flight > ");
			Program.WriteColorLine("-------------------------------------------", ConsoleColor.Green);
			Console.Write(" < Enter \""); Program.WriteColor("a", ConsoleColor.DarkCyan); Console.WriteLine("\" to output all flights > ");
			Console.Write(" < Enter \""); Program.WriteColor("i", ConsoleColor.DarkCyan); Console.WriteLine("\" to output current flight > ");
			Program.WriteColorLine("-------------------------------------------", ConsoleColor.Green);
			Console.Write(" < Enter \""); Program.WriteColor("p", ConsoleColor.DarkCyan); Console.WriteLine("\" to find flight by points > ");
			Console.Write(" < Enter \""); Program.WriteColor("k", ConsoleColor.DarkCyan); Console.WriteLine("\" to find flight by date of arrival > ");
			Console.Write(" < Enter \""); Program.WriteColor("l", ConsoleColor.DarkCyan); Console.WriteLine("\" to find flight by date of departure > ");
			Program.WriteColorLine("-------------------------------------------", ConsoleColor.Green);
			Console.Write(" < Enter \""); Program.WriteColor("d", ConsoleColor.DarkCyan); Console.WriteLine("\" to delete current flight > ");
			Console.Write(" < Enter \""); Program.WriteColor("q", ConsoleColor.DarkCyan); Console.WriteLine("\" to quit > ");
			Console.WriteLine();
		}

		public void FlightMenu( )
		{
			Flight CurFlight = null;
			char mode = ' ';

			Console.Clear();
			OutputFlightMenu();
			
			do
			{
				mode = Program.EnterMode();
				switch (mode)
				{

					case 'o':
					case 'O':
						OutputFlightMenu();
						break;

					case 'p':
					case 'P':

						CurFlight = Flights.FindByPoints();
						Program.WriteColorLine(" < Current flight was updated > \n", ConsoleColor.Green);
						OutputFlightMenu();
						break;

					case 'a':
					case 'A':
						Flights.OutputFlights();
						break;

					case 'd':
					case 'D':
						Flights.DelCurrentFlight(CurFlight);
						break;

					case 'i':
					case 'I':
						FlightsBase.OutputCurrentFlight(CurFlight);
						break;

					case 'r':
					case 'R':
						CurFlight = Flights.RegistrateFlight();
						Program.WriteColorLine(" < Current flight was updated > \n", ConsoleColor.Green);
						break;
					case 'k':
					case 'K':
						CurFlight = Flights.FindByDate(FlightsTime.arrival);
						Program.WriteColorLine(" < Current flight was updated > \n", ConsoleColor.Green);
						break;

					case 'l':
					case 'L':
						CurFlight = Flights.FindByDate(FlightsTime.departure);
						Program.WriteColorLine(" < Current flight was updated > \n", ConsoleColor.Green);
						break;

					case 'q':
					case 'Q':
						break;

					default:
						Program.WriteColorLine(" < Wrong mode > ", ConsoleColor.Red);
						break;
				}

			} while (mode != 'q' && mode != 'Q');

			Console.Clear();
		}

		private static void OutputCustomerMenu()
		{
			Console.WriteLine(" < Menu > ");
			Console.Write(" < Enter \""); Program.WriteColor("o", ConsoleColor.DarkCyan); Console.WriteLine("\" to output menu > ");
			Console.Write(" < Enter \""); Program.WriteColor("a", ConsoleColor.DarkCyan); Console.WriteLine("\" to output all customers > ");
			Console.Write(" < Enter \""); Program.WriteColor("i", ConsoleColor.DarkCyan); Console.WriteLine("\" to output current account > ");
			Program.WriteColorLine("-------------------------------------------", ConsoleColor.Green);
			Console.Write(" < Enter \""); Program.WriteColor("f", ConsoleColor.DarkCyan); Console.WriteLine("\" to find customer by login > ");
			Console.Write(" < Enter \""); Program.WriteColor("d", ConsoleColor.DarkCyan); Console.WriteLine("\" to delete current flight > ");
			Console.Write(" < Enter \""); Program.WriteColor("q", ConsoleColor.DarkCyan); Console.WriteLine("\" to quit > ");
			Console.WriteLine();
		}

		public void CustomerMenu()
		{
			Customer CurCust = null;
			char mode = ' ';

			Console.Clear();
			OutputCustomerMenu();

			do
			{
				mode = Program.EnterMode();
				switch (mode)
				{
					case 'o':
					case 'O':
						OutputCustomerMenu();
						break;

					case 'a':
					case 'A':
						Customers.OutputCustomers();
						break;

					case 'f':
					case 'F':
						CurCust = Customers.FindByLogin();
						Program.WriteColorLine(" < Current account was updated > \n", ConsoleColor.Green);
						break;

					case 'd':
					case 'D':
						Customers.DelCurrentCustomer(CurCust);
						break;

					case 'i':
					case 'I':
						CustomersBase.OutputCurrentCustomer(CurCust);
						break;

					case 'q':
					case 'Q':
						break;

					default:
						Program.WriteColorLine(" < Wrong mode > ", ConsoleColor.Red);
						break;
				}

			} while (mode != 'q' && mode != 'Q');

			Console.Clear();
		}
	}
}
