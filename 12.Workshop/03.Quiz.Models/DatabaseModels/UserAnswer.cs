using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quiz.Models.DatabaseModels
{
    public class UserAnswer
    {
        public UserAnswer()
        {
            Id = Guid.NewGuid().ToString();
        }

        [Key]
        public string Id { get; set; }

        public string IdentityUserId { get; set; }
        public IdentityUser IdentityUser { get; set; }

        public string QuestionId { get; set; }
        public Question Question { get; set; }

        public string AnswerId { get; set; }
        public Answer Answer { get; set; }
    }
}
