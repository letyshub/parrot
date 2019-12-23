using GalaSoft.MvvmLight.Command;
using Letys.Parrot.Core;
using System.Diagnostics;

namespace Letys.Parrot.ViewModel
{
    public class DisplayItemsViewModel : ApplicationViewModel
    {
        public DisplayItemsViewModel(TestSet testHeader)
        {
            this.TestHeader = testHeader;
        }

        public TestSet TestHeader { get; }

        private RelayCommand pdfGenerateCommand;
        public RelayCommand PdfGenerateCommand
        {
            get
            {
                if (this.pdfGenerateCommand == null)
                {
                    this.pdfGenerateCommand = new RelayCommand(() =>
                    {
                        using (new WaitCursor())
                        {
                            Process.Start(PdfFileCreator.CreateFlashcardPdfFile(this.TestHeader));
                        }
                    });
                }

                return this.pdfGenerateCommand;
            }
        }
    }
}
