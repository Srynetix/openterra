using Godot;

/// <summary>
/// Lazy-loaded assets.
/// </summary>
namespace Assets
{
    /// <summary>
    /// Contains lazy-loaded fonts to use.
    /// </summary>
    public static class SimpleDefaultFont
    {
        /// <summary>
        /// Regular font.
        /// </summary>
        public static Font Regular
        {
            get => LoadDefaultFont();
        }

        /// <summary>
        /// Monospace font.
        /// </summary>
        public static Font Monospace
        {
            get => LoadMonospaceFont();
        }

        private static Font _regular;
        private static Font _monospace;

        /// <summary>
        /// Get or create default font.
        /// </summary>
        /// <returns>Default font</returns>
        private static Font LoadDefaultFont()
        {
            if (_regular == null)
            {
                var fontData = (DynamicFontData)GD.Load("res://assets/fonts/kenvector_future.ttf");
                _regular = new DynamicFont
                {
                    FontData = fontData,
                    Size = 16,
                    UseFilter = true,
                    OutlineSize = 1,
                    OutlineColor = Colors.Black
                };
            }

            return _regular;
        }

        /// <summary>
        /// Get or create monospace font.
        /// </summary>
        /// <returns>Monospace font</returns>
        private static Font LoadMonospaceFont()
        {
            if (_monospace == null)
            {
                var fontData = (DynamicFontData)GD.Load("res://assets/fonts/FiraCode-Regular.ttf");
                _monospace = new DynamicFont
                {
                    FontData = fontData,
                    Size = 11,
                    UseFilter = true,
                    OutlineSize = 1,
                    OutlineColor = Colors.Black
                };
            }

            return _monospace;
        }
    }
}
