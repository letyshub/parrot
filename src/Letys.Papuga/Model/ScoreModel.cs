using Letys.Parrot.Core;
using System.Collections.ObjectModel;

namespace Letys.Parrot.Model
{
    public class ScoreModel : IScoreModel
    {
        private readonly IApplicationRepository repository;

        public ScoreModel(IApplicationRepository repository)
        {
            this.repository = repository;
        }

        public ObservableCollection<Score> GetAll()
        {
            ObservableCollection<Score> scores = new ObservableCollection<Score>();

            foreach (Score score in this.repository.GetAll<Score>())
            {
                scores.Add(score);
            }

            return scores;
        }

        public void SaveScore(Score score)
        {
            this.repository.Insert(score);
        }
    }
}
