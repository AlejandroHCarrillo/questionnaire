using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System.IO;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using Q_EF_DB.Entities;
using Q_EF_DB;
using questionnaire.Controllers;

namespace FileUploadAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FileUploadController : ControllerBase
    {
        private readonly ILogger<QuestionnarieController> _logger;
        private QContext _context;

        public FileUploadController(QContext context, ILogger<QuestionnarieController> logger)
        {
            _logger = logger;
            _context = context;
        }

        private const long MaxFileSize = 10 * 1024 * 1024; // File size maximum length permmited (10 MB)

        [HttpPost("upload")]
        public async Task<IActionResult> Upload(IFormFile file)
        {
            if (file == null || file.Length == 0)
                return BadRequest("File not found to upload.");

            if (file.Length > MaxFileSize)
                return BadRequest("File size exceeds the maximum length permmited");

            string[] allowedExtensions = { ".csv", ".txt", ".xls" }; // Permmitted extensions 

            var fileExtension = Path.GetExtension(file.FileName).ToLowerInvariant();
            if (string.IsNullOrEmpty(fileExtension) || !allowedExtensions.Contains(fileExtension))
                return BadRequest("File type extension not supported.");


            var filePath = Path.Combine(Path.GetTempPath(), file.FileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            insertCSVInfoToDB(filePath);

            return Ok("File uploaded successfully.");
        }

        private void insertCSVInfoToDB(string filePath)
        {
//            string filePath = "path/to/your/file.csv";
            using (var reader = new StreamReader(filePath))
            {
                string line;
                int lastQuestionId = 0;

                while ((line = reader.ReadLine()) != null)
                {
                    Console.WriteLine(line); 
                    if (line.Substring(0, 1).ToUpper() == "Q") { 
                        Console.WriteLine("Es una pregunta");
                        var question = line.Substring(2);
                        lastQuestionId = saveQuestion(question);
                    }
                    if (line.Substring(0, 1).ToUpper() == "A")
                    {
                        Console.WriteLine("Es una respuesta");
                        var answer = line.Substring(2);
                        saveAnswer(lastQuestionId, answer);
                    }
                }
            }
        }

        private int saveQuestion(string question)
        {
            int idQuestion = 0;
            Question retQuestion = new Question { Value = question, UserId = 1, Votes = 0 };
            try
            {
                // Save or Create question
                _context.Add(retQuestion);
                _context.SaveChanges();
                idQuestion = retQuestion.Id ?? 0;
            }
            catch (Exception e)
            {
                _logger.LogError(e.ToString());
            }

            return idQuestion;
        }

        private int saveAnswer(int questionId, string answer)
        {
            int idAnswer = 0;

            if (questionId == 0) return 0; // Ignore answers without question
            Answer retAnswer = new Answer { QuestionId = questionId, Value = answer,  UserId = 1, Votes = 0 };
            try
            {
                // Save Answer
                _context.Add(retAnswer);
                _context.SaveChanges();
                idAnswer = retAnswer.Id ?? 0;
            }
            catch (Exception e)
            {
                _logger.LogError(e.ToString());
            }

            return idAnswer;
        }

    }
}
