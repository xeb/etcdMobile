using System;
using System.Collections.Generic;
using System.Collections;
using System.Net;
using System.Linq;
using System.IO;
using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace etcdMobile.Core
{
	public class EtcdClient
	{
		private string _baseUrl;
		private const string _apiRoot = "v1/keys";
		public EtcdClient (string baseUrl)
		{
			_baseUrl = ModifyBaseUrl(baseUrl);
		}
		
		private static string ModifyBaseUrl(string baseUrl)
		{
			if(!string.IsNullOrWhiteSpace(baseUrl) && !baseUrl.EndsWith("/"))
			{
				baseUrl += "/";
			}
			
			return baseUrl;
		}
		
		public bool IsValidEndpoint()
		{
			try
			{
				GetKeys();
				return true;
			}
			catch
			{
				return false;
			}
		}
		
		public List<EtcdElement> GetKeys(string parent = "")
		{
			var url = _baseUrl + _apiRoot + parent;
			var result = HttpGet(url);
			return JsonConvert.DeserializeObject<List<EtcdElement>>(result);
		}
		
		public List<EtcdElement> GetChildKeys(EtcdElement ele)
		{
			if(ele == null) throw new ArgumentNullException("ele is NULL");
			if(ele.Dir == false) throw new ArgumentException("ele is not a Directory");
			
			return GetKeys(ele.Key);
		}

		public void SaveKey(EtcdElement key)
		{
			var formValues = new Dictionary<string,string> ();
			if (key.Ttl.HasValue)
			{
				formValues.Add ("ttl", key.Ttl.Value.ToString());
			}

			formValues.Add ("value", key.Value);

			var url = _baseUrl + _apiRoot + key.Key;
			HttpPostFormValues (url, formValues);
		}

		public void DeleteKey(EtcdElement key)
		{
			var url = _baseUrl + _apiRoot + key.Key;
			var request = HttpWebRequest.Create(url);
			request.Method = "DELETE";
			ReadResponse(request);
		}

		private string HttpGet(string url)
		{
			var request = HttpWebRequest.Create(url);
			request.Method = "GET";

			return ReadResponse(request);
		}
		
		private string HttpPost(string url, string postBody, string contentType)
		{
			var request = HttpWebRequest.Create(url);
			request.Method = "POST";
			request.ContentType = contentType;
			using(var requestStream = request.EndGetRequestStream(request.BeginGetRequestStream(null, null)))
			using(var streamWriter = new StreamWriter(requestStream))
			{
				streamWriter.Write(postBody);	
			}

			return ReadResponse(request);
		}
		
		private string HttpPostFormValues(string url, Dictionary<string,string> formValues, string contentType = "application/x-www-form-urlencoded")
		{
			var postBody = string.Join("&", formValues.Select(kv => string.Format("{0}={1}", kv.Key, kv.Value))); // TODO: encoding!
			
			return HttpPost(url, postBody, contentType);
		}
		 
		
		private string ReadResponse(WebRequest request)
		{
			using (HttpWebResponse response = request.EndGetResponse(request.BeginGetResponse(null,null)) as HttpWebResponse)
			{
				if (response.StatusCode != HttpStatusCode.OK)
				{
					return string.Empty;
				}
				using (StreamReader reader = new StreamReader(response.GetResponseStream()))
				{
					var content = reader.ReadToEnd();
					if(string.IsNullOrWhiteSpace(content)) 
					{
						return string.Empty;
					}
					else 
					{
						return content;
					}
				}
			}
		}
	}
}

