using System.Collections.ObjectModel;

namespace BudgetBuddy
{
    public partial class MainPage : ContentPage
    {
        private decimal balance = 0;
        private ObservableCollection<Transaction> transactions = new();

        public MainPage()
        {
            InitializeComponent();
            TransactionsView.ItemsSource = transactions;
            UpdateBalanceDisplay();
        }

        private void OnAddIncomeClicked(object sender, EventArgs e)
        {
            AddTransaction("Income", 100);
        }

        private void OnAddExpenseClicked(object sender, EventArgs e)
        {
            AddTransaction("Expense", -50);
        }

        private void AddTransaction(string description, decimal amount)
        {
            transactions.Insert(0, new Transaction { Description = description, Amount = amount });
            balance += amount;
            UpdateBalanceDisplay();
        }

        private void UpdateBalanceDisplay()
        {
            BalanceLabel.Text = $"${balance:F2}";
        }
    }

    public class Transaction
    {
        public string Description { get; set; }
        public decimal Amount { get; set; }
    }
}
