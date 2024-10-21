namespace Utils.Core
{
    public interface IKeyValue
    {
        object Key { get; }
        object Value { get; }
    }

    [Serializable]
    public record KeyValue<T1, T2> : IKeyValue
    {
        object IKeyValue.Key => Key;

        object IKeyValue.Value => Value;

        public T1 Key { get; set; }

        public T2 Value { get; set; }

        public KeyValue()
        {
        }

        public KeyValue(T1 key, T2 value) : this()
        {
            Key   = key;
            Value = value;
        }
    }

    [Serializable]
    public record KeyValueExtended<T1, T2, T3> : KeyValue<T1, T2>
    {
        public T3 Extenstion { get; set; }
    }
}