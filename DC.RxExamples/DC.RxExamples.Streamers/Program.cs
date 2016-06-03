namespace DC.RxExamples.Streamers
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            //OStream.ObserveFileStream();
            RegexOnStream.RegexOverFileStream();

            //new RostockMax().HotReadFileToConsole();
        }

        
    }
}
