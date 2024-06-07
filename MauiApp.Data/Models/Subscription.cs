using MauiApp.Data.Models.Abstract;

namespace MauiApp.Data.Models;

public class Subscription : Entity
{
    public string Name { get; set; }
    
    public decimal Price { get; set; }
}