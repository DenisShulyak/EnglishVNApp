using MauiApp1.Services;
using MauiAppSqlite;

namespace MauiApp1;

public partial class MainPage : ContentPage
{
	private readonly AppDbContext _context;
	private readonly ApiService _apiService;
	public MainPage(AppDbContext context, ApiService apiService)
	{
		_context = context;
		_apiService = apiService;
		InitializeComponent();
	}

	private async void Button_Clicked(object sender, EventArgs e)
	{
        var test = new StoriesPage(_context);
        var t = await _apiService.GetAsync<List<object>>("http://10.0.2.2:5062/WeatherForecast");
        await Navigation.PushAsync(test);
    }
}

