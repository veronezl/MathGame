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
using System.Windows.Threading;

namespace MathGame
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        DispatcherTimer timer = new DispatcherTimer();
        int tenthsOfSecondsElapsed;
        int matchesFound;

        public MainWindow()
        {
            InitializeComponent();

            timer.Interval = TimeSpan.FromSeconds(.1);
            timer.Tick += Timer_Tick;

            SetUpGame();
        }

        private void Timer_Tick(object? sender, EventArgs e)
        {
            tenthsOfSecondsElapsed++;
            timeTextBlock.Text = (tenthsOfSecondsElapsed / 10F).ToString("0.0s");
            if (matchesFound == 8)
            {
                timer.Stop();
                timeTextBlock.Text = timeTextBlock.Text + " - Play again?";
            }
        }
            private void SetUpGame()
            {
                List<string> animalEmoji = new List<string>()
            {
                "🦑", "🦑",
                "🐠", "🐠",
                "🐄", "🐄",
                "🦂", "🦂",
                "🦒", "🦒",
                "🦕", "🦕",
                "🦘", "🦘",
                "🦔", "🦔",
            };

                Random random = new Random();

                foreach (TextBlock textBlock in mainGrid.Children.OfType<TextBlock>())
                {
                    int index = random.Next(animalEmoji.Count);
                    string nextEmoji = animalEmoji[index];
                    textBlock.Text = nextEmoji;
                    animalEmoji.RemoveAt(index);
                }
            }

            TextBlock lastTextBlockClicked;
            bool findingMath = false;
            private void TextBlock_MouseDown(object sender, MouseButtonEventArgs e)
            {
                TextBlock textBlock = sender as TextBlock;
                if (findingMath == false)
                {
                    textBlock.Visibility = Visibility.Hidden;
                    lastTextBlockClicked = textBlock;
                    findingMath = true;
                }

                else if (textBlock.Text == lastTextBlockClicked.Text)
                {
                    textBlock.Visibility = Visibility.Hidden;
                    findingMath = false;
                }

                else
                {
                    lastTextBlockClicked.Visibility = Visibility.Visible;
                    findingMath = false;
                }
            }

            private void timeTextBlock_MouseDown(object sender, MouseButtonEventArgs e)
            {
                if (matchesFound == 8)
                {
                    SetUpGame();
                }
            }
        
    }
}