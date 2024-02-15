using artclub.Models;
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
            _connection = new SQLiteAsyncConnection(Path.Combine(FileSystem.AppDataDirectory,DatabaseName));
            _connection.CreateTableAsync<Models.Member>();
            _connection.CreateTableAsync<Models.Art>();
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

        public async Task<List<Models.Art>> GetArtsAsync()
        {
            return await _connection.Table<Models.Art>().ToListAsync();
        }
    }
}
