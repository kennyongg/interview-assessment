using System.ComponentModel.DataAnnotations;

namespace CRUD.Data
{
    public class SurveyAnswers
    {
        [Key]
        public int SurveyAnswerId { get; set; }

        [Required]
        [MaxLength(100000)]
        public string Answer { get; set; }

        [Required]
        public Surveys Survey { get; set; }

        [Required]
        public Questions Question { get; set; }
    }
}
