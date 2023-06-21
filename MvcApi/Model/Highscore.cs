using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DBcon.Model
{
    public class Highscore
    {
        [Key]
        [Required]
        [ForeignKey("Login")]
        public string Username { get; set; }

        [Required]
        public int Score { get; set; }
    }
}
