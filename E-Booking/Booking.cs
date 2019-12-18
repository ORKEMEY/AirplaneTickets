using System;
using System.Collections.Generic;


namespace E_Booking
{
	[Serializable]
	class Booking
    {
		public int IDBooking;
		public int SeatNumber;
		public Flight Flight { get; set; }
		public Customer Customer { get; set; }
		public bool IsTicketBought;
		public Baggage Baggage;
		public int Price;

		public Booking()
		{
			IDBooking = SeatNumber= - 1;
			Flight = null;
			Customer = null;
			IsTicketBought = false;
			Price = 0;
			Baggage = new Baggage(false, false);
		}

		public Booking(Flight newFlight, Customer newCustomer)
		{
			IDBooking = -1;
			Flight = newFlight;
			Customer = newCustomer;
			IsTicketBought = false;
			Price = 150;
			Baggage = new Baggage(false, false);
		}

		public void Register()
		{
			char mode = ' ';
			Console.WriteLine(" < Registration of new booking > ");

			Console.WriteLine(" < Do you want to register a hand luggage \"Y\"/\"N\" > ");
			mode = Program.EnterMode();
			if (mode.Equals('Y') || mode.Equals('y'))
			{
				Baggage.HandLuggage = true;
				Price += 20;
			}
			
			Console.WriteLine(" < Do you want to register a suitcase \"Y\"/\"N\" > ");
			mode = Program.EnterMode();
			if (mode.Equals('Y') || mode.Equals('y'))
			{
				Baggage.Suitcase = true;
				Price += 30;
			}

			Program.WriteColorLine(" < Price of ticket: " + Price + "$ > ", ConsoleColor.Green);

			Console.WriteLine(" < Do you want to buy now a ticket \"Y\"/\"N\" > ");
			mode = Program.EnterMode();
			if (mode.Equals('Y') || mode.Equals('y'))
			{
				IsTicketBought = true;
			}


		}

		public static void GetHeadOfTable()
		{
			Console.Write(" ID \t| Number of seat \t| Departure time \t|");
			Console.WriteLine(" Arrival time \t\t| Origin \t| Destination \t| Is ticket bought \t| Price \t| hand luggage \t| suitcase");
			Console.WriteLine("__________________________________________________________________________________________" +
				"_____________________________________________________________________________________");
		}

		public void GetInfo()
		{
			Console.Write($" {IDBooking} \t| {SeatNumber + 1} \t\t\t| {Flight.Departure.ToShortDateString()} {Flight.Departure.ToShortTimeString()} \t|");
			Console.Write($" {Flight.Arrival.ToShortDateString()} {Flight.Arrival.ToShortTimeString()} \t| {Flight.Origin} \t\t| {Flight.Destination} \t|");
			Console.WriteLine($" {IsTicketBought}	\t\t| {Price} \t\t| {Baggage.HandLuggage} \t\t| {Baggage.Suitcase} ");
			Console.WriteLine("__________________________________________________________________________________________" +
				"_____________________________________________________________________________________");
		}

		public static void BuyTicket(Booking CurBooking)
		{
			if (CurBooking != null)
			{
				if (CurBooking.IsTicketBought == false)
				{
					CurBooking.IsTicketBought = true;
					Program.WriteColorLine(" < Ticket was successfully bought > ", ConsoleColor.Green);
				}
				else
				{
					Program.WriteColorLine(" < Ticket is already bought > ", ConsoleColor.Magenta);
				}
			}
			else
			{
				Program.WriteColorLine(" < No current booking specified > ", ConsoleColor.Magenta);

			}
		}


	}

}
