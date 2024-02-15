using artclub.Data;
using artclub.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace artclub.Pages
{
    public class ArtPageViewModel:BindableObject
    {
        private readonly DbService _dbService;
        public List<Art> Arts { get; set; }
        public ArtPageViewModel(DbService dbService)
        {
            _dbService = dbService;
            Task.Run(async () => Arts = await _dbService.GetArtsAsync());
        }
    }
}
