using Letys.Parrot.Core;
using System;

namespace Letys.Parrot.Core
{
    public class Score : BaseObject
    {
        public decimal ScoreValue { get; set; }
        public string ScoreText
        {
            get
            {
                return string.Format("{0:0.00} %", this.ScoreValue * 100);
            }
        }
        public DateTime ScoreDate { get; set; }
        public int TestHeaderId { get; set; }
        public string TestHeaderName { get; set; }
        public string TestHeaderCategoryName { get; set; }
        public TestLanguage TestHeaderLanguage { get; set; }
    }
}
