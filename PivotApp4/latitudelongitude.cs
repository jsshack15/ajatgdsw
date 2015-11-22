using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Devices.Geolocation;
using System.IO.IsolatedStorage;
using Windows.Web.Http;
using Microsoft.Phone.Maps;


namespace PivotApp4
{
    public static class latitudelongitude
    {
        public static string Longitude = "akshay";
        public static string Latitude = null;

        public async static void getlocation()
        {


            //if ((bool)IsolatedStorageSettings.ApplicationSettings["LocationConsent"] != true)
            //{
            //    Longitude = "hello";
            //    Latitude = "world";
            //    // The user has opted out of Location.

            //}



            Geolocator geolocator = new Geolocator();
            geolocator.DesiredAccuracyInMeters = 50;

            try
            {
                Geoposition geoposition = await geolocator.GetGeopositionAsync(
                    maximumAge: TimeSpan.FromMinutes(5),
                    timeout: TimeSpan.FromSeconds(10)
                    );
                Longitude = geoposition.Coordinate.Point.Position.Longitude.ToString("0.00");
                Latitude = geoposition.Coordinate.Point.Position.Latitude.ToString("0.00");




               








                //Longitude =
                //Latitude = geoposition.Coordinate.Longitude.ToString("0.00");
            }
            catch (Exception ex)
            {
                Latitude = "vashistha";
                Longitude = "ajaja";
                if ((uint)ex.HResult == 0x80004004)
                {
                    Latitude = "vashistha";
                    Longitude = "ajahhgcja";
                }
            }  //else
            {
                // something else happened acquring the location
            }
        }

    }




    //string latitude = geoposition.Coordinate.Latitude.ToString("0.00");
    //string longitude = geoposition.Coordinate.Longitude.ToString("0.00");
    // }
    //catch (Exception ex)
    //{
    //    if ((uint)ex.HResult == 0x80004004)
    //    {
    //        // the application does not have the right capability or the location master switch is off
    //        StatusTextBlock.Text = "location  is disabled in phone settings.";
    //    }
    //    //else
    //    {
    //        // something else happened acquring the location
    //    }
    //}
}



//public static string g { get; set; }


