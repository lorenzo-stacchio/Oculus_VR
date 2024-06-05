public class DataModel
{


    // Properties to store data
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string ID { get; set; }


    public string ObjectName { get; set; }

    public string Modality { get; set; }

    public string Phase { get; set; }
    public string Step { get; set; }
    public string typeLog { get; set; }

    public string? Value { get; set; }

    public System.DateTime TimeStamp { get; set; }


    public DataModel(string FirstName, string LastName, string ID, string Modality, string ObjectName, string Phase, string Step, string typeLog, System.DateTime TimeStamp, string? Value)
    {
        this.FirstName = FirstName;
        this.LastName = LastName;
        this.ID = ID;
        this.ObjectName = ObjectName;
        this.Modality = Modality;
        this.Phase = Phase;
        this.Step = Step;
        this.typeLog = typeLog;
        this.Value = Value;
        this.TimeStamp = TimeStamp;
    }


    // Override the ToString method to create a custom string representation
    public override string ToString()
    {
        return $"{this.FirstName},{this.LastName},{this.ID},{this.ObjectName},{this.Modality},{this.Phase},{this.Step},{this.typeLog},{this.Value},{this.TimeStamp}";
    }

}