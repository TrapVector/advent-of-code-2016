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
                new Days.Day2( "day2.txt" )
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
