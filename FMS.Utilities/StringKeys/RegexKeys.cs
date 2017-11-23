using System;
using System.Collections.Generic;
using System.Text;

namespace FMS.Utilities.StringKeys
{
    public class RegexKeys
    {
        public const string EMAIL_FORMAT = @"^[A-Za-z0-9_\+-]+(\.[A-Za-z0-9_\+-]+)*@[A-Za-z0-9_\+-].[A-Za-z]$";
        public const string TIME_FORMAT = @"(1[012]|0?\d):[0-5]?\d(:[0-5]?\d)?\s+[AaPp][Mm]?";
        public const string ONE_TO_TWO_DIGIT_NUMBER_FORMAT = @"^[1-9][0-9]{0,1}$";
    }
}
