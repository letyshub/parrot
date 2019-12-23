using System.Collections.Generic;
using System.IO;

namespace Letys.Parrot.Core
{
    public static class TestItemsImporter
    {
        public static IList<TestItem> ReadTestItems(string filename)
        {
            try
            {
                return ReadTestItemsFromFile(filename);
            }
            catch
            {
                return new List<TestItem>();
            }
        }

        private static IList<TestItem> ReadTestItemsFromFile(string filename)
        {
            var items = new List<TestItem>();

            using (var reader = new StreamReader(filename))
            {
                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    var values = line.Split(';');

                    if (values == null || values.Length > 3 || values.Length < 2)
                    {
                        continue;
                    }

                    var item = new TestItem
                    {
                        Question = values[0],
                        Answer = values[1],
                        Example = values.Length == 3 ? values[2] : string.Empty
                    };
                    items.Add(item);
                }
            }

            return items;
        }
    }
}
