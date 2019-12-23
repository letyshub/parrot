using Letys.Parrot.Core;
using System.Collections.ObjectModel;

namespace Letys.Parrot.Model
{
    public interface IScoreModel
    {
        ObservableCollection<Score> GetAll();
        void SaveScore(Score score);
    }
}
