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
        int currentUserId = Preferences.Default.Get("CurrentUserId", 0);
        if (currentUserId == 0)
            return; // or prompt to log in

        var list = await _database.GetTransactionsForUserAsync(currentUserId);
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
        await Shell.Current.GoToAsync("transaction?mode=Income");

    }

    private async void OnAddExpenseClicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("transaction?mode=Expense");

    }

    private void UpdateBalanceDisplay()
    {
        BalanceLabel.Text = $"${balance:F2}";
    }

    private async void SpendingBtn_Clicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync(nameof(Spending));
    }
}
