using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2016.Days
{
    public class Day6 : Day
    {
        public Day6( string Input )
            : base( Input )
        { }

        private const int LENGTH = 8;

        private static int GetCharIndex( char Character )
        {
            return Character - 'a';
        }

        protected override void RunPart1( string Input )
        {
            var CharacterCounts = new int[ 26, LENGTH ];
            var Message = new char[ LENGTH ];

            using( var Reader = new StreamReader( Input ) )
            {
                while( !Reader.EndOfStream )
                {
                    var Line = Reader.ReadLine();
                    for( var i = 0; i < LENGTH; i++ )
                    {
                        var Character = Line[ i ];
                        var NewCount = ++CharacterCounts[ GetCharIndex( Character ), i ];

                        if( Message[ i ] == 0 || NewCount > CharacterCounts[ GetCharIndex( Message[ i ] ), i ] )
                        {
                            Message[ i ] = Character;
                        }
                    }
                }

                //for( var i = 0; i < LENGTH; i++ )
                //{
                //    var Character = (char)0;
                //    var Count = 0;
                //
                //    for( var j = 0; j < 26; j++ )
                //    {
                //        var CharacterCount = CharacterCounts[ j, i ];
                //        if( CharacterCount > Count )
                //        {
                //            Character = (char)( j + 'a' );
                //            Count = CharacterCount;
                //        }
                //    }
                //
                //    Message[ i ] = Character;
                //}
            }

            Console.Write( "Message = " );
            Console.WriteLine( Message );
        }

        protected override void RunPart2( string Input )
        {
            var CharacterCounts = new int[ 26, LENGTH ];
            var Message = new char[ LENGTH ];

            using( var Reader = new StreamReader( Input ) )
            {
                while( !Reader.EndOfStream )
                {
                    var Line = Reader.ReadLine();
                    for( var i = 0; i < LENGTH; i++ )
                    {
                        var Character = Line[ i ];
                        var NewCount = ++CharacterCounts[ GetCharIndex( Character ), i ];
                    }
                }

                for( var i = 0; i < LENGTH; i++ )
                {
                    var Character = (char)0;
                    var Count = int.MaxValue;
                
                    for( var j = 0; j < 26; j++ )
                    {
                        var CharacterCount = CharacterCounts[ j, i ];
                        if( CharacterCount < Count )
                        {
                            Character = (char)( j + 'a' );
                            Count = CharacterCount;
                        }
                    }
                
                    Message[ i ] = Character;
                }
            }

            Console.Write( "Message = " );
            Console.WriteLine( Message );
        }
    }
}
