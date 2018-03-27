namespace TP2_AnimateursWPF_AP.Models
{
    public class Race
    {
        public string Name { get; private set; }

        public Race(string name)
        {
            Name = name;
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
