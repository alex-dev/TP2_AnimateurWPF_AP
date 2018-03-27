using Newtonsoft.Json;
using System.Collections.Generic;
using System.Windows;
using TP2_AnimateursWPF_AP.Converters.Json;

namespace TP2_AnimateursWPF_AP
{
    /// <summary>
    /// Logique d'interaction pour App.xaml
    /// </summary>
    public partial class App : Application
    {
        private void Application_Startup(object sender, StartupEventArgs e)
        {
            JsonConvert.DefaultSettings = () => new JsonSerializerSettings
            {
                Converters = new List<JsonConverter>
                {
                    new AbilityJsonConverter(),
                    new PhoneNumberJsonConverter(),
                    new RaceJsonConverter()
                }
            };
        }
    }
}
