using Microsoft.AspNetCore.Mvc.RazorPages;

public class IndexModel : PageModel
{
    // example: inject AppDbContext if you want DB access
    // private readonly AppDbContext _db;
    // public IndexModel(AppDbContext db) => _db = db;

    public int PlaceringCount { get; private set; }

    public void OnGet()
    {
        // PlaceringCount = _db.Placeringer.Count();
        PlaceringCount = 0;
    }
}