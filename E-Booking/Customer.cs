using System;
using System.Collections.Generic;

namespace E_Booking
{
	[Serializable]
    class Customer : Account
    {
		public int IDCustomer;
		private string _Name;
		private string _Surname;
		private int _Age;
		public string Login;

		public string Name
		{
			get
			{
				return _Name;
			}
			set
			{
				if(char.IsLower(value[0]))
				{
					Program.WriteColorLine(" < Name cann't begin with lower case > ", ConsoleColor.Red);
					_Name = null;
				} else if (String.IsNullOrEmpty(value))
				{
					Program.WriteColorLine(" < Name cann't be empty > ", ConsoleColor.Red);
					_Name = null;
				}
				else
				{
					_Name = value;
				}
				
			}
		}
		public string Surname
		{
			get
			{
				return _Surname;
			}
			set
			{
				if (char.IsLower(value[0]))
				{
					Program.WriteColorLine(" < Surame cann't begin with lower case > ", ConsoleColor.Red);
					_Surname = null;
				}
				else if (String.IsNullOrEmpty(value))
				{
					Program.WriteColorLine(" < Surname cann't be empty > ", ConsoleColor.Red);
					_Surname = null;
				}
				else
				{
					_Surname = value;
				}

			}
		}
		public int Age
		{
			get
			{
				return _Age;
			}
			set
			{

				if(value <= 0)
				{
					Program.WriteColorLine(" < Age cnann't be equal null or be less than null > ", ConsoleColor.Red);
					_Age = -1;
				}else if(value > 125)
				{
					Program.WriteColorLine(" < Age is too big > ", ConsoleColor.Red);
					_Age = -1;
				}
				else
				{
					_Age = value;
				}
			}
		}
		public BookingsBase BookedFlights;

		public Customer()
		{
			_Name = _Surname = Login = null;
			_Age = IDCustomer = -1;
			BookedFlights = new BookingsBase();
		}


		public void Registration(CheckLogin IsLoginFree)
		{
			bool isFree;

			Console.WriteLine(" < Registration of new customer > ");

			do
			{
				Console.Write(" < Enter your name > \n>");
				Name = Console.ReadLine();

			} while (_Name == null);

			do
			{
				Console.Write(" < Enter your surame > \n>");
				Surname = Console.ReadLine();
			} while (_Surname == null);
			
			do
			{
				Console.Write(" < Enter your age > \n>");

				try
				{
					Age = Convert.ToInt32(Console.ReadLine());
				}
				catch (FormatException)
				{
					Program.WriteColorLine(" < Wrong format, age is a number > ", ConsoleColor.Red);
				}

			} while (Age == -1);

			do
			{
				Login = EnterLogin();
				isFree = IsLoginFree.Invoke(Login);
				if (!isFree) Program.WriteColorLine(" < This login isn't available, choose another > ", ConsoleColor.Magenta);

			} while (!isFree);

			SetPassword();
			
		}

		public static string EnterLogin()
		{
			string enteredLogin = null;
			Console.Write(" < Enter your login > \n>");
			enteredLogin = Console.ReadLine();
			return enteredLogin;
		}

		public static void GetHeadOfTable()
		{
			Console.WriteLine(" ID \t| Age \t| Login \t| Name \t\t| Surname");
			Console.WriteLine("____________________________________________" +
				"__________________");

		}

		public void GetInfo()
		{
			Console.WriteLine($" {IDCustomer} \t| {Age} \t| {Login} \t\t| {Name} \t| {Surname} ");
			Console.WriteLine("____________________________________________" +
				"__________________");

		}

		private static void OutputCustomersMenu()
		{
			Console.WriteLine(" < Menu > ");

			Console.Write(" < Enter \""); Program.WriteColor("o", ConsoleColor.DarkCyan); Console.WriteLine("\" to output menu > ");
			Console.Write(" < Enter \""); Program.WriteColor("f", ConsoleColor.DarkCyan); Console.WriteLine("\" to switch to flights menu > ");
			Console.Write(" < Enter \""); Program.WriteColor("b", ConsoleColor.DarkCyan); Console.WriteLine("\" to switch to bookings menu > ");
			Console.Write(" < Enter \""); Program.WriteColor("q", ConsoleColor.DarkCyan); Console.WriteLine("\" to quit > ");
			Console.WriteLine();
		}

		public void CustomersMenu()
		{
			char mode = ' ';

			Console.Clear();
			OutputCustomersMenu();

			do
			{
				mode = Program.EnterMode();

				switch (mode)
				{

					case 'o':
					case 'O':
						OutputCustomersMenu();
						break;

					case 'q':
					case 'Q':
						Console.Clear();
						break;

					case 'f':
					case 'F':
						FlightMenu();
						OutputCustomersMenu();
						break;

					case 'b':
					case 'B':
						BookingMenu();
						OutputCustomersMenu();
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
			Console.Write(" < Enter \""); Program.WriteColor("a", ConsoleColor.DarkCyan); Console.WriteLine("\" to output all flights > ");
			Console.Write(" < Enter \""); Program.WriteColor("i", ConsoleColor.DarkCyan); Console.WriteLine("\" to output current flight > ");
			Program.WriteColorLine("-------------------------------------------", ConsoleColor.Green);
			Console.Write(" < Enter \""); Program.WriteColor("b", ConsoleColor.DarkCyan); Console.WriteLine("\" to book current flight > ");
			Program.WriteColorLine("-------------------------------------------", ConsoleColor.Green);
			Console.Write(" < Enter \""); Program.WriteColor("p", ConsoleColor.DarkCyan); Console.WriteLine("\" to find flight by points > ");
			Console.Write(" < Enter \""); Program.WriteColor("k", ConsoleColor.DarkCyan); Console.WriteLine("\" to find flight by date of arrival > ");
			Console.Write(" < Enter \""); Program.WriteColor("l", ConsoleColor.DarkCyan); Console.WriteLine("\" to find flight by date of departure > ");
			Program.WriteColorLine("-------------------------------------------", ConsoleColor.Green);
			Console.Write(" < Enter \""); Program.WriteColor("q", ConsoleColor.DarkCyan); Console.WriteLine("\" to quit > ");
			Console.WriteLine();
		}

		public void FlightMenu()
		{
			Flight CurFlight = null;
			
			FlightsManager Manager = FlightsManager.GetSource();
			FlightsBase Flights = Manager.Flights;
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
						break;

					case 'a':
					case 'A':
						Flights.OutputFlights();
						break;

					case 'i':
					case 'I':
						FlightsBase.OutputCurrentFlight(CurFlight);
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

					case 'b':
					case 'B':
						BookedFlights.BookFlight(CurFlight, this);
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

		private static void OutputBookingMenu()
		{
			Console.WriteLine(" < Menu > ");
			Console.Write(" < Enter \""); Program.WriteColor("o", ConsoleColor.DarkCyan); Console.WriteLine("\" to output menu > ");
			Console.Write(" < Enter \""); Program.WriteColor("i", ConsoleColor.DarkCyan); Console.WriteLine("\" to output current booking > ");
			Console.Write(" < Enter \""); Program.WriteColor("a", ConsoleColor.DarkCyan); Console.WriteLine("\" to output all your bookings> ");
			Program.WriteColorLine("-------------------------------------------", ConsoleColor.Green);
			Console.Write(" < Enter \""); Program.WriteColor("p", ConsoleColor.DarkCyan); Console.WriteLine("\" to find booking by points of flight > ");
			Console.Write(" < Enter \""); Program.WriteColor("k", ConsoleColor.DarkCyan); Console.WriteLine("\" to find booking by date of arrival of flight > ");
			Console.Write(" < Enter \""); Program.WriteColor("l", ConsoleColor.DarkCyan); Console.WriteLine("\" to find booking by date of departure of flight > ");
			Program.WriteColorLine("-------------------------------------------", ConsoleColor.Green);
			Console.Write(" < Enter \""); Program.WriteColor("b", ConsoleColor.DarkCyan); Console.WriteLine("\" to buy ticket by current booking > ");
			Console.Write(" < Enter \""); Program.WriteColor("d", ConsoleColor.DarkCyan); Console.WriteLine("\" to delete current booking > ");
			Console.Write(" < Enter \""); Program.WriteColor("q", ConsoleColor.DarkCyan); Console.WriteLine("\" to quit > ");
			Console.WriteLine();
		}

		public void BookingMenu()
		{
			Booking CurBooking = null;

			FlightsManager Manager = FlightsManager.GetSource();
			FlightsBase Flights = Manager.Flights;
			char mode = ' ';

			Console.Clear();
			OutputBookingMenu();

			do
			{
				mode = Program.EnterMode();
				switch (mode)
				{

					case 'o':
					case 'O':
						OutputBookingMenu();
						break;

					case 'a':
					case 'A':
						BookedFlights.OutputBookings();
						break;

					case 'i':
					case 'I':
						BookingsBase.OutputCurrentBooking(CurBooking);
						break;

					case 'b':
					case 'B':
						Booking.BuyTicket(CurBooking);
						break;

					case 'k':
					case 'K':
						CurBooking = BookedFlights.FindByDate(FlightsTime.arrival);
						Program.WriteColorLine(" < Current booking was updated > \n", ConsoleColor.Green);
						break;

					case 'l':
					case 'L':
						CurBooking = BookedFlights.FindByDate(FlightsTime.departure);
						Program.WriteColorLine(" < Current booking was updated > \n", ConsoleColor.Green);
						break;

					case 'p':
					case 'P':
						CurBooking = BookedFlights.FindByPoints();
						Program.WriteColorLine(" < Current booking was updated > \n", ConsoleColor.Green);
						break;

					case 'd':
					case 'D':
						BookedFlights.DelCurrentBooking(CurBooking);
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
