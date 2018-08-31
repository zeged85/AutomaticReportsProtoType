// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MainWindow.cs" company="Microsoft Corporation">
//   Internal use only
// </copyright>
// <summary>
//   Defines the MainWindow type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace AutomaticReportsProtoType
{
    using System.Windows;
    using ViewModel;

    public partial class MainWindow
    {
        private MainWindowViewModel mainWindowViewModel;

        public MainWindow()
        {
            mainWindowViewModel = new MainWindowViewModel(this);
            DataContext = mainWindowViewModel;
            InitializeComponent();
            mainWindowViewModel.Initialize();
        }

        private void Remove_Single(object sender, RoutedEventArgs e)
        {
            if (mainWindowViewModel.ApplyEditChanges(EditOperationType.Remove, TextBoxOne.Text, TextBoxTwo.Text))
            {
                MessageBox.Show("Item was removed successfully");
                return;
            }

            MessageBox.Show("Invalid input entered, no item was removed");
        }

        private void Add_Single(object sender, RoutedEventArgs e)
        {
            if (mainWindowViewModel.ApplyEditChanges(EditOperationType.Add, TextBoxOne.Text, TextBoxTwo.Text))
            {
                MessageBox.Show("Item was added successfully");
                return;
            }

            MessageBox.Show("Invalid input entered, no item was added");
        }

        private void Soilders_RadioButton_Checked(object sender, RoutedEventArgs e)
        {
            if (IsAnyComponentNull())
            {
                return;
            }

            mainWindowViewModel.CurrentMode = EditContextMode.Soilders;
            SetItemEditingModeVisibility(Visibility.Visible, Visibility.Collapsed, Visibility.Collapsed);
        }

        private void Equipment_RadioButton_Checked(object sender, RoutedEventArgs e)
        {
            if (IsAnyComponentNull())
            {
                return;
            }

            mainWindowViewModel.CurrentMode = EditContextMode.Equipment;
            SetItemEditingModeVisibility(Visibility.Collapsed, Visibility.Visible, Visibility.Collapsed);
        }

        private void Onwerships_RadioButton_Checked(object sender, RoutedEventArgs e)
        {
            if (IsAnyComponentNull())
            {
                return;
            }

            mainWindowViewModel.CurrentMode = EditContextMode.SoildersEquipmentMapping;
            SetItemEditingModeVisibility(Visibility.Collapsed, Visibility.Collapsed, Visibility.Visible);
        }

        private bool IsAnyComponentNull()
        {
            return TextBoxOne == null || TextBoxTwo == null;
        }

        private void SubmitChanges(object sender, RoutedEventArgs e)
        {

            var pop = new PromptWindow(mainWindowViewModel);
            pop.ShowDialog();
        }

        private void SetItemEditingModeVisibility(Visibility SoildersVisibility, Visibility EquipmentVisibility, Visibility OnwershipsVisibility)
        {
           
            SoilderMetaDataItems.Visibility = SoildersVisibility;
            EquipmentMetaDataItems.Visibility = EquipmentVisibility;
            SoilderSignedEquipmentItems.Visibility = OnwershipsVisibility;

            SoilderIdLabel.Visibility = SoildersVisibility == Visibility.Visible || OnwershipsVisibility == Visibility.Visible ? Visibility.Visible : Visibility.Collapsed;
            EquipmentIdLabel.Visibility = EquipmentVisibility;
            EquipmentIdAsSecondValueLabel.Visibility = OnwershipsVisibility;
            SoilderNameLabel.Visibility = SoildersVisibility;
            EquipmentNameLabel.Visibility = EquipmentVisibility;
        }
    }
}