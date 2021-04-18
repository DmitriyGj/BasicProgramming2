	using NUnit.Framework.Constraints;
	using System;
	using System.Management.Instrumentation;
	using System.Net.Mime;
	using System.Reflection;
	using System.Security.Policy;
	using System.Text;

	namespace hashes
	{
		public class GhostsTask :
			IFactory<Document>, IFactory<Vector>, IFactory<Segment>, IFactory<Cat>, IFactory<Robot>,
			IMagic
		{
			private static byte[] contentForDoc = {1,1,1,1 };
			private static double batteryCapacity = 89.0;

			Vector vector = new Vector(1, 1);
			Cat cat = new Cat("Musya", "British", new DateTime(2005, 06, 12));
			Segment segment = new Segment(new Vector(0,0),new Vector(1,1));
			Document document = new Document("qga",Encoding.Unicode, contentForDoc);
			Robot robot = new Robot("srsX");
			public void DoMagic()
			{
				contentForDoc[0] += 15;
				segment = new Segment(segment.Start.Add(vector), segment.End.Add(vector));
				vector.Add(new Vector(2, 2));
				cat.Rename("Bulba");
				Robot.BatteryCapacity++;
			}

			Vector IFactory<Vector>.Create() => vector;

			Segment IFactory<Segment>.Create() => segment;

			Document IFactory<Document>.Create() => document;

			Cat IFactory<Cat>.Create() => cat;

			Robot IFactory<Robot>.Create() => robot;
		}
	}