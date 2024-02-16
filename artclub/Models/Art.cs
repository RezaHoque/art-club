using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ReactiveUI.Fody.Helpers;

namespace artclub.Models
{
    [Table("Arts")]
    public class Art
    {
        [PrimaryKey, AutoIncrement]

        public int Id { get; set; }

        public string Title { get; set; }

        public string Artist { get; set; }

        public string ImageUrl { get; set; }
        public string Price { get; set; }
        public string Size { get; set; }
        public string Description { get; set; }
        public string Batch { get; set; }
 
        public DateTime ActionDate { get; set; }
    }
}
