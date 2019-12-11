﻿using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace E_Booking
{
	class Serializer
	{

		private static BinaryFormatter Formatter = new BinaryFormatter();

		public static void Serialize<T>(T Object, string Path)
		{

			using (FileStream Stream = new FileStream(Path, FileMode.OpenOrCreate))
			{
				try
				{
					Formatter.Serialize(Stream, Object);
				}
				catch
				{
					Console.WriteLine($" < Serializable file {Path} is empty or defective > ");
				}
			}
		}

		public static T Deserialize<T>(string Path)
		{

			using (FileStream Stream = new FileStream(Path, FileMode.OpenOrCreate))
			{
				try
				{
					T NewObject = (T)Formatter.Deserialize(Stream);
					return NewObject;
				}
				catch
				{
					Console.WriteLine($" < Serializable file {Path} is empty or defective > ");
					return default(T);
				}
			}
		}
	}

}
