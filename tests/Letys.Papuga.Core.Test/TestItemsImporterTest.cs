using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using Xunit;

namespace Letys.Parrot.Core.Test
{
    public class TestItemsImporterTest
    {
        [Fact]
        public void ReadTestItems_GetsCorrectFile_ReturnsCorrectItems()
        {
            var actual = TestItemsImporter.ReadTestItems(CreateFilePath("import_test.csv"));
            var expected = new List<TestItem>
            {
                new TestItem { Answer = "answer_1", Question = "question_1", Example = "example_1" },
                new TestItem { Answer = "answer_2", Question = "question_2", Example = "example_2" },
                new TestItem { Answer = "answer_3", Question = "question_3", Example = string.Empty }
            };

            Assert.Equal(3, actual.Count);

            for (var i = 0; i < expected.Count; i++)
            {
                Assert.Equal(expected[i].Answer, actual[i].Answer);
                Assert.Equal(expected[i].Question, actual[i].Question);
                Assert.Equal(expected[i].Example, actual[i].Example);
            }
        }

        [Fact]
        public void ReadTestItems_GetsInCorrectFile_ReturnsCorrectItems()
        {
            var actual = TestItemsImporter.ReadTestItems(CreateFilePath("import_test_wrong.csv"));
            Assert.Empty(actual);
        }

            private string CreateFilePath(string filename)
        {
            var codeBaseUrl = new Uri(Assembly.GetExecutingAssembly().CodeBase);
            var codeBasePath = Uri.UnescapeDataString(codeBaseUrl.AbsolutePath);
            var dirPath = Path.GetDirectoryName(codeBasePath);
            return Path.Combine(dirPath, filename);
        }
    }
}
