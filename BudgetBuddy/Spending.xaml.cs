using System.Transactions;
using __XamlGeneratedCode__;
using BudgetBuddy.Services;

namespace BudgetBuddy;

public partial class Spending : ContentPage
{
    private readonly TransactionDatabase _db;

    public Spending(TransactionDatabase db)
    {
        InitializeComponent();

        _db = db;

    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();

        int userId = Preferences.Default.Get("CurrentUserId", 0);
        if (userId == 0)
        {
            await DisplayAlert("Error", "No user logged in.", "OK");
            return;
        }

        var transactions = await _db.GetTransactionsForUserAsync(userId);

        decimal totalIncome = transactions
        .Where(t => t.Type == "Income")
        .Sum(t => t.Amount);

        decimal totalSpending = transactions
        .Where(t => t.Type == "Spending")
        .Sum(t => t.Amount * -1);

        decimal balance = totalIncome - totalSpending;

        IncomeLabel.Text = $"Total Income: {totalIncome:C}";
        SpendingLabel.Text = $"Total Spendings: {totalSpending: C}";
        BalanceLabel.Text = $"Remaining Balance: {balance: C}";
        
        
    }
}