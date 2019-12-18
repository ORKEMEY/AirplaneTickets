using System;
using System.Collections.Generic;

namespace E_Booking
{
	[Serializable]
    class CustomersBase : Serializer
    {
		public List<Customer> Base = null;

		private static CustomersBase Source = Deserialize<CustomersBase>("CustomersBase.dat");
		public static CustomersBase GetSource()
		{
			if (Source == null)
				Source = new CustomersBase();
			return Source;
		}
		private CustomersBase()
		{
			if(Source == default(CustomersBase)) Base = new List<Customer>();
		}


		public static void OutputCurrentCustomer(Customer CurCust)
		{
			if (CurCust != null)
			{
				Flight.GetHeadOfTable();
				CurCust.GetInfo();
				Console.WriteLine();
			}
			else Program.WriteColorLine(" < No current account specified > ", ConsoleColor.Magenta);
		}
		public void DelCurrentCustomer(Customer CurCust)
		{
			if (CurCust != null)
			{
				Console.WriteLine(" < Do really want to delete curent account \"Y\"/\"N\" > ");
				char delete = Program.EnterMode();

				if (delete.Equals('Y') || delete.Equals('y'))
				{
					DelFromBase(CurCust);
					Program.WriteColorLine(" < Account was successfully deleted > \n", ConsoleColor.Green);
				}
			}
			else Program.WriteColorLine(" < No current account specified > ", ConsoleColor.Magenta);
		}
		public void OutputCustomers()
		{
			if (Base.Count == 0)
			{
				Program.WriteColorLine(" < There's no customers in base > ", ConsoleColor.Magenta);
				return;
			}

			Console.WriteLine(Base.Count == 1 ? " < Customer > " : " < Customers > ");
			Customer.GetHeadOfTable();

			foreach (Customer CurCust in Base) CurCust.GetInfo();
			Console.WriteLine();
		}

		public Customer FindByLogin()
		{
			Customer CurCust = FindIdByLogin(Customer.EnterLogin());
			if (CurCust == null) Program.WriteColorLine(" < No account with such login was found > \n", ConsoleColor.Magenta);
			else Program.WriteColorLine(" < Account was successfully found > \n", ConsoleColor.Green);

			return CurCust;
		}


		private void DelFromBase(Customer CurCust)
		{

			int ind = Base.IndexOf(CurCust);
			Base.RemoveAt(ind);
			Base.RefreshInd();
		}
		public bool IsLoginFree(string CurLogin)
		{
			if (FindIdByLogin(CurLogin) == null) return true;
			return false;
		}
		private Customer FindIdByLogin(string EnteredLogin)
		{

			for (int count = 0; count < Base.Count; count++)
			{

				if (Base[count].IDCustomer != count) Base.RefreshInd();

				if (Base[count].Login == EnteredLogin)
				{
					return Base[count];
				}

			}
			return null;
		}
		public Customer RegistrateCustomer()
		{
			CheckLogin checkLogin = IsLoginFree;

			Customer CurCust = new Customer();
			CurCust.Registration(checkLogin);
			Base.Add(CurCust);
			CurCust.IDCustomer = Base.IndexOf(CurCust);
			return CurCust;

		}

	}

}
