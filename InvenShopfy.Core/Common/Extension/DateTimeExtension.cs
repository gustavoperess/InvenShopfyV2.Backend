namespace InvenShopfy.Core.Common.Extension;

public static class DateTimeExtension
{
    public static DateOnly GetFirstDay(this DateOnly date, int? year=null, int? month=null) 
        => new(year ?? date.Year, month ?? date.Month, day: 1);


    public static DateOnly GetLastDay(this DateOnly date, int? year = null, int? month = null)
        => new DateOnly(year ?? date.Year, month ?? date.Month, 1).AddMonths(1).AddDays(-1);
}