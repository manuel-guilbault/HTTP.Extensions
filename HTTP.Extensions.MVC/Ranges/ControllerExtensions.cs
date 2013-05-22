using HTTP.Extensions.Ranges;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web.Mvc;

namespace HTTP.Extensions.MVC.Ranges
{
    public static class ControllerExtensions
    {
        public static StreamRangeResult Range(this Controller controller, Stream stream, string contentType)
        {
            return controller.Range(stream, contentType, RangeUnitRegistry.Default);
        }

        public static StreamRangeResult Range(this Controller controller, Stream stream, string contentType, RangeUnitRegistry acceptedUnits)
        {
            return new StreamRangeResult(stream, contentType)
            {
                AcceptedUnits = acceptedUnits
            };
        }
    }
}
