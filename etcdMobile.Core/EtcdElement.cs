using System;
using System.Linq;
using System.Globalization;

namespace etcdMobile.Core
{
	public class EtcdElement
	{
		public string Key {get;set;}
		public string KeyName 
		{
			get 
			{ 
				if(Key == null) return null;
				var lastIndex = Key.LastIndexOf("/");
				if(lastIndex == -1) return Key;
				if(lastIndex + 1 > Key.Length - 1) return Key;
				return Key.Substring(lastIndex + 1);
			}
		} 
		public string Action {get;set;}
		public string Value {get;set;}	
		public int Index {get;set;}
		public bool Dir {get;set;}
		public string Expiration {get;set;}
		public ulong? Ttl {get;set;}

		private DateTime? _expirationDate;
		public DateTime? ExpirationDate
		{
			get
			{
				if (_expirationDate != null)
					return _expirationDate;

				if(string.IsNullOrWhiteSpace(Expiration) || new[] { 34, 35 }.All(i => Expiration.Length != i) )
					return null;

				var exp = Expiration.Substring (0, 26) + Expiration.Substring(Expiration.Length - 6, 6);
				_expirationDate = DateTime.ParseExact (exp, "yyyy-MM-ddTHH:mm:ss.ffffffzzz", CultureInfo.InvariantCulture);
				return _expirationDate;
			}
		}

		public static string GetFriendlyTtl(ulong? ttl)
		{
			if (ttl.HasValue == false)
				return string.Empty;

			if (ttl.Value < 10)
				return "In a few seconds";

			if (ttl.Value < 60)
				return "In less than a minute";

			if (ttl.Value < 60 * 3)
				return "In a few minutes";

			if (ttl.Value < 60 * 57)
				return "In " + (ttl.Value / (60)) + " minutes";

			if (ttl.Value < 60 * 64)
				return "In an hour";

			if (ttl.Value < 60 * 60 * 3)
				return "In a few hours";

			if (ttl.Value < 60 * 60 * 23)
				return "In " + (ttl.Value / (60 * 60)) + " hours";

			if (ttl.Value < 60 * 60 * 25)
				return "In a day";

			if (ttl.Value < 60 * 60 * 24 * 4)
				return "In a few days";

			if (ttl.Value < 60 * 60 * 160)
				return "In " + (ttl.Value / (60*60*24)) + " days";

			if (ttl.Value < 60 * 60 * 24 * 7)
				return "In a week";

			if (ttl.Value < 60 * 60 * 24 * 7 * 3)
				return "In a few weeks";

			if (ttl.Value < 60 * 60 * 24 * 7 * 5)
				return "In a month";

			if (ttl.Value < 365 * 24 * 60 * 60)
				return "Less than a year";

			return "A long time from now";
		}

		public string ExpirationFriendly
		{
			get
			{
				return GetFriendlyTtl (Ttl);
			}
		}
	}
}

