using Letys.Parrot.Core;
using System.Collections.ObjectModel;

namespace Letys.Parrot.Model
{
    public interface ITestSetsModel
    {
        void Add(TestSet test);
        void Update(TestSet test);
        void Delete(TestSet test);
        ObservableCollection<TestSet> GetAll();
        ObservableCollection<TestLanguage> GetAllLanguages();
    }
}
