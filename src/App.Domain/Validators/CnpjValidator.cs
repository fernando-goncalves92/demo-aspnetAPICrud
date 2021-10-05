using App.Domain.Helpers;
using App.Domain.Normalizers;
using System;
using System.Linq;

namespace App.Domain.Validators
{
    public class CnpjValidator
    {
        public const int CnpjLength = 14;

        public static bool Validate(string cnpj)
        {
            var numbers = NormalizeDocument.OnlyNumbers(cnpj);

            if (!IsValidLength(numbers))
                return false;

            return !HasDuplicatedDigits(numbers) && HasValidDigits(numbers);
        }

        private static bool IsValidLength(string cnpj)
        {
            return cnpj.Length == CnpjLength;
        }

        private static bool HasDuplicatedDigits(string cnpj)
        {
            string[] invalidNumbers =
            {
                "00000000000000",
                "11111111111111",
                "22222222222222",
                "33333333333333",
                "44444444444444",
                "55555555555555",
                "66666666666666",
                "77777777777777",
                "88888888888888",
                "99999999999999"
            };

            return invalidNumbers.Contains(cnpj);
        }

        private static bool HasValidDigits(string cnpj)
        {
            var numbersWithouDigits = cnpj.Substring(0, CnpjLength - 2);

            var digit = new DigitCheckHelper(numbersWithouDigits).WithMultipliersFromTo(2, 9).Replacing("0", 10, 11);

            var firstDigit = digit.CalculateDigit();

            digit.AddDigit(firstDigit);

            var secondDigit = digit.CalculateDigit();

            return string.Concat(firstDigit, secondDigit) == cnpj.Substring(CnpjLength - 2, 2);
        }
    }
}
