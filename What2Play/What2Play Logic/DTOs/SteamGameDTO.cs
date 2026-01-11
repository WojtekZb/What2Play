using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace What2Play_Logic.DTOs
{
    public class SteamGameDTO
    {
        [JsonPropertyName("appid")]
        public int AppId { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("short_description")]
        public string Description { get; set; }

        //[JsonPropertyName("genres")]
        //public List<GameTypeDTO> Type { get; set; }

        public int Source { get; set; } = 2;

        [JsonIgnore]
        public bool Played { get; set; }

        [JsonPropertyName("playtime_forever")]
        public int PlaytimeRaw { get; set; }
    }
}
