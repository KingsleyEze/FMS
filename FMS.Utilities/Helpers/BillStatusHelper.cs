using FMS.Utilities.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace FMS.Utilities.Helpers
{
    public static class BillStatusHelper
    {
        public static BillStatusType GetType(string type)
        {
            BillStatusType status = BillStatusType.NONE;

            switch (type)
            {
                case "DRAFT":
                    status = BillStatusType.DRAFT;
                    break;
                case "FORWARDED":
                    status = BillStatusType.FORWARDED;
                    break;
                case "REVIEWED":
                    status = BillStatusType.REVIEWED;
                    break;
                case "APPROVED":
                    status = BillStatusType.APPROVED;
                    break;
                case "FORWARD_REJECTED":
                    status = BillStatusType.FORWARD_REJECTED;
                    break;
                case "REVIEW_REJECTED":
                    status = BillStatusType.REVIEW_REJECTED;
                    break;
            }

            return status;
        }
    }
}
