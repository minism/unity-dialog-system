using System.Text;
using UnityEngine;

namespace DialogSystem {

  public static class TextUtil {
    public static string PreprocessText(string text, DialogSettings settings) {
      if (settings.colorDictionary == null) {
        return text;
      }
      var builder = new StringBuilder(text);
      foreach (var entry in settings.colorDictionary) {
        builder.Replace(entry.text, WrapWithColor(entry.text, entry.color));
      }
      return builder.ToString();
    }

    public static string WrapWithColor(string text, Color color) {
      var colorCode = ColorUtility.ToHtmlStringRGB(color);
      return $"<color=#{colorCode}>{text}</color>";
    }
  }

}