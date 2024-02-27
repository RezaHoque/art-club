using SQLite;
using System.ComponentModel.DataAnnotations.Schema;
using TableAttribute = SQLite.TableAttribute;

namespace artclub.Models
{
    [Table("LotteryDraws")]
    public class LotteryDraw
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public DateTime DrawDate { get; set; }
    
        public int WinnerId { get; set; }
        public bool IsAbsent { get; set; }
        public bool IsNotInterested{ get; set; }
        public bool PickedArt { get; set; }
        public int ArtId { get; set; }
        public string BatchId { get; set; }=DateTime.Now.Year.ToString();

    }
}
