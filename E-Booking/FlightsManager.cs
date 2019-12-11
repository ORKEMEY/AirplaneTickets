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
			FlightsBase = new FlightsBase();
		}


	}
}
