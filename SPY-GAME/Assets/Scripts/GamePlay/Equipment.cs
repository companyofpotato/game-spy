public class Equipment
{
    public int id {get; private set;}
    public string name {get; private set;}
    public string description {get; private set;}
    public int bef {get; private set;}
    public int success {get; private set;}
    public int aft {get; private set;}
    public int escape {get; private set;}

    public Equipment(int id, string name, string description, int bef, int success, int aft, int escape)
    {
        this.id = id;
        this.name = name;
        this.description = description;
        this.bef = bef;
        this.success = success;
        this.aft = aft;
        this.escape = escape;
    }
}