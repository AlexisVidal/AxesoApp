using Plugin.Geolocator;
using Plugin.Geolocator.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AxesoConsumer.Services
{
    public class GeolocatorService
    {
        #region Properties
        public double Latitude
        {
            get;
            set;
        }

        public double Longitude
        {
            get;
            set;
        }
        #endregion

        #region Methods
        public async Task GetLocation()
        {
            double lati = 0;
            double longi = 0;
            try
            {
                var locator = CrossGeolocator.Current;
                if (locator == null)
                {

                }
                else
                {
                    locator.DesiredAccuracy = 50;
                    Position location = new Position();
                    try
                    {
                        location = await locator.GetPositionAsync();
                        lati = location.Latitude;
                        longi = location.Longitude;
                    }
                    catch (Exception ex)
                    {

                    }
                }
                
                
                Latitude = lati;
                Longitude = longi;

            }
            catch (Exception ex)
            {
                ex.ToString();
            }
        }
        #endregion
    }
}
