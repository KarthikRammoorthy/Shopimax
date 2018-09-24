using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Shopimax.API.Models;

namespace Shopimax.API.Helpers
{
    public static class Extensions
    {
         public static void AddApplicationError(this HttpResponse response, string message)
        {
            response.Headers.Add("Application-Error", message);
            response.Headers.Add("Access-Control-Expose-Headers", "Application-Error");
            response.Headers.Add("Access-Control-Allow-Origin", "*");
        }
        public static string CalculateOrderTotal(this ICollection<LineItem> lineitem)
        {
           var orderTotal = lineitem.Sum(y => Convert.ToInt32(y.LineItemTotalPrice)).ToString();
           orderTotal =  "$" + orderTotal;
                      
            return orderTotal;


        }
         public static string AppendDollar(this string price)
        {         
           price =  "$" + price;          
           return price;

        }
     
    }
}