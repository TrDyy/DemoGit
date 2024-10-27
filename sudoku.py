import tkinter as tk
import random
import time  # Thêm thư viện để tính thời gian chạy
from tkinter import messagebox

class SudokuApp:
    def __init__(self, root):
        self.root = root
        self.root.title("Sudoku Solver")

        # Căn giữa cửa sổ và tăng kích thước
        self.center_window(650, 600)
        self.root.configure(bg="#F0F0F0")

        # Kích thước lưới Sudoku
        self.grid_size = 9
        self.subgrid_size = 3

        # Tạo lưới Sudoku hoàn chỉnh ngay từ đầu
        self.complete_sudoku = self.generate_complete_sudoku()
        self.sudoku_grid = [[0 for _ in range(self.grid_size)] for _ in range(self.grid_size)]

        # Tạo giao diện Sudoku
        self.entries = [[None for _ in range(self.grid_size)] for _ in range(self.grid_size)]
        self.create_grid()

        # Nút chọn độ khó
        difficulty_frame = tk.Frame(root, bg="#F0F0F0")
        difficulty_frame.pack(pady=10)

        # Nút "Dễ"
        easy_btn = tk.Button(difficulty_frame, text="Dễ", command=lambda: self.generate_sudoku("easy"), 
                             font=('Helvetica', 14, 'bold'), width=12, bg="#4CAF50", fg="white", bd=0)
        easy_btn.pack(side=tk.LEFT, padx=5)

        # Nút "Trung bình"
        medium_btn = tk.Button(difficulty_frame, text="Trung bình", command=lambda: self.generate_sudoku("medium"), 
                               font=('Helvetica', 14, 'bold'), width=12, bg="#FFC107", fg="black", bd=0)
        medium_btn.pack(side=tk.LEFT, padx=5)

        # Nút "Khó"
        hard_btn = tk.Button(difficulty_frame, text="Khó", command=lambda: self.generate_sudoku("hard"), 
                             font=('Helvetica', 14, 'bold'), width=12, bg="#F44336", fg="white", bd=0)
        hard_btn.pack(side=tk.LEFT, padx=5)

        # Nút giải Sudoku
        solve_btn = tk.Button(root, text="Giải Sudoku", command=self.solve_sudoku, 
                              font=('Helvetica', 16, 'bold'), width=15, bg="#2196F3", fg="white", bd=0)
        solve_btn.pack(pady=10)

        # Nhãn hiển thị thời gian chạy
        self.time_label = tk.Label(root, text="", font=('Helvetica', 12), bg="#F0F0F0")
        self.time_label.pack(pady=5)

    def center_window(self, width, height):
        screen_width = self.root.winfo_screenwidth()
        screen_height = self.root.winfo_screenheight()
        x = (screen_width - width) // 2
        y = (screen_height - height) // 2
        self.root.geometry(f'{width}x{height}+{x}+{y}')

    def create_grid(self):
        frame = tk.Frame(self.root, bg="#333")
        frame.pack(pady=20)

        for i in range(self.grid_size):
            for j in range(self.grid_size):
                entry = tk.Entry(frame, width=3, font=('Helvetica', 20), justify='center', relief='solid', bd=1)
                entry.grid(row=i, column=j, padx=1, pady=1)

                if (i // self.subgrid_size + j // self.subgrid_size) % 2 == 0:
                    entry.configure(bg='#D3E4CD')
                else:
                    entry.configure(bg='#FFF9B0')

                if i % 3 == 0 and j % 3 == 0:
                    entry.grid_configure(padx=(3, 1), pady=(3, 1))
                elif i % 3 == 0:
                    entry.grid_configure(pady=(3, 1))
                elif j % 3 == 0:
                    entry.grid_configure(padx=(3, 1))

                self.entries[i][j] = entry

    def generate_complete_sudoku(self):
        grid = [[0 for _ in range(self.grid_size)] for _ in range(self.grid_size)]

        def is_valid(grid, row, col, num):
            for i in range(self.grid_size):
                if grid[row][i] == num:
                    return False

            for i in range(self.grid_size):
                if grid[i][col] == num:
                    return False

            start_row = row - row % self.subgrid_size
            start_col = col - col % self.subgrid_size
            for i in range(start_row, start_row + self.subgrid_size):
                for j in range(start_col, start_col + self.subgrid_size):
                    if grid[i][j] == num:
                        return False
            return True

        def solve_sudoku(grid):
            for i in range(self.grid_size):
                for j in range(self.grid_size):
                    if grid[i][j] == 0:
                        nums = list(range(1, self.grid_size + 1))
                        random.shuffle(nums)
                        for num in nums:
                            if is_valid(grid, i, j, num):
                                grid[i][j] = num
                                if solve_sudoku(grid):
                                    return True
                                grid[i][j] = 0
                        return False
            return True

        solve_sudoku(grid)
        return grid

    def clear_grid(self):
        for i in range(self.grid_size):
            for j in range(self.grid_size):
                self.sudoku_grid[i][j] = 0
                self.entries[i][j].delete(0, tk.END)
                self.entries[i][j].configure(state='normal')

    def generate_sudoku(self, difficulty):
        self.clear_grid()

        self.sudoku_grid = [row[:] for row in self.complete_sudoku]

        if difficulty == "easy":
            hints = 40
        elif difficulty == "medium":
            hints = 30
        else:
            hints = 20

        hint_positions = random.sample(range(self.grid_size * self.grid_size), hints)

        for i in range(self.grid_size):
            for j in range(self.grid_size):
                if i * self.grid_size + j not in hint_positions:
                    self.sudoku_grid[i][j] = 0

        for i in range(self.grid_size):
            for j in range(self.grid_size):
                self.entries[i][j].delete(0, tk.END)
                if self.sudoku_grid[i][j] != 0:
                    self.entries[i][j].insert(0, str(self.sudoku_grid[i][j]))
                    self.entries[i][j].configure(state='disabled')
                else:
                    self.entries[i][j].configure(state='normal')

    def solve_sudoku(self):
        self.read_grid()

        def is_valid_sudoku(row, col, num):
            for i in range(self.grid_size):
                if self.sudoku_grid[row][i] == num or self.sudoku_grid[i][col] == num:
                    return False

            start_row = row - row % self.subgrid_size
            start_col = col - col % self.subgrid_size
            for i in range(start_row, start_row + self.subgrid_size):
                for j in range(start_col, start_col + self.subgrid_size):
                    if self.sudoku_grid[i][j] == num:
                        return False
            return True

        def solve_sudoku():
            for i in range(self.grid_size):
                for j in range(self.grid_size):
                    if self.sudoku_grid[i][j] == 0:
                        for num in range(1, self.grid_size + 1):
                            if is_valid_sudoku(i, j, num):
                                self.sudoku_grid[i][j] = num
                                if solve_sudoku():
                                    return True
                                self.sudoku_grid[i][j] = 0
                        return False
            return True

        start_time = time.time()  # Bắt đầu tính thời gian
        solved = solve_sudoku()
        end_time = time.time()  # Kết thúc tính thời gian

        if solved:
            for i in range(self.grid_size):
                for j in range(self.grid_size):
                    self.entries[i][j].delete(0, tk.END)
                    self.entries[i][j].insert(0, str(self.sudoku_grid[i][j]))
            elapsed_time = end_time - start_time
            self.time_label.config(text=f"Thời gian giải: {elapsed_time:.2f} giây")
            messagebox.showinfo("Hoàn thành", f"Sudoku đã được giải trong {elapsed_time:.2f} giây!")
        else:
            self.time_label.config(text="")
            messagebox.showerror("Lỗi", "Không thể giải được Sudoku này!")

    def read_grid(self):
        for i in range(self.grid_size):
            for j in range(self.grid_size):
                value = self.entries[i][j].get()
                self.sudoku_grid[i][j] = int(value) if value.isdigit() else 0

# Khởi chạy ứng dụng
root = tk.Tk()
app = SudokuApp(root)
root.mainloop()
