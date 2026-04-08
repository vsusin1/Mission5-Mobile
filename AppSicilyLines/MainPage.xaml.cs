using System;

namespace AppSicilyLines
{
    public partial class MainPage : ContentPage
    {
        public string UserGreetSentence
        {
            get
            {
                Client? cur = ConnectionManager.Instance.CurrentClient;

                if (cur != null)
                    return $"Vous êtes connecté{(cur.ClientGender == Client.Gender.Male ? "" : "e")}, {cur.FirstName} {cur.LastName}.";

                return "Un problème s'est produit.";
            }
        }

        async void ConnectionButton_Clicked(object sender, EventArgs e)
        {
            string login = entry_login.Text;
            string password = entry_password.Text;
            
            await ConnectionManager.Instance.Login(login, password);
            UpdateLayout();
        }

        async void BookingListButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new BookingListPage());
        }
        async void EditAccountButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new EditAccountPage());
        }

        void UpdateLayout()
        {
            layout_login.IsVisible = !ConnectionManager.Instance.IsConnected;
            layout_connected.IsVisible = ConnectionManager.Instance.IsConnected;
            label_greet.Text = UserGreetSentence;
        }

        public MainPage()
        {
            InitializeComponent();

            label_greet.BindingContext = this;
            UpdateLayout();
        }
    }
}
