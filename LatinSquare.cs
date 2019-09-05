using System;
using System.Collections.Generic;
using System.Text;

namespace LatinSquare
{
    class Vertex
    {
        public int index { get; set; } //index within latin square, 0 to 80
        public List<int> AdjacencyList { get; set; }
        public int color { get; set; } //white=0, red=1, blue=2, green=3, violet=4, yellow=5, pink=6, black=7, orange=8, brown=9 
    }

    class Graph
    {
        Dictionary<String, Vertex> dictionary = new Dictionary<String, Vertex>();

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

                    dictionary.Add(name.ToString(), vertex);
                    name++;
                }

            }

            (int row, int col) position = (0,0);
            int corner = 0;

            for(int x = 1; x <= dictionary.Count; x++)
            {
                index = dictionary[x.ToString()].index;
                position = (row: index/9, col: index%9);
                position = (row: (position.row / 3) * 3, col: (position.col / 3) * 3);

                corner = position.row * 9 + position.col + 1;

                foreach (int element in offset)
                {
                    if (dictionary[x.ToString()].AdjacencyList.Contains(element + corner) == false && (element + corner) != x)
                    {
                        dictionary[x.ToString()].AdjacencyList.Add(element + corner);
                    }
                }

            }

        }

        public void Print()
        {
            int count = 0;
            String[] array = new String[81];
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
                    Console.Write(dictionary[count.ToString()].color + " ");
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
                dictionary[tokens[x]].color = Int32.Parse(tokens[x+1]);
            }
        }

        private void DSatur()
        {

        }

        private String MaxVertexCount()
        {
            string highest = String.Empty;
            int colorCount = 0;
            int highestCount = 0;

            foreach(KeyValuePair<string, Vertex> kvp in dictionary)
            {
                foreach(int vertex in kvp.Value.AdjacencyList)
                {
                    
                    if(dictionary[vertex.ToString()].color > 0)
                    {
                        colorCount++;
                    }
                }

                if(colorCount > highestCount)
                {
                    highest = kvp.Key;
                    highestCount = colorCount;
                }

                //Console.WriteLine("Key: {0} Count: {1} Highest: {2}", kvp.Key, colorCount, highestCount);
                colorCount = 0;
            }

            return highest;
        }
    }
}
