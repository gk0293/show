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
using MahApps.Metro.Controls;

namespace show
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MetroWindow
    {
        private MainVM viewModel;
        public MainWindow()
        {
            InitializeComponent();
            viewModel = new MainVM();
            DataContext = viewModel;
        }

        private void CheckBox_MouseEnter(object sender, MouseEventArgs e)
        {

            viewModel.IsMouseHover = true;
        }

        private void CheckBox_MouseLeave(object sender, MouseEventArgs e)
        {

            viewModel.IsMouseHover = false;
        }

        private void Border_MouseEnter(object sender, MouseEventArgs e)
        {
            viewModel.IsMouseHoverBorder = true;
        }

        private void Border_MouseLeave(object sender, MouseEventArgs e)
        {
            viewModel.IsMouseHoverBorder = false;
        }
    }
}