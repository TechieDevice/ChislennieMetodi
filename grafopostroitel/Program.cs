using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace grafopostroitel
{
    class graf
    {
        public node[] nodes;

        public graf(int n)
        {
           nodes = new node[n];
        }

        public void getNames(char[] charLine)
        {
            for (int i = 0; i < nodes.Length; i++)
            {
                nodes[i] = new node(charLine[i]);
                nodes[i].links.Add(new link(nodes[i], nodes[i], 0));
            }
        }
    }

    public class node
    {
        public char name;
        public List<link> links = new List<link>();
        public int linksNumber = 0;
        public node(char Name)
        {
            name = Name;
        }
    }

    public class link
    {
        public node This;
        public int length;
        public node Another;

        public link(node a, node b, int l)
        {
            This = a;
            Another = b;
            length = l;
        }
    }


    //public static class IEnumerableExtensions 
    //{
    //    public static bool EqualTo<T>(this ICollection<T> source,
    //                                            ICollection<T> other)
    //    {
    //        if (source.Count != other.Count)
    //        {
    //            return false;
    //        }
    //        var s = source.GroupBy(x => x).ToDictionary(x => x.Key, x => x.Count());
    //        var o = other.GroupBy(x => x).ToDictionary(x => x.Key, x => x.Count());
    //        int count;
    //        return s.Count == o.Count && s.All(x => o.TryGetValue(x.Key, out count) && count == x.Value);
    //    }
    //}

    class Program
    {
        public static int findLink(node firstNode, node secondNode)
        {
            for (int i = 0; i < firstNode.links.Count; i++)
            {
                if (firstNode.links[i].Another == secondNode)
                {
                    return firstNode.links[i].length;
                }
            }
            return -1;
        }

        public static void jadina(node[] grafNodes, node startNode)
        {
            Console.ReadKey();
            List<node> visitedNodes = new List<node>();
            visitedNodes.Add(startNode);
            node v = startNode;
            int min;
            node minV = startNode;
            int Sum = 0;
            int kostil;

            while (visitedNodes.Count != grafNodes.Length)
            {
                min = 10000;
                for (int i = 0; i < v.links.Count; i++)
                {
                    if (min > v.links[i].length)
                    {
                        kostil = 0;
                        for (int u = 0; u < (visitedNodes.Count); u++)
                        {
                            if (v.links[i].Another != visitedNodes[u])
                            {
                                kostil++;
                            }
                        }

                        if (kostil == visitedNodes.Count)
                        {
                            min = v.links[i].length;
                            minV = v.links[i].Another;
                        }
                    }
                }
                visitedNodes.Add(minV);
                v = minV;
                Sum = Sum + min;
                //Console.ReadKey();
            }

            Sum = Sum + findLink(v, startNode);
            Console.WriteLine("минимальный путь из точки {0} по контуру всего графа равен {1}", startNode.name, Sum);
            Console.ReadKey();
        }

        public static void deikstr(node[] grafNodes, node startNode)
        {
            //Console.ReadKey();
            List<int> minLenghts = new List<int>();
            List<int> dv = new List<int>();
            List<node> visitedNodes = new List<node>();

            node v = startNode;
            int du = 0;

            for (int i = 0; i < grafNodes.Length; i++)
            {
                if (grafNodes[i].name != startNode.name)
                {
                    minLenghts.Add(2147483646);
                }
                else
                {
                    minLenghts.Add(0);
                    visitedNodes.Add(v);
                    dv.Add(minLenghts[i]);
                }
            }

            while (visitedNodes.Count != grafNodes.Length)
            {
                for (int u = 0; u < (grafNodes.Length); u++)
                {
                    for (int i = 0; i < (visitedNodes.Count); i++)
                    {
                        if (grafNodes[u] != visitedNodes[i])
                        {
                            if (findLink(visitedNodes[i], grafNodes[u]) != -1)
                            {
                                if(minLenghts[u] > dv[i] + findLink(visitedNodes[i], grafNodes[u]))
                                {
                                    minLenghts[u] = dv[i] + findLink(visitedNodes[i], grafNodes[u]);

                                    v = grafNodes[u];
                                    du = u;
                                    //Console.ReadKey();
                                }
                            }
                        }
                    }
                }

                visitedNodes.Add(v);
                dv.Add(minLenghts[du]);
                //Console.ReadKey();

            }

            for (int i = 0; i < grafNodes.Length; i++)
            {
                Console.WriteLine("расстояние от узла {0} до узла {1} равно {2}", startNode.name, grafNodes[i].name, minLenghts[i]);

            }
            Console.ReadKey();
        }   

        static void Main(string[] args)
        {
            int lineN = 0;
            string line;

            StreamReader file = new StreamReader(@"e:\grafInfo.txt");

            line = file.ReadLine();
            int n = Convert.ToInt32(line);
            graf g = new graf(n);

            char[] charLine;
            line = file.ReadLine();
            charLine = line.ToCharArray();
            g.getNames(charLine);

            
            while ((line = file.ReadLine()) != null)
            {
                charLine = line.ToCharArray();
                line = file.ReadLine();
                n = Convert.ToInt32(line);
                for (int i = 0; i < g.nodes.Length; i++)
                {
                    if (g.nodes[i].name == charLine[0])
                    {
                        for (int j = 0; j < g.nodes.Length; j++)
                        {
                            if (g.nodes[j].name == charLine[1])
                            {
                                g.nodes[i].links.Add(new link(g.nodes[i], g.nodes[j], n));
                                g.nodes[i].linksNumber++;
                                g.nodes[j].links.Add(new link(g.nodes[j], g.nodes[i], n));
                                g.nodes[j].linksNumber++;
                            }
                        }
                    }
                }
                lineN++;
            }

            string name;
            Console.WriteLine("введите имя узла для посчета расстояния");
            name = Console.ReadLine();
            name.ToCharArray();
            for (int i = 0; i < g.nodes.Length; i++)
            {
                if (g.nodes[i].name == name[0])
                {
                    deikstr(g.nodes, g.nodes[i]);
                    //jadina(g.nodes, g.nodes[i]);
                }
            }
            


            //for (int i = 0; i < g.nodes[0].linksNumber; i++)
            //{
            //    Console.WriteLine("{0} - {1} - {2}", g.nodes[0].links[i].This.name, g.nodes[0].links[i].length, g.nodes[0].links[i].Another.name);
            //}

            file.Close();

            Console.ReadKey();
        }
    }
}
