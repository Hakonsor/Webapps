using System.Collections.Generic;
using System.Web.Mvc;

namespace Nips.Model.Entities
{
    public static class DefaultValues
    {

        public static SelectList ItemsPerPageList
        {
            get
            {
                return (new SelectList(new List<int> { 5, 10, 15, 25, 50, 100 }, selectedValue: 15));
            }
        }

    }
}
