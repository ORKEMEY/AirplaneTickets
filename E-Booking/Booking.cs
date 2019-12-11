using System;
using System.Collections.Generic;


namespace E_Booking
{
    class Booking
    {
		public int IDBooking;
		public Flight Flight { get; set; }
		public Customer Customer { get; set; }
		public bool IsTicketBought;

		public Booking()
		{
			IDBooking = -1;
			Flight = null;
			Customer = null;
			IsTicketBought = false;
		}

		public Booking(Flight newFlight, Customer newCustomer)
		{
			IDBooking = -1;
			Flight = newFlight;
			Customer = newCustomer;
		}

	}

}
