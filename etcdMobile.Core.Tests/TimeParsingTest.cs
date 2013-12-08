using NUnit.Framework;
using System.Linq;
using System;
using System.Globalization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;


namespace etcdMobile.Core.Tests
{
	[TestFixture]
	public class ExpirationFriendlyNameTest
	{
		[Test]
		public void InAFewSeconds()
		{
			var e = new EtcdElement ();
			e.Ttl = 9;
			Assert.AreEqual ("In a few seconds", e.ExpirationFriendly);
		}

		[Test]
		public void InLessThanAMinute()
		{
			for (int i = 10; i < 60; i++)
			{
				var e = new EtcdElement ();
				e.Ttl = i;
				Assert.AreEqual ("In less than a minute", e.ExpirationFriendly);
			}
		}

		[Test]
		public void InAFewMinutes()
		{
			var e = new EtcdElement ();
			e.Ttl = 2 * 60;
			Assert.AreEqual ("In a few minutes", e.ExpirationFriendly);
		}

		[Test]
		public void InNMinutes()
		{
			for(int i = 4; i < 57; i++)
			{
				var e = new EtcdElement ();
				e.Ttl = i * 60;
				Assert.AreEqual ("In "+i+" minutes", e.ExpirationFriendly);
			}
		}

		[Test]
		public void InAnHour()
		{
			for(int i = 57; i < 64; i++)
			{
				var e = new EtcdElement ();
				e.Ttl = i * 60;
				Assert.AreEqual ("In an hour", e.ExpirationFriendly);
			}
		}

		[Test]
		public void InAFewHours()
		{
			for(int i = 64; i < (3 * 60); i = i + 60)
			{
				var e = new EtcdElement ();
				e.Ttl = i * 60;
				Assert.AreEqual ("In a few hours", e.ExpirationFriendly);
			}
		}

		[Test]
		public void InNHours()
		{
			for(int i = 4; i < 23; i++)
			{
				var e = new EtcdElement ();
				e.Ttl = i * 60 * 60;
				Assert.AreEqual ("In "+i+" hours", e.ExpirationFriendly);
			}
		}

		[Test]
		public void InADay()
		{
			for(int i = 23; i < 25; i++)
			{
				var e = new EtcdElement ();
				e.Ttl = i * 60 * 60;
				Assert.AreEqual ("In a day", e.ExpirationFriendly);
			}
		}

		[Test]
		public void InAFewDays()
		{
			for(int i = 2; i < 4; i++)
			{
				var e = new EtcdElement ();
				e.Ttl = i * 60 * 60 * 24;
				Assert.AreEqual ("In a few days", e.ExpirationFriendly);
			}
		}

		[Test]
		public void InNDays() 
		{
			for (int i = 4; i < 7; i++)
			{
				var e = new EtcdElement ();
				e.Ttl = i * 60 * 60 * 24;
				Assert.AreEqual ("In " + i + " days", e.ExpirationFriendly);
			}
		}

		[Test]
		public void InAWeek() 
		{
			for (int i = 160; i < (160 + 8); i++)
			{
				var e = new EtcdElement ();
				e.Ttl = i * 60 * 60;
				Assert.AreEqual ("In a week", e.ExpirationFriendly);
			}
		}

		[Test]
		public void InNWeeks() 
		{
			for (int i = 1; i < 3; i++)
			{
				var e = new EtcdElement ();
				e.Ttl = i * 60 * 60 * 24 * 7;
				Console.WriteLine ("i={0},e.Ttl={1},Friendly={2}", i, e.Ttl, e.ExpirationFriendly);
				Assert.AreEqual ("In a few weeks", e.ExpirationFriendly);
			}
		}

		[Test]
		public void InAMonth() 
		{
			for (int i = 3; i < 5; i++)
			{
				var e = new EtcdElement ();
				e.Ttl = i * 60 * 60 * 24 * 7;
				Assert.AreEqual ("In a month", e.ExpirationFriendly);
			}
		}

		[Test]
		public void LessThanAYear() 
		{
			var e = new EtcdElement ();
			e.Ttl = 364 * 24 * 60 * 60;
			Assert.AreEqual ("Less than a year", e.ExpirationFriendly);
		}

		[Test]
		public void InNYears() 
		{
			var e = new EtcdElement ();
			e.Ttl = 9 * 365 * 24 * 60 * 60;
			Assert.AreEqual ("A long time from now", e.ExpirationFriendly);
		}

		[Test]
		public void SetTtlForSeconds()
		{
			var e = new EtcdElement ();
			e.Ttl = 60 * 60;
			Assert.AreEqual ("In an hour", e.ExpirationFriendly);
		}
	}
}

