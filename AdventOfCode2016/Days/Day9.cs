using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2016.Days
{
    public class Day9 : Day
    {
        public Day9( string Input )
            : base( Input )
        { }

        protected override void RunPart1( string Input )
        {
            using( var Reader = new StreamReader( Input ) )
            {
                var CharacterCount = ExpandMarkers( Reader );

                Console.WriteLine( "Count = {0}", CharacterCount );
            }
        }

        protected override void RunPart2( string Input )
        {
            using( var Reader = new StreamReader( Input ) )
            {
                var CharacterCount = ExpandMarkers( Reader, true );

                Console.WriteLine( "Count = {0}", CharacterCount );
            }
        }

        private static long ExpandMarkers( TextReader Reader, bool Recursive = false )
        {
            var CharacterCount = 0L;

            while( Reader.Peek() != -1 )
            {
                var Character = (char)Reader.Read();

                if( char.IsWhiteSpace( Character ) ) continue;

                if( Character == '(' )
                {
                    var Marker = "";
                    while( true )
                    {
                        Character = (char)Reader.Read();

                        if( Character == '(' ) continue;
                        if( Character == ')' ) break;
                        Marker += Character;
                    }

                    var MarkerData = Marker.Split( 'x' );
                    var Length = int.Parse( MarkerData[ 0 ] );
                    var Repeats = int.Parse( MarkerData[ 1 ] );

                    char[] MarkerOperandData = new char[ Length ];
                    Reader.ReadBlock( MarkerOperandData, 0, Length );

                    if( Recursive && MarkerOperandData.Contains( '(' ) )
                    {
                        CharacterCount += ExpandMarkers( new StringReader( new string( MarkerOperandData ) ), true ) * Repeats;
                    }
                    else
                    {
                        CharacterCount += Length * Repeats;
                    }
                }
                else
                {
                    CharacterCount++;
                }
            }

            return CharacterCount;
        }
    }
}
