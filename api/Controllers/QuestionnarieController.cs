using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
        public dynamic Get()
        {
            // TODO: Add Page, pagesize and filters
            // TODO: this is the way to get the value from a many to many
            //       but there is a bug because just retrive 1 tag
            //       perhaps is better iterate over the array and get the tags like in the getquestion enpoint
            return _context.Questions
                .Include(q => q.User)
                .Include(q => q.Answers)
                .Include(q => q.QuestionTags)
                .SelectMany( q => q.QuestionTags.Select( qt => new { 
                            id = q.Id,
                            value = q.Value,
                            answers = q.Answers,
                            user = q.User,
                            tags = new { id = qt.Id, tag = qt.Tag.Description ?? "" }
                        }                    
                    )                
                )
                .ToArray();
        }


        [HttpGet]
        [Route("getquestionsbytag")]
        public dynamic GetQuestionsByTag(int tagId)
        {
            // TODO: Add Page, pagesize and filters
            // TODO: this is the way to get the value from a many to many
            //       but there is a bug because just retrive 1 tag
            //       perhaps is better iterate over the array and get the tags like in the getquestion enpoint
            return _context.Questions
                .Include(q => q.User)
                .Include(q => q.Answers)
                .Include(q => q.QuestionTags)
                .SelectMany(q => q.QuestionTags
                                    .Where(qt => qt.TagId == tagId)
                                    .Select(qt => new {
                                        id = q.Id,
                                        value = q.Value,
                                        answers = q.Answers,
                                        user = q.User,
                                        tags = new { id = qt.Id, tag = qt.Tag.Description ?? "" }
                                    }
                    )
                )
                .ToArray();
        }

        [HttpGet]
        [Route("getquestion")]
        public Question Get(int id = 0)
        {
            var retval = _context.Questions
                .Include(q => q.User)
                .Include(q => q.Answers)
                .Include(q => q.QuestionTags)
                .Where(q => q.Id == id)
                .FirstOrDefault() ?? new Question { };
            
            foreach(var item in retval.QuestionTags)
            {
                item.Tag = _context.Tags.Where(t => t.Id == item.TagId).FirstOrDefault();
            }

            return retval;
        }

        [HttpPost]
        public Question Post(QuestionAdd question)
        {
            Question retQuestion = new Question { Value = question.Value, UserId = question.UserId, Votes = 0 };
            try
            {
                // Save or Create Question
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
                // Update question
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
                // Update Question
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
            return retQuestion.Votes;
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

                // Save question Tag 
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
                // Save or Create Answer
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
                // Update answer
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
                // Update Aswer
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
            return retAnswer.Votes;
        }



    }
}
