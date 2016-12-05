using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2016.Days
{
    public class Day3 : Day
    {
        public Day3( string InputFile )
            : base( InputFile )
        { }

        //private class Triple
        //{
        //    private int[] Sides;
        //
        //    public Triple( int Side1, int Side2, int Side3 )
        //    {
        //        Sides = new int[]{ Side1, Side2, Side3 };
        //        
        //    }
        //
        //    public bool IsTriangle
        //    {
        //        get
        //        {
        //            var LongestSize = Sides.Max();
        //            var Sum = Sides.Sum();
        //
        //            return Sum - LongestSize > LongestSize;
        //        }
        //    }
        //}

        private static bool IsTriangle( IEnumerable<int> Lengths )
        {
            var LongestSize = Lengths.Max();
            var Sum = Lengths.Sum();

            return Sum - LongestSize > LongestSize;
        }

        //private static IEnumerable<Triple> GetLinearTriples( IEnumerable<int> Values )
        //{
        //    var ValuesEnumerator = Values.GetEnumerator();
        //    while( !ValuesEnumerator.MoveNext() )
        //    {
        //        var Sides = new int[3];
        //        Sides[ 0 ] = ValuesEnumerator.Current;
        //        ValuesEnumerator.MoveNext();
        //        Sides[ 1 ] = ValuesEnumerator.Current;
        //        ValuesEnumerator.MoveNext();
        //        Sides[ 2 ] = ValuesEnumerator.Current;
        //
        //        yield return new Triple( Sides[ 0 ], Sides[ 1 ], Sides[2] );
        //    }
        //}

        protected override void RunPart1( string InputFile )
        {
            using( var Reader = new StreamReader( InputFile ) )
            {
                var TriangleCount = 0;
                var Entries = 0;

                while( !Reader.EndOfStream )
                {
                    var LengthData = Reader.ReadLine();
                    var Lengths = LengthData.Split( new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries ).Select( s => int.Parse( s ) );

                    if( IsTriangle( Lengths ) ) TriangleCount++;

                    Entries++;
                }

                Console.WriteLine( "Triangles = {0} / {1}", TriangleCount, Entries );
            }
        }

        protected override void RunPart2( string InputFile )
        {
            using( var Reader = new StreamReader( InputFile ) )
            {
                var TriangleCount = 0;
                var Entries = 0;
                var Index = 0;

                var LengthSets = new int[ 3 ][];
                LengthSets[ 0 ] = new int[ 3 ];
                LengthSets[ 1 ] = new int[ 3 ];
                LengthSets[ 2 ] = new int[ 3 ];

                while( !Reader.EndOfStream )
                {
                    var LengthData = Reader.ReadLine();
                    var Values = LengthData.Split( new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries ).Select( s => int.Parse( s ) ).ToArray();

                    for( var i = 0; i < 3; i++ )
                    {
                        LengthSets[ i ][ Index ] = Values[ i ];
                    }

                    Index++;
                    
                    if( Index == 3 )
                    {
                        foreach( var Lengths in LengthSets )
                        {
                            if( IsTriangle( Lengths ) ) TriangleCount++;
                            Entries++;
                        }

                        Index = 0;
                    }
                }

                Console.WriteLine( "Triangles = {0} / {1}", TriangleCount, Entries );
            }
        }
    }
}
