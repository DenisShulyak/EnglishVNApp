namespace MauiApp1.Utilities;

public static class PathDB
{
    public static string GetPath(string nameDB)
    {
        string pathDbSql = string.Empty;

        if (DeviceInfo.Platform == DevicePlatform.Android)
        {
            pathDbSql = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
            pathDbSql = Path.Combine(pathDbSql, nameDB);
        }
        else if (DeviceInfo.Platform == DevicePlatform.iOS)
        {
            pathDbSql = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            pathDbSql = Path.Combine(pathDbSql, "..", "Library", nameDB);
        }
        else if (DeviceInfo.Platform == DevicePlatform.WinUI)
        {
            pathDbSql = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
            pathDbSql = Path.Combine(pathDbSql, "win" + nameDB);
        }

        return pathDbSql;
    }
}