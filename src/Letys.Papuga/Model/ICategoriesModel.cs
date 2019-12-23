using Letys.Parrot.Core;
using System.Collections.ObjectModel;

namespace Letys.Parrot.Model
{
    public interface ICategoriesModel
    {
        void Add(Category category);
        void Update(Category category);
        ObservableCollection<Category> GetAll();
        void Delete(Category category);
    }
}
