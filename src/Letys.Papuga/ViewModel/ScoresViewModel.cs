using GalaSoft.MvvmLight;
using Letys.Parrot.Bootstrap;
using Letys.Parrot.Core;
using Letys.Parrot.Model;
using System;
using System.Collections.ObjectModel;

namespace Letys.Parrot.ViewModel
{
    public class ScoresViewModel : ViewModelBase
    {
        private ObservableCollection<Score> scores;
        private IScoreModel model;

        public ScoresViewModel()
        {
            try
            {
                this.model = BootStrapper.Resolve<IScoreModel>();
                this.scores = this.model.GetAll();
            }
            catch (Exception ex)
            {
                ApplicationErrorHandler.HandleException(ex);
            }
        }

        #region Properties

        public ObservableCollection<Score> Scores
        {
            get
            {
                return this.scores;
            }
        }

        #endregion
    }
}
