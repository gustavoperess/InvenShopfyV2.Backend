namespace InvenShopfy.Core.Standards;
using System.Text.RegularExpressions;

public class ZipCode
{
    public string FormatZipCode(string zipCode)
    {
        string pattern = @"^BW1-\d{5}$";
        if (Regex.IsMatch(zipCode, pattern))
        {
            return zipCode; // Already in the correct format
        }
        
        return $"BW1-{zipCode.PadLeft(5, '0')}";
    }
}