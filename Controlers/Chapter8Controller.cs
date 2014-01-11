using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using System.Collections;

namespace Programing_CSharp_5._0.Controllers
{
    public class Chapter8Controller : Controller
    {
        //
        // GET: /Chapter8/
        /// <summary>
        /// 讀取檔案目錄
        /// </summary>
        /// <returns></returns>
        public ActionResult Process1()
        {
            string returnValue = "";
            //
            //使用查詢表達式(Query Expression)
            var bigFiles = from file in GetAllFilesInDirectory(@"c:\Programing\EBook\")
                           let info = new FileInfo(file)
                           where info.Length > 10000000
                           select info;
            foreach (FileInfo info in bigFiles)
            {
                if (returnValue != "") { returnValue += "<br>"; }
                returnValue += info.ToString();  
            }
            //
            //使用Lambda表達式(Lambda Expression)
            var bigFiles2 = GetAllFilesInDirectory(@"c:\Programing\EBook\").
                Where(file => new FileInfo(file).Length > 10000000).
                Select(file => "File: " + file);
            return Content(returnValue);
        }

        /// <summary>
        /// 增加類別的方法
        /// </summary>
        /// <returns></returns>
        public ActionResult Process2()
        {
            string stationName = "Finsbury Park 12345 我是誰??";
            //
            Foo source = new Foo() { Name = "Fred" };
            var result = from f in source
                         where f.Name == "Fred"
                         select f.Name;
            //在此單純示範可以自行增加Where或Select
            var result2 = source.Where(f => f.Name == "Fred").Select(f => f.Name);
            return Content(stationName.Backwards());
        }

        /// <summary>
        /// 在LINQ裡利用變數去改變查詢的結果
        /// </summary>
        /// <returns></returns>
        public ActionResult Process3()
        {
            string returnValue = "";
            int minSize = 10000;
            var bigFiles = from file in GetAllFilesInDirectory(@"c:\Programing\EBook\")
                           where new FileInfo(file).Length > minSize
                           select file;
            var filesOver10k = bigFiles.ToArray();
            minSize = 100000;
            var filesOver100k = bigFiles.ToArray();
            minSize = 1000000;
            var filesOver1MB = bigFiles.ToArray();
            minSize = 10000000;
            var filesOver10MB = bigFiles.ToArray();
            //
            foreach (string file in filesOver10k)
            {
                if (returnValue != "") { returnValue += "<br>"; }
                returnValue += file;
            }
            returnValue += "<br>";
            returnValue += "--------------------------------------";
            foreach (string file in filesOver100k)
            {
                if (returnValue != "") { returnValue += "<br>"; }
                returnValue += file;
            }
            returnValue += "<br>";
            returnValue += "--------------------------------------";
            foreach (string file in filesOver1MB)
            {
                if (returnValue != "") { returnValue += "<br>"; }
                returnValue += file;
            }
            returnValue += "<br>";
            returnValue += "--------------------------------------";
            foreach (string file in filesOver10MB)
            {
                if (returnValue != "") { returnValue += "<br>"; }
                returnValue += file;
            }
            returnValue += "<br>";
            returnValue += "--------------------------------------";
            return Content(returnValue);
        }

        /// <summary>
        /// LINQ的Order及Group
        /// </summary>
        /// <returns></returns>
        public ActionResult Process4()
        {
            string returnValue = "";
            GetEvents getEvents = new GetEvents();
            CalendarEvent[] events = getEvents.CalenderEvent;
            //
            var eventsByStartTime = from ev in events
                                    orderby ev.StartTime ascending
                                    select ev;
            //
            var eventsByStartTime2 = events.OrderBy(ev => ev.StartTime);
            //
            var eventsByDay = from ev in events
                              group ev by ev.StartTime.Date;
            //
            var eventsByDay2 = from ev in events
                               group ev by ev.StartTime.Date into dayGroup
                               select dayGroup.ToArray();
            //
            foreach (var day in eventsByDay)
            {
                returnValue += "<br>Events for " + day.Key;
                foreach (var item in day)
                {
                    returnValue += "<br>" + item.Title;
                }
            }
            //
            foreach (var day in eventsByDay2)
            {
                returnValue += "<br>Events for " + day[0].StartTime.Date;
                for (int i = 0; i < day.Length; i++)
                {
                    returnValue += "<br>" + day[i].Title;    
                }
            }
            return Content(returnValue);
        }

        /// <summary>
        /// 同時取兩個來源
        /// </summary>
        /// <returns></returns>
        public ActionResult Process5()
        {
            string returnValue = "";
            GetEvents getEvents = new GetEvents();
            CalendarEvent[] events = getEvents.CalenderEvent;
            //
            var eventsByDay = from ev in events
                              group ev by ev.StartTime.Date;
            //
            var items = from day in eventsByDay
                        from item in day
                        select item;
            foreach (CalendarEvent item in items)
            {
                if (returnValue != "") { returnValue += "<br>"; }
                returnValue += item.Title;
            }
            //
            returnValue += "<br>itrms2";
            List<CalendarEvent> items2 = new List<CalendarEvent>();
            foreach (IGrouping<DateTime, CalendarEvent> day in eventsByDay)
            {
                foreach (CalendarEvent item in day)
                {
                    items2.Add(item);
                }
            }
            foreach (CalendarEvent item in items2)
            {
                if (returnValue != "") { returnValue += "<br>"; }
                returnValue += item.Title;
            }
            return Content(returnValue);
        }

        /// <summary>
        /// 兩個來源資料做運算
        /// </summary>
        /// <returns></returns>
        public ActionResult Process6()
        {
            string returnValues = "";
            int[] numbers = { 1, 2, 3, 4, 5 };
            var multipied = from x in numbers
                            from y in numbers
                            select x * y;
            //
            foreach (int n in multipied)
            {
                if (returnValues != "") { returnValues += "<br>"; }
                returnValues += n;
            }
            //
            var multipied2 = numbers.SelectMany(x => numbers,
                (x, y) => x * y);
            //
            returnValues += "<br>multipied2";
            foreach (int n in multipied2)
            {
                if (returnValues != "") { returnValues += "<br>"; }
                returnValues += n;
            }
            return Content(returnValues);
        }

        /// <summary>
        /// 作序號
        /// </summary>
        /// <returns></returns>
        public ActionResult Process7()
        {
            string returnValue = "";
            GetEvents getEvents = new GetEvents();
            CalendarEvent[] events = getEvents.CalenderEvent;
            var numberedEvents = events.Select((ev, i) => string.Format("{0}: {1}", i + 1, ev.Title));
            //
            foreach (string item in numberedEvents)
            {
                if (returnValue != "") { returnValue += "<br>"; }
                returnValue += item;
            }
            return Content(returnValue);
        }

        /// <summary>
        /// 實作All、Any、Sum、Average、Min、Max、Aggregate
        /// </summary>
        /// <returns></returns>
        public ActionResult Process8()
        {
            string returnValue = "";
            GetEvents getEvents = new GetEvents();
            CalendarEvent[] events = getEvents.CalenderEvent;
            DateTimeOffset newEventStart = new DateTimeOffset(2009, 7, 20, 19, 45, 00, TimeSpan.Zero);
            TimeSpan newEventDuration = TimeSpan.FromHours(5);
            bool overlaps = events.Any(
                ev => TimesOverlap(ev.StartTime, ev.Duration, newEventStart, newEventDuration));
            bool noOverlaps = events.All(
                ev => TimesOverlap(ev.StartTime, ev.Duration, newEventStart, newEventDuration));
            //總數、平均、最大、最小
            events.Sum(ev => ev.Duration.TotalHours);
            events.Average(ev => ev.Duration.TotalHours);
            events.Max(ev => ev.Duration.TotalHours);
            events.Min(ev => ev.Duration.TotalHours);
            //計算集合裡的數量
            int count = events.Aggregate(0, (c, ev) => c + 1);
            //利用Aggregate計算加總數
            double hours = events.Aggregate(0.0, (total, ev) => total + ev.Duration.TotalHours);
            //利用Aggregate計算平均值
            double averageHours = events.Aggregate(
                new { TotalHours = 0.0, Count = 0 },
                (agg, ev) => new 
                    {
                        TotalHours = agg.TotalHours + ev.Duration.TotalHours,
                        Count = agg.Count + 1
                    },
                (agg) => agg.TotalHours / agg.Count);
            //
            return Content(overlaps.ToString() + "<br>" + 
                noOverlaps.ToString() + "<br>" +
                count.ToString());
        }

        /// <summary>
        /// 實作Join
        /// </summary>
        /// <returns></returns>
        public ActionResult Process9()
        {
            string returnValue = "";
            GetEvents getEvents = new GetEvents();
            CalendarEvent[] events = getEvents.CalenderEvent;
            Phone[] phoneEvents = 
            {
                new Phone(){ Name = "", Time = new DateTimeOffset(2009, 7, 11, 15, 00, 00, TimeSpan.Zero)}
            }; 

            //
            var pairs = from localEvent in events
                        join phoneEvent in phoneEvents
                        on new { Title = localEvent.Title, Start = localEvent.StartTime }
                        equals new { Title = phoneEvent.Name, Start = phoneEvent.Time }
                        select new { Local = localEvent, phone = phoneEvent };
            //
            return View();
        }

        /// <summary>
        /// 轉型
        /// </summary>
        /// <returns></returns>
        public ActionResult ProcessA()
        {
            string returnValue = "";
            GetEvents getEvents = new GetEvents();
            CalendarEvent[] events = getEvents.CalenderEvent;
            //
            var eventsByDay = from ev in events
                              group ev by ev.StartTime.Date into dayGroup
                              select dayGroup.ToArray();
            //
            ArrayList fruits = new ArrayList();
            fruits.Add("mango");
            fruits.Add("apple");
            fruits.Add("lemon");
            IEnumerable<string> query =
                fruits.Cast<string>().Select(fruit => fruit);
            foreach (string fruit in query)
            {
                if (fruit != "") { returnValue += "<br>"; }
                returnValue += fruit;
            }
            //
            return Content(returnValue);
        }

        /// <summary>
        /// 回傳檔案目錄的清單
        /// </summary>
        /// <param name="directoryPath"></param>
        /// <returns></returns>
        public static IEnumerable<string> GetAllFilesInDirectory(string directoryPath)
        {
            return Directory.EnumerateFiles(directoryPath, "*", SearchOption.AllDirectories);
        }

        /// <summary>
        /// 比較大小
        /// </summary>
        /// <param name="startTime1"></param>
        /// <param name="duration1"></param>
        /// <param name="startTime2"></param>
        /// <param name="duration2"></param>
        /// <returns></returns>
        public static bool TimesOverlap(DateTimeOffset startTime1, TimeSpan duration1,
            DateTimeOffset startTime2, TimeSpan duration2)
        {
            DateTimeOffset end1 = startTime1 + duration1;
            DateTimeOffset end2 = startTime2 + duration2;
            //
            return (startTime1 < startTime2) ?
                (end1 > startTime2) :
                (startTime1 < end2);
        }

        
	}
}
