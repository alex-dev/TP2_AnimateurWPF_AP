namespace TP2_AnimateursWPF_AP.Models
{
    public class Ability
    {
        public string Name { get; set; }

        public Ability(string name)
        {
            Name = name;
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
