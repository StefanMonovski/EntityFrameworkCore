using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quiz.Models.DatabaseModels
{
    public class Answer
    {
        public Answer()
        {
            Id = Guid.NewGuid().ToString();
            UsersAnswers = new HashSet<UserAnswer>();
        }

        [Key]
        public string Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Title { get; set; }

        [Range(0, 100)]
        public int Points { get; set; }

        public string QuestionId { get; set; }
        public Question Question { get; set; }

        public ICollection<UserAnswer> UsersAnswers { get; set; }
    }
}
