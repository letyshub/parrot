using System;
using System.Collections.Generic;

namespace Letys.Parrot.Core
{
    public class TestSet : BaseObject, ICloneable
    {
        public TestSet()
        {
            this.Items = new List<TestItem>();
            this.Created = DateTime.Now;
        }

        public string Name { get; set; }
        public string Description { get; set; }
        public decimal? LastResult { get; set; }
        public DateTime? LastResultDate { get; set; }
        public DateTime Created { get; set; }
        public TestLanguage Language { get; set; }
        public Category Category { get; set; }
        public int MaxDaysWithoutRepeats { get; set; }
        public IList<TestItem> Items { get; set; }

        public string LastResultText
        {
            get
            {
                if (this.LastResult != null)
                {
                    return string.Format("{0:0.00}%   {1:dd-MM-yyyy HH:mm}", this.LastResult * 100.00M, this.LastResultDate.Value);
                }

                return string.Empty;
            }
        }

        public int QuestionsQuantity
        {
            get
            {
                return this.Items != null ? this.Items.Count : 0;
            }
        }

        public object Clone()
        {
            TestSet test = (TestSet)this.MemberwiseClone();

            return test;
        }
    }
}
