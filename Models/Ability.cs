using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;

namespace TP2_AnimateursWPF_AP.Models
{
    public class Ability : IEquatable<Ability>
    {
        #region Static

        private const string FileName = "abilities.json";

        public static HashSet<Ability> Abilities { get; private set; }

        static Ability()
        {
            Abilities = Load();
        }

        /// <summary>Lit le fichier des habiletés.</summary>
        /// <remarks>
        ///   Si le fichier ne peut être lu, une liste d'habiletés sera construite à partir des habiletés des personages.
        /// </remarks>
        private static HashSet<Ability> Load()
        {
            HashSet<Ability> data;
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
                if ((data = JsonConvert.DeserializeObject<HashSet<Ability>>(json)) is null)
                {
                    throw new JsonException("Liste vide dans " + FileName);
                }
            }
            catch
            {
                data = new HashSet<Ability>(Animateur.ChargerListeAnimateurs()
                    .SelectMany(animator => animator.LstPersonnages
                        .SelectMany(character => character.LstHabiletes)));
            }

            return data;
        }

        /// <summary>Enregistre la liste des habiletés.</summary>
        public static void Save()
        {
            try
            {
                var file = new StreamWriter(FileName, false);
                file.WriteLine(JsonConvert.SerializeObject(Abilities));
                file.Close();
            }
            catch { }
        }

        #endregion

        public string Name { get; set; }

        public Ability(string name)
        {
            Name = name;
        }

        public override string ToString()
        {
            return Name;
        }

        #region IEquatable<Ability>

        public bool Equals(Ability other)
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
