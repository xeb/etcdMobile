using NUnit.Framework;
using System;

namespace etcdMobile.Core.Tests
{
	[TestFixture()]
	public class EtcdClientTests
	{
		[Test()]
		public void CanGetRootKeys ()
		{
			var baseUrl = "http://127.0.0.1:4001";
			var etcdClient = new EtcdClient(baseUrl);
			var keys = etcdClient.GetKeys();
			
			Assert.IsNotNull(keys);
			Assert.AreEqual("_etcd", keys[0].KeyName);
			Assert.AreEqual("project.1.mark.settings", keys[1].KeyName);
			Assert.AreEqual("Test", keys[1].Value);
			Assert.IsTrue(keys[2].Dir);
		}
	}
}

