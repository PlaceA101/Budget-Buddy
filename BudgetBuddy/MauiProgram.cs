using BudgetBuddy;
using BudgetBuddy.Services;
using SkiaSharp.Views.Maui.Controls.Hosting;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .UseSkiaSharp();
            

        string dbPath = Path.Combine(FileSystem.AppDataDirectory, "transactions.db3");

        //if (File.Exists(dbPath))
        //    File.Delete(dbPath);

        builder.Services.AddSingleton(new TransactionDatabase(dbPath));

        return builder.Build();
    }
}