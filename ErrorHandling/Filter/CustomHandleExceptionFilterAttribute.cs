using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ErrorHandling.Filter
{
    public class CustomHandleExceptionFilterAttribute : ExceptionFilterAttribute
    {
        public string ErrorPage { get; set; }

        public override void OnException(ExceptionContext context)
        {
            // eger loglama yapmak istersek;
            if(ErrorPage == "Hata1")
            {
                // farkli bir kaynaga loglama
            }
            else
            {
                // farkli bir kaynaga loglama
            }
            var result = new ViewResult() { ViewName = ErrorPage }; 

            result.ViewData = new ViewDataDictionary(new EmptyModelMetadataProvider(), context.ModelState); // bos meta data provider veriyoruz. bu kodu yazmaz isek, hata1 sayfasına data gonderemeyiz.

            result.ViewData.Add("Exception", context.Exception); // hatayi donuyoruz.
            result.ViewData.Add("Url", context.HttpContext.Request.Path.Value);

            context.Result = result;

        }
    }
}
