using System.Collections.Generic;

public class Action
{
    public int type {get; private set;}
    public int who {get; private set;}
    public int where {get; private set;}
    public int target {get; private set;}
    public int how {get; private set;}
    public List<bool> usedEquipmentList {get; private set;}
    public int bef {get; private set;}
    public int success {get; private set;}
    public int aft {get; private set;}
    public int escape {get; private set;}

    public Action(int type, int who, int where, int target, int how, List<bool> equipmentList, int bef, int success, int aft, int escape)
    {
        this.type = type;
        this.who = who;
        this.where = where;
        this.target = target;
        this.how = how;
        this.usedEquipmentList = equipmentList;
        this.bef = bef;
        this.success = success;
        this.aft = aft;
        this.escape = escape;
    }
}

/*
- type
0 : InvestigateRegion
1 : Deploy
2 : Withdrawal
3 : Assassination
*/