#include <stdio.h>
#include <stdlib.h>


short size;
short size2;
short** board;
short** path;
short solved = 0;


void init()
{
    size2 = size*size;
    board = (short**)malloc(sizeof(short*)*size);
    for (size_t k=0; k<size; k++)
        board[k] = (short*)malloc(sizeof(short)*size);
    path = (short**)malloc(sizeof(short*)*size2);
    for (size_t k=0; k<size2; k++)
        path[k] = (short*)malloc(sizeof(short)*2);
}

void destruct()
{
    for (size_t k=0; k<size2; k++)
        free(path[k]);
    free(path);
    for (size_t k=0; k<size; k++) 
        free(board[k]);
    free(board);
}

int move(short row, short col, short moveNumber)
{
    // printf("%d: %d,%d\n", moveNumber, row, col);

    if (solved)
        return 1;
    if ( (row < 0) || (row >= size) || (col < 0) || (col >= size) )
        return 0;
    if (board[row][col] > 0)
        return 0;

    // The move is valid. Update and proceed
    board[row][col] = moveNumber;
    path[moveNumber-1][0] = row;
    path[moveNumber-1][1] = col;
    if (moveNumber == size2) {
        solved = 1;
        return 1;
    }
    move(row+1, col+2, moveNumber+1);
    move(row+2, col+1, moveNumber+1);
    move(row+1, col-2, moveNumber+1);
    // return 0;
    move(row+2, col-1, moveNumber+1);
    move(row-1, col-2, moveNumber+1);
    move(row-2, col-1, moveNumber+1);
    move(row-1, col+2, moveNumber+1);
    move(row-2, col+1, moveNumber+1);
    board[row][col] = 0;
    return solved;
}

int main(int argc, char **argv)
{
    size = 7;
    int startRow = 0;
    int startCol = 0;
    if (argc > 1) {
        size = atoi(argv[1]);
        if (argc > 2) {
            startCol = atoi(argv[2]);
            if (argc > 3)
                startRow = atoi(argv[3]);
        }
    }

    init();
    int found = move(startCol, startRow, 1);
    if (found) {
        printf("Found solution\n");
        for (size_t k=0; k<size2; k++)
            printf("%c%d\n", path[k][0]+65, path[k][1]+1);
    } else {
        printf("Could not find solution\n");
    }
    destruct();
}