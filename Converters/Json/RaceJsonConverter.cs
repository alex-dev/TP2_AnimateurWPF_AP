using System;
using Newtonsoft.Json;
using TP2_AnimateursWPF_AP.Models;

namespace TP2_AnimateursWPF_AP.Converters.Json
{
    /// <summary><see cref="JsonConverter"/> converting <see cref="Race"/> in JSON.</summary>
    public class RaceJsonConverter : JsonConverter<Race>
    {
        public override Race ReadJson(
            JsonReader reader,
            Type objectType,
            Race existingValue,
            bool hasExistingValue,
            JsonSerializer serializer)
        {
            return new Race(reader.Value.ToString());
        }

        public override void WriteJson(
            JsonWriter writer,
            Race value,
            JsonSerializer serializer)
        {
            writer.WriteValue(value.Name);
        }
    }
}
