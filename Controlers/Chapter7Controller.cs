using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Diagnostics;
using System.IO;

namespace Programing_CSharp_5._0.Controllers
{
    public class Chapter7Controller : Controller
    {
        //
        // GET: /Chapter7/
        /// <summary>
        /// 建立以Class為基礎的Array
        /// </summary>
        /// <returns></returns>
        public ActionResult Process1()
        {
            GetEvents getEvents = new GetEvents();
            CalenderEvent[] events = getEvents.CalenderEvent;
            //
            string title = events[2].Title;
            events[2] = events[0];
            return Content(title + "_" + events[2].Title);
        }

        /// <summary>
        /// 建立參考同一個Class的Array
        /// </summary>
        /// <returns></returns>
        public ActionResult Process2()
        {
            CalenderEvent theOnlyEvent = new CalenderEvent() 
            {
                Title = "Swing Dancing at the South Bank",
                StartTime = new DateTimeOffset(2009, 7, 11, 15, 00, 00, TimeSpan.Zero),
                Duration = TimeSpan.FromHours(4)
            };
            CalenderEvent[] events = 
            {
                theOnlyEvent,
                theOnlyEvent,
                theOnlyEvent,
                theOnlyEvent,
                theOnlyEvent
            };
            return View();
        }

        /// <summary>
        /// 尋找陣列裡的元素
        /// </summary>
        /// <returns></returns>
        public ActionResult Process3()
        {
            string returnValue = "";
            GetEvents getEvents = new GetEvents();
            CalenderEvent[] events = getEvents.CalenderEvent;
            DateTime dateOfInterest = new DateTime(2009, 7, 12);
            //Finding elements 第一種方法
            foreach (CalenderEvent item in events)
            {
                if (item.StartTime.Date == dateOfInterest)
                {
                    returnValue += item.Title + ":" + item.StartTime;
                }
            }
            //Finding elements 第二種方法
            CalenderEvent[] itemOnDateOfInterest = Array.FindAll(events,
                e => e.StartTime.Date == dateOfInterest);
            returnValue += "<br>";
            foreach (CalenderEvent item in itemOnDateOfInterest)
            {
                returnValue += item.Title + ":" + item.StartTime;
            }
            return Content(returnValue);
            //另外有Find、FindLast、FindIndex、FindLastIndex
        }

        /// <summary>
        /// 陣列排序
        /// </summary>
        /// <returns></returns>
        public ActionResult Process4()
        {
            string returnValue = "";
            GetEvents getEvents = new GetEvents();
            GetEvents2 getEvents2 = new GetEvents2();
            CalenderEvent[] events = getEvents.CalenderEvent;
            CalenderEvent2[] events2 = getEvents2.CalenderEvent2;
            //
            Array.Sort(events, (event1, event2) => event1.StartTime.CompareTo(event2.StartTime));
            //自訂排序
            Array.Sort(events2);
            //
            return Content("");
        }

        /// <summary>
        /// 二維陣列
        /// </summary>
        /// <returns></returns>
        public ActionResult Process5()
        {
            //建立二維陣列
            int[,] walls = new int[,] 
            {
                { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 },
                { 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 1 },
                { 1, 0, 1, 1, 1, 1, 1, 1, 1, 1, 0, 1 },
                { 1, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 1 },
                { 1, 0, 1, 1, 1, 1, 0, 1, 0, 1, 0, 1 },
                { 1, 0, 1, 0, 0, 0, 0, 0, 0, 1, 0, 0 },
                { 1, 0, 1, 0, 1, 1, 1, 1, 1, 1, 1, 1 },
                { 1, 0, 1, 0, 0, 0, 1, 0, 0, 0, 0, 1 },
                { 1, 0, 1, 0, 1, 0, 1, 0, 0, 1, 0, 1 },
                { 1, 0, 1, 0, 1, 0, 1, 0, 1, 1, 1, 1 },
                { 1, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 1 },
                { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 }
            };
            int[,] abc = new int[2,2];
            abc[0, 0] = 1;
            abc[0, 1] = 2;
            abc[1, 0] = 4;
            abc[1, 1] = 5;
            bool returnValue = CanCharacterMoveDown(0, 1, walls);
            return Content(returnValue.ToString());
        }

        /// <summary>
        /// 測試數值型別及參考型別
        /// </summary>
        /// <returns></returns>
        public ActionResult Process6()
        {
            List<CalenderEvent> events = new List<CalenderEvent>();
            List<CalenderEvent> events2 = new List<CalenderEvent>();
            CalenderEvent newEvent = new CalenderEvent() 
            {
                Title = "",
                StartTime = new DateTimeOffset(2009, 7, 14, 19, 15, 00, TimeSpan.Zero),
                Duration = TimeSpan.FromHours(1)
            };
            events.Add(newEvent);
            //新增到指定位置
            events.Insert(1, newEvent);
            //將同樣的List加進去
            events.AddRange(events2);
            //使用不存任何元素的陣列模式
            Indexable ix = new Indexable();
            ix[42] = "Xyzzy";
            //使用泛型(T)
            ArrayAndIndexer<int> aai = new ArrayAndIndexer<int>();
            aai.TheArray[10] = 42;
            aai[20] = 99;
            //使用泛型(T)及struct
            ArrayAndIndexer<CanChange> aaq = new ArrayAndIndexer<CanChange>();
            aaq.TheArray[10].Number = 42;
            aaq[20] = new CanChange { Number = 99, Name = "My Item" };
            //因struct是value type，因此無法修改class裡的值
            CanChange elem = aaq[20];
            elem.Number = 456;
            //
            return Content(aaq[10].Number + "_" + aaq[20].Number + "_" + elem.Number);
        }

        /// <summary>
        /// 測試數值型別及參考型別的執行效率
        /// </summary>
        /// <returns></returns>
        public ActionResult Process7()
        {
            string returnValue = "";
            Stopwatch sw = new Stopwatch();
            sw.Start();
            int itemCount = 500000;
            List<CanChange> items = new List<CanChange>(itemCount);
            for (int i = 0; i < itemCount; i++)
            {
                items.Add(new CanChange { Number = i });
            }
            sw.Stop();
            returnValue = "Creation:" + sw.ElapsedTicks;
            sw.Reset();
            sw.Start();
            int total = 0;
            for (int i = 0; i < itemCount; i++)
            {
                total += items[i].Number;
            }
            sw.Stop();
            returnValue += "<br>" + "Total: " + total;
            returnValue += "<br>" + "Sum: " + sw.ElapsedTicks;
            //
            return Content(returnValue);            
        }

        /// <summary>
        /// 使用IEnumerable
        /// </summary>
        /// <returns></returns>
        public ActionResult Process8()
        {
            GetMessages.Message = "";
            string[] eventNames = 
            {
                "Swing Dancing at the South Bank",
                "Saturday Night Swing",
                "Formula 1 German Grand Prix",
                "Swing Dance Picnic",
                "Stompin' at the 100 Club"
            };
            GetMessages.Message += "Calling AddNumbers";
            IEnumerable<string> numberedNames = AddNumbers2(eventNames);
            GetMessages.Message += "<br> Starting main loop";
            foreach (string numberedName in numberedNames)
            {
                GetMessages.Message += "<br> In main loop: " + numberedName;
            }
            GetMessages.Message += "<br> Leaving main loop";
            return Content(GetMessages.Message);
        }

        /// <summary>
        /// 使用Lazy Loding
        /// </summary>
        /// <returns></returns>
        public ActionResult Process9()
        {
            GetMessages.Message = "";
            //foreach (string file in GetAllFilesInDirectory(@"c:\Programing\Ebook\"))
            //{
            //    GetMessages.Message += "<br>" + file;
            //}
            //同時使用兩個Lazy Loading
            IEnumerable<string> allFiles = GetAllFilesInDirectory(@"c:\Programing\Ebook\");
            IEnumerable<string> numberedFiles = AddNumbers2(allFiles);
            foreach (string file in numberedFiles)
            {
                GetMessages.Message += "<br>" + file;
            }
            return Content(GetMessages.Message);
        }
        /// <summary>
        /// 傳入兩個陣列，回傳一個合併的陣列，完整複製
        /// </summary>
        /// <param name="events1"></param>
        /// <param name="events2"></param>
        /// <returns></returns>
        public static CalenderEvent[] CombinEvents(CalenderEvent[] events1, CalenderEvent[] events2)
        {
            CalenderEvent[] combinedEvents = new CalenderEvent[events1.Length + events2.Length];
            events1.CopyTo(combinedEvents, 0);
            events2.CopyTo(combinedEvents, events1.Length);
            //
            return combinedEvents;
        }

        /// <summary>
        /// 傳入一個陣列，回傳一個合併的陣列，排除第一個元素
        /// </summary>
        /// <param name="events"></param>
        /// <returns></returns>
        public static CalenderEvent[] RemoveFirstEvent(CalenderEvent[] events)
        {
            CalenderEvent[] croppedEvents = new CalenderEvent[events.Length - 1];
            Array.Copy(events,      //複製來源
                1,                  //起始點
                croppedEvents,      //複製目地
                0,                  //複製目地起始點
                events.Length - 1   //複製數量
                );
            return croppedEvents;
        }

        /// <summary>
        /// 回傳二維陣列
        /// </summary>
        /// <param name="allEvents"></param>
        /// <param name="firstDay"></param>
        /// <param name="numberOfDays"></param>
        /// <returns></returns>
        public static CalenderEvent[][] GetEventsByDay(CalenderEvent[] allEvents, DateTime firstDay, int numberOfDays)
        {
            CalenderEvent[][] eventsByDay = new CalenderEvent[numberOfDays][];
            for (int day = 0; day < numberOfDays; day++)
            {
                DateTime dateOfInterest = (firstDay + TimeSpan.FromDays(day)).Date;
                CalenderEvent[] itemsOnDateOfInterest = Array.FindAll(allEvents,
                    e => e.StartTime.Date == dateOfInterest);
                eventsByDay[day] = itemsOnDateOfInterest;
            }
            return eventsByDay;
        }

        /// <summary>
        /// 檢核是否陣列下一筆還有資料
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="walls"></param>
        /// <returns></returns>
        public static bool CanCharacterMoveDown(int x, int y, int[,] walls)
        {
            int newY = y + 1;
            if (newY == walls.GetLength(0))
            {
                return false;
            }
            return walls[newY, x] == 0;
        }

        /// <summary>
        /// 利用List<T>將IEnumerable轉成Array回傳
        /// </summary>
        /// <param name="names"></param>
        /// <returns></returns>
        public static string[] AddNumbers(IEnumerable<string> names)
        {
            List<string> numberedNames = new List<string>();
            using (IEnumerator<string> enumerator = names.GetEnumerator())
            {
                int i = 0;
                while (enumerator.MoveNext())
                {
                    string currentName = enumerator.Current;
                    numberedNames.Add(string.Format("{0}: {1}", 1, currentName));
                    i += 1;
                }
            }
            //直接利用foreach也可達到同樣的效果
            foreach (string currentName in names)
            {
                numberedNames.Add(string.Format("{0}: {1}", 1, currentName));
            }
            return numberedNames.ToArray();
        }

        /// <summary>
        /// 使用yield直接回傳IEnumerable集合
        /// </summary>
        /// <param name="names"></param>
        /// <returns></returns>
        public static IEnumerable<string> AddNumbers2(IEnumerable<string> names)
        {
            GetMessages.Message += "<br> Starting AddNumbers";
            int i = 0;
            foreach (string currentName in names)
            {
                GetMessages.Message += "<br> In AddNumbers: " + currentName;
                yield return string.Format("{0}: {1}", i, currentName);
                i += 1;
            }
            GetMessages.Message += "<br> Leaving AddNumbers";
        }
        
        /// <summary>
        /// 讀取硬碟裡的檔案，並使用遞回的方式讀取子目錄
        /// </summary>
        /// <param name="directoryPath"></param>
        /// <returns></returns>
        public static IEnumerable<string> GetAllFilesInDirectory(string directoryPath)
        {
            IEnumerable<string> files = null;
            IEnumerable<string> subdirectories = null;
            try
            {
                files = Directory.EnumerateFiles(directoryPath);
                subdirectories = Directory.EnumerateDirectories(directoryPath);
            }
            catch (UnauthorizedAccessException)
            {
                
                throw;
            }
            if (files != null)
            {
                foreach (string file in files)
                {
                    yield return file;
                }
            }
            if (subdirectories != null)
            {
                foreach (string subdirectorie in subdirectories)
                {
                    foreach (string file in GetAllFilesInDirectory(subdirectorie))
                    {
                        yield return file;
                    }
                }
            }
        }
	}
}
