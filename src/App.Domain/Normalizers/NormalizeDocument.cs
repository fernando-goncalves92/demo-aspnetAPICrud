namespace App.Domain.Normalizers
{
    public class NormalizeDocument
    {
        public static string OnlyNumbers(string document)
        {
            var onlyNumber = string.Empty;

            foreach (var d in document)
            {
                if (char.IsDigit(d))
                {
                    onlyNumber += d;
                }
            }

            return onlyNumber.Trim();
        }
    }
}
