﻿using News.Views;

namespace News;

public partial class AppShell : Shell
{
    public AppShell()
    {
        InitializeComponent();
        Routing.RegisterRoute("articleview", typeof(ArticleView));
    }
}