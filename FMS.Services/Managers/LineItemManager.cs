using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FMS.Core.Abstract;
using FMS.Core.Model;
using FMS.Services.Managers.Abstract;
using FMS.Utilities.Enums;

namespace FMS.Services.Managers
{
    public class LineItemManager : ILineItemManager
    {

        private readonly IUnitOfWork _unitOfWork;

        public LineItemManager(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public List<LineItem> PayableList()
        {
            return _unitOfWork.LineItemsRepository.Items
                .Where(x => x.AccountGroupType == AccountGroupType.Expenditure ||
                            x.AccountGroupType == AccountGroupType.Assets)
                .ToList();
        }

        public List<LineItem> ReceivableList()
        {
            return _unitOfWork.LineItemsRepository.Items
                .Where(x => x.AccountGroupType == AccountGroupType.Revenue)
                .ToList();
        }
    }
}
