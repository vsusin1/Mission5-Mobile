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
            
            ConnectionManager.LoginResult loginState = await ConnectionManager.Instance.Login(login, password);

            switch (loginState)
            {
                case ConnectionManager.LoginResult.EmptyLogin:
                    await DisplayAlertAsync("Erreur de connexion", "Le champ de login est vide.", "OK");
                    break;
                case ConnectionManager.LoginResult.EmptyPassword:
                    await DisplayAlertAsync("Erreur de connexion", "Le champ de mot de passe est vide.", "OK");
                    break;
                case ConnectionManager.LoginResult.WrongCredentials:
                    await DisplayAlertAsync("Erreur de connexion", "Le login ou le mot de passe est incorrect.", "OK");
                    break;
                case ConnectionManager.LoginResult.Success:
                    UpdateLayout();
                    break;
                default:
                    await DisplayAlertAsync("Erreur de connexion", "Une erreur inconnue s'est produite.", "OK");
                    break;
            }
        }

        async void BookingListButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new BookingListPage());
        }
        async void EditAccountButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new EditAccountPage());
        }

        async void DisconnectButton_Clicked(object sender, EventArgs e)
        {
            Client? cur = ConnectionManager.Instance.CurrentClient;

            if (cur != null)
            {
                var confirm = await DisplayAlertAsync("Question", $"Êtes-vous sûr{(cur.ClientGender == Client.Gender.Male ? "" : "e")} de vouloir vous déconnecter?", "Oui", "Non");

                if (confirm)
                {
                    ConnectionManager.Instance.Disconnect();

                    entry_login.SetValue(TitleProperty, String.Empty);
                    entry_password.SetValue(TitleProperty, String.Empty);
                    UpdateLayout();
                }

            }
        }

        void UpdateLayout()
        {
            layout_login.IsVisible = !ConnectionManager.Instance.IsConnected;
            layout_connected.IsVisible = ConnectionManager.Instance.IsConnected;
            button_disconnect.IsEnabled = ConnectionManager.Instance.IsConnected;
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
