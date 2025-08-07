using BudgetBuddy.Services;
namespace BudgetBuddy
{
    public partial class App : Application
    {
        private readonly TransactionDatabase _database;
        public App(TransactionDatabase database)
        {
            InitializeComponent();
            _database = database;

        }

        protected override Window CreateWindow(IActivationState? activationState)
        {
            var nav = new NavigationPage(new LoginPage(_database));
            return new Window(nav);
        }
    }
}