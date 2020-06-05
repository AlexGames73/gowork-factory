namespace GoWorkFactoryBusinessLogic.HelperModels
{
    public class WordText
    {
        public string Text { get; set; }
        public WordTextProperties Properties { get; set; }

        public static implicit operator WordText(string s)
        {
            return new WordText { Text = s };
        }
    }
}
