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

using System.Windows;

namespace WpfApp1
{
    public partial class MainWindow : Window
    {
        private char[,] board; // Массив для представления игрового поля
        private char currentPlayer; // Текущий игрок (X или O)
        private bool gameEnded; // Переменная для отслеживания завершения игры

        public MainWindow()
        {
            InitializeComponent();
            StartGame();
        }

        private void StartGame()
        {
            board = new char[3, 3];
            currentPlayer = 'X';
            gameEnded = false;

            // Инициализация игрового поля и очистка отображения
            for (int row = 0; row < 3; row++)
            {
                for (int col = 0; col < 3; col++)
                {
                    board[row, col] = '-';
                    Button button = (Button)FindName($"Button_{row}{col}");
                    button.Content = "";
                    button.IsEnabled = true;
                }
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (gameEnded)
                return;

            Button button = (Button)sender;
            string[] position = button.Name.Split('_');

            int row = int.Parse(position[1][0].ToString());
            int col = int.Parse(position[1][1].ToString());

            if (board[row, col] == '-')
            {
                board[row, col] = currentPlayer;
                button.Content = currentPlayer.ToString();

                if (CheckForWin())
                {
                    MessageBox.Show(currentPlayer + " победил!");
                    gameEnded = true;
                }
                else if (IsBoardFull())
                {
                    MessageBox.Show("Ничья!");
                    gameEnded = true;
                }
                else
                    currentPlayer = (currentPlayer == 'X') ? 'O' : 'X';
            }
        }

        private bool CheckForWin()
        {
            // Проверка строк, столбцов и диагоналей на наличие трех одинаковых символов
            for (int i = 0; i < 3; i++)
            {
                if (board[i, 0] != '-' && board[i, 0] == board[i, 1] && board[i, 1] == board[i, 2])
                    return true;
                if (board[0, i] != '-' && board[0, i] == board[1, i] && board[1, i] == board[2, i])
                    return true;
            }

            if (board[0, 0] != '-' && board[0, 0] == board[1, 1] && board[1, 1] == board[2, 2])
                return true;
            if (board[0, 2] != '-' && board[0, 2] == board[1, 1] && board[1, 1] == board[2, 0])
                return true;

            return false;
        }

        private bool IsBoardFull()
        {
            // Проверка заполненности игрового поля
            for (int row = 0; row < 3; row++)
            {
                for (int col = 0; col < 3; col++)
                {
                    if (board[row, col] == '-')
                        return false;
                }
            }
            return true;
        }

        private void NewGame_Click(object sender, RoutedEventArgs e)
        {
            StartGame();
        }
    }
}