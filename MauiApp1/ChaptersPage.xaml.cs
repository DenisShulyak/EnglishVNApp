using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MauiAppSqlite;
using MauiAppSqlite.Models;
using Microsoft.EntityFrameworkCore;

namespace MauiApp1;

/// <summary>
/// Страница с главами
/// </summary>
public partial class ChaptersPage : ContentPage
{
    private readonly AppDbContext _context;
    private readonly Story _story;
    public ChaptersPage(AppDbContext context, Story story)
    {
        _context = context;
        _story = story;
        InitializeComponent();

        // _context.Chapters.RemoveRange(_context.Chapters);
        
        // for (int i = 0; i < 4; i++)
        // {
        //     _context.Chapters.Add(new Chapter
        //     {
        //         StoryId = story.Id,
        //         ImageId = story.ImageId,
        //         Name = "Глава" + (i+1),
        //         Number = i+1
        //     });
        // }
        //
        // _context.SaveChanges();

        LoadData();
    }
    
    private async void LoadData()
    {
        List<string> chapters = await _context.Chapters
            .Where(x=>x.StoryId == _story.Id)
            .OrderBy(x=>x.Number)
            .Select(x=>x.Name)
            .ToListAsync();

        // Создание кнопок на основе полученных данных
        int row = 1;
        bool column = false;
        foreach (string chapter in chapters)
        {
            Button button = new Button
            {
                Text = chapter,
                HorizontalOptions = LayoutOptions.CenterAndExpand,
                HeightRequest = 50,
                WidthRequest = 150,
                
            };
            button.Clicked += HistoryButton_Clicked;
			
            Grid.SetRow((BindableObject)button, row);
            Grid.SetColumn((BindableObject)button, column.GetHashCode());
            
            Grid.Children.Add(button);
            row = column ? row + 1 : row;
            column = !column;
        }
    }

    private async void HistoryButton_Clicked(object sender, EventArgs e)
    {

    }
}