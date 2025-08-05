using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;

namespace BudgetBuddy.Services
{
    public class Transactions
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        public int UserId { get; set; }
        public string Type { get; set; }

        public string Category { get; set; }

        public string Note { get; set; }

        public decimal Amount { get; set; }

        public DateTime Date { get; set; } = DateTime.Now;
    }
}
