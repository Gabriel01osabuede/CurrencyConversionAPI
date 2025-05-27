using System.ComponentModel;

namespace CurrencyConvertion.Enums
{
    public enum ApiResponseCodes
    {
        [Description("AN ERROR OCURRED")]
        EXCEPTION = -4,
        [Description("NOT FOUND")]
        NOT_FOUND = -3,
        [Description("BAD REQUEST")]
        INVALID_REQUEST = -2,
        [Description("SERVER ERROR OCCURED")]
        ERROR = -1,
        [Description("FAIL")]
        FAIL = 2,
        [Description("SUCCESS")]
        OK = 1,
    }
}
