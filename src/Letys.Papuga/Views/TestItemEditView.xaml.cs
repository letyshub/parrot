using Letys.Parrot.Core;
using System.Windows;

namespace Letys.Parrot.Views
{
    public partial class TestItemEditView : Window
    {
        private enum TextBoxKeyboardEntry
        {
            None,
            Answer,
            Question,
            Example
        }

        private TextBoxKeyboardEntry textBoxKeyboardEntry;

        public string Question { get; set; }
        public string Answer { get; set; }
        public string Example { get; set; }

        public TestItemEditView()
        {
            InitializeComponent();
            this.textBoxKeyboardEntry = TextBoxKeyboardEntry.None;
        }

        public TestItemEditView(TestItem item) : this()
        {
            this.txtQuestion.Text = item.Question;
            this.txtAnswer.Text = item.Answer;
            this.txtExample.Text = item.Example;
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
            this.Close();
        }

        private void btnAccept_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
            this.Question = this.txtQuestion.Text;
            this.Answer = this.txtAnswer.Text;
            this.Example = this.txtExample.Text;
            this.Close();
        }

        private void btnTest_Click(object sender, RoutedEventArgs e)
        {
            if (this.textBoxKeyboardEntry == TextBoxKeyboardEntry.Answer)
            {
                this.txtAnswer.Text += "a";
            }
            else if (this.textBoxKeyboardEntry == TextBoxKeyboardEntry.Question)
            {
                this.txtQuestion.Text += "b";
            }
        }

        private void txtQuestion_LostFocus(object sender, RoutedEventArgs e)
        {
            this.textBoxKeyboardEntry = TextBoxKeyboardEntry.Question;
        }

        private void txtAnswer_LostFocus(object sender, RoutedEventArgs e)
        {
            this.textBoxKeyboardEntry = TextBoxKeyboardEntry.Answer;
        }
    }
}
