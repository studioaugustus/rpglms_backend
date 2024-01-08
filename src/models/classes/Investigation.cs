using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations.Schema;

namespace rpglms.src.models
{
    public class Investigation
    {
        public int InvestigationId { get; set; } // Primary key
        public required string Content { get; set; }
        public required string SerializedClues { get; set; }

        [NotMapped]
        public List<InvestigationClue> Clues
        {
            get => string.IsNullOrEmpty(SerializedClues)
                   ? []
                   : JsonConvert.DeserializeObject<List<InvestigationClue>>(SerializedClues) ?? [];
            set => SerializedClues = JsonConvert.SerializeObject(value);
        }

        // One-to-One relationship to Quest
        public int QuestId { get; set; }
        public required Quest Quest { get; set; }

        // Additional properties and methods...
    }
}
