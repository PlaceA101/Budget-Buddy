namespace BudgetBuddy
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute("transaction", typeof(TransactionPage));

            Routing.RegisterRoute("Spending", typeof(Spending));
        }
    }
}
