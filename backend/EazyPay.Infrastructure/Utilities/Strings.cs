using SnowflakeGenerator;

namespace EazyPay.Infrastructure.Utilities;

public static class Strings
{
    public static string GenerateSnowflakeId(this string value)
    {
        var settings = new Settings() { MachineID = 1 };
        var snowflake = new Snowflake(settings);
        
        return $"{value.ToUpper()}{snowflake.NextID()}".Replace(" ", "");
    }

    public static string Mask(this string value, char mask)
    {
        return new string(mask, value[..^4].Length) + value[^4..];
    }
}