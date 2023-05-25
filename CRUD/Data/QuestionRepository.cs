using Microsoft.EntityFrameworkCore;

namespace CRUD.Data
{
    public class QuestionsRepository
    {
        internal async static Task<List<Question>> GetQuestionsAsync()
        {
            using (var db = new AppDBContext())
            {
                return await db.Questions.ToListAsync();
            }
        }

        internal async static Task<Question> GetQuestionByIdAsync(int questionId)
        {
            using (var db = new AppDBContext())
            {
                return await db.Questions
                    .FirstOrDefaultAsync(question => question.QuestionId == questionId);
            }
        }

        internal async static Task<bool> CreateQuestionAsync(Question questionToCreate)
        {
            using (var db = new AppDBContext())
            {
                try
                {
                    await db.Questions.AddAsync(questionToCreate);

                    return await db.SaveChangesAsync() >= 1;
                }
                catch (Exception e)
                {
                    return false;
                }
            }
        }

        internal async static Task<bool> UpdateQuestionAsync(Question questionToUpdate)
        {
            using (var db = new AppDBContext())
            {
                try
                {
                    db.Questions.Update(questionToUpdate);

                    return await db.SaveChangesAsync() >= 1;
                }
                catch (Exception e)
                {
                    return false;
                }
            }
        }

        internal async static Task<bool> DeleteQuestionAsync(int questionId)
        {
            using (var db = new AppDBContext())
            {
                try
                {
                    Question questionToDelete = await GetQuestionByIdAsync(questionId);

                    db.Remove(questionToDelete);

                    return await db.SaveChangesAsync() >= 1;
                }
                catch (Exception e)
                {
                    return false;
                }
            }
        }
    }
}
