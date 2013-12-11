using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Text;

/// <summary>
/// 抽像的消防員類別，必須實作它
/// </summary>
public abstract class FirefighterBase : INamedPerson, ISalariedPerson
{
    public string Name { get; set; }
    public decimal Salary { get; set; }

    public abstract string ExtinguishFire();

    public void Drive(Firetruck truckToDrive, Point coordinates)
    {
        if (truckToDrive.Driver != this)
        {
            return;
        }
        truckToDrive.Drive(coordinates);
    }
}

/// <summary>
/// 消防員(繼承消防員抽象類別)
/// </summary>
public class Firefighter : FirefighterBase
{
    public override string ExtinguishFire()
    {
        string trainHoseOnfire = TrainHoseOnFire();
        string trunOnHose = TurnOnHose();
        return trunOnHose + "<br>" + trainHoseOnfire + "<br>" + Name + " is putting out the fire!";
    }

    protected virtual string TurnOnHose()
    {
        return "The fire is going out.";
    }

    protected virtual string TrainHoseOnFire()
    {
        return "Training the hose on the fire.";
    }
}

/// <summary>
/// 隊長(繼承消防員的類別)
/// </summary>
public sealed class FireChief : Firefighter
{
    public Firefighter NumberOne { get; set; }

    public string TellFirefighterToExtinguishFire(Firefighter colleafue)
    {
        return colleafue.ExtinguishFire();
    }

    /// <summary>
    /// 用new關鍵字複寫原有的ExtinguishFire方法
    /// </summary>
    /// <returns></returns>
    //public new string ExtinguishFire()
    //{
    //    return TellFirefighterToExtinguishFire(NumberOne);
    //}

    public override string ExtinguishFire()
    {
        return TellFirefighterToExtinguishFire(NumberOne);
    }
}

/// <summary>
/// 實習消防員(繼承消防員抽象類別)
/// </summary>
public class TraineeFirefighter : Firefighter
{
    private bool hoseTrainedOnFire;

    protected override string TurnOnHose()
    {
        if (hoseTrainedOnFire)
        {
            return "The fire is going out.";
        }
        else
        {
            return "There's water going everywhere!";
        }
    }

    protected override string TrainHoseOnFire()
    {
        hoseTrainedOnFire = true;
        return "Training the hose on the fire.";
    }
}

/// <summary>
/// 消防車
/// </summary>
public class Firetruck
{
    //消防員
    public Firefighter Driver { get; set; }
    //水管
    public Hose Hose { get; set; }
    //升降梯
    readonly Ladder ladder = new Ladder() { Length = 30.0 };
    //
    public void Drive(Point coordinates)
    {
        if (Driver == null)
        {
            return;
        }
        string message = "Drining to " + coordinates;
    }
    //
    public Ladder Ladder
    {
        get { return ladder; }
    }
}

/// <summary>
/// 升降梯
/// </summary>
public class Ladder
{
    public double Length { get; set; }
}

/// <summary>
/// 水管
/// </summary>
public class Hose
{ }

/// <summary>
/// 管理人員，繼承兩個實作介面(名字、薪水)
/// </summary>
public class Administrator : INamedPerson, ISalariedPerson
{
    private decimal salary;
    //
    public string Title { get; set; }
    public string Forename { get; set; }
    public string Surname { get; set; }
    public string Name 
    {
        get
        {
            StringBuilder name = new StringBuilder();
            AppendWithSpace(name, Title);
            AppendWithSpace(name, Forename);
            AppendWithSpace(name, Surname);
            return name.ToString();
        }
    }

    public decimal Salary { get; set; }

    private void AppendWithSpace(StringBuilder builder, string stringToAppend)
    {
        if (string.IsNullOrEmpty(stringToAppend))
        {
            return;
        }
        if (builder.Length > 0)
        {
            builder.Append(" ");
        }
        builder.Append(stringToAppend);
    }
}

/// <summary>
/// 消防站
/// </summary>
public class FireStation : IClockIn
{
    List<INamedPerson> clockedInStaff = new List<INamedPerson>();

    public INamedPerson ClockIn(INamedPerson staffMember)
    {
        if (!clockedInStaff.Contains(staffMember))
        {
            clockedInStaff.Add(staffMember);
            return staffMember;
        }
        return null;
    }

    public string RollCall()
    {
        string returnValue = "";
        foreach (INamedPerson staffMember in clockedInStaff)
        {
            if (returnValue != "") { returnValue += ","; }
            returnValue += staffMember.Name;
        }
        return returnValue;
    }

    /// <summary>
    /// 實作介面判斷型別可導到相對應的方法
    /// </summary>
    /// <param name="item"></param>
    /// <returns></returns>
    public string ClockIn(object item)
    {
        INamedPerson namedPerson = item as INamedPerson;
        if (namedPerson != null)
        {
            return ClockIn(item as INamedPerson).Name;
        }
        else
        {
            return "";
        }
    }
}

/// <summary>
/// 點名
/// </summary>
public interface IClockIn
{
    string ClockIn(object item);
}

/// <summary>
/// 名字
/// </summary>
public interface INamedPerson
{
    string Name
    {
        get;
    }
}

/// <summary>
/// 薪水
/// </summary>
public interface ISalariedPerson
{
    decimal Salary { get; set; }
}

public interface ISettableNamePerson
{
    string Name { get; set;}
}


public class AFootInBothCamps : INamedPerson, ISettableNamePerson
{
    private string settableName;
    //
    public string Name
    {
        get
        {
            return settableName;
        }
    }
    //
    string ISettableNamePerson.Name
    {
        get 
        {
            return settableName;
        }
        set
        {
            if (settableName != null && settableName.Contains(" "))
            {
                return;
            }
            settableName = value;
        }
    }
}
