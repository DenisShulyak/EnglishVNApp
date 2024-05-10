using MauiAppSqlite;
using MauiAppSqlite.Enums;
using MauiAppSqlite.Models;
using Microsoft.EntityFrameworkCore;
using Image = MauiAppSqlite.Models.Image;

namespace MauiApp1;

public partial class StoriesPage : ContentPage
{
	private readonly AppDbContext _context;
	public StoriesPage(AppDbContext context)
	{
		_context = context;
		InitializeComponent();
		
		// _context.Stories.RemoveRange(_context.Stories);
		// _context.SaveChanges();
		
		// for (int i = 0; i < 8; i++)
		// {
		// 	var imgId = Guid.NewGuid();
		// 	_context.Images.Add(new Image
		// 	{
		// 		Name = "Картинка истории 1",
		// 		ImageType = ImageType.Button,
		// 		Path = "path story 1",
		// 		Id = imgId
		// 	});
		// 	_context.Stories.Add(new Story
		// 	{
		// 		Name = "История " + (i + 1),
		// 		Id = Guid.NewGuid(),
		// 		Number = i + 1,
		// 		Description = "История 1",
		// 		ImageId = imgId
		// 	});
		// }
		//
		// _context.SaveChanges();
		
		LoadData();
	}
	
	/// <summary>
	/// Загрузка контента
	/// </summary>
	private async void LoadData()
	{
		List<string> stories = await _context.Stories.OrderBy(x=>x.Number).Select(x=>x.Name).ToListAsync();

		// Создание кнопок на основе полученных данных
		foreach (string story in stories)
		{
			Button button = new Button
			{
				Text = story,
				HorizontalOptions = LayoutOptions.CenterAndExpand,
				HeightRequest = 100,
				WidthRequest = 300
			};
			button.Clicked += HistoryButton_Clicked;
			
			// Добавление кнопки в вертикальный стек
			stackLayout.Children.Add(button);
		}
	}
	
	private async void HistoryButton_Clicked(object sender, EventArgs e)
	{
		Button button = (Button)sender;

		var story = _context.Stories.FirstOrDefault(x => x.Name == button.Text);

		// Здесь можно обработать нажатие кнопки, например, открыть новую страницу
		//await DisplayAlert("Выбранная история", story.Name, "OK");
		
		var chapterPage = new ChaptersPage(_context, story);
		await Navigation.PushAsync(chapterPage);
	}
}