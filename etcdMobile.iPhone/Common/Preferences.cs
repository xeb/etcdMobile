using System;

namespace etcdMobile.iPhone.Common
{
	public class Preferences
	{
		private ISqlCache _sqlCache;
		public Preferences (ISqlCache sqlCache)
		{
			_sqlCache = sqlCache;

			// If first run, save our preferences
			if (_sqlCache.GetSetting<bool> ("Preferences.Set") == false)
			{
				SetToDefault ();
			}
			else
			{
				PopulateFromCache ();
			}
		}

		public bool ReadOnly {get;set;}
		public bool RefreshOnSave {get;set;}
		public bool UseSmartValues{get;set;}
		public bool HideEtcdDir {get;set;}

		public void SetToDefault()
		{
			ReadOnly = true;
			RefreshOnSave = false;
			UseSmartValues = false;
			HideEtcdDir = true;

			SaveToCache ();
		}

		public void PopulateFromCache()
		{
			ReadOnly = _sqlCache.GetSetting<bool> ("Preferences.ReadOnly");
			RefreshOnSave = _sqlCache.GetSetting<bool> ("Preferences.RefreshOnSave");
			UseSmartValues = _sqlCache.GetSetting<bool> ("Preferences.UseSmartValues");
			HideEtcdDir = _sqlCache.GetSetting<bool> ("Preferences.HideEtcdDir");
		}

		public void SaveToCache()
		{
			_sqlCache.SaveSetting ("Preferences.ReadOnly", ReadOnly);
			_sqlCache.SaveSetting ("Preferences.RefreshOnSave", RefreshOnSave);
			_sqlCache.SaveSetting ("Preferences.UseSmartValues", UseSmartValues);
			_sqlCache.SaveSetting ("Preferences.HideEtcdDir", HideEtcdDir);
			_sqlCache.SaveSetting ("Preferences.Set", true);
		}
	}
}

