using System;
using System.Collections.Generic;
using System.Text;

namespace LatinSquare
{
    class Vertex
    {
        public int name { get; set; }
        public List<int> AdjacencyList { get; set; }
        public int color { get; set; } //white=0, red=1, blue=2, green=3, violet=4, yellow=5, pink=6, black=7, orange=8, brown=9 
    }

    class Graph
    {
        Dictionary<String, Vertex> dictionary = new Dictionary<String, Vertex>();

        public Graph() => setDictionary();

        public void setDictionary()
        {
            int[,] square = new int[9,9];
            int name = 1;
            int index;

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
                    vertex.name = name;
                    vertex.color = 0;
                    vertex.AdjacencyList = edges;

                    dictionary.Add("V" + name.ToString(), vertex);
                    name++;
                }

            }

        }
    }
}
