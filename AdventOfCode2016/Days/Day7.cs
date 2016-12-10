using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AdventOfCode2016.Days
{
    public class Day7 : Day
    {
        public Day7( string Input )
            : base( Input )
        { }

        //private const string PATTERN = @"([^\[]*((?<abba>(.)([^\1])\2\1)|(\[.*?\])))*.*";
        //private const string PATTERN = @"(?(\[[^\]]+(.)([^\1])\2\1[^\[]+\])(?!)|(?<!\[[^\]]+)(.)([^\1])\2\1(?![^\[]+\]))";

        // Matches 4-letter palindromes outside of brackets
        //private const string PATTERN = @"(?<!\[[^\]]+)(.)((?!\1).)\2\1(?![^\[]+\])";

        // Matches 4-letter palindromes inside of brackets
        //private const string PATTERN = @"\[[^\]]+(.)((?!\1).)\2\1[^\[]+\]";

        //private const string PATTERN = @"(?<!\[[^\]]+(.)((?!\1).)\2\1[^\[]+\])(?<!\[[^\]]+)(.)((?!\3).)\4\3(?![^\[]+\])(?!\[[^\]]+(.)((?!\5).)\6\5[^\[]+\])";


        protected override void RunPart1( string Input )
        {
            using( var Reader = new StreamReader( Input ) )
            {
                var Count = 0;

                var Regex = new Regex( @"(.)((?!\1).)\2\1" );
                while( !Reader.EndOfStream )
                {
                    var IP = Reader.ReadLine();
                    var Parts = IP.Split( '[', ']' );

                    bool ContainsPalindrome = false;

                    // Assumes that no IP ever starts with a bracket
                    // and no brackets are nested.
                    for( var i = 0; i < Parts.Length; i++ )
                    {
                        if( Regex.IsMatch( Parts[ i ] ) )
                        {
                            if( i % 2 == 0 )
                            {
                                ContainsPalindrome = true;
                            }
                            else if( i % 2 == 1 )
                            {
                                ContainsPalindrome = false;
                                break;
                            }
                        }
                    }

                    if( ContainsPalindrome ) Count++;
                }

                Console.WriteLine( "Count = {0}", Count );
            }
        }

        protected override void RunPart2( string Input )
        {
            //throw new NotImplementedException();
        }
    }
}
