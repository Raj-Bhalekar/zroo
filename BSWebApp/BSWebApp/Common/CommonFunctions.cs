using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BSWebApp.Common
{
    public class CommonFunctions
    {
        public List<KeyValuePair<string, string>> ConvertSelectListItemToSimpleType(List<SelectListItem> Items)
        {
            var simpleTypeItems = new List<KeyValuePair<string, string>>();
            foreach (var itm in Items)
            {
                simpleTypeItems.Add(new KeyValuePair<string, string>(itm.Value, itm.Text));
            }
            return simpleTypeItems;
        }
    }
}