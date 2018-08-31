namespace AutomaticReportsProtoType.Data
{
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;

    public class DataLayerAdapter : IDataLayerAdapter
    {
        private readonly string SoilderMetaDataTableFilePath;
        private readonly string EquipmentMetaDataTableFilePath;
        private readonly string SoilderEquipmentMappingTableFilePath;

        public DataLayerAdapter()
        {
            var baseDataPath = Path.Combine(Directory.GetCurrentDirectory(), "Data");
            SoilderMetaDataTableFilePath = Path.Combine(baseDataPath, "SoilderMetaDataTable.csv");
            EquipmentMetaDataTableFilePath = Path.Combine(baseDataPath, "EquipmentMetaDataTable.csv");
            SoilderEquipmentMappingTableFilePath = Path.Combine(baseDataPath, "SoilderEquipmentMappingTable.csv");
        }

        public bool WriteToSoilderMetaDataTable(List<SoldierMetaData> soilderMetaData)
        {
            var lines = soilderMetaData.Select(i => i.ToString()).ToList();
            File.WriteAllLines(SoilderMetaDataTableFilePath, lines);
            return true;
        }

        public bool WriteToEquipmentMetaDataTable(List<EquipmentMetaData> equipmentMetaData)
        {
            var lines = equipmentMetaData.Select(i => i.ToString()).ToList();
            File.WriteAllLines(EquipmentMetaDataTableFilePath, lines);
            return true;
        }

        public bool WriteToEquipmentMappingTable(List<SoldierEquipmentMapping> soilderEquipmentMapping)
        {
            var lines = soilderEquipmentMapping.Select(i => i.ToString()).ToList();
            File.WriteAllLines(SoilderEquipmentMappingTableFilePath, lines);
            return true;
        }

        public List<SoldierMetaData> ReadFromSoilderMetaDataTable()
        {
            List<SoldierMetaData> soildersMetaData = new List<SoldierMetaData>();
            var lines = File.ReadAllLines(SoilderMetaDataTableFilePath);

            foreach (var line in lines)
            {
                var splittedLine = line.Split(',');
                soildersMetaData.Add(new SoldierMetaData(splittedLine[0], splittedLine[1]));
            }

            return soildersMetaData;
        }

        public List<EquipmentMetaData> ReadFromEquipmentMetaDataTable()
        {
            List<EquipmentMetaData> equipmentsMetaData = new List<EquipmentMetaData>();
            var lines = File.ReadAllLines(EquipmentMetaDataTableFilePath);

            foreach (var line in lines)
            {
                var splittedLine = line.Split(',');
                equipmentsMetaData.Add(new EquipmentMetaData(splittedLine[0], splittedLine[1]));
            }

            return equipmentsMetaData;
        }

        public List<SoldierEquipmentMapping> ReadFromSoilderEquipmentMappingTable()
        {
            List<SoldierEquipmentMapping> soilderEquipmentMappings = new List<SoldierEquipmentMapping>();

            var lines = File.ReadAllLines(SoilderEquipmentMappingTableFilePath);

            foreach (var line in lines)
            {
                var splittedLine = line.Split(',');
                soilderEquipmentMappings.Add(new SoldierEquipmentMapping(splittedLine[0], splittedLine[1]));
            }

            return soilderEquipmentMappings;
        }
    }
}