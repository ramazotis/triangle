namespace Triangle.PathFinder.InputProvider
{
    public interface IInputReader
    {
        string[] ReadInput();
        int[,] PrepareInputMatrix();
        void SetFilePath(string filePath);
    }
}