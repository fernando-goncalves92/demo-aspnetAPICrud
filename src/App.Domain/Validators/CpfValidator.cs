using App.Domain.Helpers;
using App.Domain.Normalizers;
using System;
using System.Linq;

namespace App.Domain.Validators
{
    public class CpfValidator
    {
        public const int CpfLength = 11;

        public static bool Validate(string cpf)
        {
            var numbers = NormalizeDocument.OnlyNumbers(cpf);

            if (!IsValidLength(numbers))
                return false;

            return !HasDuplicatedDigits(numbers) && HasValidDigits(numbers);
        }

        private static bool IsValidLength(string cpf)
        {
            return cpf.Length == CpfLength;
        }

        private static bool HasDuplicatedDigits(string cpf)
        {
            string[] invalidNumbers =
            {
                "00000000000",
                "11111111111",
                "22222222222",
                "33333333333",
                "44444444444",
                "55555555555",
                "66666666666",
                "77777777777",
                "88888888888",
                "99999999999"
            };

            return invalidNumbers.Contains(cpf);
        }

        private static bool HasValidDigits(string cpf)
        {
            var numbersWithouDigits = cpf.Substring(0, CpfLength - 2);

            var digit = new DigitCheckHelper(numbersWithouDigits).WithMultipliersFromTo(2, 11).Replacing("0", 10, 11);

            var firstDigit = digit.CalculateDigit();

            digit.AddDigit(firstDigit);

            var secondDigit = digit.CalculateDigit();

            return string.Concat(firstDigit, secondDigit) == cpf.Substring(CpfLength - 2, 2);
        }
    }
}
