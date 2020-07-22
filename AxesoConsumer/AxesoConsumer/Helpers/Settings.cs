using Axeso_BE;
using Plugin.Settings;
using Plugin.Settings.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;

namespace AxesoConsumer.Helpers
{
	public static class Settings
	{
		private const string token = "token";
		private const string userEmail = "userEmail";
		private const string userID = "userID";
		private const string userName = "userName";
		private const string userPassword = "userPassword";
		private const string isRemember = "isRemember";
		private const string latitude = "latitude";
		private const string longitude = "longitude";
		private const string distancia = "distancia";
		private const string address = "address";
		private const string expires = "expires";
		public const string productoId = "productoId";
		public const string cantResultado = "cantResultado";
		public const string errorText = "errorText";
		public const string successText = "successText";
		public const string cantFarmacias = "cantFarmacias";
		public const string cotizacionID = "cotizacionID";
		public const string usuarioDireccionID = "usuarioDireccionID";
		public const string categoriaID = "categoriaID";
		public const string productoMarcaID = "productoMarcaID";
		public const string productoBuscado = "productoBuscado";

		public const string currentSolicitud = "currentSolicitud";
		public const string currentSolicitudID = "currentSolicitudID";
		public const string farmaciasOnlineList = "farmaciasOnlineList";
		public const string cotizacionProductoJson = "cotizacionProductoJson"; 
		public const string cotizacionJson = "cotizacionJson";

		public const string pedidoID = "pedidoID";

		private static readonly string stringDefault = string.Empty;
		private static readonly double doubleDefault = 0;
		private static readonly float floatDefault = 0;
		private static readonly int intDefault = 0;
		private static readonly bool boolDefault = false;
		private static readonly DateTime dateDefault = DateTime.Now;

        private static ISettings AppSettings => CrossSettings.Current;

		public static string Token
		{
			get => AppSettings.GetValueOrDefault(token, stringDefault);
			set => AppSettings.AddOrUpdateValue(token, value);
		}

		public static string FarmaciasOnlineList
		{
			get => AppSettings.GetValueOrDefault(farmaciasOnlineList, stringDefault);
			set => AppSettings.AddOrUpdateValue(farmaciasOnlineList, value);
		}
		
		public static string UserEmail
		{
			get => AppSettings.GetValueOrDefault(userEmail, stringDefault);
			set => AppSettings.AddOrUpdateValue(userEmail, value);
		}
		public static string UserName
		{
			get => AppSettings.GetValueOrDefault(userName, stringDefault);
			set => AppSettings.AddOrUpdateValue(userName, value);
		}
		public static string UserPassword
		{
			get => AppSettings.GetValueOrDefault(userPassword, stringDefault);
			set => AppSettings.AddOrUpdateValue(userPassword, value);
		}

		public static bool IsRemember
		{
			get => AppSettings.GetValueOrDefault(isRemember, boolDefault);
			set => AppSettings.AddOrUpdateValue(isRemember, value);
		}

		public static double Latitude
		{
			get => AppSettings.GetValueOrDefault(latitude, doubleDefault);
			set => AppSettings.AddOrUpdateValue(latitude, value);
		}
		public static double Longitude
		{
			get => AppSettings.GetValueOrDefault(longitude, doubleDefault);
			set => AppSettings.AddOrUpdateValue(longitude, value);
		}
		public static double Distancia
		{
			get => AppSettings.GetValueOrDefault(distancia, doubleDefault);
			set => AppSettings.AddOrUpdateValue(distancia, value);
		}
		
		public static DateTime Expires
		{
			get => AppSettings.GetValueOrDefault(expires, dateDefault);
			set => AppSettings.AddOrUpdateValue(expires, value);
		}
		public static string Address
		{
			get => AppSettings.GetValueOrDefault(address, stringDefault);
			set => AppSettings.AddOrUpdateValue(address, value);
		}
		public static int ProductoId
		{
			get => AppSettings.GetValueOrDefault(productoId, intDefault);
			set => AppSettings.AddOrUpdateValue(productoId, value);
		}
		public static string CurrentSolicitud
		{
			get => AppSettings.GetValueOrDefault(currentSolicitud, stringDefault);
			set => AppSettings.AddOrUpdateValue(currentSolicitud, value);
		}
		public static int CantResultado
		{
			get => AppSettings.GetValueOrDefault(cantResultado, intDefault);
			set => AppSettings.AddOrUpdateValue(cantResultado, value);
		}
		public static string ErrorText
		{
			get => AppSettings.GetValueOrDefault(errorText, stringDefault);
			set => AppSettings.AddOrUpdateValue(errorText, value);
		}
		public static string SuccessText 
		{
			get => AppSettings.GetValueOrDefault(successText, stringDefault);
			set => AppSettings.AddOrUpdateValue(successText, value);
		}
		public static int UserID
		{
			get => AppSettings.GetValueOrDefault(userID, intDefault);
			set => AppSettings.AddOrUpdateValue(userID, value);
		}

		public static int CantFarmacias
		{
			get => AppSettings.GetValueOrDefault(cantFarmacias, intDefault);
			set => AppSettings.AddOrUpdateValue(cantFarmacias, value);
		}

		public static int CurrentSolicitudID
		{
			get => AppSettings.GetValueOrDefault(currentSolicitudID, intDefault);
			set => AppSettings.AddOrUpdateValue(currentSolicitudID, value);
		}
		public static int CotizacionID
		{
			get => AppSettings.GetValueOrDefault(cotizacionID, intDefault);
			set => AppSettings.AddOrUpdateValue(cotizacionID, value);
		}
		public static int UsuarioDireccionID
		{
			get => AppSettings.GetValueOrDefault(usuarioDireccionID, intDefault);
			set => AppSettings.AddOrUpdateValue(usuarioDireccionID, value);
		}
		public static int CategoriaID
		{
			get => AppSettings.GetValueOrDefault(categoriaID, intDefault);
			set => AppSettings.AddOrUpdateValue(categoriaID, value);
		}
		public static int ProductoMarcaID
		{
			get => AppSettings.GetValueOrDefault(productoMarcaID, intDefault);
			set => AppSettings.AddOrUpdateValue(productoMarcaID, value);
		}

		public static string ProductoBuscado
		{
			get => AppSettings.GetValueOrDefault(productoBuscado, stringDefault);
			set => AppSettings.AddOrUpdateValue(productoBuscado, value);
		}
        public static int PedidoID
		{
			get => AppSettings.GetValueOrDefault(pedidoID, intDefault);
			set => AppSettings.AddOrUpdateValue(pedidoID, value);
		}

		public static string CotizacionProductoJson
        {
			get => AppSettings.GetValueOrDefault(cotizacionProductoJson, stringDefault);
			set => AppSettings.AddOrUpdateValue(cotizacionProductoJson, value);
		}
		public static string CotizacionJson
		{
			get => AppSettings.GetValueOrDefault(cotizacionJson, stringDefault);
			set => AppSettings.AddOrUpdateValue(cotizacionJson, value);
		}
		
	}
}
