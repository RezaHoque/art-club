using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace artclub.Models
{
    public class WinnerViewModel
    {
        public int MemberId { get; set; }
        public string MemberName { get; set; }
        public string BatchId { get; set; }
        public string ArtId { get; set; }
        public string ArtSrc { get; set; }
        public bool IsAbsent { get; set; }
        public bool IsNotInterested { get; set; }
    }
}
