using Microsoft.AspNetCore.Mvc;

namespace questionnaire.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class QuestionnarieController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<QuestionnarieController> _logger;

        public QuestionnarieController(ILogger<QuestionnarieController> logger)
        {
            _logger = logger;
        }

        [HttpGet(Name = "GetQuestion")]
        public IEnumerable<Question> Get()
        {
            return Enumerable.Range(1, 5).Select(index => new Question
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
        }
    }
}
