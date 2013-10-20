using System;
using etcdMobile.Core;

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
					_client = new EtcdClient (BaseUrl);
				}

				return _client;
			}
		}
	}
}

