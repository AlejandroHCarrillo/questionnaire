using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using Q_EF_DB;
using Q_EF_DB.Entities;
using questionnaireApi.DTO;
using System.Linq;

namespace questionnaire.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class QuestionnarieController : ControllerBase
    {
        private readonly ILogger<QuestionnarieController> _logger;
        private QContext _context;

        public QuestionnarieController(QContext context, ILogger<QuestionnarieController> logger)
        {
            _logger = logger;
            _context = context;
        }

        [HttpGet]
        [Route("allquestions")]
        public IEnumerable<Question> Get()
        {
            return _context.Questions.ToArray<Question>();
        }

        [HttpGet]
        [Route("getquestion")]
        public Question Get(int id = 0)
        {
            var retval = _context.Questions.Where(x => x.Id == id).FirstOrDefault();
            return retval!=null?retval: new Question();
        }

        [HttpPost]
        public Question Post(QuestionAdd question)
        {
            Question retQuestion = new Question { Value = question.Value, UserId = question.UserId, Votes = 0 };
            try
            {
                // Save or Create Post
                _context.Add(retQuestion);
                _context.SaveChanges();

            }
            catch (Exception e)
            {
                _logger.LogError(e.ToString());
            }
            //finally
            //{
            //}
            return retQuestion;
        }

        [HttpPut]
        public dynamic Put(QuestionUpdate question)
        {
            // Verify the question Exists
            Question retQuestion = _context.Questions.Where(x => x.Id == question.Id).FirstOrDefault();

            if (retQuestion == null)
            {
                return new
                {
                    success = false,
                    message = "The question does not exists",
                    result = "Error"
                };
            }

            retQuestion.Value = question.Value;

            try
            {
                // Save or Create Post
                _context.Update<Question>(retQuestion);
                _context.SaveChanges();

            }
            catch (Exception e)
            {
                _logger.LogError(e.ToString());
            }
            //finally
            //{
            //}
            return retQuestion;
        }

        [HttpPut]
        [Route("votequestion")]
        public dynamic VoteQuestion(int id)
        {
            // TODO Verify the user already voted.

            // Verify the Post Exists
            Question retQuestion = _context.Questions.Where(x => x.Id == id).FirstOrDefault();

            if (retQuestion == null)
            {
                return new
                {
                    success = false,
                    message = "The question does not exists",
                    result = "Error"
                };
            }

            retQuestion.Votes = retQuestion.Votes == null ? 1 : retQuestion.Votes + 1;

            try
            {
                // Save or Create Post
                _context.Update<Question>(retQuestion);
                _context.SaveChanges();

            }
            catch (Exception e)
            {
                _logger.LogError(e.ToString());
            }
            //finally
            //{
            //}
            return retQuestion;
        }

        [HttpPut]
        [Route("tagquestion")]
        public dynamic TagQuestion(QuestionTagDTO qTag)
        {
            // Verify the Question Exists
            Question question = _context.Questions.Where(x => x.Id == qTag.questionId).FirstOrDefault();
            if (question == null)
            {
                return new
                {
                    success = false,
                    message = "The question does not exists",
                    result = "Error"
                };
            }
            // Verify the Tag Exists
            Tag tag = _context.Tags.Where(x => x.Id == qTag.tagId).FirstOrDefault();
            if (tag == null)
            {
                return new
                {
                    success = false,
                    message = "The tag does not exists",
                    result = "Error"
                };
            }

            // Verify the Questiontag Exists
            QuestionTag retQuestionTag = _context.QuestionTags.Where(x => x.QuestionId == qTag.questionId && x.TagId == qTag.tagId).FirstOrDefault();

            if (retQuestionTag != null)
            {
                // Remove question tag
                // Delete question tag
                _context.Remove(retQuestionTag);
                _context.SaveChanges();

                return retQuestionTag;
            }

            try
            {
                QuestionTag newQuestionTag = new QuestionTag { QuestionId = qTag.questionId, TagId = qTag.tagId };

                // Save or Create Post
                _context.Add<QuestionTag>(newQuestionTag);
                _context.SaveChanges();

            }
            catch (Exception e)
            {
                _logger.LogError(e.ToString());
            }
            //finally
            //{
            //}
            return retQuestionTag;
        }

        [HttpPost]
        [Route("answer")]
        public Answer PostAnswer(AnswerAddDto answer)
        {
            Answer retAnswer = new Answer { Value = answer.Value, QuestionId = answer.QuestionId, UserId = answer.UserId, Votes = 0 };
            try
            {
                // Save or Create Post
                _context.Add(retAnswer);
                _context.SaveChanges();

            }
            catch (Exception e)
            {
                _logger.LogError(e.ToString());
            }
            //finally
            //{
            //}
            return retAnswer;
        }

        [HttpPut]
        [Route("answer")]
        public dynamic PutAnswer(AnswerUpdateDto answer)
        {
            // Verify the question Exists
            Answer retAnswer = _context.Answers.Where(x => x.Id == answer.Id ).FirstOrDefault();

            if (retAnswer == null)
            {
                return new
                {
                    success = false,
                    message = "The answer does not exists",
                    result = "Error"
                };
            }

            retAnswer.Value = answer.Value;

            try
            {
                // Save or Create Post
                _context.Update(retAnswer);
                _context.SaveChanges();

            }
            catch (Exception e)
            {
                _logger.LogError(e.ToString());
            }
            //finally
            //{
            //}
            return retAnswer;
        }

        [HttpPut]
        [Route("voteanswer")]
        public dynamic VoteAnswer(int id)
        {
            // TODO Verify the user already voted for this answer.

            // Verify the Post Exists
            Answer retAnswer = _context.Answers.Where(x => x.Id == id).FirstOrDefault();

            if (retAnswer == null)
            {
                return new
                {
                    success = false,
                    message = "The answer does not exists",
                    result = "Error"
                };
            }

            retAnswer.Votes = retAnswer.Votes == null ? 1 : retAnswer.Votes + 1;

            try
            {
                // Save or Create Post
                _context.Update<Answer>(retAnswer);
                _context.SaveChanges();

            }
            catch (Exception e)
            {
                _logger.LogError(e.ToString());
            }
            //finally
            //{
            //}
            return retAnswer;
        }



    }
}
