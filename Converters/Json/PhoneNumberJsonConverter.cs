using System;
using Newtonsoft.Json;
using TP2_AnimateursWPF_AP.Models;

namespace TP2_AnimateursWPF_AP.Converters.Json
{
    /// <summary><see cref="JsonConverter"/> converting <see cref="PhoneNumber"/> in JSON.</summary>
    public class PhoneNumberJsonConverter : JsonConverter<PhoneNumber>
    {
        public override PhoneNumber ReadJson(
            JsonReader reader,
            Type objectType,
            PhoneNumber existingValue,
            bool hasExistingValue,
            JsonSerializer serializer)
        {
            return new PhoneNumber(reader.Value.ToString());
        }

        public override void WriteJson(
            JsonWriter writer,
            PhoneNumber value,
            JsonSerializer serializer)
        {
            writer.WriteValue(value.Number);
        }
    }
}
