using System;
using System.Collections.Generic;

namespace E_Booking
{
    class CustomersBase : Serializer
    {
		public List<Customer> Base = null;

		private static CustomersBase Source;
		public static CustomersBase GetSource()
		{
			if (Source == null)
				Source = new CustomersBase();
			return Source;
		}

		private CustomersBase()
		{
			Base = new List<Customer>(1);
		}

		public Customer RegistrateCustomer()
		{
			Customer CurCust = new Customer(); 
			CurCust.Registration();
			Base.Add(CurCust);
			CurCust.IDCustomer = Base.IndexOf(CurCust);
			return CurCust;

		}

		public void DelFromBase(Customer CurCust)
		{

			int ind = Base.IndexOf(CurCust);
			Base.RemoveAt(ind);
			Base.RefreshInd();
		}

		public int FindIdByLogin(string CurrentLogin)
		{

			for (int count = 0; count < Base.Count; count++)
			{

				if (Base[count].IDCustomer != count) Base.RefreshInd();

				if (Base[count].Login == CurrentLogin)
				{
					return count;
				}

			}
			return -1;
		}

	}
}
