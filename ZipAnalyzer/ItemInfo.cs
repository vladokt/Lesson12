using System.Collections;

namespace ZipAnalyzer
{
    public record ItemInfo
    {
        public enum Type
        {
            Directory,
            File,
        }

        Type _itemType;
        string _itemName;
        DateTime _itemUpdateTime;

        public Type ItemType
        {
            get { return _itemType; }
            init { _itemType = value; }
        }

        public string ItemName
        {
            get { return _itemName; }
            init { _itemName = value; }
        }

        public DateTime ItemUpdateTime
        {
            get { return _itemUpdateTime; }
            init { _itemUpdateTime = value; }
        }

        public ItemInfo (Type itemType, string itemName, DateTime itemUpdateTime)
        {
            ItemType = itemType;
            ItemName = itemName;
            ItemUpdateTime = itemUpdateTime;
        }

        public override string ToString() => $"{ItemType} {ItemName} {ItemUpdateTime}";
    }
}
