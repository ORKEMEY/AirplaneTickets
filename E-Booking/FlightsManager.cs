using System;
using System.Collections.Generic;


namespace E_Booking
{
	class FlightsManager : Account
	{
		public CustomersBase CustomersBase { get; set; }
		public FlightsBase FlightsBase { get; set; }

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
			CustomersBase = CustomersBase.GetSource();

			FlightsBase = Deserialize<FlightsBase>("FlightsBase.dat");
			if(FlightsBase == default(FlightsBase)) FlightsBase = new FlightsBase();
		}

		public static void OutputManagersMenu()
		{
			Console.WriteLine(" < Menu > ");

			Console.Write(" < Enter \""); Program.WriteColor("o", ConsoleColor.DarkCyan); Console.WriteLine("\" to output menu > ");
			Console.Write(" < Enter \""); Program.WriteColor("r", ConsoleColor.DarkCyan); Console.WriteLine("\" to register new flight > ");
			Console.Write(" < Enter \""); Program.WriteColor("a", ConsoleColor.DarkCyan); Console.WriteLine("\" to output all flights > ");
			Console.Write(" < Enter \""); Program.WriteColor("i", ConsoleColor.DarkCyan); Console.WriteLine("\" to output current flights > ");
			Console.Write(" < Enter \""); Program.WriteColor("f", ConsoleColor.DarkCyan); Console.WriteLine("\" to find flight > ");
			Console.Write(" < Enter \""); Program.WriteColor("q", ConsoleColor.DarkCyan); Console.WriteLine("\" to quit > ");
			Console.WriteLine();
		}

		public void Menu()
		{
			//Customer CurCust = null;
			Flight CurFlight = null;
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

					case 'a':
					case 'A':
							FlightsBase.OutputFlights();
						break;

					case 'i':
					case 'I':
						if (CurFlight != null)
						{
							Flight.GetHeadOfTable();
							CurFlight.GetInfo();
						}
						else Program.WriteColorLine(" < No current flight specified > ", ConsoleColor.Magenta);
						break;

					case 'r':
					case 'R':
						CurFlight = FlightsBase.RegistrateFlight();
						break;

					case 'f':
					case 'F':

						int Ind = FlightMenu();
						if (Ind != -1)
						{
							CurFlight = FlightsBase.Base[Ind];
						}
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

		public static void OutputFlightMenu()
		{
			Console.WriteLine(" < Menu > ");
			Console.Write(" < Enter \""); Program.WriteColor("q", ConsoleColor.DarkCyan); Console.WriteLine("\" to quit > ");
			Console.Write(" < Enter \""); Program.WriteColor("p", ConsoleColor.DarkCyan); Console.WriteLine("\" to find flight by points > ");
			Console.WriteLine();
		}

		public int FlightMenu()
		{
			int CurFlightInd = -1;

			Console.Clear();
			OutputFlightMenu();

			switch (Program.EnterMode())
			{

				case 'p':
				case 'P':

					CurFlightInd = FlightsBase.FindByPoints();
					break;

				case 'q':
				case 'Q':
					
					break;

				default:
					Program.WriteColorLine(" < Wrong mode > ", ConsoleColor.Red);	
					break;
			}

			Console.Write(" < Enter any key > \n>");
			Console.ReadKey();
			Console.Clear();
			return CurFlightInd;
		}



	}
}
