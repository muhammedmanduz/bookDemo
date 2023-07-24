namespace bookDemo.Models
{
    public class ApplicationContext
    {
        public static List<Book> Books { get; set; }
        static ApplicationContext()
        { 
               Books= new List<Book>()
            {
                   new Book(){Id=1,Title="Hacivat ve Karagöz",Price=75},
                   new Book(){Id=2,Title="Mesnevi",Price=150},
                   new Book(){Id=3,Title="Dede Korkut",Price=75}
            };
        }
    }
}
