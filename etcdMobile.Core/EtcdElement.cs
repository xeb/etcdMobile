using System;

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
		public DateTime Expiration {get;set;}
		public int Ttl {get;set;}
	}
}

