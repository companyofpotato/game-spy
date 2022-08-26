public class Person
{
    public int id {get; private set;}
    public bool gender {get; private set;}
    public string name {get; private set;}
    public int age {get; private set;}
    public string codename {get; private set;}
    public int status {get; private set;}
    public int country {get; private set;}
    public int location {get; private set;} //거주 지역의 id
    public int exposure {get; private set;}
    public int appearance {get; private set;}
    public int sexual {get; private set;}
    public int personality {get; private set;}
    public int job {get; private set;}
    public int belong {get; private set;}
    public bool hidden {get; private set;}
    public int trait {get; private set;}

    public Person()
    {

    }

    public void SetNewPerson(int id, string name)
    {
        this.id = id;
        this.name = name;
    }

}