using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Programing_CSharp_5._0.Controllers
{
    public class Chapter5Controller : Controller
    {
        //
        // GET: /Chapter5/
        public ActionResult Index()
        {
            return View();
        }

        Document doc1 = new Document()
        {
            Author = "Matthew Adams",
            DocumentDate = new DateTime(2000, 01, 01),
            Text = "Am I a year early?"
        };
        Document doc2 = new Document()
        {
            Author = "Ian Griffiths",
            DocumentDate = new DateTime(2001, 01, 01),
            Text = "This is the new millennium, I promise you."
        };
        Document doc3 = new Document()
        {
            Author = "Matthew Adams",
            DocumentDate = new DateTime(2003, 01, 01),
            Text = "Another year, another document."
        };

        public ActionResult Process1()
        {
            DocumentProcessor1 processor = Configure1();

            string returnValue = "";
            //
            GetMessages.Message = "";
            processor.Process(doc1);
            returnValue += "Processing document 1"  + GetMessages.Message;
            //
            GetMessages.Message = "";
            processor.Process(doc2);
            returnValue += "<br>";
            returnValue += "Processing document 2" + GetMessages.Message;
            //
            return Content(returnValue);
        }

        public ActionResult Process2()
        {
            DocumentProcessor2 processor = Configure2();

            string returnValue = "";
            //
            GetMessages.Message = "";
            processor.Process(doc1);
            returnValue += "Processing document 1" + GetMessages.Message;
            //
            GetMessages.Message = "";
            processor.Process(doc2);
            returnValue += "<br>";
            returnValue += "Processing document 2" + GetMessages.Message;
            //
            return Content(returnValue);
        }

        public ActionResult Process3()
        {
            DocumentProcessor3 processor = Configure3();

            string returnValue = "";
            //
            GetMessages.Message = "";
            processor.Process(doc1);
            returnValue += "Processing document 1" + GetMessages.Message;
            //
            GetMessages.Message = "";
            processor.Process(doc2);
            returnValue += "<br>";
            returnValue += "Processing document 2" + GetMessages.Message;
            //
            return Content(returnValue);
        }

        public ActionResult Process4()
        {
            DocumentProcessor3 processor = Configure4();

            string returnValue = "";
            //
            GetMessages.Message = "";
            processor.Process(doc1);
            returnValue += "Processing document 1" + GetMessages.Message;
            //
            GetMessages.Message = "";
            processor.Process(doc2);
            returnValue += "<br>";
            returnValue += "Processing document 2" + GetMessages.Message;
            //
            return Content(returnValue);
        }

        public ActionResult Process5()
        {
            DocumentProcessor3 processor = Configure4();
            string documentBeingProcessed = "(No document set)";
            int processCount = 0;
            string returnValue = "";
            //
            //processor.LogTextProvider = (doc => documentBeingProcessed);
            processor.LogTextProvider = (doc =>
            {
                processCount += 1;
                return documentBeingProcessed;
            });

            documentBeingProcessed = "(Document 1)";
            GetMessages.Message = "";
            processor.Process(doc1);
            returnValue += GetMessages.Message;
            //
            documentBeingProcessed = "(Document 2)";
            GetMessages.Message = "";
            processor.Process(doc2);
            returnValue += "<br>";
            returnValue += GetMessages.Message;
            //
            documentBeingProcessed = "(Document 3)";
            GetMessages.Message = "";
            processor.Process(doc3);
            returnValue += "<br>";
            returnValue += GetMessages.Message;
            //
            returnValue += "<br>Excuted " + processCount + " processes.";
            //
            return Content(returnValue);
        }

        public ActionResult Process6()
        {
            DocumentProcessor4 processor = Configure5();
            string documentBeingProcessed = "(No document set)";
            int processCount = 0;
            string returnValue = "";
            //
            //processor.LogTextProvider = (doc => documentBeingProcessed);
            processor.LogTextProvider = (doc =>
            {
                processCount += 1;
                return documentBeingProcessed;
            });
            ProductionDeptTool1 tool1 = new ProductionDeptTool1();
            //tool1.Subscribe(processor);
            ProductionDeptTool2 tool2 = new ProductionDeptTool2();
            //tool2.Subscribe(processor);

            documentBeingProcessed = "(Document 1)";
            GetMessages.Message = "";
            processor.Process(doc1);
            returnValue += GetMessages.Message;
            //
            documentBeingProcessed = "(Document 2)";
            GetMessages.Message = "";
            processor.Process(doc2);
            returnValue += "<br>";
            returnValue += GetMessages.Message;
            //
            documentBeingProcessed = "(Document 3)";
            GetMessages.Message = "";
            processor.Process(doc3);
            returnValue += "<br>";
            returnValue += GetMessages.Message;
            //
            returnValue += "<br>Excuted " + processCount + " processes.";
            //
            return Content(returnValue);
        }

        public ActionResult Process7()
        {
            DocumentProcessor5 processor = Configure6();
            string documentBeingProcessed = "(No document set)";
            int processCount = 0;
            string returnValue = "";
            //
            processor.LogTextProvider = (doc =>
            {
                processCount += 1;
                return documentBeingProcessed;
            });
            ProductionDeptTool1 tool1 = new ProductionDeptTool1();
            tool1.Subscribe(processor);
            ProductionDeptTool2 tool2 = new ProductionDeptTool2();
            tool2.Subscribe(processor);

            documentBeingProcessed = "(Document 1)";
            GetMessages.Message = "";
            processor.Process(doc1);
            returnValue += GetMessages.Message;
            //
            documentBeingProcessed = "(Document 2)";
            GetMessages.Message = "";
            processor.Process(doc2);
            returnValue += "<br>";
            returnValue += GetMessages.Message;
            //
            documentBeingProcessed = "(Document 3)";
            GetMessages.Message = "";
            processor.Process(doc3);
            returnValue += "<br>";
            returnValue += GetMessages.Message;
            //
            returnValue += "<br>Excuted " + processCount + " processes.";
            //
            return Content(returnValue);
        }

        public ActionResult Process8()
        {
            DocumentProcessor6 processor = Configure7();
            string documentBeingProcessed = "(No document set)";
            int processCount = 0;
            string returnValue = "";
            //
            processor.LogTextProvider = (doc =>
            {
                processCount += 1;
                return documentBeingProcessed;
            });
            ProductionDeptTool3 tool3 = new ProductionDeptTool3();
            //tool3.Subscribe(processor);
            ProductionDeptTool4 tool4 = new ProductionDeptTool4();
            //tool4.Subscribe(processor);

            documentBeingProcessed = "(Document 1)";
            GetMessages.Message = "";
            processor.Process(doc1);
            returnValue += GetMessages.Message;
            //
            documentBeingProcessed = "(Document 2)";
            GetMessages.Message = "";
            processor.Process(doc2);
            returnValue += "<br>";
            returnValue += GetMessages.Message;
            //
            documentBeingProcessed = "(Document 3)";
            GetMessages.Message = "";
            processor.Process(doc3);
            returnValue += "<br>";
            returnValue += GetMessages.Message;
            //
            returnValue += "<br>Excuted " + processCount + " processes.";
            //
            return Content(returnValue);
        }

        public ActionResult Process9()
        {
            DocumentProcessor7 processor = Configure8();
            string documentBeingProcessed = "(No document set)";
            int processCount = 0;
            string returnValue = "";
            //
            processor.LogTextProvider = (doc =>
            {
                processCount += 1;
                return documentBeingProcessed;
            });
            ProductionDeptTool3 tool3 = new ProductionDeptTool3();
            tool3.Subscribe(processor);
            ProductionDeptTool4 tool4 = new ProductionDeptTool4();
            tool4.Subscribe(processor);

            documentBeingProcessed = "(Document 1)";
            GetMessages.Message = "";
            processor.Process(doc1);
            returnValue += GetMessages.Message;
            //
            documentBeingProcessed = "(Document 2)";
            GetMessages.Message = "";
            processor.Process(doc2);
            returnValue += "<br>";
            returnValue += GetMessages.Message;
            //
            documentBeingProcessed = "(Document 3)";
            GetMessages.Message = "";
            processor.Process(doc3);
            returnValue += "<br>";
            returnValue += GetMessages.Message;
            //
            returnValue += "<br>Excuted " + processCount + " processes.";
            //
            return Content(returnValue);
        }

        public static DocumentProcessor1 Configure1()
        {
            DocumentProcessor1 rc = new DocumentProcessor1();
            rc.Processes.Add(DocumentProcesses.TranslateIntoFrench);
            rc.Processes.Add(DocumentProcesses.Spellcheck);
            rc.Processes.Add(DocumentProcesses.Repaginate);
            //
            TrademarkFilter trademarkFilter = new TrademarkFilter();
            trademarkFilter.Trademarks.Add("O'Relly");
            trademarkFilter.Trademarks.Add("millennium");
            //
            rc.Processes.Add(trademarkFilter.HeighlightTrademarks);
            //
            return rc;
        }

        public static DocumentProcessor2 Configure2()
        {
            DocumentProcessor2 rc = new DocumentProcessor2();
            rc.Processes.Add(DocumentProcesses.TranslateIntoFrench);
            rc.Processes.Add(DocumentProcesses.Spellcheck);
            rc.Processes.Add(DocumentProcesses.Repaginate);
            //
            TrademarkFilter trademarkFilter = new TrademarkFilter();
            trademarkFilter.Trademarks.Add("O'Relly");
            trademarkFilter.Trademarks.Add("millennium");
            //
            rc.Processes.Add(trademarkFilter.HeighlightTrademarks);
            //
            return rc;
        }

        public static DocumentProcessor3 Configure3()
        {
            DocumentProcessor3 rc = new DocumentProcessor3();
            rc.AddProcess(DocumentProcesses.TranslateIntoFrench);
            rc.AddProcess(DocumentProcesses.Spellcheck);
            rc.AddProcess(DocumentProcesses.Repaginate);
            //
            TrademarkFilter trademarkFilter = new TrademarkFilter();
            trademarkFilter.Trademarks.Add("Ian");
            trademarkFilter.Trademarks.Add("Griffiths");
            trademarkFilter.Trademarks.Add("millennium");
            //
            rc.AddProcess(trademarkFilter.HeighlightTrademarks);
            //
            return rc;
        }

        public static DocumentProcessor3 Configure4()
        {
            DocumentProcessor3 rc = new DocumentProcessor3();
            //匿名方法(Anonymous Methods)，並回傳檢核是否通過
            rc.AddProcess(DocumentProcesses.TranslateIntoFrench,
                delegate(Document doc)
                {
                    return !doc.Text.Contains("?");
                });
            //使用類似Function的方式
            Predicate<Document> predicate2 =
                delegate(Document doc)
                {
                    return !doc.Text.Contains("?");
                };
            rc.AddProcess(DocumentProcesses.Spellcheck, predicate2);
            Predicate<Document> predicate3 = doc => !doc.Text.Contains("?");
            rc.AddProcess(DocumentProcesses.Repaginate, predicate3);
            //
            TrademarkFilter trademarkFilter = new TrademarkFilter();
            trademarkFilter.Trademarks.Add("Ian");
            trademarkFilter.Trademarks.Add("Griffiths");
            trademarkFilter.Trademarks.Add("millennium");
            //
            rc.AddProcess(trademarkFilter.HeighlightTrademarks);
            //
            return rc;
        }

        public static DocumentProcessor4 Configure5()
        {
            DocumentProcessor4 rc = new DocumentProcessor4();
            //匿名方法(Anonymous Methods)，並回傳檢核是否通過
            rc.AddProcess(DocumentProcesses.TranslateIntoFrench,
                delegate(Document doc)
                {
                    return !doc.Text.Contains("?");
                });
            //使用類似Function的方式
            Predicate<Document> predicate2 =
                delegate(Document doc)
                {
                    return !doc.Text.Contains("?");
                };
            rc.AddProcess(DocumentProcesses.Spellcheck, predicate2);
            Predicate<Document> predicate3 = doc => !doc.Text.Contains("?");
            rc.AddProcess(DocumentProcesses.Repaginate, predicate3);
            //
            TrademarkFilter trademarkFilter = new TrademarkFilter();
            trademarkFilter.Trademarks.Add("Ian");
            trademarkFilter.Trademarks.Add("Griffiths");
            trademarkFilter.Trademarks.Add("millennium");
            //
            rc.AddProcess(trademarkFilter.HeighlightTrademarks);
            //
            return rc;
        }

        public static DocumentProcessor5 Configure6()
        {
            DocumentProcessor5 rc = new DocumentProcessor5();
            //匿名方法(Anonymous Methods)，並回傳檢核是否通過
            rc.AddProcess(DocumentProcesses.TranslateIntoFrench,
                delegate(Document doc)
                {
                    return !doc.Text.Contains("?");
                });
            //使用類似Function的方式
            Predicate<Document> predicate2 =
                delegate(Document doc)
                {
                    return !doc.Text.Contains("?");
                };
            rc.AddProcess(DocumentProcesses.Spellcheck, predicate2);
            Predicate<Document> predicate3 = doc => !doc.Text.Contains("?");
            rc.AddProcess(DocumentProcesses.Repaginate, predicate3);
            //
            TrademarkFilter trademarkFilter = new TrademarkFilter();
            trademarkFilter.Trademarks.Add("Ian");
            trademarkFilter.Trademarks.Add("Griffiths");
            trademarkFilter.Trademarks.Add("millennium");
            //
            rc.AddProcess(trademarkFilter.HeighlightTrademarks);
            //
            return rc;
        }

        public static DocumentProcessor6 Configure7()
        {
            DocumentProcessor6 rc = new DocumentProcessor6();
            //匿名方法(Anonymous Methods)，並回傳檢核是否通過
            rc.AddProcess(DocumentProcesses.TranslateIntoFrench,
                delegate(Document doc)
                {
                    return !doc.Text.Contains("?");
                });
            //使用類似Function的方式
            Predicate<Document> predicate2 =
                delegate(Document doc)
                {
                    return !doc.Text.Contains("?");
                };
            rc.AddProcess(DocumentProcesses.Spellcheck, predicate2);
            Predicate<Document> predicate3 = doc => !doc.Text.Contains("?");
            rc.AddProcess(DocumentProcesses.Repaginate, predicate3);
            //
            TrademarkFilter trademarkFilter = new TrademarkFilter();
            trademarkFilter.Trademarks.Add("Ian");
            trademarkFilter.Trademarks.Add("Griffiths");
            trademarkFilter.Trademarks.Add("millennium");
            //
            rc.AddProcess(trademarkFilter.HeighlightTrademarks);
            //
            return rc;
        }

        public static DocumentProcessor7 Configure8()
        {
            DocumentProcessor7 rc = new DocumentProcessor7();
            //匿名方法(Anonymous Methods)，並回傳檢核是否通過
            rc.AddProcess(DocumentProcesses.TranslateIntoFrench,
                delegate(Document doc)
                {
                    return !doc.Text.Contains("?");
                });
            //使用類似Function的方式
            Predicate<Document> predicate2 =
                delegate(Document doc)
                {
                    return !doc.Text.Contains("?");
                };
            rc.AddProcess(DocumentProcesses.Spellcheck, predicate2);
            Predicate<Document> predicate3 = doc => !doc.Text.Contains("?");
            rc.AddProcess(DocumentProcesses.Repaginate, predicate3);
            //
            TrademarkFilter trademarkFilter = new TrademarkFilter();
            trademarkFilter.Trademarks.Add("Ian");
            trademarkFilter.Trademarks.Add("Griffiths");
            trademarkFilter.Trademarks.Add("millennium");
            //
            rc.AddProcess(trademarkFilter.HeighlightTrademarks);
            //
            return rc;
        }
	}
}
