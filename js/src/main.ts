class Move {
    row:number;
    col:number;

    constructor(row:number, col:number) {
        this.row = row;
        this.col = col;
    }

    update(row:number, col:number) {
        this.row = row;
        this.col = col;
    }

    display() : string {
        return String.fromCharCode(65+this.row) + (this.col+1);
    }
}

class KnightSolver {
    size:number;
    board:number[][] = [];
    path:Move[] = [];
    solved:boolean = false;

    constructor(size:number) {
        this.size = size;
        let arr:number[] = [];
        for (var k=0; k<size; k++)
            arr.push(0);
        for (var k=0; k<size; k++)
            this.board.push(arr.slice());
        for (var k=0; k<size*size; k++)
            this.path.push(new Move(0, 0));
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
        this.path[moveNumber-1].update(row, col);
        if (moveNumber == this.size*this.size) {
            console.log("Found solution.");
            this.solved = true;
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
let startPos = new Move(startCol, startRow);
console.log(`Starting solver on ${size}x${size} from ${startPos.display()}...`);
if (!solver.move(startCol, startRow, 1))
    console.log("Unable to find a solution.")
else
    solver.path.forEach( x => {
        console.log(x.display());
});
