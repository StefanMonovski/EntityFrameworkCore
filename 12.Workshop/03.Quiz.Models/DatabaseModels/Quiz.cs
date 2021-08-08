using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quiz.Models.DatabaseModels
{
    public class Quiz
    {
        public Quiz()
        {
            Id = Guid.NewGuid().ToString();
            Questions = new HashSet<Question>();
        }

        [Key]
        public string Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Title { get; set; }

        public ICollection<Question> Questions { get; set; }
    }
}
