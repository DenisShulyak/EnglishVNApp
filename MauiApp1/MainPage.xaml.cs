

using MauiAppSqlite;

namespace MauiApp1;

public partial class MainPage : ContentPage
{
	private readonly AppDbContext _context;
	
	public MainPage(AppDbContext context)
	{
		_context = context;
		InitializeComponent();
	}

	private async void Button_Clicked(object sender, EventArgs e)
	{
        var test = new StoriesPage(_context);
        await Navigation.PushAsync(test);
    }
}

