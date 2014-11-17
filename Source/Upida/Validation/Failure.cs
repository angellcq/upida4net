﻿namespace Upida.Validation
{
    /// <summary>
    /// Represents a single validation failure
    /// </summary>
    public class Failure
    {
        private string key;
        private string text;

        /// <summary>
        /// Creates instance of the Failure class
        /// </summary>
        /// <param name="key">key</param>
        /// <param name="text">text</param>
        /// <param name="severity">severity</param>
        public Failure(string key, string text)
        {
            this.key = key;
            this.text = text;
        }

        /// <summary>
        /// Property path of the failure
        /// </summary>
        public string Key
        {
            get { return this.key; }
        }

        /// <summary>
        /// Text message of the failure
        /// </summary>
        public string Text
        {
            get { return this.text; }
        }
    }
}