using BudgetBuddy;
using BudgetBuddy.Services;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>();

        string dbPath = Path.Combine(FileSystem.AppDataDirectory, "transactions.db3");
        builder.Services.AddSingleton(new TransactionDatabase(dbPath));

        return builder.Build();
    }
}