class KnightSolver {
    size:number;
    board:number[][] = [];
    path:string[] = [];
    solved:boolean = false;

    constructor(size:number) {
        this.size = size;
        let arr:number[] = [];
        for (var k=0; k<size; k++)
            arr.push(0);
        for (var k=0; k<size; k++)
            this.board.push(arr.slice());
    }

    field(row:number, col:number) : string {
        return String.fromCharCode(65+row) + (col+1);
    }

    move(row:number, col:number, moveNumber:number) : boolean {
        if (this.solved)
            return true;
        if ((row < 0) || (row >= this.size) || (col < 0) || col >= this.size)
            return false;
        if (this.board[row][col] > 0)
            return false;

        // This is a valid move, so update everything and move on
        this.board[row][col] = moveNumber;
        this.path.push(this.field(row, col));
        if (moveNumber == this.size*this.size) {
            console.log("Found solution.");
            this.solved = true;
            this.path.forEach( x => {
                console.log(x);
            });
            return true;
        }
        this.move(row+1, col+2, moveNumber+1);
        this.move(row+2, col+1, moveNumber+1);
        this.move(row+1, col-2, moveNumber+1);
        this.move(row+2, col-1, moveNumber+1);
        this.move(row-1, col-2, moveNumber+1);
        this.move(row-2, col-1, moveNumber+1);
        this.move(row-1, col+2, moveNumber+1);
        this.move(row-2, col+1, moveNumber+1);
        this.board[row][col] = 0;
        this.path.pop();
        return this.solved;
    }
}

// Default board size and start position is 7x7 and A1
let size = 7;
let startRow, startCol = 0;
// Parse command line args
if (process.argv.length > 2)
    size = Number(process.argv[2]);
if (process.argv.length > 3)
    startCol = Number(process.argv[3]);
if (process.argv.length > 4)
    startRow = Number(process.argv[4]);

// Start solver
let solver = new KnightSolver(size);
console.log(`Starting solver on ${size}x${size} from ${solver.field(startCol, startRow)}...`);
if (!solver.move(startCol, startRow, 1))
    console.log("Unable to find a solution.")
