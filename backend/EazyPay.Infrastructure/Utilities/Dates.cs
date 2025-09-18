namespace EazyPay.Infrastructure.Utilities;

public static class Dates
{
    public static DateTime Create(int month, int year)
    {
        return new DateTime(year, month, 1);
    }
    
    public static bool IsFutureDate(this DateTime date)
    {
        return date.Date >= DateTime.Today;
    }
}