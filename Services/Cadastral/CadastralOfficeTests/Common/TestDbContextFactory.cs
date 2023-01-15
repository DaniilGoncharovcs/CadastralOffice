using Microsoft.EntityFrameworkCore;

namespace CadastralOfficeTests.Common;

public class TestDbContextFactory
{
    public static Guid DocumentForDelete = Guid.NewGuid();
    public static Guid DocumentForUpdate = Guid.NewGuid();

    public static AppDbContext Create()
    {
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;

        var context = new AppDbContext(options);

        var document1 = new Document
        {
            Id = Guid.Parse("640e147a-08aa-4b50-a40c-77b90a210b80"),
            Name = "document1"
        };
        var document2 = new Document
        {
            Id = DocumentForUpdate,
            Name = "document2"
        };
        var document3 = new Document
        {
            Id = DocumentForDelete,
            Name = "document3"
        };

        context.Documents.AddRange(document1, document2, document3);
        context.SaveChanges();
        return context;
    }

    public static void Destroy(AppDbContext context)
    {
        context.Database.EnsureDeleted();
        context.Dispose();
    }
}