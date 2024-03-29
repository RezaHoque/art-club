﻿using artclub.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace artclub.Data
{
    public class DbService
    {
        private const string DatabaseName = "ArtClubDb.db3";
        private readonly SQLiteAsyncConnection _connection;

        public DbService()
        {
            //var appDataPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
            //string dbPath = Path.Combine(appDataPath, "VeluxArtClub", DatabaseName);
            _connection = new SQLiteAsyncConnection(Path.Combine(FileSystem.Current.AppDataDirectory,DatabaseName));
            //_connection = new SQLiteAsyncConnection(dbPath);

             _connection.CreateTableAsync<Models.Member>();
             _connection.CreateTableAsync<Models.Art>();
             _connection.CreateTableAsync<Models.LotteryDraw>();
           
        }
        //Member
        public async Task<List<Models.Member>> GetMembersForDrawAsync(string batch)
        {
           var members= await _connection.Table<Models.Member>().Where(x=>!string.IsNullOrEmpty(x.Name)).ToListAsync();
           var membersForDraw=new List<Models.Member>();
           foreach(var member in members)
            {
                var draws=await _connection.Table<Models.LotteryDraw>().Where(x=>x.BatchId==batch).ToListAsync();
                if(!draws.Any(x=>x.WinnerId==member.Id))
                {
                    membersForDraw.Add(member);
                }
              
            }
           return membersForDraw;
        }
        public async Task<List<Models.Member>> GetMembersAsync()
        {
            return await _connection.Table<Models.Member>().Where(x=>!string.IsNullOrEmpty(x.Name)).ToListAsync();
        }
        public async Task<Member> GetById(int id)
        {
            return await _connection.Table<Models.Member>().Where(x => x.Id == id).FirstOrDefaultAsync();
        }
        public async Task Create(Member member)
        {
            await _connection.InsertAsync(member);
        }
        public async Task Update(Member member)
        {
            await _connection.UpdateAsync(member);
        }
        public async Task Delete(Member member)
        {
            await _connection.DeleteAsync(member);
        }
        //Art
        public async Task<List<Models.Art>> GetArtsAsync()
        {
            return await _connection.Table<Models.Art>().Where(x=>!string.IsNullOrEmpty(x.ImageUrl)).ToListAsync();
        }
        public async Task<List<Art>> GetArtsForSelectionAsync()
        {
            //return a list of art objects that is not present in LotteryDraw table
            var arts=await _connection.Table<Models.Art>().Where(x=>!string.IsNullOrEmpty(x.ImageUrl)).ToListAsync();
            var draws=await _connection.Table<Models.LotteryDraw>().ToListAsync();
            var artForSelection=new List<Models.Art>();
            foreach(var art in arts)
            {
                if(!draws.Any(x=>x.ArtId==art.Id))
                {
                    artForSelection.Add(art);
                }
            }
            return artForSelection;
        }
        public async Task CreateArtAsync(Art art)
        {
            await _connection.InsertAsync(art);
        }
        public async Task UpdateArtAsync(Art art)
        {
            await _connection.UpdateAsync(art);
        }
        public async Task DeleteArtAsync(Art art)
        {
            await _connection.DeleteAsync(art);
        }
        //Lottery draw
        public async Task<List<LotteryDraw>> GetDrawsAsync()
        {
            return await _connection.Table<LotteryDraw>().ToListAsync();
        }
        public async Task<LotteryDraw> GetDrawsAsync(int memberId, string batch)
        {
            return await _connection.Table<LotteryDraw>().Where(x => x.BatchId == batch && x.WinnerId == memberId).FirstOrDefaultAsync();
        }
        public async Task CreateDrawAsync(LotteryDraw draw)
        {
            await _connection.InsertAsync(draw);
        }
        public async Task UpdateDrawAsync(LotteryDraw draw)
        {
            await _connection.UpdateAsync(draw);
        }

        public async Task<List<Models.WinnerViewModel>> GetWinnersAsync()
        {
            var winners=new List<Models.WinnerViewModel>();
            var draws=await _connection.Table<Models.LotteryDraw>().Where(x=>x.WinnerId!=0).ToListAsync();
            foreach(var draw in draws)
            {
                var member=await GetById(draw.WinnerId);
                var art=await _connection.Table<Models.Art>().Where(x=>x.Id==draw.ArtId).FirstOrDefaultAsync();
                winners.Add(new Models.WinnerViewModel
                {
                    MemberName=member.Name,
                    MemberId=member.Id,
                    ArtId=art != null?art.Id.ToString():"N/A",
                    ArtSrc=art?.ImageUrl,
                    IsAbsent=draw.IsAbsent,
                    IsNotInterested=draw.IsNotInterested,
                    BatchId=draw.BatchId,
                });
            }
            return winners;
        }

    }
}
