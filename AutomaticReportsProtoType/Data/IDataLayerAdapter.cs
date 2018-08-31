using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomaticReportsProtoType.Data
{
    interface IDataLayerAdapter
    {
        bool WriteToSoilderMetaDataTable(List<SoldierMetaData> soilderMetaData);
        bool WriteToEquipmentMetaDataTable(List<EquipmentMetaData> equipmentMetaData);

        bool WriteToEquipmentMappingTable(List<SoldierEquipmentMapping> soilderEquipmentMapping);


        List<SoldierMetaData> ReadFromSoilderMetaDataTable();

        List<EquipmentMetaData> ReadFromEquipmentMetaDataTable();

        List<SoldierEquipmentMapping> ReadFromSoilderEquipmentMappingTable();
    }
}

