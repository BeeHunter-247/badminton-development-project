using System.Globalization;

namespace Badminton.Web.Services.Vnpay.Lib
{
    public class VnpayCompare : IComparer<string>
    {
        //so sánh để sắp xếp theo thứ tự theo bảng chữ cái
        public int Compare(string x, string y)
        {
            if (x == y) return 0;
            if (x == null) return -1;
            if (y == null) return 1;
            var vnpCompare = CompareInfo.GetCompareInfo("en-US");
            return vnpCompare.Compare(x, y, CompareOptions.Ordinal);
        }
    }
}
