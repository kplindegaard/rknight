import argparse
import time
import sys

if sys.version_info >= (3, 3):
    clock = time.perf_counter
else:
    clock = time.clock


size = 7
size2 = size*size
board = []
path = []
solved = False


def init():
    global size, size2, board, path, solved
    size2 = size*size
    row = [0 for k in range(size)]
    valid_range = frozenset(range(size))
    board = [list(row) for k in range(size)]
    path = [[0, 0] for k in range(size2)]
    solved = False


def move(row, col, move_number):
    global size, size2, board, path, solved
    if solved:
        return True
    if row < 0 or row >= size or col < 0 or col >= size:
        return False
    if board[row][col] > 0:
        return False

    # The move is valid. Update everything and move on
    board[row][col] = move_number
    path[move_number-1] = (row, col)
    if move_number == size2:
        solved = True
        return True
    move(row+1, col+2, move_number+1)
    move(row+2, col+1, move_number+1)
    move(row+1, col-2, move_number+1)
    move(row+2, col-1, move_number+1)
    move(row-1, col-2, move_number+1)
    move(row-2, col-1, move_number+1)
    move(row-1, col+2, move_number+1)
    move(row-2, col+1, move_number+1)
    board[row][col] = 0
    return solved


def run(row, col):
    return move(row, col, 1)


def display_square(square):
    return "{0}{1}".format(chr(65+square[0]), square[1]+1)


if __name__ == "__main__":

    parser = argparse.ArgumentParser()
    parser.add_argument('size', default=size, type=int)
    parser.add_argument('start_col', default=0, type=int)
    parser.add_argument('start_row', default=0, type=int)
    args = parser.parse_args()

    size = args.size
    start_pos = display_square((args.start_col, args.start_row))
    print("Starting solver from pos {0}".format(start_pos))
    print("Initializing board. Size={0}".format(args.size))
    init()

    t0 = clock()
    found = run(args.start_col, args.start_row)
    t = clock() - t0
    if found:
        print("Found solution in {0} sec:".format(t))
        for square in path:
            print("{0}{1}".format(chr(65+square[0]), square[1]+1))
    else:
        print("No solution found")
