// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MainWindowViewModel.cs" company="Microsoft Corporation">
//   Internal use only
// </copyright>
// <summary>
//   Defines the MainWindowViewModel type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace AutomaticReportsProtoType.ViewModel
{
    using AutomaticReportsProtoType.Data;
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Windows;

    public class MainWindowViewModel
    {
        public MainWindowViewModel(MainWindow mainWindow)
        {
            MainWindow = mainWindow;
            DataLayerAdapter = new DataLayerAdapter();
            CurrentMode = EditContextMode.Soilders;
            InitializeData();
        }

        public void Initialize()
        {
            MainWindow.SoilderMetaDataItems.ItemsSource = null;
            MainWindow.SoilderMetaDataItems.ItemsSource = SoilderMetaDataTable;
            MainWindow.EquipmentMetaDataItems.ItemsSource = null;
            MainWindow.EquipmentMetaDataItems.ItemsSource = EquipmentMetaDataTable;
            MainWindow.SoilderSignedEquipmentItems.ItemsSource = null;
            MainWindow.SoilderSignedEquipmentItems.ItemsSource = SoilderSignedEquipmentTable;
        }

        internal bool ApplyEditChanges(EditOperationType operation, string firstValue, string secondValue)
        {
            if (operation == EditOperationType.Add)
            {
                if (string.IsNullOrEmpty(firstValue) || string.IsNullOrEmpty(secondValue))
                {
                    return false;
                }
                switch (CurrentMode)
                {
                    case EditContextMode.Soilders:
                        var updatedSoilderMetaDataTable = new List<SoldierMetaData>(SoilderMetaDataTable);
                        updatedSoilderMetaDataTable.Add(new SoldierMetaData(firstValue, secondValue));
                        DataLayerAdapter.WriteToSoilderMetaDataTable(updatedSoilderMetaDataTable);
                        SoilderMetaDataTable = new ObservableCollection<SoldierMetaData>(updatedSoilderMetaDataTable);
                        MainWindow.SoilderMetaDataItems.ItemsSource = null;
                        MainWindow.SoilderMetaDataItems.ItemsSource = SoilderMetaDataTable;
                        break;
                    case EditContextMode.Equipment:
                        var updatedEquipmentMetaDataTable = new List<EquipmentMetaData>(EquipmentMetaDataTable);
                        updatedEquipmentMetaDataTable.Add(new EquipmentMetaData(firstValue, secondValue));
                        DataLayerAdapter.WriteToEquipmentMetaDataTable(updatedEquipmentMetaDataTable);
                        EquipmentMetaDataTable = new ObservableCollection<EquipmentMetaData>(updatedEquipmentMetaDataTable);
                        MainWindow.EquipmentMetaDataItems.ItemsSource = null;
                        MainWindow.EquipmentMetaDataItems.ItemsSource = EquipmentMetaDataTable;
                        break;
                    case EditContextMode.SoildersEquipmentMapping:
                        break;
                }
            }

            else if (operation == EditOperationType.Remove)
            {
                if (string.IsNullOrEmpty(firstValue) || string.IsNullOrEmpty(secondValue))
                {
                    return false;
                }
                switch (CurrentMode)
                {
                    case EditContextMode.Soilders:
                        var updatedSoilderMetaDataTable = new List<SoldierMetaData>(SoilderMetaDataTable);
                        updatedSoilderMetaDataTable.Remove(new SoldierMetaData(firstValue, secondValue));
                        if (updatedSoilderMetaDataTable.Count == SoilderMetaDataTable.Count)
                        {
                            return false;
                        }
                        DataLayerAdapter.WriteToSoilderMetaDataTable(updatedSoilderMetaDataTable);
                        SoilderMetaDataTable = new ObservableCollection<SoldierMetaData>(updatedSoilderMetaDataTable);
                        MainWindow.SoilderMetaDataItems.ItemsSource = null;
                        MainWindow.SoilderMetaDataItems.ItemsSource = SoilderMetaDataTable;
                        break;
                    case EditContextMode.Equipment:
                        break;
                    case EditContextMode.SoildersEquipmentMapping:
                        break;
                }
            }

            return true;
        }

        internal EditContextMode CurrentMode { get; set; }

        internal Dictionary<string, SoldierMetaData> IndexedSoilderMetaData { get; set; }
        internal ObservableCollection<SoldierMetaData> SoilderMetaDataTable { get; set; }

        internal Dictionary<string, EquipmentMetaData> IndexedEquipmentMetaData { get; set; }
        internal ObservableCollection<EquipmentMetaData> EquipmentMetaDataTable { get; set; }

        internal Dictionary<string, string> IndexedSoilderIdToEquipmentId { get; set; }
        internal ObservableCollection<SoldierSignedEquipment> SoilderSignedEquipmentTable { get; set; }

        internal IDataLayerAdapter DataLayerAdapter { get; set; }
        internal MainWindow MainWindow { get; set; }
        public void InitializeData()
        {
            try
            {
                var soilderMetaDataTable = DataLayerAdapter.ReadFromSoilderMetaDataTable();
                SoilderMetaDataTable = new ObservableCollection<SoldierMetaData>(soilderMetaDataTable);
                IndexedSoilderMetaData = SoilderMetaDataTable.ToDictionary(i => i.Id, i => i);

                var equipmentMetaDataTable = DataLayerAdapter.ReadFromEquipmentMetaDataTable();
                EquipmentMetaDataTable = new ObservableCollection<EquipmentMetaData>(equipmentMetaDataTable);
                IndexedEquipmentMetaData = equipmentMetaDataTable.ToDictionary(i => i.Id, i => i);

                var soilderEquipmentMappingTable = DataLayerAdapter.ReadFromSoilderEquipmentMappingTable();

                var indexedSoilderEquipmentMapping = new Dictionary<string, List<string>>();

                foreach (var soldierEquipmentMapping in soilderEquipmentMappingTable)
                {
                    if (indexedSoilderEquipmentMapping.ContainsKey(soldierEquipmentMapping.SoilderId) == false)
                    {
                        indexedSoilderEquipmentMapping.Add(soldierEquipmentMapping.SoilderId, new List<string>());
                    }

                    indexedSoilderEquipmentMapping[soldierEquipmentMapping.SoilderId].Add(soldierEquipmentMapping.EquipmentId); 
                }

                 var tmp = indexedSoilderEquipmentMapping.Select(i => new SoldierSignedEquipment(i.Key, i.Value)).ToList();
                SoilderSignedEquipmentTable = new ObservableCollection<SoldierSignedEquipment>(tmp);

                //SoilderSignedEquipmentTable
                //indexedSoilderEquipmentMapping


                //IndexedSoilderIdToEquipmentId = soilderEquipmentMappingTable.ToDictionary(i => i.SoilderId, i => i.EquipmentId);

                //var soildersSignedEquipment = new List<SoilderSignedEquipment>();

                /*foreach (var soilderId in IndexedSoilderIdToEquipmentId.Keys)
                {
                    if (IndexedSoilderIdToEquipmentId
                    var soilderSignedEquipment = new SoilderSignedEquipment(
                }*/


                //IndexedSoilderIdToEquipmentId = new ObservableCollection<SoilderEquipmentMapping>(soilderEquipmentMappingTable);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
            }
        }
    }
}