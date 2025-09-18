namespace EazyPay.Infrastructure.Utilities;

public static class Numbers
{
    public static bool IsNDigits(int number, int count)
    {
        return number.ToString().Length == count;
    }
}