import random
def get_best_move(board, player):
    opponent = 2 if player == 1 else 1

    def is_winner(board, player):
        winning_patterns = [
            [0, 1, 2], [3, 4, 5], [6, 7, 8],  # Горизонтальные
            [0, 3, 6], [1, 4, 7], [2, 5, 8],  # Вертикальные
            [0, 4, 8], [2, 4, 6]  # Диагонали
        ]
        for pattern in winning_patterns:
            if all(board[i] == player for i in pattern):
                return True
        return False

    def minimax(board, depth, is_maximizing):
        if is_winner(board, player):
            return 1
        elif is_winner(board, opponent):
            return -1
        elif 0 not in board:  # Нет пустых клеток
            return 0

        if is_maximizing:
            best_score = float("-inf")
            for i in range(len(board)):
                if board[i] == 0:
                    board[i] = player
                    score = minimax(board, depth + 1, False)
                    board[i] = 0
                    best_score = max(score, best_score)
            return best_score
        else:
            best_score = float("inf")
            for i in range(len(board)):
                if board[i] == 0:
                    board[i] = opponent
                    score = minimax(board, depth + 1, True)
                    board[i] = 0
                    best_score = min(score, best_score)
            return best_score

    best_move = None
    best_score = float("-inf")
    for i in range(len(board)):
        if board[i] == 0:
            board[i] = player
            score = minimax(board, 0, False)
            board[i] = 0
            if score > best_score:
                best_score = score
                best_move = i
    return best_move

# Используем функцию get_best_move для определения хода ИИ на основе поля s

s = [1,0,1,
     0,2,1,
     0,0,0]
best_move = get_best_move(s, 1)
print("Лучший ход для крестиков:", best_move)

"""
for i in range(10):
    s = chek(Gen())
    if s:
        print(s)"""