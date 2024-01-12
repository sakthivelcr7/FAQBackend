using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UmLogic.Models
{
    public class Users
    {
        public int Id { get; set; }
        [Key]
        public string Ques { get; set; }
        public string Ans { get; set; }
      
    }
}
