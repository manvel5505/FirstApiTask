using FirstApiTask.Domain.interfaces;
using FirstApiTask.Infrastructure.Services;
using Moq;

namespace FirstApiTask_Test.Test
{
    public class CombinationGenerationTest
    {
        private readonly ICombinationGenerator generator;
        public CombinationGenerationTest()
        {
            generator = new CombinationGeneration();
        }
        [Fact]
        public void GenerateCombinations_NullItems_ThrowsArgumentNullException()
        {
            List<int> items = null;
            int length = 1;

            var exception = Assert.Throws<ArgumentNullException>(() => generator.GenerateCombinations(items, length));
            Assert.Equal("items cant be null!", exception.ParamName);
        }
        [Fact]
        public void GenerateCombinations_InvalidLength_ArgumentException()
        {

            List<int> items = new List<int>() { 1, 2, 1 };
            int length = 4;

            var exception = Assert.Throws<ArgumentException>(() => generator.GenerateCombinations(items, length));
            Assert.Equal("length does not much with items!", exception.Message);
        }
        [Fact]
        public void GenerateCombinations_NegativeLength_ArgumentException()
        {

            List<int> items = new List<int>() { 1, 2, 1 };
            int length = -5;

            var exception = Assert.Throws<ArgumentException>(() => generator.GenerateCombinations(items, length));
            Assert.Equal("length does not much with items!", exception.Message);
        }
        [Fact]
        public void GenerateCombinations_InvalidValue_ArgumentException()
        {

            List<int> items = new List<int>() { 1, 4 };
            int length = 1;

            var exception = Assert.Throws<ArgumentException>(() => generator.GenerateCombinations(items, length));
            Assert.Equal("items supposed to be between 1 to 3!", exception.Message);
        }
        [Fact]
        public void GenerateCombinations_Duplicates_UniqueLetters()
        {

            List<int> items = new List<int>() { 1, 1, 2 };
            int length = 2;
            var right = new List<List<string>>()
            {
                new List<string> { "A1", "B1" },
                new List<string> { "A2", "B1" }
            };

            var result = generator.GenerateCombinations(items, length);

            Assert.Equal(right.Count, result.Count);
            foreach (var item in right)
            {
                Assert.Contains(item, result);
            }
        }
    }
}