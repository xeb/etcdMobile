using System;
using System.Collections.Generic;

namespace etcdMobile.iPhone
{
	public static class BoolValueParser
	{
		public static readonly List<string> OnValues = new List<string> { "ON", "YES", "TRUE", "1" };
		public static readonly List<string> OffValues = new List<string> { "OFF", "NO", "FALSE", "0" };
	}
}

