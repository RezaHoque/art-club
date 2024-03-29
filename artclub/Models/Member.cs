﻿using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace artclub.Models
{
    [Table("Members")]
    public class Member
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public int TotalLots { get; set; }
        public string Company { get; set; }
        public DateTime MembershipDate { get; set; }


    }
}
