using Microsoft.EntityFrameworkCore;

namespace CRUD.Data
{
    public class SurveyAnswersRepository
    {
        internal async static Task<List<SurveyAnswers>> GetSurveyAnswersAsync()
        {
            using (var db = new AppDBContext())
            {
                return await db.SurveyAnswers.ToListAsync();
            }
        }

        internal async static Task<SurveyAnswers> GetSurveyAnswerByIdAsync(int surveyAnswerId)
        {
            using (var db = new AppDBContext())
            {
                return await db.SurveyAnswers
                    .FirstOrDefaultAsync(surveyAnswer => surveyAnswer.SurveyAnswerId == surveyAnswerId);
            }
        }

        internal async static Task<bool> CreateSurveyAnswerAsync(SurveyAnswers surveyAnswerToCreate)
        {
            using (var db = new AppDBContext())
            {
                try
                {
                    await db.SurveyAnswers.AddAsync(surveyAnswerToCreate);

                    return await db.SaveChangesAsync() >= 1;
                }
                catch (Exception e)
                {
                    return false;
                }
            }
        }

        internal async static Task<bool> UpdateSurveyAnswerAsync(SurveyAnswers surveyAnswerToUpdate)
        {
            using (var db = new AppDBContext())
            {
                try
                {
                    db.SurveyAnswers.Update(surveyAnswerToUpdate);

                    return await db.SaveChangesAsync() >= 1;
                }
                catch (Exception e)
                {
                    return false;
                }
            }
        }

        internal async static Task<bool> DeleteSurveyAnswerAsync(int surveyAnswerId)
        {
            using (var db = new AppDBContext())
            {
                try
                {
                    SurveyAnswers surveyAnswerToDelete = await GetSurveyAnswerByIdAsync(surveyAnswerId);

                    db.Remove(surveyAnswerToDelete);

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
