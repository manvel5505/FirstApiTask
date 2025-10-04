namespace FirstApiTask.Domain.interfaces
{
    public interface ICombinationGenerator
    {
        List<List<string>> GenerateCombinations(List<int> items, int length);
    }
}
