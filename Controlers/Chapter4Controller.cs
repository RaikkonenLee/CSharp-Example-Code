using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Drawing;

namespace Programing_CSharp_5._0.Controllers
{
    public class Chapter4Controller : Controller
    {
        //
        // GET: /Chapter4/
        public ActionResult Index()
        {
            Firetruck truckOne = new Firetruck();
            //隊員
            Firefighter joe = new Firefighter() { Name = "Joe" };
            Firefighter frank = new Firefighter() { Name = "Frank" };
            //隊長，繼承Firefighter
            FireChief bigChiefHarry = new FireChief() { Name = "Harry"};
            //
            truckOne.Driver = bigChiefHarry;
            bigChiefHarry.Drive(truckOne, new Point(100, 300));
            bigChiefHarry.TellFirefighterToExtinguishFire(joe);
            //return Content(bigChiefHarry.ExtinguishFire());
            //
            //取代Base Class裡的ExtinguishFire方法
            FireChief harry = new FireChief() { Name = "Harry", NumberOne = joe };
            
            return Content(harry.ExtinguishFire());
        }

        public ActionResult Delegate()
        {
            Firefighter joe = new Firefighter() { Name = "Joe"};
            //
            FireChief harry = new FireChief() { Name = "Harry", NumberOne = joe};
            FireChief tom = new FireChief() { Name = "Tom", NumberOne = harry};
            //
            return Content(harry.ExtinguishFire() + "<br>" +
                           tom.ExtinguishFire());
        }

        public ActionResult Override()
        {
            Firefighter joe = new Firefighter() { Name = "Joe"};
            FirefighterBase bill = new TraineeFirefighter() { Name = "Bill"};
            return Content(joe.ExtinguishFire() + "<br>" + bill.ExtinguishFire());
        }

        public ActionResult RollCall()
        {
            FireStation station = new FireStation();
            Firefighter joe = new Firefighter() { Name = "Joe" };
            FirefighterBase bill = new TraineeFirefighter() { Name = "Bill" };
            FireChief bigChiefHarry = new FireChief() { Name = "Harry" };
            Administrator arthur = new Administrator()
            {
                Title = "Mr",
                Forename = "Arthur",
                Surname = "Askey"
            };
            station.ClockIn(joe);
            station.ClockIn(bill);
            station.ClockIn(bigChiefHarry);
            station.ClockIn(arthur);
            //
            return Content(station.RollCall());
        }

        public ActionResult ObjectType()
        {
            int myIntValues = 1;
            //此為Boxing
            object myObject = myIntValues;
            //此為UnBoxing
            int anotherIntVarible = (int)myObject;
            return View();
        }

        public ActionResult Interface()
        {
            AFootInBothCamps both = new AFootInBothCamps();
            //
            ISettableNamePerson settablePerson = both;
            INamedPerson namedPerson = both;
            //
            settablePerson.Name = "hello";
            
            return Content(settablePerson.Name + "_" + namedPerson.Name + "_" + both.Name);
        }
	}
}
