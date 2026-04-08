using System.Xml.Serialization;

namespace AppSicilyLines;

public partial class BookingListPage : ContentPage
{
    List<Booking> _bookings;
	public BookingListPage()
	{
        InitializeComponent();
	}

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        if (ConnectionManager.Instance.IsConnected)
        {
            int clientId = ConnectionManager.Instance.CurrentClient.Id;

            _bookings = await Helper.GetHttpResource<List<Booking>>($"api/client/{clientId}/bookings");
            list_bookings.ItemsSource = _bookings;
        }
    }

    async void BookingDetails_Clicked(object sender, EventArgs e)
    {
        SelectionChangedEventArgs? ev = e as SelectionChangedEventArgs;

        if (ev != null)
        {

            Booking? selected = ev.CurrentSelection[0] as Booking;

            if (selected == null)
                return;

            Booking? current = selected;

            string choice = "";

            while (choice != "Fermer")
            {
                int thisId = _bookings.FindIndex(b => b.Id == current.Id);
                choice = await DisplayActionSheetAsync(
                    $"Votre réservation pour la traversée de {current.Crossing.StartPort} à {current.Crossing.EndPort},\nle {current.PrettyDate}.",
                    null,
                    null,
                    (thisId > 0 ? "Précédente" : null),
                    (thisId < _bookings.Count - 1 ? "Suivante" : null),
                    "Fermer"
                    );
                
                if (choice == "Précédente")
                {
                    int prevId = Math.Min(Math.Max(thisId - 1, 0), _bookings.Count - 1);

                    current = _bookings[prevId];
                }
                if (choice == "Suivante")
                {
                    int nextId = Math.Min(Math.Max(thisId + 1, 0), _bookings.Count - 1);
                    current = _bookings[nextId];
                }
            }
        }
    }
}