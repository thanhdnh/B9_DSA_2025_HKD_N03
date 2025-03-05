public class Vertex{
    public bool wasVisited;
    public string label;
    public object data;
    public Vertex(string label, object data){
        this.label = label;
        wasVisited = false;
        this.data = data;
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
    public void AddVertex(string label, object data){
        vertices[numVerts] = new Vertex(label, data);
        numVerts++;
    }
    public void AddEdge(int start, int eend){
        adjMatrix[start, eend] = 1;
        adjMatrix[eend, start] = 1;
    }
    private int SeqSearch(string label){
        for(int i=0; i<vertices.Length; i++)
            if(vertices[i].label == label)
                return i;
        return -1;
    }
    public void AddEdge(string lstart, string lend){
        int start = SeqSearch(lstart);
        int end = SeqSearch(lend);
        AddEdge(start, end);
    }
    public void ShowVertex(int v){
        Console.Write(vertices[v].label + " ");
    }
    public Vertex FindMin(){
        Vertex min = vertices[0];
        vertices[0].wasVisited = true;
        Stack<int> gStack = new Stack<int>();
        gStack.Push(0); int v;
        while (gStack.Count > 0){
            v = GetAdjUnvisitedVertex(gStack.Peek());
            if (v == -1)
                gStack.Pop();
            else{
                vertices[v].wasVisited = true;
                if((int)(min.data)>(int)(vertices[v].data)){
                    min = vertices[v];
                }
                gStack.Push(v);
            }
        }
        for (int j = 0; j <= NUM_VERTICES - 1; j++)
            vertices[j].wasVisited = false;
        return min;
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
        graph.AddVertex("A", 5); graph.AddVertex("B", 5);//0 1
        graph.AddVertex("C", 7); graph.AddVertex("D", 11);//2 3
        graph.AddVertex("E", 9); graph.AddVertex("F", 10);//4 5
        graph.AddVertex("G", 2); graph.AddVertex("H", 5);//6 7
        graph.AddVertex("I", 4); graph.AddVertex("J", 9);//8 9
        graph.AddVertex("K", 1); graph.AddVertex("L", 22);//10  11
        graph.AddVertex("M", 6);//12
        graph.AddEdge("A", "B");
        //graph.AddEdge(0, 1);
        graph.AddEdge("A", "E");
        //graph.AddEdge(0, 4);
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

        Vertex min = graph.FindMin();
        Console.WriteLine("\nMin: {0} -> {1}", 
                                min.label, min.data);
    }
}