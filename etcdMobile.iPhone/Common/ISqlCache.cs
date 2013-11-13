using System;
using etcdMobile.iPhone.Common;
using System.Collections.Generic;

namespace etcdMobile.iPhone
{
	public interface ISqlCache
	{
		List<Server> GetServers ();
		
		void SaveServer (Server server);
		
		void SaveServers (IEnumerable<Server> servers);
		
		void DeleteServer (Server server);

		T GetSetting<T> (string name);
		
		void SaveSetting<T> (string name, T value);

	}
}

