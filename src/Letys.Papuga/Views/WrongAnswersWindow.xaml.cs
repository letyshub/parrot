using Letys.Parrot.Core;
using System.Collections.Generic;
using System.Windows;

namespace Letys.Parrot.Views
{
    public partial class WrongAnswersWindow : Window
    {
        public WrongAnswersWindow(IList<TestItem> items)
        {
            InitializeComponent();
            this.lvwItems.ItemsSource = items;
            this.lblQuestionQuantity.Content = items.Count.ToString();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
