using System;
using System.Collections.Generic;

namespace E_Booking
{
    class Customer : Account
    {

		public int IDCustomer;
		private string _Name;
		private string _Surname;
		private int _Age;
		public string Login;

		public string Name
		{
			get
			{
				return _Name;
			}
			set
			{
				if(char.IsLower(value[0]))
				{
					Program.WriteColorLine(" < Name cann't begin with lower case > ", ConsoleColor.Red);
					_Name = null;
				} else if (String.IsNullOrEmpty(value))
				{
					Program.WriteColorLine(" < Name cann't be empty > ", ConsoleColor.Red);
					_Name = null;
				}
				else
				{
					_Name = value;
				}
				
			}
		}
		public string Surname
		{
			get
			{
				return _Surname;
			}
			set
			{
				if (char.IsLower(value[0]))
				{
					Program.WriteColorLine(" < Surame cann't begin with lower case > ", ConsoleColor.Red);
					_Surname = null;
				}
				else if (String.IsNullOrEmpty(value))
				{
					Program.WriteColorLine(" < Surname cann't be empty > ", ConsoleColor.Red);
					_Surname = null;
				}
				else
				{
					_Surname = value;
				}

			}
		}
		public int Age
		{
			get
			{
				return _Age;
			}
			set
			{

				if(value <= 0)
				{
					Program.WriteColorLine(" < Age cnann't be equal null or be less than null > ", ConsoleColor.Red);
					_Age = -1;
				}else if(value > 125)
				{
					Program.WriteColorLine(" < Age is too big > ", ConsoleColor.Red);
					_Age = -1;
				}
				else
				{
					_Age = value;
				}
			}
		}
		public FlightsBase BookedFlights;



		public Customer()
		{
			_Name = _Surname = Login = null;
			_Age = IDCustomer = -1;
			BookedFlights = new FlightsBase();
		}

		public void Registration()
		{
			Console.WriteLine(" < Registration of new customer > ");

			do
			{
				Console.Write(" < Enter your name > \n>");
				Name = Console.ReadLine();

			} while (_Name == null);

			do
			{
				Console.Write(" < Enter your surame > \n>");
				Surname = Console.ReadLine();
			} while (_Surname == null);
			
			do
			{
				Console.Write(" < Enter your age > \n>");

				try
				{
					Age = Convert.ToInt32(Console.ReadLine());
				}
				catch (FormatException)
				{
					Program.WriteColorLine(" < Wrong format, age is a number > ", ConsoleColor.Red);
				}

			} while (Age == -1);

			Console.Write(" < Enter your login > \n>");
			Login = Console.ReadLine();

			SetPassword();
			
		}

		public static void GetHeadOfTable()
		{
			Console.WriteLine(" ID \t| Age \t| Login \t| Name \t\t| Surname");
			Console.WriteLine("____________________________________________" +
				"_____________________________________");

		}

		public void GetInfo()
		{
			Console.WriteLine($" {IDCustomer} \t| {Age} \t| {Login} \t\t| {Name} \t| {Surname} ");
			Console.WriteLine("____________________________________________" +
				"_____________________________________");

		}

	}
}
