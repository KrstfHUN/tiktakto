using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace tiktaktó
{
    public partial class MainWindow : Window
    {
        private string[,] board = new string[3, 3];
        private string currentPlayer = "X";
        private bool gameOver = false;
        private int moveCount = 0;

        public MainWindow()
        {
            InitializeComponent();
            InitializeBoard();
        }

        private void InitializeBoard()
        {
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    board[i, j] = "";
                }
            }
            currentPlayer = "X";
            gameOver = false;
            moveCount = 0;
            StatusText.Text = "X játékos következik";
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (gameOver) return;

            Button btn = sender as Button;
            if (btn == null || !string.IsNullOrEmpty(btn.Content as string)) return;

            string[] pos = btn.Tag.ToString().Split(',');
            int row = int.Parse(pos[0]);
            int col = int.Parse(pos[1]);

            board[row, col] = currentPlayer;
            btn.Content = currentPlayer;
            btn.Foreground = currentPlayer == "X" ? Brushes.Blue : Brushes.Red;
            moveCount++;

            if (CheckWinner())
            {
                StatusText.Text = $"{currentPlayer} játékos nyert!";
                StatusText.Foreground = Brushes.Green;
                gameOver = true;
                HighlightWinningCells();
            }
            else if (moveCount == 9)
            {
                StatusText.Text = "Döntetlen!";
                StatusText.Foreground = Brushes.Orange;
                gameOver = true;
            }
            else
            {
                currentPlayer = currentPlayer == "X" ? "O" : "X";
                StatusText.Text = $"{currentPlayer} játékos következik";
            }
        }

        private bool CheckWinner()
        {
            for (int i = 0; i < 3; i++)
            {
                if (!string.IsNullOrEmpty(board[i, 0]) &&
                    board[i, 0] == board[i, 1] && board[i, 1] == board[i, 2])
                    return true;
            }

            for (int j = 0; j < 3; j++)
            {
                if (!string.IsNullOrEmpty(board[0, j]) &&
                    board[0, j] == board[1, j] && board[1, j] == board[2, j])
                    return true;
            }

            if (!string.IsNullOrEmpty(board[0, 0]) &&
                board[0, 0] == board[1, 1] && board[1, 1] == board[2, 2])
                return true;

            if (!string.IsNullOrEmpty(board[0, 2]) &&
                board[0, 2] == board[1, 1] && board[1, 1] == board[2, 0])
                return true;

            return false;
        }

        private void HighlightWinningCells()
        {
            for (int i = 0; i < 3; i++)
            {
                if (!string.IsNullOrEmpty(board[i, 0]) &&
                    board[i, 0] == board[i, 1] && board[i, 1] == board[i, 2])
                {
                    GetButton(i, 0).Background = Brushes.LightGreen;
                    GetButton(i, 1).Background = Brushes.LightGreen;
                    GetButton(i, 2).Background = Brushes.LightGreen;
                    return;
                }
            }

            for (int j = 0; j < 3; j++)
            {
                if (!string.IsNullOrEmpty(board[0, j]) &&
                    board[0, j] == board[1, j] && board[1, j] == board[2, j])
                {
                    GetButton(0, j).Background = Brushes.LightGreen;
                    GetButton(1, j).Background = Brushes.LightGreen;
                    GetButton(2, j).Background = Brushes.LightGreen;
                    return;
                }
            }

            if (!string.IsNullOrEmpty(board[0, 0]) &&
                board[0, 0] == board[1, 1] && board[1, 1] == board[2, 2])
            {
                GetButton(0, 0).Background = Brushes.LightGreen;
                GetButton(1, 1).Background = Brushes.LightGreen;
                GetButton(2, 2).Background = Brushes.LightGreen;
                return;
            }

            if (!string.IsNullOrEmpty(board[0, 2]) &&
                board[0, 2] == board[1, 1] && board[1, 1] == board[2, 0])
            {
                GetButton(0, 2).Background = Brushes.LightGreen;
                GetButton(1, 1).Background = Brushes.LightGreen;
                GetButton(2, 0).Background = Brushes.LightGreen;
            }
        }

        private Button GetButton(int row, int col)
        {
            string btnName = $"Btn{row}{col}";
            return (Button)this.FindName(btnName);
        }

        private void ResetButton_Click(object sender, RoutedEventArgs e)
        {
            InitializeBoard();
            StatusText.Foreground = Brushes.Black;

            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    Button btn = GetButton(i, j);
                    btn.Content = "";
                    btn.Background = Brushes.White;
                }
            }
        }
    }
}