using BudgetBuddy.Services;
using System.Collections.ObjectModel;
using System.Transactions;

namespace BudgetBuddy;

public partial class MainPage : ContentPage
{
    private readonly TransactionDatabase _database;

    private ObservableCollection<Transactions> transactions = new();
    private decimal balance = 0;

    public MainPage(TransactionDatabase database)
    {
        InitializeComponent();
        _database = database;

        TransactionsView.ItemsSource = transactions;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await LoadTransactions();
    }

    private async Task LoadTransactions()
    {
        var list = await _database.GetTransactionsAsync();
        transactions.Clear();
        balance = 0;

        foreach (var t in list)
        {
            transactions.Add(t);
            balance += t.Amount;
        }

        UpdateBalanceDisplay();
    }

    private async void OnAddIncomeClicked(object sender, EventArgs e)
    {
        string amountStr = await DisplayPromptAsync("Add Income", "Enter amount:", "OK", "Cancel", keyboard: Keyboard.Numeric);
        if (decimal.TryParse(amountStr, out decimal amount) && amount > 0)
        {
            var t = new Transactions { Description = "Income", Amount = amount };
            await _database.SaveTransactionAsync(t);
            await LoadTransactions();
        }
    }

    private async void OnAddExpenseClicked(object sender, EventArgs e)
    {
        string amountStr = await DisplayPromptAsync("Add Expense", "Enter amount:", "OK", "Cancel", keyboard: Keyboard.Numeric);
        if (decimal.TryParse(amountStr, out decimal amount) && amount > 0)
        {
            var t = new Transactions { Description = "Expense", Amount = -amount };
            await _database.SaveTransactionAsync(t);
            await LoadTransactions();
        }
    }

    private void UpdateBalanceDisplay()
    {
        BalanceLabel.Text = $"${balance:F2}";
    }
}
