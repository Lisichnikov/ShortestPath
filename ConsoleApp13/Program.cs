class Program
{
    static void Main()
    {
        // Создание графа в виде списка смежности
        Dictionary<int, List<Tuple<int, int>>> graph = new Dictionary<int, List<Tuple<int, int>>>()
        {
            {0, new List<Tuple<int, int>> {Tuple.Create(1, 1), Tuple.Create(2, 2),Tuple.Create(3, 3)} },
            {1, new List<Tuple<int, int>> {Tuple.Create(5, 2), Tuple.Create(7, 2)}},
            {2, new List<Tuple<int, int>> {Tuple.Create(4, 2), Tuple.Create(6, 2)}},
            {3, new List<Tuple<int, int>> {Tuple.Create(5, 2)}},
            {4, new List<Tuple<int, int>> {Tuple.Create(8, 2)}},
            {5, new List<Tuple<int, int>> {Tuple.Create(7, 2), Tuple.Create(9, 2)}},
            {6, new List<Tuple<int, int>> {Tuple.Create(8, 2)}},
            {7, new List<Tuple<int, int>> {Tuple.Create(10, 4)}},
            {8, new List<Tuple<int, int>> {Tuple.Create(10, 5)}},
            {9, new List<Tuple<int, int>> {Tuple.Create(10, 6)}},
            {10, new List<Tuple<int, int>> {}}
        };

        int startNode = 0;

        // Вызов функции для нахождения кратчайшего пути
        Dictionary<int, int> shortestPaths = Dijkstra(graph, startNode);

        // Вывод результатов
        foreach (var kvp in shortestPaths)
        {
            Console.WriteLine($"Кратчайший путь из точки {startNode} до точки {kvp.Key}: {kvp.Value}");
        }
    }

    static Dictionary<int, int> Dijkstra(Dictionary<int, List<Tuple<int, int>>> graph, int start)
    {
        var shortestPaths = new Dictionary<int, int>();
        var queue = new PriorityQueue<int>();

        foreach (var node in graph.Keys)
        {
            shortestPaths[node] = int.MaxValue;
        }

        shortestPaths[start] = 0;
        queue.Enqueue(start, 0);

        while (queue.Count > 0)
        {
            var (node, distance) = queue.Dequeue();

            if (shortestPaths[node] < distance)
            {
                continue;
            }

            foreach (var neighbor in graph[node])
            {
                var alt = distance + neighbor.Item2;
                if (alt < shortestPaths[neighbor.Item1])
                {
                    shortestPaths[neighbor.Item1] = alt;
                    queue.Enqueue(neighbor.Item1, alt);
                }
            }
        }

        return shortestPaths;
    }
}
// Простая реализация очереди с приоритетами для алгоритма Дейкстры
public class PriorityQueue<T>
{
    private List<Tuple<int, T>> elements = new List<Tuple<int, T>>();

    public int Count { get { return elements.Count; } }

    public void Enqueue(T item, int priority)
    {
        elements.Add(Tuple.Create(priority, item));
    }
    public Tuple<T, int> Dequeue()
    {
        elements.Sort((a, b) => a.Item1 - b.Item1);
        var element = elements[0];
        elements.RemoveAt(0);
        return Tuple.Create(element.Item2, element.Item1);
    }
}