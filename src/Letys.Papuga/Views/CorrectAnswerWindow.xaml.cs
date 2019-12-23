using Letys.Parrot.Core;
using System.Windows;

namespace Letys.Parrot.Views
{
    public partial class CorrectAnswerWindow : Window
    {
        public CorrectAnswerWindow(TestItem item)
        {
            InitializeComponent();
            this.tblAnswer.Text = item.Answer;
            this.tblQuestion.Text = item.Question;
            this.tblExample.Text = item.Example;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
