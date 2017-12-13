using System;
using System.Collections.Generic;
using System.Text;
using FMS.Core.Model;

namespace FMS.Services.Managers.Abstract
{
    public interface ILineItemManager
    {
        List<LineItem> PayableList();
        List<LineItem> ReceivableList();
        IList<LineItem> GetListItems();
    }
}
