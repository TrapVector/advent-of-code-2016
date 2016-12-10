using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2016.Days
{
    public class Day1 : Day
    {
        private enum Direction
        {
            North,
            East,
            South,
            West
        }

        private class Position
        {
            public int X;
            public int Y;

            public int Distance
            {
                get { return Math.Abs( X ) + Math.Abs( Y ); }
            }

            public void Move( Direction Direction, int Rate )
            {
                switch( Direction )
                {
                case Direction.North: Y += Rate; break;
                case Direction.East:  X += Rate; break;
                case Direction.South: Y -= Rate; break;
                case Direction.West:  X -= Rate; break;
                }
            }

            public override bool Equals( object obj )
            {
                if( ReferenceEquals( this, obj ) ) return true;

                var OtherPosition = obj as Position;
                if( obj == null ) return false;

                return X == OtherPosition.X && Y == OtherPosition.Y;
            }

            public override int GetHashCode()
            {
                return X ^ Y;
            }

            public override string ToString()
            {
                return string.Format( "[{0}, {1}]", X, Y );
            }
        }

        public Day1( string Input )
            : base( Input )
        { }

        private Direction TurnLeft( Direction OriginalDirection )
        {
            if( OriginalDirection == Direction.North )
            {
                return Direction.West;
            }
            else
            {
                return OriginalDirection - 1;
            }
        }

        private Direction TurnRight( Direction OriginalDirection )
        {
            if( OriginalDirection == Direction.West )
            {
                return Direction.North;
            }
            else
            {
                return OriginalDirection + 1;
            }
        }

        protected override void RunPart1( string Input )
        {
            using( var Reader = new StreamReader( Input ) )
            {
                var Data = Reader.ReadToEnd();
                var Directives = Data.Split( new string[] { ", " }, StringSplitOptions.None );

                var CurrentDirection = Direction.North;
                var CurrentPosition = new Position { X = 0, Y = 0 };

                foreach( var Directive in Directives )
                {
                    CurrentDirection = Turn( CurrentDirection, Directive[ 0 ] );

                    var MovementRate = int.Parse( Directive.Substring( 1 ) );

                    CurrentPosition.Move( CurrentDirection, MovementRate );
                }

                var Distance = CurrentPosition.Distance;

                Console.WriteLine( "Distance = {0}", Distance );
            }
        }

        protected override void RunPart2( string Input )
        {
            using( var Reader = new StreamReader( Input ) )
            {
                var Data = Reader.ReadToEnd();
                var Directives = Data.Split( new string[] { ", " }, StringSplitOptions.None );

                var CurrentDirection = Direction.North;
                var CurrentPosition = new Position { X = 0, Y = 0 };

                var VisitedPositions = new HashSet<Position>();

                foreach( var Directive in Directives )
                {
                    CurrentDirection = Turn( CurrentDirection, Directive[0] );

                    var MovementRate = int.Parse( Directive.Substring( 1 ) );

                    for( var i = 0; i < MovementRate; i++ )
                    {
                        CurrentPosition.Move( CurrentDirection, 1 );
                        if( VisitedPositions.Contains( CurrentPosition ) )
                        {
                            Console.WriteLine( "First location is {0}", CurrentPosition );
                            Console.WriteLine( "Distance = {0}", CurrentPosition.Distance );
                            return;
                        }
                        else
                        {
                            VisitedPositions.Add( new Position { X = CurrentPosition.X, Y = CurrentPosition.Y } );
                        }
                    }
                }
            }
        }

        private Direction Turn( Direction CurrentDirection, char Direction )
        {
            if( Direction == 'L' )
            {
                CurrentDirection = TurnLeft( CurrentDirection );
            }
            else if( Direction == 'R' )
            {
                CurrentDirection = TurnRight( CurrentDirection );
            }

            return CurrentDirection;
        }
    }
}
