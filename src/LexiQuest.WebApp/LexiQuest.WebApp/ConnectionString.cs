namespace LexiQuest.WebApp;

public class ConnectionString
{
    private ConnectionString(string str)
    {
        Value = str;
    }

    public string Value { get; }

    public static ConnectionString FromString(string str) => new(str);

    public override string ToString() => Value;
}
