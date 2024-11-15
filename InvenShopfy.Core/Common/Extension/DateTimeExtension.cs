namespace InvenShopfy.Core.Common.Extension;

public static class DateTimeExtension
{
    // Get month report
    public static DateOnly GetFirstDayOfMonth(this DateOnly date, int? year=null, int? month=null) 
        => new(year ?? date.Year, month ?? date.Month, day: 1);
    public static DateOnly GetLastDayOfMonth(this DateOnly date, int? year = null, int? month = null)
        => new DateOnly(year ?? date.Year, month ?? date.Month, 1).AddMonths(1).AddDays(-1);

    // Get week report
    public static DateOnly GetFirstDayOfWeek(this DateOnly date, int? year = null, int? month = null, int? day = null)
        => new DateOnly(year ?? date.Year, month ?? date.Month, day ?? date.Day).AddDays(-7);
    
    
    // Get year report
    public static DateOnly GetFirstDayOfYear(this DateOnly date, int? year = null, int? month = null, int? day = null)
        => new DateOnly(year ?? date.Year, month ?? date.Month, day ?? date.Day).AddYears(-1);
}