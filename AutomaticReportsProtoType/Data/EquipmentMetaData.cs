namespace AutomaticReportsProtoType.Data
{
    public class EquipmentMetaData
    {
        public EquipmentMetaData(string id, string name)
        {
            this.Id = id; //makat
            this.Name = name;
            //this.Z = z; if without z cant have more than 1
        }

        public string Id { get; private set; }
        public string Name { get; private set; }
        //public string Z { get; private set; }

        public override string ToString()
        {
            return string.Format("{0},{1}", this.Id, this.Name);
        }
    }
}
