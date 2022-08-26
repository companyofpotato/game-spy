public class City
{
    public int id {get; private set;}
    public string name {get; private set;}
    public int type {get; private set;}
    public int buildings {get; private set;}
    public int perks {get; private set;}
    public int[] equipmentList {get; private set;}
    public int[] personList {get; private set;}

    public City()
    {
        
    }

    public void SetNewCity(int id, string name, int type, int buildings, int perks, int[] equipmentList, int[] personList)
    {
        this.id = id;
        this.name = name;
        this.type = type;
        this.buildings = buildings;
        this.perks = perks;
        this.equipmentList = equipmentList;
        this.personList = personList;
    }
}