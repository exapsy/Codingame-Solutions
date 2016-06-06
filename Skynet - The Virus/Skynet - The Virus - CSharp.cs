using System;
using System.Linq;
using System.IO;
using System.Text;
using System.Collections;
using System.Collections.Generic;

/**
 * Auto-generated code below aims at helping you parse
 * the standard input according to the problem statement.
 **/

/*
* Shortest Root Algorithm (B&B (Branch N' Bound)) :
* 00: Check if Current Distance is >= BestDistance, true: Break;
* 10: Take Current Node's Children
* 20: Put them into Search Sequence
* 30: Check if there is any Gateway in the SearchSequence ->
* 40: IF ( YES ) THEN return the path of the gateway
* 50: ELSE : GOTO : 00 
*/
class Player
{

	static int DEBUG = 1;

	static void Main(string[] args)
	{

		int[] mountainsH = new int[8];
		
		for (int i = 0; i < 8; i++)
		{
			mountainsH[i] = i+1;
			Console.Error.Write("{0} ", mountainsH[i]);
		}
		Console.WriteLine(mountainsH.ToList().IndexOf(mountainsH.Max()));
		/*string[] inputs;
		inputs = Console.ReadLine().Split(' ');
		int N = int.Parse(inputs[0]); // the total number of nodes in the level, including the gateways
		int L = int.Parse(inputs[1]); // the number of links
		int E = int.Parse(inputs[2]); // the number of exit gateways
		Graph g1 = new Graph(N);

		for (int i = 0; i < L; i++)
		{
			inputs = Console.ReadLine().Split(' ');
			int N1 = int.Parse(inputs[0]); // N1 and N2 defines a link between these nodes
			int N2 = int.Parse(inputs[1]);
			g1.MakeLink(N1, N2);
		}
		for (int i = 0; i < E; i++)
		{
			int EI = int.Parse(Console.ReadLine()); // the index of a gateway node
			g1.MakeGateway(EI);
		}

		// game loop
		while (true)
		{
			int SI = int.Parse(Console.ReadLine()); // The index of the node on which the Skynet agent is positioned this turn
			g1.SI = SI;

			// Write an action using Console.WriteLine()
			// To debug: Console.Error.WriteLine("Debug messages...");

			List<int> sp = g1.ShortestPath();

			if (DEBUG >= 1)
			{
				Console.Error.WriteLine("sp Count = {0}, Contains :", sp.Count);
				foreach (int s in sp)
				{
					Console.Error.WriteLine(s);
				}
			}

			g1.DestroyLink(sp[sp.Count - 1], sp[sp.Count - 2]);
			// Example: 0 1 are the indices of the nodes you wish to sever the link between
			Console.WriteLine("{0} {1}", sp[sp.Count - 1], sp[sp.Count - 2]);
		}*/
	}
	class Graph
	{
		int[,] matrix;
		int N;
		public int SI; // The index of the node on which the Skynet agent is positioned this turn
		public Graph(int N)
		{
			this.N = N;
			matrix = new int[N, N];
			for (int i = 0; i < N; i++)
			{
				for (int j = 0; j < N; j++)
				{
					matrix[i, j] = 0;
				}
			}
			SI = 0;
		}

		/// <summary>
		/// Marks the specific Node with 2s on each link
		/// </summary>
		/// <param name="g"></param>
		public void MakeGateway(int g)
		{
			for (int i = 0; i < N; i++)
			{
				if (matrix[i, g] == 1)
				{
					matrix[i, g] = 2;
					matrix[g, i] = 2;
				}
			}
		}

		/// <summary>
		/// Links the two nodes given in the 'matrix' array
		/// </summary>
		/// <param name="g"></param>
		public void MakeLink(int a, int b)
		{
			matrix[b, a] = 1;
			matrix[a, b] = 1;
		}

		/// <summary>
		/// Removes the link of the two nodes from 'matrix' variable
		/// </summary>
		/// <param name="g"></param>
		public void DestroyLink(int a, int b)
		{
			matrix[b, a] = 0;
			matrix[a, b] = 0;
		}


		/// <summary>
		/// Returns the link that has to be cut
		/// </summary>
		/// <param name="d"></param>
		public List<int> ShortestPath()
		{
			if (DEBUG == 2)
			{
				Console.Error.WriteLine("Matrix : ");
				printList(matrix);
			}


			int bestDistance = int.MaxValue;
			List<int> closestGateway = new List<int>();
			List<List<int>> searchSeq = new List<List<int>>();
			searchSeq.AddRange(GetChilds());
			/* DEBUGGING */
			if (DEBUG >= 1)
			{
				Console.Error.WriteLine("Childs of the VIRUS :");
				printList(searchSeq);
			}
			/* END DEBUGGING*/
			while (searchSeq.Count != 0)
			{
				/* DEBUGGING */
				if (DEBUG >= 1)
				{
					Console.Error.WriteLine("New Turn, SearchSeq = ");
					printList(searchSeq);
					Console.Error.WriteLine("");
				}
				/* END DEBUGGING*/
				if (GetGateway(searchSeq) != null && GetGateway(searchSeq).Count <= bestDistance)
				{
					bestDistance = GetGateway(searchSeq).Count;
					closestGateway.Clear();
					closestGateway = GetGateway(searchSeq);
					Console.Error.WriteLine(searchSeq.Remove(closestGateway));

					/* DEBUGGING */
					if (DEBUG >= 1)
					{
						Console.Error.WriteLine("Best Distance : {0}\nList :", bestDistance);
						printList(closestGateway);
						Console.Error.WriteLine("New searchSeq = ");
						printList(searchSeq);
						Console.Error.WriteLine("\n");
					}
					/* END DEBUGGING*/

				}
				else if (GetGateway(searchSeq) != null)
				{
					List<int> tempGateway = new List<int>(GetGateway(searchSeq));
					tempGateway.RemoveAt(tempGateway.Count - 1);
					searchSeq.RemoveRange(0, GetChilds(tempGateway).Count);
					/* DEBUGGING */
					if (DEBUG >= 1)
					{
						Console.Error.WriteLine("Count of Removed Childs : {0}\nList :", GetChilds(tempGateway).Count);
						printList(GetChilds(tempGateway));
						Console.Error.WriteLine("New searchSeq = ");
						printList(searchSeq);
						Console.Error.WriteLine("\n");
					}
					/* END DEBUGGING*/
				}

				if (bestDistance == 2) return closestGateway; /* For Optimization - AFTER DEBUGGING ONLY */

				if (searchSeq.Count > 0)
				{
					if (DEBUG >= 1)
					{
						Console.Error.WriteLine("Before Inserting childs of FirstIndex, searchSeq : ");
						printList(searchSeq);
					}
					searchSeq.InsertRange(1, GetChilds(searchSeq[0]));
					searchSeq.RemoveAt(0);

					/* DEBUGGING */
					if (DEBUG >= 1 && searchSeq.Count > 0)
					{
						Console.Error.WriteLine("Count of Added Childs : {0}\nList :", GetChilds(searchSeq[0]).Count);
						printList(GetChilds(searchSeq[0]));
						Console.Error.WriteLine("New searchSeq = ");
						printList(searchSeq);
						Console.Error.WriteLine("\n");
					}
					/* END DEBUGGING*/
				}

			}

			return closestGateway;
		}

		/// <summary>
		/// Gets all the valid paths accordingly to the path given
		/// </summary>
		/// <param name="list"></param>
		/// <returns></returns>
		List<List<int>> GetChilds(List<int> list)
		{
			List<List<int>> rList = new List<List<int>>();
			if (list.Count > 0)
			{
				int lastNode = list.Last();
				for (int i = 0; i < N; i++)
				{
					if (matrix[lastNode, i] != 0 && !list.Contains(i))
					{
						List<int> child = new List<int>();
						child.AddRange(list);
						child.Add(i);
						rList.Add(child);
					}

				}
			}
			else Console.Error.WriteLine("Error in GetChilds : No Elements");

			return rList;
		}

		/// <summary>
		/// <para>Gets all the valid paths from the Starting Point of the Virus.</para>
		/// <para>Only once used !</para>
		/// </summary>
		/// <returns></returns>
		List<List<int>> GetChilds()
		{
			List<List<int>> rList = new List<List<int>>();
			for (int i = 0; i < N; i++)
				if (matrix[SI, i] != 0)
				{
					List<int> child = new List<int>();
					child.Add(SI);
					child.Add(i);
					rList.Add(child);
				}

			return rList;
		}


		List<int> GetGateway(List<List<int>> list)
		{
			foreach (var child in list)
			{
				var subchild = child.Last();

				bool gateway = true;
				for (int i = 0; i < N && gateway == true; i++)
				{
					if (matrix[subchild, i] == 1) gateway = false;
				}

				if (gateway == true) return child;
			}


			return null;
		}

		public void printList(List<List<int>> list)
		{
			foreach (var child in list)
			{
				Console.Error.Write("{");
				foreach (var subchild in child)
				{

					Console.Error.Write("{0},", subchild);
				}
				Console.Error.Write("} ");
			}
			Console.Error.WriteLine("\n");
		}
		public void printList(List<int> list)
		{
			Console.Error.Write("{");
			foreach (var subchild in list)
			{

				Console.Error.Write("{0},", subchild);
			}
			Console.Error.Write("} ");
			Console.Error.WriteLine("\n");
		}

		public void printList(int[,] list)
		{
			
            int c = N.ToString().Length + " : ".Length;
			for (int i = 0; i < c; i++) Console.Error.Write(" ");
			for (int i = 0; i < N; i++) Console.Error.Write("{0} ", i);
			Console.Error.WriteLine();
			for (int i = 0; i < N; i++)
			{
				Console.Error.Write("{0} : ", i);
				for (int l = 0; l < c - " : ".Length - i.ToString().Length; l++) Console.Error.Write(" ");
				for (int j = 0; j < N; j++)
				{
					Console.Error.Write(matrix[i, j]);
					Console.Error.Write(" ");
				}
				Console.Error.WriteLine();
			}

			Console.Error.WriteLine("\n");
		}


	}

}