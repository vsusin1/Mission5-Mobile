using System.Security.Cryptography;
using System.Text;

namespace AppSicilyLines;

public partial class EditAccountPage : ContentPage
{
	Client? client;
	public EditAccountPage()
	{
		InitializeComponent();

		client = ConnectionManager.Instance.CurrentClient;

        form_editCoordinates.BindingContext = client;

    }

	void ChangePasswordCheckbox_Clicked(object sender, EventArgs e)
	{
		label_newPassword.IsVisible = checkbox_changePassword.IsChecked;
		entry_password.IsVisible = checkbox_changePassword.IsChecked;

    }


    async void ValidateButton_Clicked(object sender, EventArgs e)
	{
		if (client != null)
        {
			bool changePwd = checkbox_changePassword.IsChecked;

			if (entry_password.Text == null && changePwd)
			{
				await DisplayAlertAsync("Alerte", "Le mot de passe ne peut pas être vide.", "Ok.");
				return;
			}

			if (entry_birthDate.Date == null)
            {
                await DisplayAlertAsync("Alerte", "La date de naissance ne peut pas être vide.", "Ok.");
				return;
            }

			string finalPwd = client.Password;

			if (changePwd)
			{
                byte[] passwordToHash = Encoding.ASCII.GetBytes(entry_password.Text);

                byte[] hashedPwd = SHA256.HashData(passwordToHash);

                finalPwd = Convert.ToHexStringLower(hashedPwd);
            }

            var statusCode = await Helper.EditProfileInfo(new Client(client.Id, client.LastName, client.FirstName, DateOnly.FromDateTime(entry_birthDate.Date.Value), client.ClientGender, client.Login, finalPwd));

			if (statusCode == System.Net.HttpStatusCode.OK) // HTTP 200
                await DisplayAlertAsync("Notification", "Votre profil utilisateur vient d'être modifié.", "Ok.");
			else
				await DisplayAlertAsync("Erreur", "Une erreur s'est produite, veuillez réessayer plus tard.", "Ok.");

            await Navigation.PopAsync();
        }
	}


}