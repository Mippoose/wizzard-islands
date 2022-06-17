HashSet<Tuple<int, int>> visited = new HashSet<Tuple<int, int>>();
List<Island> islands = new List<Island>();

/* int[][] matrix = {
    new[] { 3, 1, 1, 7, 7, 9, 3, 1 },
    new[] { 4, 1, 1, 7, 8, 6, 1, 9 },
    new[] { 1, 7, 3, 5, 6, 1, 8, 9 },
    new[] { 1, 4, 7, 5, 1, 5, 6, 8 },
    new[] { 1, 8, 8, 1, 6, 6, 3, 4 },
    new[] { 1, 2, 7, 7, 2, 2, 2, 3 },
    new[] { 2, 6, 5, 3, 3, 0, 2, 7 },
    new[] { 1, 2, 3, 4, 4, 4, 8, 9 },
}; */
// Is matrix quadratic btw? :D
Console.WriteLine("Enter the number of rows and columns: ");
int rows = int.Parse(Console.ReadLine());
int columns = rows;

int[][] matrix = new int[rows][];
var rand = new Random();
Console.WriteLine("Matrix: ");
for (int i = 0; i < rows; i++)
{
    matrix[i] = new int[columns];
    for (int j = 0; j < columns; j++)
    {
        matrix[i][j] = rand.Next(0, 10);
        Console.Write(matrix[i][j] + " ");
    }
    Console.WriteLine();
}

Console.ReadLine();

// matrix should have more than 6 and less than 13 rows/columns
if (matrix.Length <= 6 || matrix.Length >= 13) return;
if (matrix[0].Length <= 6 || matrix[0].Length >= 13) return;

for (int i = 0; i < matrix.Length; i++)
{
    for (int j = 0; j < matrix[i].Length; j++)
    {
        if (visited.Contains(new Tuple<int, int>(i, j)) == false && matrix[i][j] != 0)
            CheckMatrix(i, j, matrix[i][j]);
    }
}

List<Island> result = islands.Where(x => x.GetSize() >= 4).ToList();
foreach (var island in result) Console.WriteLine(island.GetReport());

void CheckMatrix(int row, int column, int former, Island? island = null)
{
    // do we need this cell
    if (row < 0 || row >= matrix.Length || column < 0 || column >= matrix[row].Length) return;
    if (visited.Contains(new Tuple<int, int>(row, column))) return;
    int cellValue = matrix[row][column];
    if (cellValue != former && cellValue != 0) return;

    // is island already created
    if (island == null)
    {
        island = new Island(former, matrix.Length);
        islands.Add(island);
    }

    // adding cell to the island
    var added = island.AddCell(row, column);
    if (added == false) return;
    if (cellValue != 0) visited.Add(new Tuple<int, int>(row, column));

    // check neighbors
    CheckMatrix(row - 1, column, former, island); // top
    CheckMatrix(row + 1, column, former, island); // bottom
    CheckMatrix(row, column - 1, former, island); // left
    CheckMatrix(row, column + 1, former, island); // right
}
class Island
{
    public int FormerNumber { get; set; }


    private HashSet<Tuple<int, int>> Cells { get; set; } = new HashSet<Tuple<int, int>>();

    private readonly int _matrixSideLength;

    public Island(int former, int length)
    {
        FormerNumber = former;
        _matrixSideLength = length;
    }

    public bool AddCell(int x, int y)
    {
        return Cells.Add(new Tuple<int, int>(x, y));
    }

    public int GetSize() => Cells.Count;

    public string GetReport()
    {
        return
            $"Island {FormerNumber} has {Cells.Count} cells. Locations: {string.Join(", ", Cells.Select(c => $"{c.Item1 * _matrixSideLength + c.Item2}"))}";
    }
}