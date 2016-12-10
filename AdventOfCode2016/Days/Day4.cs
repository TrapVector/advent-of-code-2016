using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AdventOfCode2016.Days
{
    public class Day4 : Day
    {
        public Day4( string Input )
            : base( Input )
        { }

        private class Room
        {
            public string EncryptedName;
            public string DecryptedName;
            public int SectorId;

            public Room( string EncryptedName, int SectorId )
            {
                this.SectorId = SectorId;
                this.EncryptedName = EncryptedName;
                this.DecryptedName = Decrypt( EncryptedName );
            }

            private char Decrypt( char Encrypted )
            {
                if( Encrypted == '-' ) return ' ';

                int Offset = Encrypted - 'a';
                Offset = ( Offset + SectorId ) % 26;
                return (char)( 'a' + Offset );
            }

            private string Decrypt( string Encrypted )
            {
                return new string( Encrypted.Select( c => Decrypt( c ) ).ToArray() );
            }
        }

        protected override void RunPart1( string Input )
        {
            using( var Reader = new StreamReader( Input ) )
            {
                var SectorTotal = 0;

                while( !Reader.EndOfStream )
                {
                    var RoomData = Reader.ReadLine();
                    Room Room;
                    if( TryCreateRoom( RoomData, out Room ) )
                    {
                        SectorTotal += Room.SectorId;
                    }
                }

                Console.WriteLine( "Total = {0}", SectorTotal );
            }
        }

        private static bool TryCreateRoom( string RoomData, out Room Room )
        {
            Room = null;

            var Regex = new Regex( @"(([a-z]+-)*)(\d+)\[([a-z]+)\]" );
            var Match = Regex.Matches( RoomData )[ 0 ];
            var RoomId = Match.Groups[ 1 ].Value.TrimEnd( '-' );
            var SectorId = int.Parse( Match.Groups[ 3 ].Value );
            var Checksum = Match.Groups[ 4 ].Value;

            var Appearances = new Dictionary<char, int>( RoomData.Length );
            foreach( var Character in RoomId )
            {
                if( Character == '-' ) continue;

                if( Appearances.ContainsKey( Character ) )
                {
                    Appearances[ Character ]++;
                }
                else
                {
                    Appearances[ Character ] = 1;
                }
            }

            var ComputedChecksum = new string( Appearances.OrderBy( p => p.Key ).OrderByDescending( p => p.Value ).Take( 5 ).Select( p => p.Key ).ToArray() );

            if( ComputedChecksum.Equals( Checksum ) )
            {
                Room = new Room( RoomId, SectorId );
            }

            return Room != null;
        }

        protected override void RunPart2( string Input )
        {
            using( var Reader = new StreamReader( Input ) )
            {
                while( !Reader.EndOfStream )
                {
                    var RoomData = Reader.ReadLine();
                    Room Room;
                    if( TryCreateRoom( RoomData, out Room ) )
                    {
                        if( Room.DecryptedName.Contains( "northpole" ) )
                        {
                            Console.WriteLine( "{0}: {1}", Room.SectorId, Room.DecryptedName );
                        }
                    }
                }
            }
        }
    }
}
