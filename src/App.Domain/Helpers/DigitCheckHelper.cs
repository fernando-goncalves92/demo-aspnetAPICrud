using System.Collections.Generic;

namespace App.Domain.Helpers
{
    public class DigitCheckHelper
    {
        private const int Module = 11;

        private string _number;        
        private readonly List<int> _multipliers = new List<int> { 2, 3, 4, 5, 6, 7, 8, 9 };
        private readonly IDictionary<int, string> _replacements = new Dictionary<int, string>();
        private bool _moduleComplement = true;

        public DigitCheckHelper(string number)
        {
            _number = number;
        }

        public DigitCheckHelper WithMultipliersFromTo(int firstMultiplier, int lastMultiplier)
        {
            _multipliers.Clear();

            for (var i = firstMultiplier; i <= lastMultiplier; i++)
                _multipliers.Add(i);

            return this;
        }

        public DigitCheckHelper Replacing(string substitute, params int[] digits)
        {
            foreach (var d in digits)
            {
                _replacements[d] = substitute;
            }

            return this;
        }

        public void AddDigit(string digit)
        {
            _number = string.Concat(_number, digit);
        }

        public string CalculateDigit()
        {
            return !(_number.Length > 0) ? string.Empty : GetDigitSum();
        }

        private string GetDigitSum()
        {
            var sum = 0;
            
            for (int i = _number.Length - 1, m = 0; i >= 0; i--)
            {
                var product = (int)char.GetNumericValue(_number[i]) * _multipliers[m];

                sum += product;

                if (++m >= _multipliers.Count) m = 0;
            }

            var module = (sum % Module);
            var result = _moduleComplement ? Module - module : module;

            return _replacements.ContainsKey(result) ? _replacements[result] : result.ToString();
        }
    }
}
