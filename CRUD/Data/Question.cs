using System.ComponentModel.DataAnnotations;

namespace CRUD.Data
{
    public class Question
    {
        [Key]
        public int QuestionId { get; set; }

        [Required]
        [MaxLength(100)]
        public string Title { get; set; } = string.Empty;
    }
}
