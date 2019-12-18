using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Booking
{
	[Serializable]
	class BookingsBase
	{
		public List<Booking> Base = null;

		public BookingsBase()
		{
			Base = new List<Booking>();
		}

		public void OutputBookings()
		{
			if (Base.Count == 0)
			{
				Program.WriteColorLine(" < There's no bookings in base > ", ConsoleColor.Magenta);
				return;
			}

			Console.WriteLine(Base.Count == 1 ? " < Booking > " : " < Bookings > ");
			Booking.GetHeadOfTable();

			foreach (Booking CurFlight in Base) CurFlight.GetInfo();
			Console.WriteLine();
		}
		public void OutputBookings(List<int> Indices)
		{
			if (Indices.Count == 0)
			{
				Program.WriteColorLine(" < There's no bookings > ", ConsoleColor.Magenta);
				return;
			}

			Console.WriteLine(Indices.Count == 1 ? " < Booking > " : " < Bookings > ");
			Booking.GetHeadOfTable();
			foreach (int Ind in Indices) Base[Ind].GetInfo();
			Console.WriteLine();
		}
		private void DelFromBase(Booking CurBooking)
		{
			int ind = Base.IndexOf(CurBooking);
			Base.RemoveAt(ind);
			Base.RefreshInd();
		}
		public Booking BookFlight(Flight CurFlight, Customer CurCust)
		{
			Booking NewBooking = null;

			if (CurFlight != null)
			{
				if (CurFlight.Tickets.Base.Count >= CurFlight.NumberOfSeats)
					Program.WriteColorLine(" < There is no free seats on this flight > \n", ConsoleColor.Red);
				else
				{
					NewBooking = new Booking(CurFlight, CurCust);
					NewBooking.Register();
					Base.Add(NewBooking);
					CurFlight.Tickets.Base.Add(NewBooking);
					NewBooking.IDBooking = Base.IndexOf(NewBooking);

					NewBooking.SeatNumber = CurFlight.Tickets.Base.IndexOf(NewBooking);

					Program.WriteColorLine(" < Your booking has been successfuly created > \n", ConsoleColor.Green);
				}
			}
			else
				Program.WriteColorLine(" < No current flight specified > ", ConsoleColor.Red);

			return NewBooking;
		}
		public static void OutputCurrentBooking(Booking CurBooking)
		{
			if (CurBooking != null)
			{
				Booking.GetHeadOfTable();
				CurBooking.GetInfo();
				Console.WriteLine();
			}
			else Program.WriteColorLine(" < No current booking specified > ", ConsoleColor.Magenta);
		}

		public Booking FindByPoints()
		{
			string origin = null, destination = null;
			List<int> Indices = new List<int>();
			int Ind = -1;

			Console.WriteLine(" < Booking search > ");

			Console.Write(" < Enter origin of flight, or empty line to skip this filter > \n>");

			origin = Console.ReadLine();

			if (String.IsNullOrEmpty(origin)) Console.Write(" < Enter destination of flight of booking > \n>");
			else Console.Write(" < Enter destination of flight of booking, or empty line to skip this filter > \n>");

			destination = Console.ReadLine();

			Indices = FindIndbyPonts(origin, destination);

			if (Indices.Count == 0)
			{
				Console.WriteLine(" < No bookings were found > ");
				return null;
			}
			else
			{
				OutputBookings(Indices);
				if (Indices.Count > 1) Ind = ChooseItem(Indices);
				else Ind = Indices[0];
			}

			return Ind == -1 ? null : Base[Ind];
		}
		private List<int> FindIndbyPonts(string origin, string destination)
		{
			List<int> Indices = new List<int>();

			foreach (Booking CurBooking in Base)
			{
				if ((CurBooking.Flight.Origin == origin && CurBooking.Flight.Destination == destination) ||
					(String.IsNullOrEmpty(origin) && CurBooking.Flight.Destination == destination) ||
					(String.IsNullOrEmpty(destination) && CurBooking.Flight.Origin == origin))
				{
					Indices.Add(CurBooking.IDBooking);
				}

			}
			return Indices;
		}

		public static int ChooseItem(List<int> Indices)
		{
			int Ind = -1;
			Console.Write(" < Enter ID(number) of booking > \n>");

			try
			{
				Ind = int.Parse(Console.ReadLine());

			}
			catch (FormatException)
			{
				Program.WriteColorLine(" < Wrong format of ID > \n", ConsoleColor.Red);
				return Ind;
			}
			catch (ArgumentNullException)
			{
				Program.WriteColorLine(" < You need to enter ID > ", ConsoleColor.Red);
				return Ind;
			}

			if (!Indices.Contains(Ind))
			{
				Program.WriteColorLine(" < There is no booking with such ID in this context > \n", ConsoleColor.Magenta);
				return -1;
			}
			return Ind;
		}


		public Booking FindByDate(FlightsTime mode)
		{
			List<int> Indices = new List<int>();
			int Ind = -1;

			var Time = (earliestTime: DateTime.MinValue, latestTime: DateTime.MinValue);

			do
			{
				do
				{
					Console.Write($" < Enter earliest {(mode == FlightsTime.arrival ? "arrival" : "departure")} time of flight of booking > \n>");
					Time.earliestTime = Flight.EnterDateTime();

				} while (Time.earliestTime == DateTime.MinValue);
				do
				{
					Console.Write($" < Enter latest {(mode == FlightsTime.arrival ? "arrival" : "departure")} time of flight of booking > \n>");
					Time.latestTime = Flight.EnterDateTime();

				} while (Time.latestTime == DateTime.MinValue);

			} while (!Flight.CheckTime(Time.earliestTime, Time.latestTime));

			Indices = FindIndbyDate(Time, mode);

			if (Indices.Count == 0)
			{
				Console.WriteLine(" < No bookings were found > ");
				return null;
			}
			else
			{
				OutputBookings(Indices);
				if (Indices.Count > 1) Ind = ChooseItem(Indices);
				else Ind = Indices[0];
			}

			return Ind == -1 ? null : Base[Ind];
		}

		private List<int> FindIndbyDate((DateTime earliestTime, DateTime latestTime) Time, FlightsTime mode)
		{
			List<int> Indices = new List<int>();

			foreach (Booking CurBooking in Base)
			{

				if (DateTime.Compare(CurBooking.Flight[mode], Time.earliestTime) >= 0 &&
					DateTime.Compare(CurBooking.Flight[mode], Time.latestTime) <= 0)
				{
					Indices.Add(CurBooking.IDBooking);
				}

			}
			return Indices;
		}

		public void DelCurrentBooking(Booking CurBooking)
		{
			if (CurBooking != null)
			{
				Console.WriteLine(" < Do really want to delete curent booking \"Y\"/\"N\" > ");
				char delete = Program.EnterMode();
				if (delete.Equals('Y') || delete.Equals('y'))
				{
					DelFromBase(CurBooking);
					Program.WriteColorLine(" < Booking was successfully deleted > \n", ConsoleColor.Green);
				}
			}
			else Program.WriteColorLine(" < No current booking specified > ", ConsoleColor.Magenta);
		}
	}
}
