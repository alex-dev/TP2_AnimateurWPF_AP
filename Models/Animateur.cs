using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

namespace TP2_AnimateursWPF_AP.Models
{
   /// <summary>
   /// Un animateur.
   /// </summary>
   public class Animateur
   {
      #region Static

      private const string NOM_FICHIER = "ListeAnimateurs.json";

      /// <summary>
      /// Fait la lecture du fichier de données et retourne une liste d'animateurs.
      /// </summary>
      /// <remarks>
      /// Si le fichier est inexistant, vide ou corrompu, la méthode retourne une liste vide.
      /// </remarks>
      /// <returns>Une liste d'animateurs.</returns>
      public static List<Animateur> ChargerListeAnimateurs()
      {
         StreamReader fichierAnimateurs;
         string json = null;
         List<Animateur> lstAnim;

         try
         {
            fichierAnimateurs = new StreamReader(File.OpenRead(NOM_FICHIER));
            json = fichierAnimateurs.ReadLine();
            fichierAnimateurs.Close();
         }
         catch { }

         // Si le fichier ne se lit pas correctement (pour quelque raison que ce soit), on charge une liste vide.
         try
         {
            lstAnim = JsonConvert.DeserializeObject<List<Animateur>>(json);

            if (lstAnim == null)
            {
               throw new Exception("Liste vide dans " + NOM_FICHIER);
            }
         }
         catch
         {
            lstAnim = new List<Animateur>();
         }

         return lstAnim;
      }
                                 
      /// <summary>
      /// Enregistre la liste des animateurs reçue dans le fichier de données.
      /// </summary>
      /// <remarks>
      /// Le contenu existant est écrasé à l'écriture.
      /// Si le fichier n'existe pas, il sera créé.
      /// </remarks>
      /// <param name="lst">La liste des animateurs qui doit être enregistrée dans le fichier.</param>
      public static void EnregistrerListeAnimateurs(List<Animateur> lst)
      {
         StreamWriter fichierAnimateurs;

         string json = JsonConvert.SerializeObject(lst);

         try
         {
            fichierAnimateurs = new StreamWriter(NOM_FICHIER, false);
            fichierAnimateurs.WriteLine(json);
            fichierAnimateurs.Close();
         }
         catch { }
      }
      #endregion

      #region Attributs

      public string Prenom { get; set; }
      public string Nom { get; set; }
      public PhoneNumber Telephone { get; set; }

      public List<Personnage> LstPersonnages { get; set; }

      #endregion

      /// <summary>
      /// Constructeur de la classe Animateur.
      /// </summary>
      /// <param name="prenom">Le prenom de l'animateur.</param>
      /// <param name="nom">Le nom de famille de l'animateur.</param>
      /// <param name="telephone">Le numéro de téléphone de l'animateur.</param>
      public Animateur(string prenom, string nom, PhoneNumber telephone)
      {
         Prenom = prenom;
         Nom = nom;
         Telephone = telephone;

         LstPersonnages = new List<Personnage>();
      }

   }
}
