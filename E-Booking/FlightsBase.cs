using System;
using System.Collections.Generic;


namespace E_Booking
{
	[Serializable]
    class FlightsBase : Serializer
    {
		public List<Flight> Base = null;
		public FlightsBase()
		{
			Base = new List<Flight>();
		}

		public void OutputFlights(List<int> Indices)
		{
			if (Indices.Count == 0) 
			{
				Program.WriteColorLine(" < There's no flights > ", ConsoleColor.Magenta);
				return; 
			}

			Console.WriteLine(Indices.Count == 1 ? " < Flight > " : " < Flights > "); 
			Flight.GetHeadOfTable();

			foreach (int Ind in Indices) Base[Ind].GetInfo();
			Console.WriteLine();
		}
		public void OutputFlights()
		{
			if (Base.Count == 0)
			{
				Program.WriteColorLine(" < There's no flights in base > ", ConsoleColor.Magenta);
				return;
			}

			Console.WriteLine(Base.Count == 1 ? " < Flight > " : " < Flights > ");
			Flight.GetHeadOfTable();

			foreach (Flight CurFlight in Base) CurFlight.GetInfo();
			Console.WriteLine();
		}
		public void DelCurrentFlight(Flight CurFlight)
		{
			if (CurFlight != null)
			{
				Console.WriteLine(" < Do really want to delete curent flight \"Y\"/\"N\" > ");
				char delete = Program.EnterMode();
				if (delete.Equals('Y') || delete.Equals('y'))
				{
					DelFromBase(CurFlight);
					Program.WriteColorLine(" < Flight was successfully deleted > \n", ConsoleColor.Green);
				}
			}
			else Program.WriteColorLine(" < No current flight specified > ", ConsoleColor.Magenta);
		}
		public static void OutputCurrentFlight(Flight CurFlight)
		{
			if (CurFlight != null)
			{
				Flight.GetHeadOfTable();
				CurFlight.GetInfo();
				Console.WriteLine();
			}
			else Program.WriteColorLine(" < No current flight specified > ", ConsoleColor.Magenta);
		}
		public static int ChooseItem(List<int> Indices)
		{
			int Ind = -1;
			Console.Write(" < Enter ID(number) of flight > \n>");

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
				Program.WriteColorLine(" < There is no flight with such ID in this context > \n", ConsoleColor.Magenta);
				return -1;
			}
			return Ind;
		}
		public Flight FindByPoints()
		{
			string origin = null, destination = null;
			List<int> Indices = new List<int>();
			int Ind = -1;

			Console.WriteLine(" < Flight search > ");

			Console.Write(" < Enter origin of flight, or empty line to skip this filter > \n>");

			origin = Console.ReadLine();

			if (String.IsNullOrEmpty(origin)) Console.Write(" < Enter destination of flight > \n>");
			else Console.Write(" < Enter destination of flight, or empty line to skip this filter > \n>");

			destination = Console.ReadLine();

			Indices = FindIndbyPonts(origin, destination);

			if(Indices.Count == 0)
			{
				Console.WriteLine(" < No flights were found > ");
				return null;
			}
			else
			{
				OutputFlights(Indices);
				if (Indices.Count > 1) Ind = ChooseItem(Indices);
				else Ind = Indices[0];
			}

			return Ind == -1 ? null :  Base[Ind];
		}
		public Flight FindByDate(FlightsTime mode)
		{
			List<int> Indices = new List<int>();
			int Ind = -1;

			var Time = (earliestTime: DateTime.MinValue, latestTime: DateTime.MinValue);

			do
			{
				do
				{
					Console.Write($" < Enter earliest {(mode == FlightsTime.arrival ? "arrival" : "departure")} time of flight > \n>");
					Time.earliestTime = Flight.EnterDateTime();

				} while (Time.earliestTime == DateTime.MinValue);
				do
				{
					Console.Write($" < Enter latest {(mode == FlightsTime.arrival ? "arrival" : "departure")} time of flight > \n>");
					Time.latestTime = Flight.EnterDateTime();

				} while (Time.latestTime == DateTime.MinValue);

			} while (!Flight.CheckTime(Time.earliestTime, Time.latestTime));

			Indices = FindIndbyDate(Time, mode);

			if (Indices.Count == 0)
			{
				Console.WriteLine(" < No flights were found > ");
				return null;
			}
			else
			{
				OutputFlights(Indices);
				if (Indices.Count > 1) Ind = ChooseItem(Indices);
				else Ind = Indices[0];
			}

			return Ind == -1 ?  null : Base[Ind];
		}

		public Flight RegistrateFlight()
		{
			Flight CurFlight = new Flight();
			CurFlight.Registration();
			Base.Add(CurFlight);
			CurFlight.IDFlight = Base.IndexOf(CurFlight);
			Program.WriteColorLine(" < New flight has been successfuly created > \n", ConsoleColor.Green);
			return CurFlight;
		}
		private List<int> FindIndbyPonts(string origin, string destination)
		{
			List<int> Indices = new List<int>();

			foreach (Flight CurFlight in Base)
			{
				if ((CurFlight.Origin == origin && CurFlight.Destination == destination) ||
					(String.IsNullOrEmpty(origin) && CurFlight.Destination == destination) ||
					(String.IsNullOrEmpty(destination) && CurFlight.Origin == origin))
				{
					Indices.Add(CurFlight.IDFlight);
				}

			}
			return Indices;
		}
		private void DelFromBase(Flight CurFlight)
		{
			int ind = Base.IndexOf(CurFlight);
			Base.RemoveAt(ind);
			Base.RefreshInd();
		}
		private List<int> FindIndbyDate((DateTime earliestTime, DateTime latestTime) Time, FlightsTime mode)
		{
			List<int> Indices = new List<int>();

			foreach (Flight CurFlight in Base)
			{

				if (DateTime.Compare(CurFlight[mode], Time.earliestTime) >= 0  &&
					DateTime.Compare(CurFlight[mode], Time.latestTime) <= 0)
				{
					Indices.Add(CurFlight.IDFlight);
				}

			}
			return Indices;
		}

	}


}
