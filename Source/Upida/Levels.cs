namespace Upida
{
    /// <summary>
    /// Represents default set of serialization levels (or rules).
    /// </summary>
    public static class Levels
    {
        /// <summary>
        /// LEVEL 10 - ID. Includes only ID fields.
        /// </summary>
        public const byte ID = 10;

        /// <summary>
        /// LEVEL 20 - LOOKUP. Includes LOOKUP and ID fields.
        /// </summary>
        public const byte LOOKUP = 20;

        /// <summary>
        /// LEVEL 30 - GRID. Includes GRID, LOOKUP and ID fields.
        /// </summary>
        public const byte GRID = 30;

        /// <summary>
        /// LEVEL 40 - DEEP. Includes DEEP, GRID, LOOKUP and ID fields.
        /// </summary>
        public const byte DEEP = 40;

        /// <summary>
        /// Level 50 - FULL. Includes FULL, DEEP, GRID, LOOKUP and ID fields.
        /// </summary>
        public const byte FULL = 50;

        /// <summary>
        /// Highest level. Includes all levels. Fields marked with NEVER level, are supposed to be never included in the outgoing data.
        /// </summary>
        public const byte NEVER = byte.MaxValue;
    }
}