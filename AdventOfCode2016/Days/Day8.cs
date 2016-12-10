using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AdventOfCode2016.Days
{
    public class Day8 : Day
    {
        private class Screen
        {
            private bool[,] Pixels;

            private int Width  { get { return Pixels.GetLength( 0 ); } }
            private int Height { get { return Pixels.GetLength( 1 ); } }

            public int ActiveCount
            {
                get
                {
                    var Count = 0;

                    for( var i = 0; i < Width;  i++ )
                    {
                        for( var j = 0; j < Height; j++ )
                        {
                            if( Pixels[ i, j ] ) Count++;
                        }
                    }

                    return Count;
                }
            }

            public Screen( int Width, int Height )
            {
                Pixels = new bool[ Width, Height ];
            }

            public void DrawRect( int Width, int Height )
            {
                for( var i = 0; i < Width; i++ )
                {
                    for( var j = 0; j < Height; j++ )
                    {
                        Pixels[ i, j ] = true;
                    }
                }
            }

            private bool[] CopyRow( int Row )
            {
                var RowPixels = new bool[ Width ];

                for( var i = 0; i < Width; i++ )
                {
                    RowPixels[ i ] = Pixels[ i, Row ];
                }

                return RowPixels;
            }

            private bool[] CopyColumn( int Column )
            {
                var ColumnPixels = new bool[ Height ];

                for( var i = 0; i < Height; i++ )
                {
                    ColumnPixels[ i ] = Pixels[ Column, i ];
                }

                return ColumnPixels;
            }

            public void RotateRow( int Row, int Amount )
            {
                var RowPixels = CopyRow( Row );
                var Offset = Amount % Width;

                for( var i = 0; i < Width; i++ )
                {
                    var Index = ( i + Offset ) % Width;
                    Pixels[ Index, Row ] = RowPixels[ i ];
                }
            }

            public void RotateColumn( int Column, int Amount )
            {
                var ColumnPixels = CopyColumn( Column );
                var Offset = Amount % Height;

                for( var i = 0; i < Height; i++ )
                {
                    var Index = ( i + Offset ) % Height;
                    Pixels[ Column, Index ] = ColumnPixels[ i ];
                }
            }

            public void Print()
            {
                for( var j = 0; j < Height; j++ )
                {
                    for( var i = 0; i < Width; i++ )
                    {
                        Console.Write( Pixels[ i, j ] ? "#" : " " );
                    }

                    Console.WriteLine();
                }
            }
        }

        private static Regex RectRegex         = new Regex( @"rect (\d+)x(\d+)" );
        private static Regex RotateRowRegex    = new Regex( @"rotate row y=(\d+) by (\d+)" );
        private static Regex RotateColumnRegex = new Regex( @"rotate column x=(\d+) by (\d+)" );

        public Day8( string Input )
            : base( Input )
        { }

        private static void ExecuteInstructionsFromFile( string Input, Screen Screen )
        {
            using( var Reader = new StreamReader( Input ) )
            {
                while( !Reader.EndOfStream )
                {
                    var Instruction = Reader.ReadLine();

                    if( RectRegex.IsMatch( Instruction ) )
                    {
                        var Match = RectRegex.Match( Instruction );
                        var Width = int.Parse( Match.Groups[ 1 ].Value );
                        var Height = int.Parse( Match.Groups[ 2 ].Value );

                        Screen.DrawRect( Width, Height );
                    }
                    else if( RotateRowRegex.IsMatch( Instruction ) )
                    {
                        var Match = RotateRowRegex.Match( Instruction );
                        var Row = int.Parse( Match.Groups[ 1 ].Value );
                        var Amount = int.Parse( Match.Groups[ 2 ].Value );

                        Screen.RotateRow( Row, Amount );
                    }
                    else if( RotateColumnRegex.IsMatch( Instruction ) )
                    {
                        var Match = RotateColumnRegex.Match( Instruction );
                        var Column = int.Parse( Match.Groups[ 1 ].Value );
                        var Amount = int.Parse( Match.Groups[ 2 ].Value );

                        Screen.RotateColumn( Column, Amount );
                    }
                }
            }
        }

        protected override void RunPart1( string Input )
        {
            var Screen = new Screen( 50, 6 );

            ExecuteInstructionsFromFile( Input, Screen );

            Console.WriteLine( "Count = {0}", Screen.ActiveCount );
        }

        protected override void RunPart2( string Input )
        {
            var Screen = new Screen( 50, 6 );

            ExecuteInstructionsFromFile( Input, Screen );

            Screen.Print();
        }
    }
}
