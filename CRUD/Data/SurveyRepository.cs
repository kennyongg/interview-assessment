using Microsoft.EntityFrameworkCore;

namespace CRUD.Data
{
    public class SurveyRepository
    {
        internal async static Task<List<Surveys>> GetSurveysAsync()
        {
            using (var db = new AppDBContext())
            {
                return await db.Surveys.ToListAsync();
            }
        }

        internal async static Task<Surveys> GetSurveyByIdAsync(int surveyId)
        {
            using (var db = new AppDBContext())
            {
                return await db.Surveys
                    .FirstOrDefaultAsync(survey => survey.SurveyId == surveyId);
            }
        }

        internal async static Task<bool> CreateSurveyAsync(Surveys surveyToCreate)
        {
            using (var db = new AppDBContext())
            {
                try
                {
                    surveyToCreate.SubmitTime = DateTime.Now;
                    await db.Surveys.AddAsync(surveyToCreate);
                    await db.SaveChangesAsync(); // Save changes to generate the ID for the new survey

                    return await db.SaveChangesAsync() >= 1;
                }
                catch (Exception e)
                {
                    return false;
                }
            }
        }

        internal async static Task<bool> UpdateSurveyAsync(Surveys surveyToUpdate)
        {
            using (var db = new AppDBContext())
            {
                try
                {
                    db.Surveys.Update(surveyToUpdate);

                    return await db.SaveChangesAsync() >= 1;
                }
                catch (Exception e)
                {
                    return false;
                }
            }
        }

        internal async static Task<bool> DeleteSurveyAsync(int surveyId)
        {
            using (var db = new AppDBContext())
            {
                try
                {
                    Surveys surveyToDelete = await GetSurveyByIdAsync(surveyId);

                    db.Remove(surveyToDelete);

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
