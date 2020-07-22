using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.Web.Mvc;

using Google.Apis.Auth.OAuth2;
using Google.Apis.Auth.OAuth2.Flows;
using Google.Apis.Auth.OAuth2.Mvc;
using Google.Apis.Calendar.v3;
using Google.Apis.Util.Store;

namespace AxesoWeb.App_Start
{
    public class AppFlowMetadata : FlowMetadata
    {
        public static string ClientID = System.Configuration.ConfigurationManager.AppSettings["ClientID"];
        public static string ClientSecret = System.Configuration.ConfigurationManager.AppSettings["ClientSecret"];
        private static readonly IAuthorizationCodeFlow flow =
            new GoogleAuthorizationCodeFlow(new GoogleAuthorizationCodeFlow.Initializer
            {
                ClientSecrets = new ClientSecrets
                {
                    ClientId = ClientID,
                    ClientSecret = ClientSecret
                },
                Scopes = new[] { CalendarService.Scope.CalendarReadonly },
                DataStore = new FileDataStore(System.Web.HttpContext.Current.Server.MapPath("~/Content/calendar-dotnet-quickstart.json"), true)
            });
        public override string GetUserId(Controller controller)
        {
            // In this sample we use the session to store the user identifiers.
            // That's not the best practice, because you should have a logic to identify
            // a user. You might want to use "OpenID Connect".
            // You can read more about the protocol in the following link:
            // https://developers.google.com/accounts/docs/OAuth2Login.
            var user = controller.Session["user"];
            if (user == null)
            {
                user = Guid.NewGuid();
                controller.Session["user"] = user;
            }
            return user.ToString();

        }
        public override IAuthorizationCodeFlow Flow
        {
            get { return flow; }
        }
    }
}