use std::env;
use std::vec;


pub struct KnightSolver {
    size: i32,
    size2: i32,
    solved: bool,
    board: Vec<i32>,
}

impl KnightSolver {
    pub fn new(_size: i32) -> KnightSolver {
        KnightSolver {
            size: _size,
            size2: _size*_size,
            solved: false,
            board: vec![0; (_size*_size) as usize],
        }
    }

    pub fn moveit(&mut self, row: i32, col: i32, move_number:i32) -> bool {
        if self.solved {
            return true;
        }
        if (row < 0) || (row >= self.size) || (col < 0) || (col >= self.size) {
            return false;
        }
        let idx: usize = (row*self.size + col) as usize;
        if self.board[idx] > 0 {
            return false;
        }

        // This is a valid move, so update everything and move on
        self.board[idx] = move_number;
        if move_number == self.size2 {
            self.solved = true;
            println!("Found solution");
            return true;
        }
        self.moveit(row+1, col+2, move_number+1);
        self.moveit(row+2, col+1, move_number+1);
        self.moveit(row+1, col-2, move_number+1);
        self.moveit(row+2, col-1, move_number+1);
        self.moveit(row-1, col-2, move_number+1);
        self.moveit(row-2, col-1, move_number+1);
        self.moveit(row-1, col+2, move_number+1);
        self.moveit(row-2, col+1, move_number+1);
        if !self.solved {
            self.board[idx] = 0;
        }
        return self.solved;
    }    
}

fn display(row:i32, col:i32) -> String {
    let rc: u32 = 10u32 + (row as u32);
    let mut s = String::with_capacity(2);
    if let Some(d) = std::char::from_digit(rc, 36) {
        s.push(d);
    }
    s.push_str(&(col+1).to_string());
    return s.to_uppercase();
}

fn main() {
    let mut size = 7;
    let mut start_col = 0;
    let mut start_row = 0;

    let args: Vec<String> = env::args().collect();
    if args.len() > 1 {
        match args[1].parse::<i32>() {
            Ok(n) => size = n,
            Err(e) => panic!("Invalid board size {}", e),
        }
        if args.len() > 2 {
            match args[2].parse::<i32>() {
                Ok(n) => start_col = n,
                Err(e) => panic!("Invalid start column {}", e),
            }
            if args.len() > 3 {
                match args[3].parse::<i32>() {
                    Ok(n) => start_row = n,
                    Err(e) => panic!("Invalid start row {}", e),
                }
            }        
        }
    }

    println!("Starting solver on {0}x{0} board from {1}", size, display(start_col, start_row));
    let mut solver = KnightSolver::new(size);
    let found = solver.moveit(start_col, start_row, 1);

    if found {
        let iter = solver.board.rchunks(size as usize);
        for row in iter {
            println!("{:?}", row);
        }
    } else {
        println!("No solution Solution found.");
    }
}
