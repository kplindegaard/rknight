using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace vscode
{
    class KnightSolver
    {
        int size;
        public int[,] board;

        List<string> path;

        bool solved;

        public KnightSolver(int size) 
        {
            this.size = size;
            board = new int[size, size];
            for (var i=0; i<size; i++)
                for (var j=0; j<size; j++)
                    board[i,j] = 0;
            path = new List<string>();
            solved = false;
        }

        public static string field(int col, int row)
        {
            return $"{(char)(65+col)}{(char)(49+row)}";
        }

        public void DumpCurrentPath()
        {
            foreach (string move in path)
            {
                Console.WriteLine(move);
            }            
        }

        public bool Move(int row, int col, int moveNumber)
        {
            if (solved)
                return true;
            if ((row < 0) || (row >= size) || (col < 0) || col >= size)
                return false;
            if (board[row,col] > 0)
                return false;
            
            // This is a valid move, so update everything and move on
            board[row,col] = moveNumber;
            path.Add(field(row, col));
            if (moveNumber == size*size)
            {
                Console.WriteLine("Found solution");
                solved = true;
                foreach(string move in path.ToArray())
                    Console.WriteLine(move);
                return true;
            }
            Move(row+1, col+2, moveNumber+1);
            Move(row+2, col+1, moveNumber+1);
            Move(row+1, col-2, moveNumber+1);
            Move(row+2, col-1, moveNumber+1);
            Move(row-1, col-2, moveNumber+1);
            Move(row-2, col-1, moveNumber+1);
            Move(row-1, col+2, moveNumber+1);
            Move(row-2, col+1, moveNumber+1);
            board[row,col] = 0;
            path.RemoveAt(path.Count-1);
            return solved;
        }

        public bool Run(int row, int col)
        {
            return Move(row, col, 1);
        }

        public Task<bool> RunAsync(int row, int col)
        {
            return Task.Run(() => { 
                return Run(row, col);
            });
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            int size = 7;
            int startCol = 0;
            int startRow = 0;
            if (args.Length > 0)
                size = int.Parse(args[0]);
                    if (args.Length > 1)
                        startCol = int.Parse(args[1]);
                        if (args.Length > 2)
                            startRow = int.Parse(args[2]);
            Console.WriteLine($"Starting solver on {size}x{size} from field {KnightSolver.field(startCol, startRow)}...");

            KnightSolver solver = new KnightSolver(size);
            DateTime t0 = DateTime.Now;
            var awaiter = solver.RunAsync(startCol, startRow).GetAwaiter();
            bool found = awaiter.GetResult();
            TimeSpan t = DateTime.Now-t0;
            Console.WriteLine($"Found solution: {found} in {t.TotalSeconds} seconds");
        }
    }
}
