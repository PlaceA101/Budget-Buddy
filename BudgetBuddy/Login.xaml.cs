namespace budgetBuddy;

public partial class Login : ContentPage
{
    public Login()
    {
        InitializeComponent();
    }

    private async void LoginBtnClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new MainPage());
    }

    private async void CreateBtnClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new CreateUser());
    }



}
