
namespace AutomaticReportsProtoType.Data
{
    public class SoldierEquipmentMapping
    {
        public SoldierEquipmentMapping(string soilderId, string equipmentId)
        {
            this.SoilderId = soilderId;
            this.EquipmentId = equipmentId;
        }

        public string SoilderId { get; private set; }
        public string EquipmentId { get; private set; }

        public override string ToString()
        {
            return string.Format("{0},{1}", this.SoilderId, this.EquipmentId);
        }
    }
}
