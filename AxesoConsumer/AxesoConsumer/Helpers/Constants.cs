using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace AxesoConsumer.Helpers
{
	public static class Constants
	{
		public static string AppName = "AxesoUINative";

		// OAuth
		// For Google login, configure at https://console.developers.google.com/
		public static string iOSClientId = "314791326411-j7n9i0v7macm934mr92rfbvhs41cbinj.apps.googleusercontent.com";
		public static string AndroidClientId = "314791326411-l4do85avd3kp3ju2bbtoust7s4o5pn1f.apps.googleusercontent.com";

		// These values do not need changing
		public static string Scope = "https://www.googleapis.com/auth/userinfo.email";
		public static string AuthorizeUrl = "https://accounts.google.com/o/oauth2/auth";
		public static string AccessTokenUrl = "https://www.googleapis.com/oauth2/v4/token";
		public static string UserInfoUrl = "https://www.googleapis.com/oauth2/v2/userinfo";

		// Set these to reversed iOS/Android client ids, with :/oauth2redirect appended
		public static string iOSRedirectUrl = "com.googleusercontent.apps.314791326411-j7n9i0v7macm934mr92rfbvhs41cbinj:/oauth2redirect";
		public static string AndroidRedirectUrl = "com.googleusercontent.apps.314791326411-l4do85avd3kp3ju2bbtoust7s4o5pn1f:/oauth2redirect";

		public const string GoogleMapsApiKey = "AIzaSyAJ2Y_hbwcXftOfQbnUD_8ZNjpzwMQ3Dgs";

		public static string GeneraPass()
		{
			var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
			var stringChars = new char[8];
			var random = new Random();

			for (int i = 0; i < stringChars.Length; i++)
			{
				stringChars[i] = chars[random.Next(chars.Length)];
			}

			return new String(stringChars);

		}
		public static string EncriptaClave(string token)
		{
			string claveencrip = "";
			try
			{
				SHA1 sha1 = new SHA1CryptoServiceProvider();
				byte[] inputBytes = (new System.Text.UnicodeEncoding()).GetBytes(token);
				byte[] hash = sha1.ComputeHash(inputBytes);

				claveencrip = Convert.ToBase64String(hash);
			}
			catch (Exception ex)
			{

			}
			return claveencrip;
		}

		public const string DatabaseFilename = "Axeso.db3";

		public const SQLite.SQLiteOpenFlags Flags =
			// open the database in read/write mode
			SQLite.SQLiteOpenFlags.ReadWrite |
			// create the database if it doesn't exist
			SQLite.SQLiteOpenFlags.Create |
			// enable multi-threaded database access
			SQLite.SQLiteOpenFlags.SharedCache;

		public static string DatabasePath
		{
			get
			{
				var basePath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
				return Path.Combine(basePath, DatabaseFilename);
			}
		}

	}
}
