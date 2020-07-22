using System;
using System.Collections.Generic;
using System.Text;

namespace AxesoConsumer.Services
{
    public interface IGoogleAuthService
    {
        void Autheticate(IGoogleAuthenticationDelegate googleAuthenticationDelegate);

    }
    
}
