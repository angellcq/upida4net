using System.Collections;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;

namespace UpidaExampleStraight.Controllers
{
    public static class MyHtmlExtentions
    {
        public static IHtmlString MyDropDown(this HtmlHelper helper, string name, IEnumerable data, string valueField, string textField, string selectedValue)
        {
            SelectList list = new SelectList(data, valueField, textField, selectedValue);
            return helper.DropDownList(name, list);
        }
    }
}