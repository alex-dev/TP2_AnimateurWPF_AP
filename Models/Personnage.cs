using System.Collections.Generic;

namespace TP2_AnimateursWPF_AP.Models
{
   /// <summary>
   /// Un personnage.
   /// </summary>
   public class Personnage
   {
      public string Nom { get; set; }
      public int PointsVie { get; set; }
      public int PointsDommage { get; set; }
      public Race Race { get; set; }
      public List<Ability> LstHabiletes { get; set; }

      /// <summary>
      /// Constructeur de la classe Personnage.
      /// </summary>
      /// <param name="nom">Le nom du personnage.</param>
      /// <param name="pointsVie">Le nombre de points de vie du personnage.</param>
      /// <param name="pointsDommage">Le nombre de dommage infligés par les attaques du personnage.</param>
      /// <param name="race">La race du personnage.</param>
      /// <param name="lstHabiletes">La liste des habiletés du personnage.</param>
      public Personnage(string nom, int pointsVie, int pointsDommage, Race race, List<Ability> lstHabiletes)
      {
         Nom = nom;
         PointsVie = pointsVie;
         PointsDommage = pointsDommage;
         Race = race;
         LstHabiletes = lstHabiletes;
      }
   }
}
