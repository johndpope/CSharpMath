using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CSharpMath.Display {
  public static class DisplayEnumerableExtensions {
    public static float CollectionAscent<TFont, TGlyph>(this IEnumerable<IDisplay<TFont, TGlyph>> displays)
      where TFont : IMathFont<TGlyph> {
      float r = 0;
      foreach (var display in displays) {
        if (display != null) r = Math.Max(r, display.Ascent + display.Position.Y);
      }
      return r;
    }
    public static float CollectionDescent<TFont, TGlyph>(this IEnumerable<IDisplay<TFont, TGlyph>> displays)
      where TFont : IMathFont<TGlyph> {
      float r = 0;
      foreach (var display in displays) {
        if(display != null) r = Math.Max(r, display.Descent - display.Position.Y);
      }
      return r;
    }

    public static float CollectionX<TFont, TGlyph>(this IEnumerable<IDisplay<TFont, TGlyph>> displays)
      where TFont : IMathFont<TGlyph>
    {
      float r = 0;
      if (displays.IsNonempty()) {
        r = displays.Min(d => d.Position.X);
      }
      return r;
    }
    public static float CollectionMaxX<TFont, TGlyph>(this IEnumerable<IDisplay<TFont, TGlyph>> displays)
      where TFont : IMathFont<TGlyph>
    {
      float r = 0;
      if (displays.IsNonempty())
      {
        r = displays.Max(d => d.Position.X + d.Width);
      }
      return r;
    }

    public static float CollectionWidth<TFont, TGlyph>(this IEnumerable<IDisplay<TFont, TGlyph>> displays)
      where TFont : IMathFont<TGlyph>
      => displays.CollectionMaxX() - displays.CollectionX();
  }
}
