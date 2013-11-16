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

				if(string.IsNullOrWhiteSpace(Expiration) || Expiration.Length != 35)
					return null;

				var exp = Expiration.Substring (0, 26) + Expiration.Substring(Expiration.Length - 6, 6);
				_expirationDate = DateTime.ParseExact (exp, "yyyy-MM-ddTHH:mm:ss.ffffffzzz", CultureInfo.InvariantCulture);
				return _expirationDate;
			}
		}
	}
}

