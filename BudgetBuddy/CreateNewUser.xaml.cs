namespace BudgetBuddy
{
    public partial class CreateNewUser : ContentPage
    {
        

        public CreateNewUser()
        {
            InitializeComponent();
        }

        private void SubBtnClicked(object sender, EventArgs e)
        {
            Welcome.Text= "Welcome to BudgetBuddy, " + NameEntry.Text;
            NameLabel.Text = "Name: " + NameEntry.Text;
            EmailLabel.Text = "Email: " + EmailEntry.Text;

            StartBtn.IsVisible=true;

            
        }

        private async void StartBtn_Clicked(object sender, EventArgs e)
        {
          
            await Navigation.PushAsync(new MainPage());
        
    }
    }

}
