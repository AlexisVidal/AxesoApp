using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace AxesoConsumer.Models
{
    public class TokenResponse
    {
        #region Properties
        [SQLite.PrimaryKey, SQLite.AutoIncrement]
        public int TokenResponseId { get; set; }

        [JsonProperty(PropertyName = "access_token")]
        public string AccessToken { get; set; }

        [JsonProperty(PropertyName = "token_type")]
        public string TokenType { get; set; }

        [JsonProperty(PropertyName = "expires_in")]
        public int ExpiresIn { get; set; }

        [JsonProperty(PropertyName = "userName")]
        public string UserName { get; set; }

        [JsonProperty(PropertyName = ".issued")]
        public DateTime Issued { get; set; }

        [JsonProperty(PropertyName = ".expires")]
        public DateTime Expires { get; set; }

        [JsonProperty(PropertyName = "error_description")]
        public string ErrorDescription { get; set; }

        public bool IsRemembered
        {
            get;
            set;
        }

        public String Password
        {
            get;
            set;
        }
        #endregion

        #region Methods
        public override int GetHashCode()
        {
            return TokenResponseId;
        }
        #endregion
    }
}
