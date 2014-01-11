using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// this的範例，附加在string的方法裡
/// </summary>
public static class StringAdditions
{
    public static string Backwards(this string input)
    {
        char[] characters = input.ToCharArray();
        Array.Reverse(characters);
        return new string(characters);
    }
}

/// <summary>
/// 自訂LINQ的Where及Select
/// </summary>
public class Foo
{
    public string Name { get; set; }
    public Foo Where(Func<Foo, bool> predicate)
    {
        return this;
    }
    public TResult Select<TResult>(Func<Foo, TResult> selector)
    {
        return selector(this);
    }
}

public class Phone
{
    public string Name { get; set; }
    public DateTimeOffset Time { get; set; }
}

