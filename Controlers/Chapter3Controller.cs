using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Programing_CSharp_5._0.Controllers
{
    public class Chapter3Controller : Controller
    {
        //
        // GET: /TestClass/
        public ActionResult Index()
        {
            Plane plane = new Plane("BA00049");
            Plane plane2 = new Plane("CA00050");
            //
            plane.SpeedInMilesPerHour = 100.0;
            plane.SendMessage("123");
            plane.Direction = Plane.DirectionOfApproaching.Approaching;
            string msg1 = "Your plane has identifier " + plane.Identifier + "," +
                          "and is traveling at " + plane.SpeedInMilesPerHour + "mph [" + plane.SpeedInKilometersPerHour + "kph]";
            //
            plane.SpeedInMilesPerHour = 20.0;
            plane.Direction = Plane.DirectionOfApproaching.Leaving;
            string msg2 = "Your plane has identifier " + plane.Identifier + "," +
                          "and is traveling at " + plane.SpeedInMilesPerHour + "mph [" + plane.SpeedInKilometersPerHour + "kph]"; ;
            return Content(msg1 + "_" + msg2);
        }

        public ActionResult Struct()
        {
            PolarPoint3D polarPoint3D = new PolarPoint3D() { Distance = 1};
            //
            //polarPoint3D.Position = new PolarPoint3D(1,2);
            return Content(polarPoint3D.Altitude + "_" + polarPoint3D.Angle + "_" + polarPoint3D.Distance + "_" + polarPoint3D.abc());
        }


	}
}
