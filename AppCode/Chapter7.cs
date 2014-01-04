using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

public class CalenderEvent
{
    public string Title { get; set; }
    public DateTimeOffset StartTime { get; set; }
    public TimeSpan Duration { get; set; }
}

/// <summary>
/// 實做自訂排序
/// </summary>
public class CalenderEvent2 : IComparable<CalenderEvent2>
{
    public string Title { get; set; }
    public DateTimeOffset StartTime { get; set; }
    public TimeSpan Duration { get; set; }

    public int CompareTo(CalenderEvent2 other)
    {
        if (other == null) { return 1; }
        return StartTime.CompareTo(other.StartTime);
    }
}

/// <summary>
/// 建立Array範本Class
/// </summary>
public class GetEvents
{
    public CalenderEvent[] CalenderEvent { get; set; }

    public GetEvents()
    {
        CalenderEvent[] events = 
            {
                new CalenderEvent
                {
                    Title = "Swing Dancing ar the South Bank",
                    StartTime = new DateTimeOffset(2009, 7, 11, 15, 00, 00, TimeSpan.Zero),
                    Duration = TimeSpan.FromHours(4)
                },
                new CalenderEvent
                {
                    Title = "Saturday Night Swing",
                    StartTime = new DateTimeOffset(2009, 7, 11, 19, 30, 00, TimeSpan.Zero),
                    Duration = TimeSpan.FromHours(6.5)
                },
                new CalenderEvent
                {
                    Title = "Formula 1 German Grand Prix",
                    StartTime = new DateTimeOffset(2009, 7, 12, 12, 10, 00, TimeSpan.Zero),
                    Duration = TimeSpan.FromHours(3)
                },
                new CalenderEvent
                {
                    Title = "Swing Dance Picnic",
                    StartTime = new DateTimeOffset(2009, 7, 12, 15, 00, 00, TimeSpan.Zero),
                    Duration = TimeSpan.FromHours(4)
                },
                new CalenderEvent
                {
                    Title = "Stopmpin' at the 100 Club",
                    StartTime = new DateTimeOffset(2009, 7, 13, 19, 45, 00, TimeSpan.Zero),
                    Duration = TimeSpan.FromHours(5)
                }
            };
        CalenderEvent = events;
    }
}

public class GetEvents2
{
    public CalenderEvent2[] CalenderEvent2 { get; set; }

    public GetEvents2()
    {
        CalenderEvent2[] events = 
            {
                new CalenderEvent2
                {
                    Title = "Swing Dancing ar the South Bank",
                    StartTime = new DateTimeOffset(2009, 7, 11, 15, 00, 00, TimeSpan.Zero),
                    Duration = TimeSpan.FromHours(4)
                },
                new CalenderEvent2
                {
                    Title = "Saturday Night Swing",
                    StartTime = new DateTimeOffset(2009, 7, 11, 19, 30, 00, TimeSpan.Zero),
                    Duration = TimeSpan.FromHours(6.5)
                },
                new CalenderEvent2
                {
                    Title = "Formula 1 German Grand Prix",
                    StartTime = new DateTimeOffset(2009, 7, 12, 12, 10, 00, TimeSpan.Zero),
                    Duration = TimeSpan.FromHours(3)
                },
                new CalenderEvent2
                {
                    Title = "Swing Dance Picnic",
                    StartTime = new DateTimeOffset(2009, 7, 12, 15, 00, 00, TimeSpan.Zero),
                    Duration = TimeSpan.FromHours(4)
                },
                new CalenderEvent2
                {
                    Title = "Stopmpin' at the 100 Club",
                    StartTime = new DateTimeOffset(2009, 7, 13, 19, 45, 00, TimeSpan.Zero),
                    Duration = TimeSpan.FromHours(5)
                }
            };
        CalenderEvent2 = events;
    }
}

/// <summary>
/// 使用不存任何元素的陣列模式
/// </summary>
public class Indexable
{
    public string this[int index]
    {
        get
        {
            return "Item " + index;
        }
        set
        {
 
        }
    }
}

/// <summary>
/// 使用泛型(T)做陣列的宣告即使用
/// </summary>
/// <typeparam name="T"></typeparam>
public class ArrayAndIndexer<T>
{
    public T[] TheArray = new T[100];
    public T this[int index] 
    {
        get 
        {
            return TheArray[index];
        }
        set
        {
            TheArray[index] = value;
        }
    }
}

/// <summary>
/// 泛型使用的建構式型別
/// </summary>
struct CanChange
{
    public int Number { get; set; }
    public string Name { get; set; }
}
