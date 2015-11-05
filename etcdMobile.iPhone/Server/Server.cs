using System;
using etcetera;

namespace etcdMobile.iPhone.Common
{
	public class Server
	{

		public int Id {get;set;}
		public string Name {get;set;}
		public string BaseUrl {get;set;}

		private EtcdClient _client;
		public EtcdClient Client 
		{
			get 
			{
				if (_client == null && !string.IsNullOrWhiteSpace(BaseUrl))
				{
					_client = new EtcdClient (new Uri(BaseUrl));
				}

				return _client;
			}
		}
	}
}

