using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AdventOfCode2016.Days
{
    public class Day10 : Day
    {
        private interface IReceiver
        {
            void Receive( int Value );
        }

        private class Input
        {
            public int Index;

            public IReceiver Output = null;

            public Input( int Index )
            {
                this.Index = Index;
            }

            public void Send()
            {
                Output.Receive( Index );
            }
        }

        private class Bot : IReceiver
        {
            private int[] Values = new int[] { -1, -1 };

            public int Index;

            public int LowValue { get { return Math.Min( Values[ 0 ], Values[ 1 ] ); } }
            public int HighValue { get { return Math.Max( Values[ 0 ], Values[ 1 ] ); } }

            public IReceiver High = null;
            public IReceiver Low = null;

            public Bot( int Index )
            {
                this.Index = Index;
            }

            public void Receive( int Value )
            {
                if( Values[ 0 ] == -1 )
                {
                    Values[ 0 ] = Value;
                }
                else
                {
                    Values[ 1 ] = Value;
                    Low.Receive( LowValue);
                    High.Receive( HighValue );
                }
            }
        }

        private class Output : IReceiver
        {
            public int Index;

            public List<int> Values { get; } = new List<int>();

            public Output( int Index )
            {
                this.Index = Index;
            }

            public void Receive( int Value )
            {
                Values.Add( Value );
            }
        }

        private List<Input> Inputs = new List<Input>();
        private Dictionary<int, Bot> Bots = new Dictionary<int, Bot>();
        private Dictionary<int, Output> Outputs = new Dictionary<int, Output>();

        private Input CreateInput( int Index )
        {
            var NewInput = new Input( Index );
            Inputs.Add( NewInput );
            return NewInput;
        }

        private Bot FindOrCreateBot( int Index )
        {
            if( Bots.ContainsKey( Index ) ) return Bots[ Index ];

            var NewBot = new Bot( Index );
            Bots[ Index ] = NewBot;
            return NewBot;
        }

        private Output FindOrCreateOutput( int Index )
        {
            if( Outputs.ContainsKey( Index ) ) return Outputs[ Index ];

            var NewOutput = new Output( Index );
            Outputs[ Index ] = NewOutput;
            return NewOutput;
        }

        private enum ReceiverType { Bot, Output };

        private IReceiver FindOrCreateReceiver( int Index, ReceiverType Type )
        {
            switch( Type )
            {
            case ReceiverType.Bot:
                return FindOrCreateBot( Index );
            case ReceiverType.Output:
                return FindOrCreateOutput( Index );
            }

            return null;
        }

        private Regex InputRegex = new Regex( @"value (\d+) goes to bot (\d+)" );
        private Regex TransferRegex = new Regex( @"bot (\d+) gives low to (bot|output) (\d+) and high to (bot|output) (\d+)" );

        public Day10( string Input )
            : base( Input )
        { }

        private void ReadInstructions( StreamReader Reader )
        {
            while( !Reader.EndOfStream )
            {
                var Instruction = Reader.ReadLine();

                if( InputRegex.IsMatch( Instruction ) )
                {
                    ProcessInputInstruction( Instruction );
                }
                else if( TransferRegex.IsMatch( Instruction ) )
                {
                    ProcessTransferInstruction( Instruction );
                }
            }
        }

        private void ProcessInputInstruction( string Instruction )
        {
            var Match = InputRegex.Match( Instruction );
            var InputIndex = int.Parse( Match.Groups[ 1 ].Value );
            var BotIndex = int.Parse( Match.Groups[ 2 ].Value );

            var Input = CreateInput( InputIndex );
            var Bot = FindOrCreateBot( BotIndex );

            Input.Output = Bot;
        }

        private void ProcessTransferInstruction( string Instruction )
        {
            var Match = TransferRegex.Match( Instruction );
            var BotIndex = int.Parse( Match.Groups[ 1 ].Value );
            var LowType =  Match.Groups[ 2 ].Value.Equals( "bot" ) ? ReceiverType.Bot : ReceiverType.Output;
            var HighType =  Match.Groups[ 4 ].Value.Equals( "bot" ) ? ReceiverType.Bot : ReceiverType.Output;
            var LowIndex = int.Parse( Match.Groups[ 3 ].Value );
            var HighIndex = int.Parse( Match.Groups[ 5 ].Value );

            var Bot = FindOrCreateBot( BotIndex );
            var LowReceiver = FindOrCreateReceiver( LowIndex, LowType );
            var HighReceiver = FindOrCreateReceiver( HighIndex, HighType );

            Bot.Low = LowReceiver;
            Bot.High = HighReceiver;
        }


        protected override void RunPart1( string InputFile )
        {
            using( var Reader = new StreamReader( InputFile ) )
            {
                ReadInstructions( Reader );
            }

            foreach( var Input in Inputs )
            {
                Input.Send();
            }

            foreach( var Bot in Bots.Values )
            {
                if( Bot.LowValue == 17 && Bot.HighValue == 61 )
                {
                    Console.WriteLine( "Bot Index = {0}", Bot.Index );
                    break;
                }
            }
        }

        protected override void RunPart2( string Input )
        {
            // Graph was populated by Part1
            var Product = Outputs[ 0 ].Values[ 0 ] * Outputs[ 1 ].Values[ 0 ] * Outputs[ 2 ].Values[ 0 ];
            Console.WriteLine( "Product = {0}", Product );
        }
    }
}
