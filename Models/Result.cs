using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrossPlatformProject2.Models
{
    public class Result
    {
        public string type { get; set; } //question type
        public string difficulty { get; set; }//dificulty level
        public string category { get; set; }//question category
        public string question { get; set; }//the question
        public string correct_answer { get; set; }//the answrer
        public List<string> incorrect_answers { get; set; }//incorrect answers
    }
}
