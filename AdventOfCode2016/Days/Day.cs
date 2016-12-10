using System;

namespace AdventOfCode2016.Days
{
    public abstract class Day
    {
        private string Part1Input;
        private string Part2Input;
        private bool ShouldRun;

        public Day( string Input, bool ShouldRun = true )
        {
            Part1Input = Input;
            Part2Input = Input;
            this.ShouldRun = ShouldRun;
        }

        public Day( string Part1Input, string Part2Input, bool ShouldRun = true )
        {
            this.Part1Input = Part1Input;
            this.Part2Input = Part2Input;
            this.ShouldRun = ShouldRun;
        }

        public void RunPart1()
        {
            if( ShouldRun )
            {
                RunPart1( Part1Input );
            }
            else
            {
                Console.WriteLine( "Execution disabled..." );
            }
        }

        public void RunPart2()
        {
            if( ShouldRun )
            {
                RunPart2( Part2Input );
            }
            else
            {
                Console.WriteLine( "Execution disabled..." );
            }
        }

        protected abstract void RunPart1( string Input );
        protected abstract void RunPart2( string Input );
    }
}