using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace demotestq1
{
    class Graph
    {
        public int V; // No. of vertices
        public List<int>[] adj; // No. of vertices

        static int level;

        // Constructor
        public Graph(int V)
        {
            this.V = V;
            this.adj = new List<int>[2 * V];

            for (int i = 0; i < 2 * V; i++)
                this.adj[i] = new List<int>();
        }

    }
}
