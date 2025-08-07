using BudgetBuddy.Services;
using SQLite;

namespace BudgetBuddy;

public partial class LoginPage : ContentPage
{
    private readonly TransactionDatabase _database;

    public LoginPage(TransactionDatabase database)
    {
        InitializeComponent();
        _database = database;
    }

    private async void OnLoginClicked(object sender, EventArgs e)
    {
        var email = EmailEntry.Text?.Trim();
        var password = PasswordEntry.Text;

        if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(password))
        {
            ErrorLabel.Text = "Email and password required.";
            ErrorLabel.IsVisible = true;
            return;
        }

        var user = await _database.GetUserAsync(email, password);
        if (user != null)
        {
            Preferences.Default.Set("CurrentUserId", user.Id);

            var app = Application.Current;
            if (app != null && app.Windows.Count > 0)
            {
                app.Windows[0].Page = new AppShell();
            }
        }
        else
        {
            ErrorLabel.Text = "Invalid credentials. Click Create Account to create account with these credentials.";
            ErrorLabel.IsVisible = true;
        }
    }

    private async void OnCreateAccountClicked(object sender, EventArgs e)
    {
        var email = EmailEntry.Text?.Trim();
        var password = PasswordEntry.Text;

        if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(password))
        {
            ErrorLabel.Text = "Email and password required.";
            ErrorLabel.IsVisible = true;
            return;
        }
        bool create = await DisplayAlert(
        "Create Account",
        $"Do you want to create a new account with\n\n{email}?",
        "Yes",
        "No"
    );

        if (!create)
            return;
        try
        {
            var newUser = new User { Email = email, Password = password };
            await _database.CreateUserAsync(newUser);
            await DisplayAlert("Success", "Account created!", "OK");

            EmailEntry.Text = "";
            PasswordEntry.Text = "";
        }
        catch (SQLiteException ex)
        {
            ErrorLabel.Text = "Email already in use.";
            ErrorLabel.IsVisible = true;
        }
    }
}
