namespace Upida.Validation
{
    public class Failure
    {
        private string key;
        private string text;

        public Failure(string key, string text)
        {
            this.key = key;
            this.text = text;
        }

        public string Key
        {
            get { return this.key; }
        }

        public string Text
        {
            get { return this.text; }
        }
    }
}