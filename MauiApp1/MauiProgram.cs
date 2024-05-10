using MauiApp1.Utilities;
using MauiAppSqlite;
using Microsoft.EntityFrameworkCore;

namespace MauiApp1;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
			.ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
			});
		builder.Services.AddDbContext<AppDbContext>(
			options=>options.UseSqlite($"Filename={PathDB.GetPath("englishvndb.db3")}", x=>
				x.MigrationsAssembly(nameof(MauiAppSqlite))));
		builder.Services.AddTransient<MainPage>();
		builder.Services.AddTransient<StoriesPage>();
		
		return builder.Build();
	}

	public static string GetDatabasePath()
	{
		var databasePath = "";

		var dbName = "englishvndb.db3";
		
		if (DeviceInfo.Platform == DevicePlatform.Android)
		{
			databasePath = Path.Combine(FileSystem.AppDataDirectory, dbName);
		}
		else if (DeviceInfo.Platform == DevicePlatform.iOS)
		{
			SQLitePCL.Batteries_V2.Init();
			databasePath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
			databasePath = Path.Combine(databasePath, "..", "Library", dbName);
		}

		return databasePath;
	}
}
