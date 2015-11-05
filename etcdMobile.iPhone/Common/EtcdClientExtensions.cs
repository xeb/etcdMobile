using System;
using etcetera;

namespace etcdMobile.iPhone
{
	public static class EtcdClientExtensions
	{
		public static bool IsValidEndpoint(this EtcdClient client, out Exception ex)
		{
			ex = null;
			try
			{
				client.Get("", recursive: true);
				return true;
			}
			catch(Exception e)
			{
				ex = e;
				return false;
			}
		}
	}
}

