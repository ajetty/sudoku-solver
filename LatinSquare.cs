using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LatinSquare
{
    class Vertex
    {
        public int index { get; set; } //index within latin square, 0 to 80
        public List<int> AdjacencyList { get; set; } //not including itself
        public int color { get; set; } //white=0, red=1, blue=2, green=3, violet=4, yellow=5, pink=6, black=7, orange=8, brown=9 
    }

    class Graph
    {
        Dictionary<int, Vertex> dictionary = new Dictionary<int, Vertex>();

        public Graph() => setDictionary();

        private void setDictionary()
        {
            int name = 1;
            int index;
            int[] offset = { 0, 1, 2, 9, 10, 11, 18, 19, 20 };

            for (int rows = 0; rows < 9; rows++)
            {

                for (int cols = 0; cols < 9; cols++)
                {
                    List<int> edges = new List<int>();
                    
                    for(int x = 0; x < 9; x++)
                    {
                        if(cols != x)
                        {
                            index = (rows * 9) + x;
                            edges.Add(index + 1);
                        }

                    }


                    for (int y = 0; y < 9; y++)
                    {
                        if (rows != y)
                        {
                            edges.Add(((y * 9) + cols) + 1);
                        }

                    }

                    Vertex vertex = new Vertex();
                    vertex.index = name-1;
                    vertex.color = 0;
                    vertex.AdjacencyList = edges;

                    dictionary.Add(name, vertex);
                    name++;
                }

            }

            (int row, int col) position = (0,0);
            int corner = 0;

            for(int x = 1; x <= dictionary.Count; x++)
            {
                index = dictionary[x].index;
                position = (row: index/9, col: index%9);
                position = (row: (position.row / 3) * 3, col: (position.col / 3) * 3);

                corner = position.row * 9 + position.col + 1;

                foreach (int element in offset)
                {
                    if (dictionary[x].AdjacencyList.Contains(element + corner) == false && (element + corner) != x)
                    {
                        dictionary[x].AdjacencyList.Add(element + corner);
                    }
                }

            }

        }

        public void Print()
        {
            int count = 0;
            int[] array = new int[81];
            dictionary.Keys.CopyTo(array, 0);

            Console.WriteLine("---------------------------------");
            for(int row = 0; row < 9; row++)
            {
                for(int col = 0; col < 9; col++)
                {
                    Console.Write(array[count] + " ");
                    count++;
                }

                Console.WriteLine();
            }
            Console.WriteLine("---------------------------------");

            count = 1;

            for (int row = 0; row < 9; row++)
            {
                for (int col = 0; col < 9; col++)
                {
                    Console.Write(dictionary[count].color + " ");
                    count++;
                }

                Console.WriteLine();
            }
            Console.WriteLine("---------------------------------");
        }

        public void Update(string[] tokens)
        {
            for (int x = 0; x < tokens.Length; x += 2)
            {
                dictionary[Convert.ToInt32(tokens[x])].color = Int32.Parse(tokens[x+1]);
            }
        }

        public void DSatur()
        {
            List<Tuple<int, int>> vertexUncoloredList = MaxVertexCountList();
            int length = vertexUncoloredList.Count;
            int color = 0;

            foreach(Tuple<int,int> element in vertexUncoloredList) { Console.WriteLine("{0} {1} ", element.Item1, element.Item2); }

            for (int x = 0; x < vertexUncoloredList.Count; x++)
            {
                color = AssignColor(vertexUncoloredList[x].Item1);

                if (color == -1)
                {
                    Console.WriteLine("Vertex: {0} {1} Color: {2}", vertexUncoloredList[x].Item1, vertexUncoloredList[x].Item2, color);
                    break;
                }
                else
                {
                    dictionary[vertexUncoloredList[x].Item1].color = color;
                }

            }

            Print();
        }

        public int AssignColor(int vertex)
        {
            int assignedColor = 0;
            //String vertex = MaxVertexCount();

            assignedColor = SelectColor(vertex);

            //Console.WriteLine("Vertex: {0} Assigned Color: {1}", vertex, assignedColor);
            //foreach (int element in dictionary[vertex].AdjacencyList) { Console.Write(element + " "); };
            return assignedColor;
        }

        private int SelectColor(int vertexName) //insertion sort for vertex's color
        {
            int length = dictionary[vertexName].AdjacencyList.Count;
            int[] colorArray = new int[length];
            int count = 0;
            int color = -1;

            foreach(int element in dictionary[vertexName].AdjacencyList)
            {
                colorArray[count] = dictionary[element].color;
                count++;
            }

            Array.Sort(colorArray);

            for(int x = 0; x < colorArray.Length - 1; x++)
            {
                if(colorArray[x+1] - colorArray[x] > 1)
                {
                    color = colorArray[x] + 1;
                    break;
                }
            }

            return color;

        }


        private int MaxVertexCount()
        {
            int highest = -1;
            int colorCount = 0;
            int highestCount = 0;

            foreach(KeyValuePair<int, Vertex> kvp in dictionary)
            {
                if (kvp.Value.color == 0)
                {
                    foreach (int vertex in kvp.Value.AdjacencyList)
                    {

                        if (dictionary[vertex].color > 0)
                        {
                            colorCount++;
                        }
                    }

                    if (colorCount > highestCount)
                    {
                        highest = kvp.Key;
                        highestCount = colorCount;
                    }

                    colorCount = 0;
                }
            }
            //Console.WriteLine("Highest: {0}", highest);
            return highest;
        }

        private List<Tuple<int,int>> MaxVertexCountList()
        {
            string highest = String.Empty;
            int colorCount = 0;
            var vertexCountList = new List<Tuple<int, int>>();

            foreach(KeyValuePair<int, Vertex> kvp in dictionary)
            {
                if (kvp.Value.color == 0)
                {
                    foreach (int vertex in kvp.Value.AdjacencyList)
                    {

                        if (dictionary[vertex].color > 0)
                        {
                            colorCount++;
                        }
                    }

                    vertexCountList.Add(new Tuple<int, int>(kvp.Key, colorCount));

                    colorCount = 0;
                }
            }

            var sortedVertexCount = vertexCountList.OrderByDescending(t => t.Item2).ToList();

            return sortedVertexCount;
        }
    }
}
