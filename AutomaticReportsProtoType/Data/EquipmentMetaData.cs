namespace AutomaticReportsProtoType.Data
{
    public class EquipmentMetaData
    {
        public EquipmentMetaData(string id, string name)
        {
            this.Id = id;
            this.Name = name;
        }

        public string Id { get; private set; }
        public string Name { get; private set; }

        public override string ToString()
        {
            return string.Format("{0},{1}", this.Id, this.Name);
        }
    }
}
