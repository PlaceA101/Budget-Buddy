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
        }

        public Task<List<Transactions>> GetTransactionsAsync()
            => _database.Table<Transactions>().OrderByDescending(t => t.Date).ToListAsync();

        public Task<int> SaveTransactionAsync(Transactions transaction)
            => _database.InsertAsync(transaction);
    }
}
