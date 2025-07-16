using System.Web;

namespace News.Views;

[QueryProperty("Url", "url")]
public partial class ArticleView : ContentPage
{
    public ArticleView() { InitializeComponent(); }

    public string Url
    {
        set =>
            BindingContext = new UrlWebViewSource
            {
                Url = HttpUtility.UrlDecode(value)
            };
    }
}