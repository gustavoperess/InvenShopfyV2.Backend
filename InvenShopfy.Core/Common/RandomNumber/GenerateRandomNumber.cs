namespace InvenShopfy.Core.Common.RandomNumber;

public class GenerateRandomNumber
{
    
    private static readonly Random RandomNumber = new Random();
    public static string RandomNumberGenerator()
    {
        char letter = (char)RandomNumber.Next('A', 'Z' + 1); 
        int randNum = RandomNumber.Next(1000000); 
        return letter + "-" + randNum.ToString("D6"); 
    }
}