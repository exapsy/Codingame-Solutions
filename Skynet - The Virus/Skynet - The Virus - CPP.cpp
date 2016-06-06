#include <iostream>
#include <string>
#include <vector>
#include <algorithm>
#include <climits>
#include <cstring>

using namespace std;

/*
 * Algorithm :
 * 10: Search Shortest Root (B&B (Branch N' Bound))
 * 20: Cut the last link that leads to the Gateway
*/

/*
 * Shortest Root Algorithm (B&B (Branch N' Bound)) :
 * 00: Check if Current Distance is >= BestDistance, true: Break;
 * 10: Take Current Node's Children
 * 20: Put them into Search Sequence
 * 30: See if there is any Gateway (Matrix[][] = 2)
 * 40: If yes: pop SearchSequence[0]
 * 50: If not: CurrentNode = SearchSequence[0]
*/

/**
 * Auto-generated code below aims at helping you parse
 * the standard input according to the problem statement.
 **/

 class Graph {
 public:
     Graph(int N) : this->N(N) {
         matrix = new matrix[N][N]();
     }

     void makeGateway (int g) {
         for (int i = 0; i<N; i++) {
             if (matrix[i][g] == 1) {
                 matrix[i][g] = 2;
                 matrix[g][i] = 2;
             }
         }
     }

     /*
      * Returns the int that has to be cut
     */
     /*void shortestPath(int& ([2]ref)) {
         int bestCost = INT_MAX;
         vector<int> bestPath;
         if (bestPath.size() == 0)
         // Search into Matrix
         while (true) {
             cost = 0;
             vector<int> curPath;
             int index = 0;
             while (matrix[*(curPath.end()-1)][index] != 2) {
                 index = 0;
                 // (While there is not a link to another node) && (node is not visited)
                 while(matrix[*(curPath.end()-1)][index] == 0 && vectorContains(curPath, index)))  index++;
                 if (cost >= bestCost) break;
             }

            ref[0] = 1;
            ref[1] = 2;
        }


         int[2] r = {(bestPath.end()-1), (bestPath.end()-2)}
         return r;
     }*/
     int matrix[][];
     int N;
     int SI; // Start point to the/a gateway
 }

 template<class T>
 inline bool vectorContains (const vector<T> &v, const T &key) {
     return std::find(v.begin(), v.end(), key) != v.end() ? true : false;
 }

int main() {

    Graph g;

    int N; // the total number of nodes in the level, including the gateways
    int L; // the number of links
    int E; // the number of exit gateways
    cin >> N >> L >> E; cin.ignore();
    g = new Graph (N);
    for (int i = 0; i < L; i++) {
        int N1; // N1 and N2 defines a link between these nodes
        int N2;
        cin >> N1 >> N2; cin.ignore();
    }
    for (int i = 0; i < E; i++) {
        int EI; // the index of a gateway node
        cin >> EI; cin.ignore();
        g.makeGateway(EI);
    }

    for (int i = 0; i < N; i++) {
        for (int j = 0; j < N; j++) {
            cout << g.matrix[i][j];
        }
        cout << endl;
    }
    // game loop
    while (1) {
        int SI; // The index of the node on which the Skynet agent is positioned this turn
        g.SI = SI;
        cin >> SI; cin.ignore();

        // Write an action using cout. DON'T FORGET THE "<< endl"
        // To debug: cerr << "Debug messages..." << endl;

        //int[2] pathToBeCut;
        //g.shortestPath(&pathToBeCut)));
        // Example: 0 1 are the indices of the nodes you wish to sever the link between
        cout << pathToBeCut[0] << pathToBeCut[1] << endl;
    }
}
