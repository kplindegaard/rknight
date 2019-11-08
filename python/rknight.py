import argparse
import time
import sys

if sys.version_info >= (3, 3):
    clock = time.perf_counter
else:
    clock = time.clock


class KnightSolver:
    def __init__(self, size):
        self.size = size
        self.size2 = size*size
        row = [0 for k in range(size)]
        self.board = [list(row) for k in range(size)]
        self.path = [(0, 0) for k in range(size*size)]
        self.solved = False

    def move(self, row, col, move_number):
        if self.solved:
            return True
        if row < 0 or row >= self.size or col < 0 or col >= self.size:
            return False
        if self.board[row][col] > 0:
            return False

        # The move is valid. Update everything and move on
        self.board[row][col] = move_number
        self.path[move_number-1] = (row, col)
        if move_number == self.size2:
            self.solved = True
            return True
        self.move(row+1, col+2, move_number+1)
        self.move(row+2, col+1, move_number+1)
        self.move(row+1, col-2, move_number+1)
        self.move(row+2, col-1, move_number+1)
        self.move(row-1, col-2, move_number+1)
        self.move(row-2, col-1, move_number+1)
        self.move(row-1, col+2, move_number+1)
        self.move(row-2, col+1, move_number+1)
        self.board[row][col] = 0
        return self.solved

    def run(self, row, col):
        return self.move(row, col, 1)


def display_square(square):
    return "{0}{1}".format(chr(65+square[0]), square[1]+1)


if __name__ == "__main__":

    parser = argparse.ArgumentParser()
    parser.add_argument('size', default=7, type=int)
    parser.add_argument('start_col', default=0, type=int)
    parser.add_argument('start_row', default=0, type=int)
    args = parser.parse_args()

    start_pos = display_square((args.start_col, args.start_row))
    print("Starting solver from pos {0}".format(start_pos))
    print("Initializing board. Size={0}".format(args.size))
    solver = KnightSolver(args.size)

    t0 = clock()
    found = solver.run(args.start_col, args.start_row)
    t = clock() - t0
    if found:
        print("Found solution in {0} sec:".format(t))
        for square in solver.path:
            print("{0}{1}".format(chr(65+square[0]), square[1]+1))
    else:
        print("No solution found")
