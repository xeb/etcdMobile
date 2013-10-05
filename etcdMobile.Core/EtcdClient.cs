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
		public EtcdClient (string baseUrl)
		{
			if(!string.IsNullOrWhiteSpace(baseUrl) && !baseUrl.EndsWith("/"))
			{
				baseUrl += "/";
			}
			
			_baseUrl = baseUrl;
		}
		
		public List<EtcdElement> GetKeys(string parent = "")
		{
			var url = _baseUrl + "v1/keys/" + parent;
			var result = HttpGet(url);
			return JsonConvert.DeserializeObject<List<EtcdElement>>(result);
		}
		
		public List<EtcdElement> GetChildKeys(EtcdElement ele)
		{
			if(ele == null) throw new ArgumentNullException("ele is NULL");
			if(ele.Dir == false) throw new ArgumentException("ele is not a Directory");
			
			return GetKeys(ele.Key);
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

