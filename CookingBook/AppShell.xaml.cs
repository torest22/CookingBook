using CookingBook.Pages;
namespace CookingBook
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

            Routing.RegisterRoute(nameof(FistPage),   typeof(FistPage));
            Routing.RegisterRoute(nameof(AddPage),    typeof(AddPage));
            Routing.RegisterRoute(nameof(EditPage),   typeof(EditPage));
            Routing.RegisterRoute(nameof(ListPage),   typeof(ListPage));
            Routing.RegisterRoute(nameof(ViewPage),   typeof(ViewPage));
            Routing.RegisterRoute(nameof(RandomPage), typeof(RandomPage));
        }
    }
}
