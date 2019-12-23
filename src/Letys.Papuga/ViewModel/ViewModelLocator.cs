using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Ioc;
using Microsoft.Practices.ServiceLocation;

namespace Letys.Parrot.ViewModel
{
    public class ViewModelLocator
    {
        public ViewModelLocator()
        {
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);
            SimpleIoc.Default.Register<MainViewModel>();
            SimpleIoc.Default.Register<TestSetsViewModel>();
        }

        public MainViewModel Main
        {
            get
            {
                return ServiceLocator.Current.GetInstance<MainViewModel>();
            }
        }

        public TestSetsViewModel Tests
        {
          get
          {
            return ServiceLocator.Current.GetInstance<TestSetsViewModel>();
          }
        }

        public static void Cleanup()
        {
            // TODO Clear the ViewModels
        }
    }
}