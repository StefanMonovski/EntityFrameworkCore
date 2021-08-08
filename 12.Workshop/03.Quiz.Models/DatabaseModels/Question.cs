using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quiz.Models.DatabaseModels
{
    public class Question
    {
        public Question()
        {
            Id = Guid.NewGuid().ToString();
            Answers = new HashSet<Answer>();
            UsersAnswers = new HashSet<UserAnswer>();
        }

        [Key]
        public string Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Title { get; set; }

        public string QuizId { get; set; }
        public Quiz Quiz { get; set; }

        public ICollection<Answer> Answers { get; set; }

        public ICollection<UserAnswer> UsersAnswers { get; set; }
    }
}
