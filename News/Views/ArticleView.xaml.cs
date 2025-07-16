using System.Web;

namespace News.Views;

public partial class ArticleView : ContentPage
{
    public ArticleView() { InitializeComponent(); }

    [QueryProperty("Url", "url")]
    public class ArticleView : ContentPage
    {
        public string Url
        {
            set =>
                BindingContext = new UrlWebViewSource
                {
                    Url = HttpUtility.UrlDecode(value)
                };
        }
    }
}