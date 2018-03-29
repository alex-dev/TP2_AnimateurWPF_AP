using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using TP2_AnimateursWPF_AP.Converters.Json;
using TP2_AnimateursWPF_AP.Models;

namespace TP2_AnimateursWPF_AP
{
    /// <summary>
    /// Logique d'interaction pour App.xaml
    /// </summary>
    public partial class App : Application
    {
        public bool IsDirty { get; private set; }

        public App()
        {
            IsDirty = false;
        }

        /// <summary>Enregistre les changements s'il y a lieu.</summary>
        /// <param name="animators">Tous les animateurs.</param>
        /// <param name="abilities">Toutes les habiletés.</param>
        /// <param name="races">Toutes les races.</param>
        /// <remarks>
        ///   Même si <paramref cref="races"/> n'est pas sauvegardé et <paramref cref="abilities"/> n'est pas utilisé directement,
        ///   forcer l'interface permet de limiter les changements au code.
        /// </remarks>
        public void SaveChanges(IEnumerable<Animateur> animators, IEnumerable<Ability> abilities, IEnumerable<Race> races)
        {
            Animateur.EnregistrerListeAnimateurs(animators.ToList());
            Ability.Save();

            IsDirty = false;
        }

        #region Event Handlers

        public void Application_DetectedChange(object sender, EventArgs e)
        {
            IsDirty = true;
        }

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

        #endregion
    }
}
