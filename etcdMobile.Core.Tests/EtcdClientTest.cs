using NUnit.Framework;
using System.Linq;
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
			Assert.IsTrue (keys.Any (k => k.KeyName == "github" && k.Dir));
		}

		[Test]
		public void CanSaveKey()
		{
			var baseUrl = "http://127.0.0.1:4001";
			var etcdClient = new EtcdClient(baseUrl);

			TimeSpan t = DateTime.UtcNow - new DateTime(1970, 1, 1);
			int secondsSinceEpoch = (int)t.TotalSeconds;

			var rand1 = (secondsSinceEpoch * 2 / 3.25).ToString();
			var rand2 = (secondsSinceEpoch * 4 / 8.345).ToString();

			Console.WriteLine ("Key is " + rand1);

			var newElement = new EtcdElement { Key = rand1, Value = rand2, Ttl = 10 };
			etcdClient.SaveKey (newElement);

			var keys = etcdClient.GetKeys ();
			var targetKey = keys.FirstOrDefault (k => k.KeyName == rand1);

			Assert.AreEqual (newElement.Value, targetKey.Value);
		}

		[Test]
		public void CanDeleteKey()
		{
			var baseUrl = "http://127.0.0.1:4001";
			var etcdClient = new EtcdClient(baseUrl);

			TimeSpan t = DateTime.UtcNow - new DateTime(1970, 1, 1);
			int secondsSinceEpoch = (int)t.TotalSeconds;

			var rand1 = (secondsSinceEpoch * 2 / 3.25).ToString();
			var rand2 = (secondsSinceEpoch * 4 / 8.345).ToString();

			Console.WriteLine ("Key is " + rand1);

			var newElement = new EtcdElement { Key = rand1, Value = rand2, Ttl = 10 };
			etcdClient.SaveKey (newElement);

			var keys = etcdClient.GetKeys ();
			var targetKey = keys.FirstOrDefault (k => k.KeyName == rand1);

			Assert.AreEqual (newElement.Value, targetKey.Value);

			etcdClient.DeleteKey (newElement);

			keys = etcdClient.GetKeys ();
			targetKey = keys.FirstOrDefault (k => k.KeyName == rand1);

			Assert.IsNull (targetKey);
		}
	}
}

