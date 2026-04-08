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
}