using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AdventOfCode2016.Days
{
    public class Day5 : Day
    {
        public Day5()
            : base( "" )
        { }

        private const string DOOR_ID = "uqwqemis";

        private static int Iteration = -1;

        private static void FindCharacters( object Context )
        {
            var Characters = (ConcurrentDictionary<int, char>)Context;

            while( Characters.Keys.Count < 8 )
            {
                using( var Hasher = MD5.Create() )
                {
                    var ThreadIteration = Interlocked.Increment( ref Iteration );

                    var Input = DOOR_ID + ThreadIteration;
                    
                    char Character;
                    if( TryFindNextCharacter( Hasher, Input, out Character ) )
                    {
                        Characters.TryAdd( ThreadIteration, Character );
                        Console.WriteLine( "Found char '{0}'.", Character );
                    }
                }
            }
        }

        private static bool TryFindNextCharacter( MD5 Hasher, string Input, out char Character )
        {
            Character = '-';

            var Hash = Hasher.ComputeHash( Encoding.ASCII.GetBytes( Input ) );
            if( Hash[ 0 ] != 0 || Hash[ 1 ] != 0 || ( Hash[ 2 ] & 0xF0 ) != 0 ) return false;

            Character = Hash[ 2 ].ToString( "x2" )[ 1 ];
            return true;
        }

        protected override void RunPart1( string InputFile )
        {
            //Iteration = -1;
            //
            //var Characters = new ConcurrentDictionary<int, char>( Environment.ProcessorCount, 8 );
            //
            //var Threads = new List<Thread>( Environment.ProcessorCount );
            //for( var i = 0; i < Environment.ProcessorCount; i++ )
            //{
            //    var WorkerThread = new Thread( FindCharacters );
            //    WorkerThread.Start( Characters );
            //
            //    Threads.Add( WorkerThread );
            //}
            //
            //foreach( var Thread in Threads )
            //{
            //    Thread.Join();
            //}
            //
            //Console.Write( "Password = " );
            //Console.WriteLine( Characters.OrderBy( p => p.Key ).Select( p => p.Value ).ToArray() );
        }

        private static void FindCharactersPositions( object Context )
        {
            var Characters = (ConcurrentDictionary<int, char>)Context;

            while( Characters.Keys.Count < 8 )
            {
                using( var Hasher = MD5.Create() )
                {
                    var ThreadIteration = Interlocked.Increment( ref Iteration );

                    var Input = DOOR_ID + ThreadIteration;

                    char Character;
                    int Position;
                    if( TryFindNextCharacterPosition( Hasher, Input, out Character, out Position ) )
                    {
                        if( Position < 0 || Position >= 8 ) continue;

                        if( Characters.TryAdd( Position, Character ) )
                        {
                            Console.WriteLine( Character.ToString().PadLeft( Position + 1, '-' ).PadRight( 8, '-' ) );
                        }
                    }
                }
            }
        }

        private static bool TryFindNextCharacterPosition( MD5 Hasher, string Input, out char Character, out int Position )
        {
            Character = '-';
            Position = -1;

            var Hash = Hasher.ComputeHash( Encoding.ASCII.GetBytes( Input ) );
            if( Hash[ 0 ] != 0 || Hash[ 1 ] != 0 || Hash[ 2 ] >= 0xA ) return false;

            Position = int.Parse( Hash[ 2 ].ToString( "x2" ) );
            Character = Hash[ 3 ].ToString( "x2" )[ 0 ];
            return true;
        }

        protected override void RunPart2( string InputFile )
        {
            Iteration = -1;

            var Characters = new ConcurrentDictionary<int, char>( Environment.ProcessorCount, 8 );

            var Threads = new List<Thread>( Environment.ProcessorCount );
            for( var i = 0; i < Environment.ProcessorCount; i++ )
            {
                var WorkerThread = new Thread( FindCharactersPositions );
                WorkerThread.Start( Characters );

                Threads.Add( WorkerThread );
            }

            foreach( var Thread in Threads )
            {
                Thread.Join();
            }

            Console.Write( "Password = " );
            Console.WriteLine( Characters.OrderBy( p => p.Key ).Select( p => p.Value ).ToArray() );
        }
    }
}
