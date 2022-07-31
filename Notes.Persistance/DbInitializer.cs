namespace Notes.Persistence
{
    public class DbInitializer
    {
        public static void Initialize(NoteDbContext context)
        {
            context.Database.EnsureCreated();
        }
    }
}