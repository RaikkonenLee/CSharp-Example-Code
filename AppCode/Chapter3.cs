using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

class Plane
{
    static Plane()
    { }

    /// <summary>
    /// 建構子(Constructer)
    /// </summary>
    /// <param name="newIdentifier"></param>
    public Plane(string newIdentifier)
    {
        _identifier = newIdentifier;
    }

    private readonly string _identifier;
    private const double feetPerMile = 5280;

    public enum DirectionOfApproaching : int
    {
        Approaching = 0,
        Leaving = 1
    }

    public DirectionOfApproaching Direction { get; set; }

    public PolarPoint3D Position { get; set; }

    public string Identifier { get { return _identifier; } }

    //public double SpeedInMilesPerHour { get; set; }

    //public double SpeedInKilometersPerHour 
    //{
    //    get
    //    {
    //        return SpeedInMilesPerHour * 1.609344;
    //    }
    //    set
    //    {
    //        SpeedInMilesPerHour = value / 1.609344;
    //    }
    //}

    public double SpeedInKilometersPerHour { get; set; }

    public double SpeedInMilesPerHour
    {
        get
        {
            return SpeedInKilometersPerHour / 1.609344;
        }
        set
        {
            //_identifier += ": speed modified to " + value;
            SpeedInKilometersPerHour = value * 1.609344;
        }
    }

    public void SendMessage(string messageName, TimeSpan delay = default(TimeSpan))
    {
    }

    public void UpdatePosition(double minutesToAdvance)
    {
        double hours = minutesToAdvance / 60.0;
        double milesMoved = this.SpeedInMilesPerHour * hours;
        double milesToTower = this.Position.Distance;
        if (this.Direction == DirectionOfApproaching.Approaching)
        {
            milesToTower -= milesMoved;
            if (milesToTower < 0)
            {
                // We've arrived!
                milesToTower = 0;
            }
        }
        else
        {
            milesToTower += milesMoved;
        }
        PolarPoint3D newPosition = new PolarPoint3D(milesToTower, this.Position.Angle, this.Position.Altitude);
    }

    public static bool TooClose(Plane first, Plane second, double minimumMiles)
    {
        double x1 = first.Position.Distance * Math.Cos(first.Position.Angle);
        double x2 = second.Position.Distance * Math.Cos(second.Position.Angle);
        double y1 = first.Position.Distance * Math.Sin(first.Position.Angle);
        double y2 = second.Position.Distance * Math.Sin(second.Position.Angle);
        double z1 = first.Position.Altitude / feetPerMile;
        double z2 = second.Position.Altitude / feetPerMile;
        double dx = x1 - x2;
        double dy = y1 - y2;
        double dz = z1 - z2;
        double distanceSquared = dx * dx + dy * dy + dz * dz;
        double minimumSquared = minimumMiles * minimumMiles;
        return distanceSquared < minimumSquared;
    }

}


struct PolarPoint3D
{
    public PolarPoint3D(double distance, double angle, double altitude) : this()
    {
        Distance = distance;
        Angle = angle;
        Altitude = altitude;
    }

    public PolarPoint3D(double distance, double angle)
        : this(distance, angle, 0)
    { }
    
    public double Distance { get; set; }
    public double Angle { get; private set; }
    public double Altitude { get; private set; }

    public string abc()
    {
        return "HaHaHa!!";
    }
}


