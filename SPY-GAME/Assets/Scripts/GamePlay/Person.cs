/*
- status
-15 : 인물이 실종된 상태
-14 : 인물이 생포된 상태
-13 : 인물이 사망한 상태
-12 ~ -1 : 인물이 부상당한 상태, 숫자만큼 턴을 쉰다. 매 턴 종료 시 해당 값이 1씩 증가한다.
0 : 행동 가능한 상태
1 : Deploy
2 : Withdrawal
3 : Assassination
4 : Persuasion

- isTargeted
일단은 status의 양수 값과 똑같이 간다.

- belong
-3 : 적군인 척하는 아군인 척하는 적군(적군의 이중 첩자)
-2 : 적군인 척하는 아군(아군의 이중 첩자)
-1 : 적군
 0 : 중립
 1 : 아군
 2 : 아군인 척하는 적군(적군의 이중 첩자)
 3 : 아군인 척하는 적군인 척하는 아군(아군의 이중 첩자)

- trait
- 0번째 : 이중 첩자 함정
*/

using System.Collections.Generic;

public class Person
{
    public int id {get; private set;}
    public bool isAgent {get; private set;}
    public int reveal {get; private set;} //비트맵 인덱스 위치의 값이 1이면 드러난 것이다.
    public int isReal {get; private set;} //passion, exposure, aim, stealth, handicraft, analysis, narration
    public string firstName {get; private set;}
    public string familyName {get; private set;}
    public string codename {get; private set;}
    public int age {get; private set;}
    public bool gender {get; private set;}
    public bool sexualHomo {get; private set;}
    public bool sexualHetero {get; private set;}
    public int appearance {get; private set;}
    public int status {get; private set;}
    public int isTargeted {get; private set;}
    public int belong {get; private set;}

    public int passion {get; private set;}
    public int passionReal {get; private set;}

    public int location {get; private set;} //거주 지역의 id

    public int exposure {get; private set;}
    public int exposureReal {get; private set;}

    public int rank {get; private set;}

    public int aim {get; private set;}
    public int aimReal {get; private set;}

    public int stealth {get; private set;}
    public int stealthReal {get; private set;}

    public int handicraft {get; private set;}
    public int handicraftReal {get; private set;}

    public int analysis {get; private set;}
    public int analysisReal {get; private set;}

    public int narration {get; private set;}
    public int narrationReal {get; private set;}

    public int trait {get; private set;}
    public int traitReal {get; private set;}
    public int traitReveal {get; private set;}

    public int perk {get; private set;}
    public int perkReal {get; private set;}
    public int perkReveal {get; private set;}

    public List<Action> actionList {get; private set;}
    public List<Action> doubleList {get; private set;}
    public string reportText {get; private set;}

/*
    public Person()
    {

    }
*/

    public Person(int id, bool isAgent, string codename, int reveal, int isReal, string firstName, string familyName, int age, bool gender, bool sexualHomo, bool sexualHetero, int appearance, int status, int belong, int passion, int location, int exposure, int rank, int aim, int stealth, int handicraft, int analysis, int narration, int trait, int perk)
    {
        this.id = id;
        this.isAgent = isAgent;
        this.codename = codename;
        this.reveal = reveal;
        this.isReal = isReal;
        this.firstName = firstName;
        this.familyName = familyName;
        this.age = age;
        this.gender = gender;
        this.sexualHomo = sexualHomo;
        this.sexualHetero = sexualHetero;
        this.appearance = appearance;
        this.status = status;
        this.isTargeted = 0;
        this.belong = belong;

        this.passion = passion;
        this.passionReal = passion;

        this.location = location;

        this.exposure = exposure;
        this.exposureReal = exposure;

        this.rank = rank;

        this.aim = aim;
        this.stealth = stealth;
        this.handicraft = handicraft;
        this.analysis = analysis;
        this.narration = narration;

        this.aimReal = aim;
        this.stealthReal = stealth;
        this.handicraftReal = handicraft;
        this.analysisReal = analysis;
        this.narrationReal = narration;

        this.trait = trait;
        this.traitReal = trait;
        this.traitReveal = 0;

        this.perk = perk;
        this.perkReal = perk;
        this.perkReveal = 0;

        actionList = new List<Action>();
        doubleList = new List<Action>();
        reportText = "";
    }

    public void ChangeStatus(int after)
    {
        this.status = after;
    }

    public void ChangeCity(int after)
    {
        this.location = after;
    }

    public void ChangeBelong(int after)
    {
        this.belong = after;
    }

    public void ChangeIsTargeted(int after)
    {
        this.isTargeted = after;
    }

    public void AddTrait(int add)
    {
        this.trait |= 1 << add;
    }

    public void RemoveTrait(int remove)
    {
        this.trait &= ~(1 << remove);
    }

    public void AddPerk(int add)
    {
        this.perk |= 1 << add;
    }

    public void RemovePerk(int remove)
    {
        this.perk &= ~(1 << remove);
    }

    public bool CheckReveal(int check)
    {
        return ((this.reveal & (1 << check)) > 0);
    }

    public bool CheckReal(int check)
    {
        return ((this.isReal & (1 << check)) > 0);
    }

    public void AddAction(Action newAction)
    {
        actionList.Add(newAction);
    }

    public void AddReport(string text)
    {
        reportText += text + "\n";
    }

    public void AddDoubleAction(Action newAction)
    {
        doubleList.Add(newAction);
    }
}

