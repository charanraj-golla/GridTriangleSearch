using GridSearch.Domain;

namespace GridSearch.Logic
{
    public interface IGridOperation
    {
        TriangleCoordinate GetTriangleCoordinate(Grid grid, string referenceName);
        string GetTriangleName(Grid grid, TriangleCoordinate triangleCoordinate);
    }
}