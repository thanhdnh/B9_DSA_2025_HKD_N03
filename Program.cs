public class Vertex{
    public bool wasVisited;
    public string label;
    public Vertex(string label){
        this.label = label;
        wasVisited = false;
    }
}
public class Graph{
    int NUM_VERTICES; Vertex[] vertices;
    int[,] adjMatrix; int numVerts;
    public Graph(int number_of_vertex){
        NUM_VERTICES = number_of_vertex;
        vertices = new Vertex[NUM_VERTICES];
        adjMatrix = new int[NUM_VERTICES, NUM_VERTICES];
        numVerts = 0;
        for (int j = 0; j < NUM_VERTICES; j++)
            for (int k = 0; k < NUM_VERTICES; k++)
                adjMatrix[j, k] = 0;
    }
    public void AddVertex(string label){
        vertices[numVerts] = new Vertex(label);
        numVerts++;
    }
    public void AddEdge(int start, int eend){
        adjMatrix[start, eend] = 1;
        adjMatrix[eend, start] = 1;
    }
    public void ShowVertex(int v){
        Console.Write(vertices[v].label + " ");
    }
    private int GetAdjUnvisitedVertex(int v){
        for (int j = 0; j <= NUM_VERTICES - 1; j++)
            if ((adjMatrix[v, j] == 1) &&
                    (vertices[j].wasVisited == false))
                return j;
        return -1;
    }
    public void DepthFirstSearch(){
        vertices[0].wasVisited = true;
        ShowVertex(0);
        Stack<int> gStack = new Stack<int>();
        gStack.Push(0);
        int v;

        while (gStack.Count > 0){
            v = GetAdjUnvisitedVertex(gStack.Peek());
            if (v == -1)
                gStack.Pop();
            else{
                vertices[v].wasVisited = true;
                ShowVertex(v);
                gStack.Push(v);
            }
        }
        for (int j = 0; j <= NUM_VERTICES - 1; j++)
            vertices[j].wasVisited = false;
    }
    public void BreadthFirstSearch()
    {
        Queue<int> gQueue = new Queue<int>();
        vertices[0].wasVisited = true;
        ShowVertex(0);
        gQueue.Enqueue(0);
        int vert1, vert2;

        while (gQueue.Count > 0){
            vert1 = gQueue.Dequeue();
            vert2 = GetAdjUnvisitedVertex(vert1);

            while (vert2 != -1)
            {
                vertices[vert2].wasVisited = true;
                ShowVertex(vert2);
                gQueue.Enqueue(vert2);
                vert2 = GetAdjUnvisitedVertex(vert1);
            }
        }
        for (int i = 0; i <= NUM_VERTICES - 1; i++)
            vertices[i].wasVisited = false;
    }
}

public class Program{
    public static void Main(string[] args){
        Console.Clear();

        Graph graph = new Graph(13);
        graph.AddVertex("A"); graph.AddVertex("B");//0 1
        graph.AddVertex("C"); graph.AddVertex("D");//2 3
        graph.AddVertex("E"); graph.AddVertex("F");//4 5
        graph.AddVertex("G"); graph.AddVertex("H");//6 7
        graph.AddVertex("I"); graph.AddVertex("J");//8 9
        graph.AddVertex("K"); graph.AddVertex("L");//10  11
        graph.AddVertex("M");//12
        graph.AddEdge(0, 1);
        graph.AddEdge(0, 4);
        graph.AddEdge(0, 7);
        graph.AddEdge(0, 10);
        graph.AddEdge(1, 2);
        graph.AddEdge(2, 3);
        graph.AddEdge(4, 5);
        graph.AddEdge(5, 6);
        graph.AddEdge(7, 8);
        graph.AddEdge(8, 9);
        graph.AddEdge(10, 11);
        graph.AddEdge(11, 12);
        Console.Write("DFS: ");
        graph.DepthFirstSearch();
        Console.Write("\nBFS: ");
        graph.BreadthFirstSearch();
    }
}