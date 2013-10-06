using System;
using System.Collections.Generic;
using System.Linq;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using System.Data;
using System.IO;
using System.Threading.Tasks;
using Mono.Data.Sqlite;

namespace etcdMobile.iPhone.Common
{
	public class SqlCache
	{	
		private string _dbName = "etcd.d3";
		
		public List<Server> GetServers()
		{
			var projects = new List<Server>();
			var conn = GetConnection();
			
			using(var cmd = conn.CreateCommand())
			{
				conn.Open();
				cmd.CommandText = "SELECT * FROM Server";
				
				using (var reader = cmd.ExecuteReader ()) {
					// loop through each record and add the name to our collection
					while (reader.Read ()) { 
						var p = new Server();
						p.Id = Convert.ToInt32(reader["ID"]);
						p.Name = Convert.ToString(reader["Name"]);
						p.BaseUrl = Convert.ToString(reader["BaseUrl"]);
						projects.Add(p);
					}
				}
				conn.Close();
			}
			
			return projects;
		}
		
		public void SaveProjects(List<Server> servers)
		{
			var conn = GetConnection();
			conn.Open();
			
			foreach(var server in servers)
			{
				using(var cmd = conn.CreateCommand())
				{
					cmd.CommandText = @"INSERT OR IGNORE INTO Server (Id, Name, BaseUrl) 
										VALUES (@Id, @Name, @BaseUrl)
										;
										UPDATE Project SET Name = @Name, BaseUrl = @BaseUrl
										WHERE Id = @Id
										;
										";
					cmd.Parameters.AddWithValue("@Id", server.Id);
					cmd.Parameters.AddWithValue("@Name", server.Name);
					cmd.Parameters.AddWithValue("@BaseUrl", server.BaseUrl);
					cmd.ExecuteNonQuery();
				}
			}
			
			conn.Close();
		}
	    
	    public T GetSetting<T>(string name)
	    {
			var conn = GetConnection();
			conn.Open();
			
			T returnValue = default(T);
			using(var cmd = conn.CreateCommand())
			{
				cmd.CommandText = "SELECT value FROM Settings WHERE Name = @Name";
				cmd.Parameters.AddWithValue("@Name", name);
				var result = cmd.ExecuteScalar();
				
				if(result != null)
					returnValue = (T)Convert.ChangeType(result, typeof(T));
			}
			
			conn.Close();
			
			return returnValue;
	    }
	    
	    public void SaveSetting<T>(string name, T value)
	    {
	    	var conn = GetConnection();
	    	conn.Open();
	    	
	    	using(var cmd = conn.CreateCommand())
	    	{
				cmd.CommandText = "INSERT OR IGNORE INTO Settings (Name, Value) VALUES (@Name, @Value); UPDATE Settings SET Value = @Value WHERE Name = @Name;";
				cmd.Parameters.AddWithValue("@Name", name);
				cmd.Parameters.AddWithValue("@Value", value);
				cmd.ExecuteNonQuery();
	    	} 
	    	
	    	conn.Close();
	    }
	
		protected SqliteConnection GetConnection()
	    {
			bool isNew;
			var documents = Environment.GetFolderPath (Environment.SpecialFolder.Personal);
			
			string db = Path.Combine (documents, _dbName);
			isNew = !File.Exists (db);
			if (isNew) 
				SqliteConnection.CreateFile (db);
			
			var conn = new SqliteConnection("Data Source=" + db);
			
			if (isNew) 
				CreateDBSchema(conn);
			
	        return conn;
	    }
	    
		/// <summary>
		/// Creates a People table and inserts some data
		/// </summary>
		protected void CreateDBSchema(SqliteConnection connection)
		{
			// create a an array of commands
			var commands = new[]
			{
				"CREATE TABLE Server (ID INTEGER PRIMARY KEY, Name ntext, BaseUrl ntext)",
				"CREATE TABLE Settings (Name ntext PRIMARY KEY, Value ntext)",
			};
			
			// execute each command, using standard ADO.NET calls
			foreach (var cmd in commands) {
				using (var c = connection.CreateCommand()) {
					c.CommandText = cmd;
					c.CommandType = CommandType.Text;
					connection.Open ();
					c.ExecuteNonQuery ();
					connection.Close ();
				}
			}
			
		}
	}
}

