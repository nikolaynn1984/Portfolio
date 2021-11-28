using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Landing.Win.View
{
    /// <summary>
    /// Логика взаимодействия для WindowWeb.xaml
    /// </summary>
    public partial class WindowWeb : Window
    {
        public WindowWeb()
        {
            InitializeComponent();
            //webView.NavigationCompleted += WebView_NavigationCompleted;
            
        }

        //private void WebView_NavigationCompleted(object sender, Microsoft.Web.WebView2.Core.CoreWebView2NavigationCompletedEventArgs e)
        //{
        //    webView.CoreWebView2.NavigateToString("<html><body><h1>sample</body></html>");
        //}

    }
}
