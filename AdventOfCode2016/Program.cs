using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2016
{
    class Program
    {
        static void Main( string[] args )
        {
            Console.WriteLine( "-----------------------" );
            Console.WriteLine( "  Advent of Code 2016  " );
            Console.WriteLine( "-----------------------" );
            Console.WriteLine();

            Days.Day[] Days =
            {
                new Days.Day1( "day1.txt" ),
                new Days.Day2( "day2.txt" ),
                new Days.Day3( "day3.txt" ),
                new Days.Day4( "day4.txt" ),
                new Days.Day5( "uqwqemis", false ),
                new Days.Day6( "day6.txt" ),
                new Days.Day7( "day7.txt" ),
                new Days.Day8( "day8.txt" ),
                new Days.Day9( "day9.txt" ),
                new Days.Day10( "day10.txt" )
            };

            for( int i = 0; i < Days.Length; i++ )
            {
                var Day = Days[ i ];

                Console.WriteLine( "Day {0}:", i + 1 );

                Console.WriteLine( "Part 1:" );
                Day.RunPart1();

                Console.WriteLine( "Part 2:" );
                Day.RunPart2();

                Console.WriteLine();
            }

            Console.WriteLine( "Press any key to continue..." );
            Console.ReadKey( true );
        }
    }
}
