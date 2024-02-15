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
    public class Art:ReactiveUI.ReactiveObject
    {
        [PrimaryKey, AutoIncrement]
        [Reactive]
        public int Id { get; set; }
        [Reactive]
        public string Title { get; set; }
        [Reactive]
        public string Artist { get; set; }
        [Reactive]
        public string ImageUrl { get; set; }
        [Reactive]
        public DateTime ActionDate { get; set; }
    }
}
