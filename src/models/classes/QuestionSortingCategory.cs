namespace rpglms_backend.src.models
{
    public class QuestionSortingCategory
    {
        public required string Category { get; set; }
        public required List<string> Answers { get; set; }
    }
}
