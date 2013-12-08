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
		public int? Ttl {get;set;}

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

		public void SetTtl(string ttlValue)
		{
			if (!string.IsNullOrWhiteSpace (ttlValue))
			{
				int ttl;
				if (int.TryParse (ttlValue, out ttl))
					SetTtl (ttl);
			}
		}

		public void SetTtl(int ttl)
		{
			Ttl = ttl;
			_expirationDate = DateTime.Now.AddSeconds (ttl);
		}

		public string ExpirationFriendly
		{
			get
			{
				if (Ttl.HasValue == false)
					return string.Empty;

				if (Ttl.Value < 10)
					return "In a few seconds";

				if (Ttl.Value < 60)
					return "In less than a minute";

				if (Ttl.Value < 60 * 3)
					return "In a few minutes";

				if (Ttl.Value < 60 * 57)
					return "In " + (Ttl.Value / (60)) + " minutes";

				if (Ttl.Value < 60 * 64)
					return "In an hour";

				if (Ttl.Value < 60 * 60 * 3)
					return "In a few hours";

				if (Ttl.Value < 60 * 60 * 23)
					return "In " + (Ttl.Value / (60 * 60)) + " hours";

				if (Ttl.Value < 60 * 60 * 25)
					return "In a day";

				if (Ttl.Value < 60 * 60 * 24 * 4)
					return "In a few days";

				if (Ttl.Value < 60 * 60 * 160)
					return "In " + (Ttl.Value / (60*60*24)) + " days";

				if (Ttl.Value < 60 * 60 * 24 * 7)
					return "In a week";

				if (Ttl.Value < 60 * 60 * 24 * 7 * 3)
					return "In a few weeks";

				if (Ttl.Value < 60 * 60 * 24 * 7 * 5)
					return "In a month";

				if (Ttl.Value < 365 * 24 * 60 * 60)
					return "Less than a year";

				return "A long time from now";
			}
		}
	}
}

