
using System;

namespace Letys.Parrot.Core
{
    public class TestItem : ICloneable
    {
        public string Question { get; set; }
        public string Answer { get; set; }
        public string Example { get; set; }

        public object Clone()
        {
            return (TestItem)this.MemberwiseClone();
        }
    }
}
