package main

import (
	"fmt"
	"os"
	"strconv"
)

type Move struct {
	row int
	col int
}

type KnightSolver struct {
	size int
	solved bool
	board [][]int
	path []Move
}

// Move methods
func (m *Move) display() string {
	return fmt.Sprintf("%c%d", rune(65+m.row), m.col+1)
}

// KnightSolver methods
func (s *KnightSolver) init(size int) {
	fmt.Println("Initializing board. Size=", size)
	s.size = size
	s.solved = false
	s.board = make([][]int, size)
	for k := 0; k < size; k++ {
		s.board[k] = make([]int, size)
	}
	s.path = make([]Move, size*size)
}

func (s *KnightSolver) move(row int, col int, moveNumber int) bool {
	if s.solved {
		return true
	}
	if row < 0 || row >= s.size || col < 0 || col >= s.size {
		return false
	}
	if s.board[row][col] > 0 {
		return false
	}

	// This is a valid move, update everything and move on
	s.board[row][col] = moveNumber
	s.path[moveNumber-1].row = row
	s.path[moveNumber-1].col = col
	if moveNumber == s.size*s.size {
		s.solved = true
		return true
	}
	s.move(row+1, col+2, moveNumber+1)
	s.move(row+2, col+1, moveNumber+1)
	s.move(row+1, col-2, moveNumber+1)
	s.move(row+2, col-1, moveNumber+1)
	s.move(row-1, col-2, moveNumber+1)
	s.move(row-2, col-1, moveNumber+1)
	s.move(row-1, col+2, moveNumber+1)
	s.move(row-2, col+1, moveNumber+1)
	s.board[row][col] = 0
	return s.solved
}

func main() {
	size := 7
	startCol := 0
	startRow := 0

	// Parse cmd line args
	nArgs := len(os.Args)
	if nArgs > 1 {
		size, _ = strconv.Atoi(os.Args[1])
		if nArgs > 2 {
			startCol, _ = strconv.Atoi(os.Args[2])
			if nArgs > 3 {
				startRow, _ = strconv.Atoi(os.Args[3])
			}
		}
	}
	startPos := Move{row: startCol, col: startRow}
	fmt.Println("Starting solver from pos", startPos.display())

	solver := KnightSolver{}
	solver.init(size)
	found := solver.move(startCol, startRow, 1)
	if found {
		fmt.Println("Found solution")
		for k := 0; k < size*size; k++ {
			fmt.Println(solver.path[k].display())
		}
	} else {
		fmt.Println("Unable to find solution")
	}
}
