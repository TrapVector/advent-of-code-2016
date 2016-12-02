namespace AdventOfCode2016.Days
{
    public abstract class Day
    {
        private string Part1InputFile;
        private string Part2InputFile;

        public Day( string InputFile )
        {
            Part1InputFile = InputFile;
            Part2InputFile = InputFile;
        }

        public Day( string Part1InputFile, string Part2InputFile )
        {
            this.Part1InputFile = Part1InputFile;
            this.Part2InputFile = Part2InputFile;
        }

        public void RunPart1() { RunPart1( Part1InputFile ); }
        public void RunPart2() { RunPart2( Part2InputFile ); }

        protected abstract void RunPart1( string InputFile );
        protected abstract void RunPart2( string InputFile );
    }
}