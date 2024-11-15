using InvenShopfy.Core.Common.Extension;

namespace InvenShopfy.API.Common;

public class DateTimeHandler
{
    private readonly DateOnly _today;
    public DateTimeHandler()
    {
        _today = DateOnly.FromDateTime(DateTime.Now);
    }
    public (DateOnly startDate, DateOnly EndDate) GetDateRange(string? dateRange)
    {
        return dateRange switch
        {
            "Monthly" => (_today.GetFirstDayOfMonth(), _today.GetLastDayOfMonth()),
            "Weekly" => (_today.GetFirstDayOfWeek(), _today),
            "Daily" => (_today, _today),
            "Yearly" => (_today.GetFirstDayOfYear(), _today.GetLastDayOfMonth()),
            _ => (_today.GetFirstDayOfYear(), _today.GetLastDayOfMonth())
        };
    }
}