using System;
using System.Collections.Generic;
using System.Text;

namespace CSharpMath.FrontEnd {
  /// <remarks>For changing a string into glyphs which will appear on the page. </remarks>
  public interface IGlyphFinder<TFont, TGlyph> where TFont : Display.IMathFont<TGlyph> {
    TGlyph FindGlyphForCharacterAtIndex(TFont font, int index, string str);
    IEnumerable<TGlyph> FindGlyphs(TFont font, string str);
    TGlyph EmptyGlyph { get; }
    bool GlyphIsEmpty(TGlyph glyph);
  }
}