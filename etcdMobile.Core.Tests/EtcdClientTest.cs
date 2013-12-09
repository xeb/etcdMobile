using NUnit.Framework;
using System.Linq;
using System;
using System.Diagnostics;
using System.Globalization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace etcdMobile.Core.Tests
{
	[TestFixture]
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
		public void CanFindKey()
		{
			var baseUrl = "http://127.0.0.1:4001";
			var etcdClient = new EtcdClient(baseUrl);

			TimeSpan t = DateTime.UtcNow - new DateTime(1970, 1, 1);
			int secondsSinceEpoch = (int)t.TotalSeconds;

			var rand1 = (secondsSinceEpoch * -2).ToString();
			var rand2 = (secondsSinceEpoch * -4).ToString();

			Console.WriteLine ("Key is " + rand1);

			var newElement = new EtcdElement { Key = "/some/deep/nested/path/to/find/a/key/" + rand1, Value = rand2, Ttl = 30 };
			etcdClient.SaveKey (newElement);

			var sw = Stopwatch.StartNew ();
			var keys = etcdClient.FindKeys (rand1).ToList();
			sw.Stop ();

			Assert.AreEqual (1, keys.Count);
			Assert.AreEqual (rand1, keys [0].KeyName);
			Assert.AreEqual (rand2, keys [0].Value);
			Console.WriteLine ("Find took {0}ms", sw.ElapsedMilliseconds);
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

		// SWITCH THESE TESTS

		[Test]
		public void DeserializeDateTime()
		{
			var key = new EtcdElement ();
			key.Expiration = "2013-11-11T22:17:35.256488204-08:00";

			Assert.IsTrue (key.ExpirationDate.HasValue);

			var date = key.ExpirationDate.Value;
			Assert.AreEqual (date.Year, 2013);
			Assert.AreEqual (date.Month, 11);
			Assert.AreEqual (date.Day, 11);
			Assert.AreEqual (date.Hour, 22);
			Assert.AreEqual (date.Minute, 17);
			Assert.AreEqual (date.Second, 35);
			Assert.AreEqual (date.ToUniversalTime ().Hour, 6);
		}

		[Test]
		public void DeserializeDateTime2()
		{
			var key = new EtcdElement ();
			key.Expiration = "2013-12-07T19:22:01.99251035-05:00";

			Assert.IsTrue (key.ExpirationDate.HasValue);

			var date = key.ExpirationDate.Value;
			Assert.AreEqual (date.Year, 2013);
			Assert.AreEqual (date.Month, 12);
			Assert.AreEqual (date.Day, 7);
			Assert.AreEqual (date.Hour, 16);
			Assert.AreEqual (date.Minute, 22);
			Assert.AreEqual (date.Second, 01);
			Assert.AreEqual (date.ToUniversalTime ().Hour, 0);
		}
	}
}

