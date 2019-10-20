using System;
using System.Threading.Tasks;

namespace vscode
{
    struct Move {
        int row;
        int col;

        public Move(int row, int col) {
            this.row = row;
            this.col = col;
        }

        public void Update(int row, int col) {
            this.row = row;
            this.col = col;
        }

        public string Display() {
            return $"{(char)(65+col)}{(char)(49+row)}";
        }
    }

    class KnightSolver
    {
        int size;
        public int[,] board;

        public Move[] path;

        bool solved;

        public KnightSolver(int size) 
        {
            this.size = size;
            board = new int[size, size];
            for (var i=0; i<size; i++)
                for (var j=0; j<size; j++)
                    board[i,j] = 0;
            path = new Move[size*size];
            solved = false;
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
            path[moveNumber-1].Update(row, col);
            if (moveNumber == size*size)
            {
                Console.WriteLine("Found solution");
                solved = true;
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
            var startMove = new Move(startCol, startRow);
            Console.WriteLine($"Starting solver on {size}x{size} from field {startMove.Display()}");

            DateTime t0 = DateTime.Now;
            var solver = new KnightSolver(size);
            var awaiter = solver.RunAsync(startCol, startRow).GetAwaiter();
            bool found = awaiter.GetResult();
            TimeSpan t = DateTime.Now-t0;
            if (found)
                foreach (var move in solver.path)
                    Console.WriteLine(move.Display());
            Console.WriteLine($"Found solution: {found} in {t.TotalSeconds} seconds");
        }
    }
}
