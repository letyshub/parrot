using Letys.Parrot.Core;
using System.Collections.ObjectModel;

namespace Letys.Parrot.Model
{
    public class TestSetsModel : ITestSetsModel
    {
        private readonly IApplicationRepository repository;

        public TestSetsModel(IApplicationRepository repository)
        {
            this.repository = repository;
        }

        public void Add(TestSet test)
        {
            this.repository.Insert(test);
        }

        public void Update(TestSet test)
        {
            this.repository.Update(test);
        }

        public void Delete(TestSet test)
        {
            this.repository.Delete<TestSet>(test.Id);
        }

        public ObservableCollection<TestSet> GetAll()
        {
            ObservableCollection<TestSet> tests = new ObservableCollection<TestSet>();
            foreach (TestSet test in this.repository.GetAll<TestSet>())
            {
                tests.Add(test);
            }

            return tests;
        }

        public ObservableCollection<TestLanguage> GetAllLanguages()
        {
            ObservableCollection<TestLanguage> langugaes = new ObservableCollection<TestLanguage>();
            langugaes.Add(TestLanguage.English);
            langugaes.Add(TestLanguage.German);
            langugaes.Add(TestLanguage.Russian);

            return langugaes;
        }
    }
}
