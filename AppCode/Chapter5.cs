using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

/// <summary>
/// 回傳測試訊息的建構式
/// </summary>
public struct GetMessages
{
    public static string Message { get; set; }
}

/// <summary>
/// 文件的Class
/// </summary>
public sealed class Document
{
    //文件的文字
    public string Text { get; set; }
    //文件的時間
    public DateTime DocumentDate { get; set; }
    //文件的作者
    public string Author { get; set; }
}

/// <summary>
/// 文件的檢查程序
/// </summary>
static class DocumentProcesses
{
    public static void Spellcheck(Document doc)
    {
        GetMessages.Message += "<br>" + "Spellchecked document.";
    }
    public static void Repaginate(Document doc)
    {
        GetMessages.Message += "<br>" + "Repaginated document.";
    }
    public static void TranslateIntoFrench(Document doc)
    {
        GetMessages.Message += "<br>" + "Document traduit.";
    }
}

public delegate void DocumentProcess(Document doc);
public delegate bool Check(Document doc);
public delegate string LogTextProvider(Document doc);
public delegate void ProcessEventHandler(object sender, ProcessEventArgs e);

/// <summary>
/// 委派的範例一(基本用法)
/// </summary>
public class DocumentProcessor1
{
    private readonly List<DocumentProcess> processes = new List<DocumentProcess>();

    public List<DocumentProcess> Processes
    {
        get { return processes; }
    }

    public void Process(Document doc)
    {
        foreach (DocumentProcess process in Processes)
        {
            process(doc);
        }
    }
}

/// <summary>
/// 委派的範例二(使用Action)
/// </summary>
public class DocumentProcessor2
{
    private readonly List<Action<Document>> processes = new List<Action<Document>>();

    public List<Action<Document>> Processes
    {
        get { return processes; }
    }

    public void Process(Document doc)
    {
        foreach (Action<Document> process in Processes)
        {
            process(doc);
        }
    }
}

/// <summary>
/// 委派的範例三(使用Action，並加強檢核作業)
/// </summary>
public class DocumentProcessor3
{
    //委派檢核Class
    class ActionCheckPair
    {
        public Action<Document> Action { get; set; }
        //對應Check的委派宣告
        //public Check QuickCheck { get; set; }
        //使用Predicate<T>
        public Predicate<Document> QuickCheck { get; set; }
    }

    private readonly List<ActionCheckPair> processes = new List<ActionCheckPair>();
    //委派使用屬性，對應LogTextProvider的委派宣告
    public LogTextProvider LogTextProvider { get; set; }

    public void AddProcess(Action<Document> action)
    {
        AddProcess(action, null);
    }

    public void AddProcess(Action<Document> action, Predicate<Document> quickCheck)
    {
        processes.Add(new ActionCheckPair { Action = action, QuickCheck = quickCheck });
    }

    public void Process(Document doc)
    {
        foreach (ActionCheckPair process in processes)
        {
            if (process.QuickCheck != null && !process.QuickCheck(doc))
            {
                GetMessages.Message += "<br> The process will not succeed.";
                if (LogTextProvider != null)
                {
                    GetMessages.Message += "<br>" + LogTextProvider(doc);
                }
                return;
            }
        }
        foreach (ActionCheckPair process in processes)
        {
            process.Action(doc);
            if (LogTextProvider != null)
            {
                GetMessages.Message += "<br>" + LogTextProvider(doc);
            }
        }
    }
}

/// <summary>
/// 委派的範例四(EventHandler)
/// </summary>
public class DocumentProcessor4
{
    //委派檢核Class
    class ActionCheckPair
    {
        public Action<Document> Action { get; set; }
        public Predicate<Document> QuickCheck { get; set; }
    }

    //使用EventHandler作委派，對應EventHandler的委派
    public event EventHandler Processing;
    public event EventHandler Processed;

    private readonly List<ActionCheckPair> processes = new List<ActionCheckPair>();
    //使用泛型的方式定義委派屬性
    public Func<Document, string> LogTextProvider { get; set; }

    public void AddProcess(Action<Document> action)
    {
        AddProcess(action, null);
    }

    public void AddProcess(Action<Document> action, Predicate<Document> quickCheck)
    {
        processes.Add(new ActionCheckPair { Action = action, QuickCheck = quickCheck });
    }

    public void Process(Document doc)
    {
        OnProcessing(EventArgs.Empty);
        foreach (ActionCheckPair process in processes)
        {
            if (process.QuickCheck != null && !process.QuickCheck(doc))
            {
                GetMessages.Message += "<br> The process will not succeed.";
                if (LogTextProvider != null)
                {
                    GetMessages.Message += "<br>" + LogTextProvider(doc);
                }
                OnProcessed(EventArgs.Empty);
                return;
            }
        }
        foreach (ActionCheckPair process in processes)
        {
            process.Action(doc);
            if (LogTextProvider != null)
            {
                GetMessages.Message += "<br>" + LogTextProvider(doc);
            }
        }
        OnProcessed(EventArgs.Empty);
    }
    private void OnProcessing(EventArgs e)
    {
        if (Processing != null)
        {
            Processing(this, e);
        }
    }

    private void OnProcessed(EventArgs e)
    {
        if (Processed != null)
        {
            Processed(this, e);
        }
    }
}

/// <summary>
/// 委派的範例五(客製化EventHandler)
/// </summary>
public class DocumentProcessor5
{
    //委派檢核Class
    class ActionCheckPair
    {
        public Action<Document> Action { get; set; }
        public Predicate<Document> QuickCheck { get; set; }
    }

    //使用EventHandler作委派，對應EventHandler的委派
    public event EventHandler<ProcessEventArgs> Processing;
    public event EventHandler<ProcessEventArgs> Processed;

    private readonly List<ActionCheckPair> processes = new List<ActionCheckPair>();
    //使用泛型的方式定義委派屬性
    public Func<Document, string> LogTextProvider { get; set; }

    public void AddProcess(Action<Document> action)
    {
        AddProcess(action, null);
    }

    public void AddProcess(Action<Document> action, Predicate<Document> quickCheck)
    {
        processes.Add(new ActionCheckPair { Action = action, QuickCheck = quickCheck });
    }

    public void Process(Document doc)
    {
        ProcessEventArgs e = new ProcessEventArgs(doc);
        OnProcessing(e);
        //
        foreach (ActionCheckPair process in processes)
        {
            if (process.QuickCheck != null && !process.QuickCheck(doc))
            {
                GetMessages.Message += "<br> The process will not succeed.";
                if (LogTextProvider != null)
                {
                    GetMessages.Message += "<br>" + LogTextProvider(doc);
                }
                OnProcessed(e);
                return;
            }
        }
        foreach (ActionCheckPair process in processes)
        {
            process.Action(doc);
            if (LogTextProvider != null)
            {
                GetMessages.Message += "<br>" + LogTextProvider(doc);
            }
        }
        OnProcessed(e);
    }
    private void OnProcessing(ProcessEventArgs e)
    {
        if (Processing != null)
        {
            Processing(this, e);
        }
    }

    private void OnProcessed(ProcessEventArgs e)
    {
        if (Processed != null)
        {
            Processed(this, e);
        }
    }
}

/// <summary>
/// 委派的範例六(客製化CancelEventHandler)
/// </summary>
public class DocumentProcessor6
{
    //委派檢核Class
    class ActionCheckPair
    {
        public Action<Document> Action { get; set; }
        public Predicate<Document> QuickCheck { get; set; }
    }

    //使用EventHandler作委派，對應EventHandler的委派
    public event EventHandler<ProcessCancelEventArgs> Processing;
    public event EventHandler<ProcessEventArgs> Processed;

    private readonly List<ActionCheckPair> processes = new List<ActionCheckPair>();
    //使用泛型的方式定義委派屬性
    public Func<Document, string> LogTextProvider { get; set; }

    public void AddProcess(Action<Document> action)
    {
        AddProcess(action, null);
    }

    public void AddProcess(Action<Document> action, Predicate<Document> quickCheck)
    {
        processes.Add(new ActionCheckPair { Action = action, QuickCheck = quickCheck });
    }

    public void Process(Document doc)
    {
        ProcessEventArgs e = new ProcessEventArgs(doc);
        ProcessCancelEventArgs ce = new ProcessCancelEventArgs(doc);
        OnProcessing(ce);
        //
        if (ce.Cancel)
        {
            GetMessages.Message += "<br>Process canceled.";
            if (LogTextProvider != null)
            {
                GetMessages.Message += LogTextProvider(doc);
            }
            return;
        }
        foreach (ActionCheckPair process in processes)
        {
            if (process.QuickCheck != null && !process.QuickCheck(doc))
            {
                GetMessages.Message += "<br> The process will not succeed.";
                if (LogTextProvider != null)
                {
                    GetMessages.Message += "<br>" + LogTextProvider(doc);
                }
                OnProcessed(e);
                return;
            }
        }
        foreach (ActionCheckPair process in processes)
        {
            process.Action(doc);
            if (LogTextProvider != null)
            {
                GetMessages.Message += "<br>" + LogTextProvider(doc);
            }
        }
        OnProcessed(e);
    }
    private void OnProcessing(ProcessCancelEventArgs e)
    {
        if (Processing != null)
        {
            Processing(this, e);
        }
    }

    private void OnProcessed(ProcessEventArgs e)
    {
        if (Processed != null)
        {
            Processed(this, e);
        }
    }
}

/// <summary>
/// 委派的範例七(可大量新增或刪除委派的事件，利用Dictionary物件作存放)
/// </summary>
public class DocumentProcessor7
{
    //委派檢核Class
    class ActionCheckPair
    {
        public Action<Document> Action { get; set; }
        public Predicate<Document> QuickCheck { get; set; }
    }

    private Dictionary<string, Delegate> events;
    
    public event EventHandler<ProcessCancelEventArgs> Processing
    {
        add
        {
            Delegate theDelegate = EnsureEvent("Processing");
            events["Processing"] = ((EventHandler<ProcessCancelEventArgs>)theDelegate) + value;
        }
        remove
        {
            Delegate theDelegate = EnsureEvent("Processing");
            events["Processing"] = ((EventHandler<ProcessCancelEventArgs>)theDelegate) - value;
        }
    }
    public event EventHandler<ProcessEventArgs> Processed
    {
        add
        {
            Delegate theDelegate = EnsureEvent("Processed");
            events["Processed"] = ((EventHandler<ProcessEventArgs>)theDelegate) + value;
        }
        remove
        {
            Delegate theDelegate = EnsureEvent("Processed");
            events["Processed"] = ((EventHandler<ProcessEventArgs>)theDelegate) - value;
        }
    }

    private Delegate EnsureEvent(string eventName)
    {
        if (events == null)
        {
            events = new Dictionary<string, Delegate>();
        }
        Delegate theDelegate = null;
        if (!events.TryGetValue(eventName, out theDelegate))
        {
            events.Add(eventName, null);
        }
        return theDelegate;
    }

    private readonly List<ActionCheckPair> processes = new List<ActionCheckPair>();
    //使用泛型的方式定義委派屬性
    public Func<Document, string> LogTextProvider { get; set; }

    public void AddProcess(Action<Document> action)
    {
        AddProcess(action, null);
    }

    public void AddProcess(Action<Document> action, Predicate<Document> quickCheck)
    {
        processes.Add(new ActionCheckPair { Action = action, QuickCheck = quickCheck });
    }

    public void Process(Document doc)
    {
        ProcessEventArgs e = new ProcessEventArgs(doc);
        ProcessCancelEventArgs ce = new ProcessCancelEventArgs(doc);
        OnProcessing(ce);
        //
        if (ce.Cancel)
        {
            GetMessages.Message += "<br>Process canceled.";
            if (LogTextProvider != null)
            {
                GetMessages.Message += LogTextProvider(doc);
            }
            return;
        }
        foreach (ActionCheckPair process in processes)
        {
            if (process.QuickCheck != null && !process.QuickCheck(doc))
            {
                GetMessages.Message += "<br> The process will not succeed.";
                if (LogTextProvider != null)
                {
                    GetMessages.Message += "<br>" + LogTextProvider(doc);
                }
                OnProcessed(e);
                return;
            }
        }
        foreach (ActionCheckPair process in processes)
        {
            process.Action(doc);
            if (LogTextProvider != null)
            {
                GetMessages.Message += "<br>" + LogTextProvider(doc);
            }
        }
        OnProcessed(e);
    }

    private void OnProcessing(ProcessCancelEventArgs e)
    {
        Delegate eh = null;
        if (events != null && events.TryGetValue("Processing", out eh))
        {
            EventHandler<ProcessCancelEventArgs> pceh = eh as EventHandler<ProcessCancelEventArgs>;
            if (pceh != null)
            {
                pceh(this, e);
            }
        }
    }

    private void OnProcessed(ProcessEventArgs e)
    {
        Delegate eh = null;
        if (events != null && events.TryGetValue("Processed", out eh))
        {
            EventHandler<ProcessEventArgs> pceh = eh as EventHandler<ProcessEventArgs>;
            if (pceh != null)
            {
                pceh(this, e);
            }
        }
    }
}

/// <summary>
/// 檢核出版商
/// </summary>
public class TrademarkFilter
{
    readonly List<string> trademarks = new List<string>();

    public List<string> Trademarks 
    {
        get
        {
            return trademarks;
        }
    }

    public void HeighlightTrademarks(Document doc)
    {
        string returnValue = "";
        string[] words = doc.Text.Split(' ', '.', ',');
        foreach (string word in words)
        {
            if (Trademarks.Contains(word))
            {
                returnValue += "Heighlighting, '" + word + "' ";
            }
        }
        GetMessages.Message += "<br>" + returnValue;
    }
}

/// <summary>
/// 使用一般委派的方式增加Event事件
/// </summary>
public class ProductionDeptTool1
{
    public void Subscribe(DocumentProcessor5 processor)
    {
        processor.Processing += processor_Processing;
        processor.Processed += processor_Processed;
    }

    public void Unsubscribe(DocumentProcessor5 processor)
    {
        processor.Processing -= processor_Processing;
        processor.Processed -= processor_Processed;
    }

    void processor_Processing(object sender, EventArgs e)
    {
        GetMessages.Message += "<br>Tool1 has seen processing.";
    }

    void processor_Processed(object sender, EventArgs e)
    {
        GetMessages.Message += "<br>Tool1 has seen that processing is complete.";
    }
}

/// <summary>
/// 使用lambda增加Event事件
/// </summary>
public class ProductionDeptTool2
{
    public void Subscribe(DocumentProcessor5 processor)
    {
        processor.Processing += (sender, e) => GetMessages.Message += "<br>Tool2 has seen processing.";
        processor.Processed += (sender, e) => GetMessages.Message += "<br>Tool2 has seen that processing is complete.";
    }
}

/// <summary>
/// 使用一般委派的方式增加Event事件及CancelEvent
/// </summary>
public class ProductionDeptTool3
{
    public void Subscribe(DocumentProcessor7 processor)
    {
        processor.Processing += processor_Processing;
        processor.Processed += processor_Processed;
    }

    public void Unsubscribe(DocumentProcessor7 processor)
    {
        processor.Processing -= processor_Processing;
        processor.Processed -= processor_Processed;
    }

    void processor_Processing(object sender, ProcessCancelEventArgs e)
    {
        GetMessages.Message += "<br>Tool1 has seen processing, and not canceled.";
    }

    void processor_Processed(object sender, EventArgs e)
    {
        GetMessages.Message += "<br>Tool1 has seen that processing is complete.";
    }
}

/// <summary>
/// 使用lambda增加Event事件及CancelEvent
/// </summary>
public class ProductionDeptTool4
{
    public void Subscribe(DocumentProcessor7 processor)
    {
        processor.Processing += (sender, e) =>
        {
            GetMessages.Message += "<br>Tool2 has seen processing and canceled it.";
            if (e.Document.Text.Contains("document"))
            {
                e.Cancel = true;
            }
        };
        processor.Processed += (sender, e) => GetMessages.Message += "<br>Tool2 has seen that processing is complete.";
    }
}

/// <summary>
/// 客製化EventHanlder，Event
/// </summary>
public class ProcessEventArgs : EventArgs
{
    public ProcessEventArgs(Document document)
    {
        Document = document;
    }

    public Document Document { get; private set; }
}

/// <summary>
/// 客製化EventHanlder，CancelEvent
/// </summary>
public class ProcessCancelEventArgs : CancelEventArgs
{
    public ProcessCancelEventArgs(Document document)
    {
        Document = document;
    }

    public Document Document { get; private set; }
}
