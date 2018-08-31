
namespace AutomaticReportsProtoType
{

    using System.Windows;
    using ViewModel;

    public partial class PromptWindow 
    {
        private MainWindowViewModel Vm;

        public PromptWindow(MainWindowViewModel vm)
        {
            InitializeComponent();
            Vm = vm;
        }

        private void Yes_Button_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Yes!!!");
            this.Close();
        }

        private void No_Button_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("No!!!");
            this.Close();
        }
    }
}
