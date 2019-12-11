using System;
using System.Collections.Generic;


namespace E_Booking
{
    class FlightsBase
    {

		public List<Flight> Base = null;

		public FlightsBase()
		{
			Base = new List<Flight>();
		}

		public void AddToBase(Flight CurFlight)
		{
			CurFlight.Registration();
			Base.Add(CurFlight);
			CurFlight.IDFlight = Base.IndexOf(CurFlight);
		}

		public void DelFromBase(Flight CurFlight)
		{

			int ind = Base.IndexOf(CurFlight);
			Base.RemoveAt(ind);
			Base.RefreshInd();
		}

	}
}
