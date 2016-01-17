using System.Web;
using System.Web.Mvc;
using TranslationsSite.Filters;

namespace TranslationsSite
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
            filters.Add(new InternationalizationAttribute());
        }
    }
}