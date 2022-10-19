using System;
using System.Drawing;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace SiliFish.Helpers.JsonConverters
{
    //https://stackoverflow.com/questions/69664644/serialize-deserialize-system-drawing-color-with-system-text-json
    public class ColorJsonConverter : JsonConverter<Color>
    {
        public override Color Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) => ColorTranslator.FromHtml(reader.GetString());

        public override void Write(Utf8JsonWriter writer, Color value, JsonSerializerOptions options) => writer.WriteStringValue("#" + value.R.ToString("X2") + value.G.ToString("X2") + value.B.ToString("X2").ToLower());
    }
}
