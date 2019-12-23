using Letys.Parrot.Bootstrap;
using Letys.Parrot.Model;
using Letys.Parrot.ViewModel;
using System.Windows.Controls;

namespace Letys.Parrot.Views
{
    public partial class CategoriesView : UserControl
    {
        public CategoriesView()
        {
            this.DataContext = new CategoriesViewModel(BootStrapper.Resolve<ICategoriesModel>());
            InitializeComponent();
        }
    }
}
