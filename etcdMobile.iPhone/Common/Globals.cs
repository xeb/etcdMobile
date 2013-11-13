using System;

namespace etcdMobile.iPhone.Common
{
	public static class Globals
	{
		public static ISqlCache SqlCache = new SqlCache();
		public static Preferences Preferences = new Preferences(SqlCache);
	}
}

