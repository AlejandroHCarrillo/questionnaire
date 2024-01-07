namespace questionnaireApi.DTO
{
    public class AnswerAddDto
    {
        public int QuestionId { get; set; }
        public string? Value { get; set; }
        public int UserId { get; set; }
    }
}
