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

		public Flight RegistrateFlight()
		{
			Flight CurFlight = new Flight();
			CurFlight.Registration();
			Base.Add(CurFlight);
			CurFlight.IDFlight = Base.IndexOf(CurFlight);
			return CurFlight;
		}

		public void DelFromBase(Flight CurFlight)
		{
			int ind = Base.IndexOf(CurFlight);
			Base.RemoveAt(ind);
			Base.RefreshInd();
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

		}


		public static int ChooseItem(List<int> Indices)
		{
			int Ind = -1;
			Console.Write(" < Enter ID(number) of flight > \n>");

			try
			{
				Ind = Convert.ToInt32(Console.ReadLine());
			}
			catch (ArgumentNullException)
			{
				Program.WriteColorLine(" < You need to enter ID > ", ConsoleColor.Red);
			}
			catch (FormatException)
			{
				Program.WriteColorLine(" < Wrong format of ID > ", ConsoleColor.Red);
			}

			if (!Indices.Contains(Ind))
			{
				Program.WriteColorLine(" < There is no flight with such ID > ", ConsoleColor.Magenta);
				return -1;
			}

			return Ind;
		}

		public int FindByPoints()
		{
			string origin = null, destination = null;
			List<int> Indices = new List<int>();
			int Ind = -1;

			Console.WriteLine(" < Flight search > ");

			Console.Write(" < Enter origin of flight, or emty line to skip this filter > \n>");

			origin = Console.ReadLine();

			if (String.IsNullOrEmpty(origin)) Console.Write(" < Enter destination of flight > \n>");
			else Console.Write(" < Enter destination of flight, or emty line to skip this filter > \n>");

			destination = Console.ReadLine();

			Indices = FindIndbyPonts(origin, destination);

			if(Indices.Count == 0)
			{
				Console.WriteLine(" < No flights were found > ");
			}
			else
			{
				OutputFlights(Indices);
				Ind = ChooseItem(Indices);
			}

			return Ind;
		}

		private List<int> FindIndbyPonts(string origin, string destination)
		{
			List<int> Indices = new List<int>();

			foreach (Flight CurFlight in Base)
			{
				if((CurFlight.Origin == origin && CurFlight.Destination == destination) ||
					(String.IsNullOrEmpty(origin) && CurFlight.Destination == destination) ||
					(String.IsNullOrEmpty(destination) && CurFlight.Origin == origin))
				{
					Indices.Add(CurFlight.IDFlight);
				}

			}
			return Indices;
		}

	}
}
