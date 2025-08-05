// TransactionPage.xaml.cs
using BudgetBuddy.Services;

namespace BudgetBuddy
{
    // allow passing "mode" via Shell query if you like
    [QueryProperty(nameof(Mode), "mode")]
    public partial class TransactionPage : ContentPage
    {
        private readonly List<string> _expenseCategories = new()
{
    "Food", "Rent", "Transport", "Health", "Entertainment", "Other"
};

        private readonly List<string> _incomeCategories = new()
{
    "Salary", "Bonus", "Investment", "Gift", "Other"
};
        private readonly TransactionDatabase _db;
        private string _type = "Income";

        public TransactionPage(TransactionDatabase db)
        {
            InitializeComponent();
            _db = db;

            // default highlight
            IncomeBtn.BackgroundColor = Colors.Green;
            ExpenseBtn.BackgroundColor = Colors.LightGray;
            UpdateTypeUI();

            CategoryPicker.ItemsSource = _incomeCategories;
            CategoryPicker.SelectedIndex = 0;
        }

        // optional: let Shell navigation pre-select Income/Expense
        public string Mode
        {
            set
            {
                _type = value;
                UpdateTypeUI();

                // NEW: swap the picker’s source based on mode
                if (_type == "Income")
                    CategoryPicker.ItemsSource = _incomeCategories;
                else
                    CategoryPicker.ItemsSource = _expenseCategories;

                CategoryPicker.SelectedIndex = 0;
            }
        }

        void OnTypeClicked(object sender, EventArgs e)
        {
            _type = ((Button)sender).Text;
            UpdateTypeUI();

            if (_type == "Income")
            {
                CategoryPicker.ItemsSource = _incomeCategories;
            }
            else // Expense
            {
                CategoryPicker.ItemsSource = _expenseCategories;
            }
            CategoryPicker.SelectedIndex = 0;
        }

        void UpdateTypeUI()
        {
            IncomeBtn.BackgroundColor = _type == "Income" ? Colors.Green : Colors.LightGray;
            ExpenseBtn.BackgroundColor = _type == "Expense" ? Colors.Red : Colors.LightGray;
        }

        private async void OnSaveClicked(object sender, EventArgs e)
        {
            ErrorLabel.IsVisible = false;

            // 1) Validate amount
            if (!decimal.TryParse(AmountEntry.Text, out var amt) || amt <= 0)
            {
                ErrorLabel.Text = "Enter a valid amount.";
                ErrorLabel.IsVisible = true;
                return;
            }
            // 2) Validate category
            if (CategoryPicker.SelectedItem == null)
            {
                ErrorLabel.Text = "Select a category.";
                ErrorLabel.IsVisible = true;
                return;
            }

            int currentUserId = Preferences.Default.Get("CurrentUserId", 0);
            if (currentUserId == 0)
            {
                await DisplayAlert("Error", "No user logged in.", "OK");
                return;
            }
            var tx = new Transactions
            {
                UserId = currentUserId,
                Type = _type,
                Category = CategoryPicker.SelectedItem.ToString(),
                Amount = _type == "Expense" ? -amt : amt,
                Date = DatePicker.Date,
                Note = NoteEditor.Text
            };
            await _db.SaveTransactionAsync(tx);

            // 4) Go back (Shell “..” means parent route)
            await Shell.Current.GoToAsync("..");
        }

    }
}
