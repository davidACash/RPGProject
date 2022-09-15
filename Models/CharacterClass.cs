using System.Text.Json.Serialization;

namespace RPGProject.Models
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum CharacterClass
    {
        Warrior = 1,
        Mage = 2,
        Rogue = 3
    }
}