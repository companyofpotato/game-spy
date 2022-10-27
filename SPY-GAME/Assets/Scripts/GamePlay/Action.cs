using System.Collections.Generic;

/*
- what
0 : InvestigateRegion
1 : Deploy
2 : Withdrawal
3 : Assassination
4 : persuasion
*/

public class Action
{
    public int when {get; private set;}
    public int what {get; private set;}
    public int who {get; private set;}
    public int where {get; private set;}
    public int whom {get; private set;}
    public int how {get; private set;}
    public List<bool> usedEquipmentList {get; private set;}
    public int befOdd {get; private set;}
    public int successOdd {get; private set;}
    public int aftOdd {get; private set;}
    public int escapeOdd {get; private set;}

    public bool bef {get; private set;}//행동 전 발각 여부
    
    public int success {get; private set;}
    //0 : 실패
    //양수 : 성공
    //1 ~ 12 : 목표 부상(암살)
    //13 : 목표 사망(암살)

    public bool aft {get; private set;}//행동 후 발각 여부
    public int escape {get; private set;}//탈출 여부
    //0 : 탈출 성공, 부상 없음
    //1 ~ 12 : 탈출 성공, 부상
    //13 : 탈출 중 사망
    //14 : 탈출 실패 및 생포
    //15 : 탈출 실패 및 실종

    public Action(int what, int who, int where, int whom, int how, List<bool> equipmentList, int bef, int success, int aft, int escape)
    {
        this.what = what;
        this.who = who;
        this.where = where;
        this.whom = whom;
        this.how = how;
        this.usedEquipmentList = equipmentList;
        this.befOdd = bef;
        this.successOdd = success;
        this.aftOdd = aft;
        this.escapeOdd = escape;

        this.when = -1;
        this.bef = false;
        this.success = -1;
        this.aft = false;
        this.escape = -1;
    }

    public void Executed(int when, bool bef, int success, bool aft, int escape)
    {
        this.when = when;
        this.bef = bef;
        this.success = success;
        this.aft = aft;
        this.escape = escape;
    }

    public string MakeText(bool includeTurn, bool includeCodename)
    {
        string result = "", pronoun;
        if(includeTurn)
            result = $"Turn {when} : ";

        result += $"In city {CityManager.cityList[where].name}, ";

        if(includeCodename)
            result += $"codename {PersonManager.personList[who].codename} ";

        switch(what)
        {
            case 0 : result += "investigated city."; break;
            case 1 : result += $"tried to deploy to city {CityManager.cityList[whom].name}."; break;
            case 2 : result += $"tried to withdrawn to {CityManager.cityList[whom].name}."; break;
            case 3 : result += $"tried to assassinate Codename {PersonManager.personList[whom].codename} with {MethodName()}."; break;
            case 4 : result += $"tried to persuade Codename {PersonManager.personList[whom].codename}."; break;
        }

        if(PersonManager.personList[who].gender)
            pronoun = "He";
        else
            pronoun = "She";

        if(bef)
        {
            result += $" {pronoun} was discovered before mission and ";
            switch(escape)
            {
                case 0 : result += "successfully escaped."; break;
                case 13 : result += "died during escaping."; break;
                case 14 : result += "captured during escaping."; break;
                case 15 : result += "went missing during escaping."; break;
                case 1 : result += "escaped, but wounded for 1 turn."; break;
                default : result += $"escaped, but wounded for {escape.ToString()} turns."; break;
            }
        }
        else
        {
            switch(success)
            {
                case 0 : result += $" {pronoun} failed mission."; break;
                default : result += $" {pronoun} succeeded mission."; break;
            }
            switch(what)
            {
                case 3 : 
                    switch(success)
                    {
                        case 1 : result += $" Codename {PersonManager.personList[whom].codename} is wounded for 1 turn."; break;
                        case 13 : result += $" Codename {PersonManager.personList[whom].codename} is assassinated."; break;
                        default : result += $" Codename {PersonManager.personList[whom].codename} is wounded for {success} turns."; break;
                    }
                    break;
            }

            if(aft)
            {
                result += $" After mission, {pronoun} was discovered and ";
                switch(escape)
                {
                    case 0 : result += "successfully escaped."; break;
                    case 13 : result += "died during escaping."; break;
                    case 14 : result += "captured during escaping."; break;
                    case 15 : result += "went missing during escaping."; break;
                    case 1 : result += "escaped, but wounded for 1 turn."; break;
                    default : result += $"escaped, but wounded for {escape.ToString()} turns."; break;
                }
            }
            else
            {
                result += $" After mission, {pronoun} escaped without suspect.";
            }
        }

        return result;
    }

    public string MethodName()
    {
        string result = "";
        switch(what)
        {
            case 3 : 
                switch(how)
                {
                    case 0 : result = "Pistol"; break;
                    case 1 : result = "Sniper Rifle"; break;
                    case 2 : result = "Small Bomb"; break;
                    case 3 : result = "Big Bomb"; break;
                }
                break;
        }

        return result;
    }

    public void ChangeAftOdd(int n)
    {
        aftOdd = n;
    }
}