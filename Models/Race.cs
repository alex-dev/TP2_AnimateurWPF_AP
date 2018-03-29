using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;

namespace TP2_AnimateursWPF_AP.Models
{
    public class Race : IEquatable<Race>
    {
        #region Static

        private const string FileName = "abilities.json";

        public static HashSet<Race> Races { get; private set; }

        static Race()
        {
            Races = Load();
        }

        /// <summary>Lit le fichier des races.</summary>
        /// <remarks>
        ///   Si le fichier ne peut être lu, une liste de races sera construite à partir des races des personages.
        /// </remarks>
        private static HashSet<Race> Load()
        {
            HashSet<Race> data;
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
                if ((data = JsonConvert.DeserializeObject<HashSet<Race>>(json)) is null)
                {
                    throw new JsonException("Liste vide dans " + FileName);
                }
            }
            catch
            {
                data = new HashSet<Race>(Animateur.ChargerListeAnimateurs()
                    .SelectMany(animator => (from character in animator.LstPersonnages
                                             select character.Race)));
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
            return Name == other.Name;
        }

        public override int GetHashCode()
        {
            return 539060726 + EqualityComparer<string>.Default.GetHashCode(Name);
        }

        #endregion
    }
}
