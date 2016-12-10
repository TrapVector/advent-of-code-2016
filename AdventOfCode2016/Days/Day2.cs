using System;
using System.IO;
using System.Text;

namespace AdventOfCode2016.Days
{
    public class Day2 : Day
    {
        private enum Direction
        {
            Up,
            Right,
            Down,
            Left
        }

        private interface IKeyPad
        {
            char Current { get; }

            void Move( Direction Direction );
        }

        private class PhoneKeyPad : IKeyPad
        {
            private int X = 1;
            private int Y = 1;

            private char[,] Buttons = 
            {
                { '1', '4', '7' },
                { '2', '5', '8' },
                { '3', '6', '9' }
            };

            public void Move( Direction Direction )
            {
                switch( Direction )
                {
                case Direction.Up:    Y--; break;
                case Direction.Right: X++; break;
                case Direction.Down:  Y++; break;
                case Direction.Left:  X--; break;
                }

                X = Math.Min( Math.Max( 0, X ), Buttons.GetLength( 0 ) - 1 );
                Y = Math.Min( Math.Max( 0, Y ), Buttons.GetLength( 1 ) - 1 );
            }

            public char Current { get { return Buttons[ X, Y ]; } }
        }

        private class DiamondKeyPad : IKeyPad
        {
            private int X = 0;
            private int Y = 2;

            private char[,] Keys =
            {
                { 'X', 'X', '5', 'X', 'X' },
                { 'X', '2', '6', 'A', 'X' },
                { '1', '3', '7', 'B', 'D' },
                { 'X', '4', '8', 'C', 'X' },
                { 'X', 'X', '9', 'X', 'X' }
            };

            public char Current
            {
                get { return Keys[ X, Y ]; }
            }

            public void Move( Direction Direction )
            {
                switch( Direction )
                {
                case Direction.Up:    TryMove(  0, -1 ); break;
                case Direction.Right: TryMove(  1,  0 ); break;
                case Direction.Down:  TryMove(  0,  1 ); break;
                case Direction.Left:  TryMove( -1,  0 ); break;
                }
            }

            private void TryMove( int dX, int dY )
            {
                var NewX = X + dX;
                var NewY = Y + dY;

                if( NewX >= 0 && NewX < Keys.GetLength( 0 ) &&
                    NewY >= 0 && NewY < Keys.GetLength( 1 ) &&
                    Keys[ NewX, NewY ] != 'X' )
                {
                    X = NewX;
                    Y = NewY;
                }
            }
        }

        public Day2( string Input )
            : base( Input )
        { }

        protected override void RunPart1( string Inout )
        {
            Console.WriteLine( "Key Code = {0}", FindKeyCode( Inout, new PhoneKeyPad() ) );
        }

        protected override void RunPart2( string Input )
        {
            Console.WriteLine( "Key Code = {0}", FindKeyCode( Input, new DiamondKeyPad() ) );
        }

        private static string FindKeyCode( string Input, IKeyPad KeyPad )
        {
            using( var Reader = new StreamReader( Input ) )
            {
                var Builder = new StringBuilder();

                while( !Reader.EndOfStream )
                {
                    var Instructions = Reader.ReadLine();
                    foreach( var Instruction in Instructions )
                    {
                        switch( Instruction )
                        {
                        case 'U': KeyPad.Move( Direction.Up );    break;
                        case 'R': KeyPad.Move( Direction.Right ); break;
                        case 'D': KeyPad.Move( Direction.Down );  break;
                        case 'L': KeyPad.Move( Direction.Left );  break;
                        default:
                            break;
                        }
                    }

                    Builder.Append( KeyPad.Current );
                }

                return Builder.ToString();
            }
        }
    }
}
