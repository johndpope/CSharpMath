using System.Globalization;
using numericType = System.Double;

namespace CSharpMath.Extensions.NumericTypes
{
  internal static class DoubleExtensions {
    [System.Obsolete("Is any code using this?", true)]
    public static numericType TryParse(string s, numericType failValue = numericType.NaN) {
      numericType r = failValue;
      if (s?.Trim() == string.Empty) {
        r = 0;
      } else {
        bool success = numericType.TryParse(s, NumberStyles.Any, CultureInfo.InvariantCulture, out numericType dub);
        if (success) {
          r = dub;
        }
      }
      return r;
    }
  }
}
