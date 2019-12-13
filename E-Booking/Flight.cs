using System;
using System.Collections.Generic;

namespace E_Booking
{
	[Serializable]
	class Flight
    {
		private string _Origin;
		private string _Destination;
		private DateTime _Departure;
		private DateTime _Arrival;

		public int IDFlight;

		private int _NumberOfSeats;

		public List<Booking> Tickets;

		public int NumberOfSeats
		{
			get
			{
				return _NumberOfSeats;
			}
			set
			{
				if(value <= 0)
				{
					Program.WriteColorLine(" < Number of seats in aeroplane cann't be less than null or be equal > ", ConsoleColor.Red);
					_NumberOfSeats = -1;
				}
				else if (value > 750)
				{
					Program.WriteColorLine(" < Number of seats in aeroplane is too big > ", ConsoleColor.Red);
					_NumberOfSeats = -1;
				}
				else
				{
					_NumberOfSeats = value;
				}
			}
		}
		public string Origin
		{
			get
			{
				return _Origin;
			}
			set
			{
				if (char.IsLower(value[0]))
				{
					Program.WriteColorLine(" < Geographical name cann't begin with lower case > ", ConsoleColor.Red);
					_Origin = null;
				}
				else if (String.IsNullOrEmpty(value))
				{
					Program.WriteColorLine(" < Geographical name cann't be empty > ", ConsoleColor.Red);
					_Origin = null;
				}
				else
				{
					_Origin = value;
				}
			}
		}
		public string Destination
		{
			get
			{
				return _Destination;
			}
			set
			{
				if (char.IsLower(value[0]))
				{
					Program.WriteColorLine(" < Geographical name cann't begin with lower case > ", ConsoleColor.Red);
					_Destination = null;
				}
				else if (String.IsNullOrEmpty(value))
				{
					Program.WriteColorLine(" < Geographical name cann't be empty > ", ConsoleColor.Red);
					_Destination = null;
				}
				else
				{
					_Destination = value;
				}
			}
		}
		public DateTime Departure
		{
			get
			{
				return _Departure;
			}
			set
			{
				if(DateTime.Compare(value, DateTime.Now) <= 0)
				{
					Program.WriteColorLine(" < This time cann't be set earlier than now > ", ConsoleColor.Red);
					_Arrival = DateTime.MinValue;
				}
				else
				{
					_Departure = value;
				}


			}
		}
		public DateTime Arrival
		{
			get
			{
				return _Arrival;
			}
			set
			{
				if (DateTime.Compare(value, DateTime.Now) <= 0)
				{
					Program.WriteColorLine(" < This time cann't be set earlier than now > ", ConsoleColor.Red);
					_Arrival = DateTime.MinValue;
				}
				else
				{
					_Arrival = value;
				}


			}
		}

		public Flight()
		{
			IDFlight = _NumberOfSeats = -1;
			_Origin = _Destination = null;
			_Departure = _Arrival = DateTime.MinValue;
			Tickets = new List<Booking>();
		}

		public void Registration()
		{
			var Time = (Departure, Arrival);

			Console.WriteLine(" < Registration of new flight > ");

			do
			{
				Console.Write(" < Enter origin of flight > \n>");
				Origin = Console.ReadLine();

			} while (Origin == null);

			do
			{
				Console.Write(" < Enter destination of flight > \n>");
				Destination = Console.ReadLine();

			} while (Destination == null);

			do
			{
				Console.Write(" < Enter number of seats in aeroplane > \n>");

				try
				{
					NumberOfSeats = Convert.ToInt32(Console.ReadLine());
				}
				catch(FormatException)
				{
					Program.WriteColorLine(" < Wrong format, number of seats is a number > ", ConsoleColor.Red);
				}

			} while (_NumberOfSeats == -1);

			SetTime(out Time);
			Departure = Time.Departure;
			Arrival = Time.Arrival;
			Console.WriteLine();
		}

		public static void SetTime(out (DateTime departure, DateTime arrival) Time)
		{
			
			do
			{
				do
				{
					Console.Write(" < Enter departure time of flight like \"dd.mm.yyyy hh:mm\" > \n>");
					Time.departure = EnterDateTime();

				} while (Time.departure == DateTime.MinValue);

				do
				{
					Console.Write(" < Enter arrival time of flight like \"dd.mm.yyyy hh:mm\" > \n>");
					Time.arrival = EnterDateTime();

				} while (Time.arrival == DateTime.MinValue);

			} while (!CheckTime(Time.departure, Time.arrival));

		}

		public static bool CheckTime(DateTime Departure, DateTime Arrival)
		{
			if (DateTime.Compare(Departure, Arrival) > 0)
			{
				Program.WriteColorLine( " < Arrival cann't be earlier than departure > " , ConsoleColor.Red);
				return false;
			}else if (DateTime.Compare(Departure, Arrival) == 0)
			{
				Program.WriteColorLine(" Departure and arrival cann't be at the same time > ", ConsoleColor.Red);
				return false;
			}
			return true;
		}

		public static DateTime EnterDateTime()
		{
			DateTime Time = DateTime.MinValue;
			try
			{
				Time = DateTime.Parse(Console.ReadLine());
			}
			catch (ArgumentNullException)
			{
				Program.WriteColorLine(" < You need to enter date like \"dd.mm.yyyy hh:mm\" > ", ConsoleColor.Red);
			}
			catch (FormatException)
			{
				Program.WriteColorLine(" < Wrong format of time, you need to enter date like \"dd.mm.yyyy hh:mm\"  > ", ConsoleColor.Red);
			}
			return Time;
		}

		public static void GetHeadOfTable()
		{
			Console.Write(" ID \t| Number of seats \t| Departure time \t|");
			Console.WriteLine(" Arrival time \t\t| Origin \t| Destination ");
			Console.WriteLine("_____________________________________________________________" +
				"____________________________________________________");
		}

		public void GetInfo()
		{
			Console.Write($" {IDFlight} \t| {NumberOfSeats} \t\t\t| {Departure.ToShortDateString()} {Departure.ToShortTimeString()} \t|");
			Console.WriteLine($" {Arrival.ToShortDateString()} {Arrival.ToShortTimeString()} \t| {Origin} \t\t| {Destination} ");
			Console.WriteLine("_____________________________________________________________" +
				"____________________________________________________");
		}

	}
}
