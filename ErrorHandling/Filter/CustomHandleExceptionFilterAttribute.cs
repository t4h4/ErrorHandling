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
        public override void OnException(ExceptionContext context)
        {
            var result = new ViewResult() { ViewName = "Hata1" }; // Hata1 sayfasina git

            result.ViewData = new ViewDataDictionary(new EmptyModelMetadataProvider(), context.ModelState); // bos meta data provider veriyoruz. bu kodu yazmaz isek, hata1 sayfasına data gonderemeyiz.

            result.ViewData.Add("Exception", context.Exception); // hatayi donuyoruz.

            context.Result = result;

        }
    }
}
