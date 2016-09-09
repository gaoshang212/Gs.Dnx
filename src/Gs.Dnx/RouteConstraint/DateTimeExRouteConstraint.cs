using System;
using System.Globalization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Gs.Dnx.RouteConstraint
{
    public class DateTimeExRouteConstraint : IRouteConstraint
    {
        private readonly string _dateTimeformat;

        public DateTimeExRouteConstraint(string p_dateTimeformat)
        {
            _dateTimeformat = p_dateTimeformat;
        }

        public DateTimeExRouteConstraint()
        {

        }

        public bool Match(HttpContext httpContext, IRouter route, string routeKey, RouteValueDictionary values, RouteDirection routeDirection)
        {
            if (httpContext == null)
            {
                throw new ArgumentNullException(nameof(httpContext));
            }

            if (route == null)
            {
                throw new ArgumentNullException(nameof(route));
            }

            if (routeKey == null)
            {
                throw new ArgumentNullException(nameof(routeKey));
            }

            if (values == null)
            {
                throw new ArgumentNullException(nameof(values));
            }

            object value;
            if (values.TryGetValue(routeKey, out value) && value != null)
            {
                if (value is DateTime)
                {
                    return true;
                }

                DateTime result;
                var valueString = Convert.ToString(value, CultureInfo.InvariantCulture);

                if (!string.IsNullOrEmpty(_dateTimeformat))
                {
                    var success = DateTime.TryParseExact(valueString, _dateTimeformat, CultureInfo.InvariantCulture, DateTimeStyles.None, out result);
                    if (success)
                    {
                        values[routeKey] = result;
                    }

                    return success;
                }

                return DateTime.TryParse(valueString, CultureInfo.InvariantCulture, DateTimeStyles.None, out result);
            }

            return false;
        }
    }
}