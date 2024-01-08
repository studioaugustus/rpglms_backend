using Newtonsoft.Json;
using rpglms.src.models.enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace rpglms.src.models
{
    public class Question
    {
        public int QuestionId { get; set; } // Primary key

        // Relationships
        public int ChronicleId { get; set; }
        public Chronicle? Chronicle { get; set; }

        public int ChapterId { get; set; }
        public Chapter? Chapter { get; set; }

        public int PracticeId { get; set; }
        public Practice? Practice { get; set; }

        public int PerformanceId { get; set; }
        public Performance? Performance { get; set; }

        // Enum for QuestionType
        public QuestionType Type { get; set; }

        // Nullable lists
        public string? SerializedChoices { get; set; }
        public string? SerializedAnswers { get; set; }

        [NotMapped]
        public List<string> Choices
        {
            get => string.IsNullOrEmpty(SerializedChoices)
                   ? []
                   : JsonConvert.DeserializeObject<List<string>>(SerializedChoices) ?? [];
            set => SerializedChoices = JsonConvert.SerializeObject(value);
        }

        [NotMapped]
        public List<string> Answers
        {
            get => string.IsNullOrEmpty(SerializedAnswers)
                   ? []
                   : JsonConvert.DeserializeObject<List<string>>(SerializedAnswers) ?? [];
            set => SerializedAnswers = JsonConvert.SerializeObject(value);
        }

        // Complex property for QuestionSortingCategories
        public string? SerializedSortingCategories { get; set; }

        [NotMapped]
        public List<QuestionSortingCategory> SortingCategories
        {
            get => string.IsNullOrEmpty(SerializedSortingCategories)
                   ? []
                   : JsonConvert.DeserializeObject<List<QuestionSortingCategory>>(SerializedSortingCategories) ?? [];
            set => SerializedSortingCategories = JsonConvert.SerializeObject(value);
        }

        // Additional properties and methods...
    }
}
