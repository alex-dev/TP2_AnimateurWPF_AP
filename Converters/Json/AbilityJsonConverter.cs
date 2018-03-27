using System;
using Newtonsoft.Json;
using TP2_AnimateursWPF_AP.Models;

namespace TP2_AnimateursWPF_AP.Converters.Json
{
    /// <summary><see cref="JsonConverter"/> converting <see cref="Ability"/> in JSON.</summary>
    public class AbilityJsonConverter : JsonConverter<Ability>
    {
        public override Ability ReadJson(
            JsonReader reader,
            Type objectType,
            Ability existingValue,
            bool hasExistingValue,
            JsonSerializer serializer)
        {
            return new Ability(reader.Value.ToString());
        }

        public override void WriteJson(
            JsonWriter writer,
            Ability value,
            JsonSerializer serializer)
        {
            writer.WriteValue(value.Name);
        }
    }
}
