using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace BudgetBuddy.Services
{
    public class TransactionDatabase
    {
        private readonly SQLiteAsyncConnection _database;

        public TransactionDatabase(string dbPath)
        {
            _database = new SQLiteAsyncConnection(dbPath);
            _database.CreateTableAsync<Transactions>().Wait();
            _database.CreateTableAsync<User>().Wait();
        }

        public Task<List<Transactions>> GetTransactionsAsync()
            => _database.Table<Transactions>().OrderByDescending(t => t.Date).ToListAsync();

        public Task<int> SaveTransactionAsync(Transactions transaction)
            => _database.InsertAsync(transaction);

        public Task<int> CreateUserAsync(User user)
            => _database.InsertAsync(user);

        public Task<User> GetUserAsync(string email, string password)
            => _database.Table<User>()
                        .Where(u => u.Email == email && u.Password == password)
                        .FirstOrDefaultAsync();
        public Task<List<Transactions>> GetTransactionsForUserAsync(int userId)
           => _database.Table<Transactions>()
                       .Where(t => t.UserId == userId)
                       .OrderByDescending(t => t.Date)
                       .ToListAsync();
    }
}
