using NUnit.Framework;
using System.Linq;
using System;
using System.Globalization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

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
			Assert.IsTrue (keys.Any (k => k.KeyName == "github" && k.Dir));
			Assert.IsTrue (keys.Any (k => k.KeyName == "_etcd" && k.Dir));
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

			var newElement = new EtcdElement { Key = rand1, Value = rand2, Ttl = 30 };
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

			var newElement = new EtcdElement { Key = rand1, Value = rand2, Ttl = 30 };
			etcdClient.SaveKey (newElement);

			Console.WriteLine ("Saved Key");

			var keys = etcdClient.GetKeys ();

			Console.WriteLine ("Found Keys: {0}", keys.Count);

			var targetKey = keys.FirstOrDefault (k => k.KeyName == rand1);

			Assert.AreEqual (newElement.Value, targetKey.Value);

			etcdClient.DeleteKey (newElement);

			keys = etcdClient.GetKeys ();
			targetKey = keys.FirstOrDefault (k => k.KeyName == rand1);

			Assert.IsNull (targetKey);
		}

		[Test]
		public void DeserializeDateTime()
		{
			var key = new EtcdElement ();
			key.Expiration = "2013-10-20T12:51:25.86327532-07:00";

			var date = DateTime.ParseExact (key.Expiration, "yyyy-MM-dd", CultureInfo.InvariantCulture);
			Assert.AreEqual (date.Year, 2013);
		}
	}
}

