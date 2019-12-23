using System.Collections.Generic;

namespace Letys.Parrot.Core
{
    public class Category : BaseObject
    {
        public string Name { get; set; }
        public string Description { get; set; }

        public override string ToString()
        {
            return this.Name;
        }
    }
}
