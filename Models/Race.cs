using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Windows;
using Newtonsoft.Json;

namespace TP2_AnimateursWPF_AP.Models
{
    public class Race : IEquatable<Race>
    {
        #region Static

        private const string FileName = "Races.json";

        private static ObservableCollection<Race> races;

        public static ObservableCollection<Race> Races
        {
            get { return races; }
            private set
            {
                races = value;
                races.CollectionChanged += ((App)Application.Current).Application_DetectedChange;
            }
        }
        static Race()
        {
            Races = Load();
        }

        /// <summary>Lit le fichier des races.</summary>
        /// <remarks>
        ///   Si le fichier ne peut être lu, une liste de races sera construite à partir des races des personages.
        /// </remarks>
        private static ObservableCollection<Race> Load()
        {
            ObservableCollection<Race> data;
            string json = null;

            try
            {
                var file = new StreamReader(FileName);
                json = file.ReadLine();
                file.Close();
            }
            catch { }

            try
            {
                if ((data = JsonConvert.DeserializeObject<ObservableCollection<Race>>(json)) is null)
                {
                    throw new JsonException("Liste vide dans " + FileName);
                }
            }
            catch
            {
                data = new ObservableCollection<Race>(
                    Animateur.ChargerListeAnimateurs()
                        .SelectMany(animator => (from character in animator.LstPersonnages
                                                 select character.Race))
                        .Distinct());
            }

            return data;
        }

        /// <summary>Enregistre la liste des races.</summary>
        public static void Save()
        {
            try
            {
                var file = new StreamWriter(FileName, false);
                file.WriteLine(JsonConvert.SerializeObject(Races));
                file.Close();
            }
            catch { }
        }

        #endregion

        public string Name { get; private set; }

        public Race(string name)
        {
            Name = name;
        }

        public override string ToString()
        {
            return Name;
        }

        #region IEquatable<Race>

        public bool Equals(Race other)
        {
            return EqualityComparer<string>.Default.Equals(Name, other.Name);
        }

        public override int GetHashCode()
        {
            return 539060726 + EqualityComparer<string>.Default.GetHashCode(Name);
        }

        public static bool operator ==(Race self, Race other)
        {
            return self?.Equals(other) ?? false;
        }

        public static bool operator !=(Race self, Race other)
        {
            return !(self?.Equals(other) ?? false);
        }

        #endregion
    }
}
