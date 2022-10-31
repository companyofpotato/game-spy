using System.Collections.Generic;

public class City
{
    public int id {get; private set;}
    public string name {get; private set;}
    public int type {get; private set;}
    public int buildings {get; private set;}
    public int traits {get; private set;}
    public int traitsHidden {get; private set;}

    public List<int> personList {get; private set;} //값은 Person의 id이다.

    public List<Action> actionList {get; private set;}
    public List<string> reportList {get; private set;}

    public City(int id, string name, int type, int buildings, int traits, List<int> personList)
    {
        this.id = id;
        this.name = name;
        this.type = type;
        this.buildings = buildings;
        this.traits = traits;
        this.personList = personList;

        actionList = new List<Action>();
        reportList = new List<string>();
    }

    public void ChangeInfo(City changedCity)
    {
        this.id = changedCity.id;
        this.name = changedCity.name;
        this.type = changedCity.type;
        this.buildings = changedCity.buildings;
        this.traits = changedCity.traits;
        this.personList = changedCity.personList;
    }

    public void ChangeId(int newId)
    {
        this.id = newId;
    }

    public void ChangeName(string newName)
    {
        this.name = newName;
    }

    public void ChangeType(int newType)
    {
        this.type = newType;
    }

    public void ChangeBuildings(int newBuildings)
    {
        this.buildings = newBuildings;
    }

    public void ChangeTraits(int newTraits)
    {
        this.traits = newTraits;
    }

    public void ChangePersonList(List<int> newPersonList)
    {
        this.personList = newPersonList;
    }

    public void AddAction(Action newAction)
    {
        actionList.Add(newAction);
    }

    public void AddReport(string text)
    {
        reportList.Add(text);
    }

    public bool CheckBuilding(int num)
    {
        return ((1 << num) & buildings) > 0;
    }
}