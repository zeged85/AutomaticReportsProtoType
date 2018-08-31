
namespace AutomaticReportsProtoType.Data
{
    using System.Collections.Generic;

    public class SoldierSignedEquipment
    {
        public SoldierSignedEquipment(string soilderId) : this(soilderId, new List<string>())
        {
        }

        public SoldierSignedEquipment(string soilderId, List<string> equipmentIds)
        {
            this.SoilderId = soilderId;
            this.EquipmentIds = equipmentIds;
        }

        public string SoilderId { get; set; }
        private List<string> EquipmentIds { get; set; }
        public string SignedEquipments => string.Join(",", EquipmentIds);
    }
}
