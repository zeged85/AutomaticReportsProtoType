
namespace AutomaticReportsProtoType.Data
{
    public class SoldierMetaData
    {
        public SoldierMetaData(string id, string name)
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

        public override int GetHashCode()
        {
            return this.Id.GetHashCode() ^ this.Name.GetHashCode() * 7;
        }

        public bool Equals(SoldierMetaData other)
        {
            if (other == null)
            {
                return false;
            }

            return other.Id == this.Id && other.Name == this.Name;


        }

        public override bool Equals(object other)
        {
            SoldierMetaData otherSoldierMetaData = other as SoldierMetaData;
            if (other == null || otherSoldierMetaData == null)
            {
                return false;
            }

            if (other == this) {
                return true;
            }

            return this.Equals(otherSoldierMetaData);

        }
    }
}
