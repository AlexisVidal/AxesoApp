
using AxesoConsumer.Interfaces;
using SQLite.Net.Interop;
using Xamarin.Forms;

[assembly: Dependency(typeof(AxesoConsumer.Droid.Config))]
namespace AxesoConsumer.Droid
{
    public class Config : IConfig
    {
        private string directorioDB;
        private ISQLitePlatform plataforma;

        public string DirectoryDB
        {
            get
            {
                if (string.IsNullOrEmpty(directorioDB) || directorioDB.Equals(".."))
                {
                    directorioDB = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
                    //directorioDB = System.IO.Path.Combine(directorio, "..");
                }
                return directorioDB;
            }
        }
        public ISQLitePlatform Platform
        {
            get
            {
                if (plataforma == null)
                {
                    plataforma = new SQLite.Net.Platform.XamarinAndroid.SQLitePlatformAndroid();
                }
                return plataforma;
            }
        }
    }
}