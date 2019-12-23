using Letys.Parrot.Core;
using System.Collections.ObjectModel;

namespace Letys.Parrot.Model
{
    public class CategoriesModel : ICategoriesModel
    {
        private readonly IApplicationRepository context;

        public CategoriesModel(IApplicationRepository context)
        {
            this.context = context;
        }

        public void Add(Category category)
        {
            this.context.Insert(category);
        }

        public void Update(Category category)
        {
            this.context.Update(category);
        }

        public ObservableCollection<Category> GetAll()
        {
            ObservableCollection<Category> result = new ObservableCollection<Category>();

            foreach (Category category in this.context.GetAll<Category>())
            {
                result.Add(category);
            }

            return result;
        }

        public void Delete(Category category)
        {
            this.context.Delete<Category>(category.Id);
        }
    }
}
