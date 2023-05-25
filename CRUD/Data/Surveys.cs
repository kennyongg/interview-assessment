using System.ComponentModel.DataAnnotations;

namespace CRUD.Data
{
    public class Surveys
    {
        [Key]
        public int SurveyId { get; set; }

        [Required]
        public DateTime SubmitTime { get; set; }
    }
}
