using System;
using System.IO;
using System.Text;
using System.Collections.Generic;

namespace CodeGenerator
{
   public class ReplacementTreeReader
   {
      private enum ReadState
      {
         NodeOpened,
         Placeholder,
         Replacement,
         PairTransition,
         NodeClosed
      }

      public ReplacementTree Read( string filePath )
      {
         using ( var reader = new StreamReader( filePath ) )
         {
            return this.ReadNode( reader );
         }
      }

      private ReplacementTree ReadNode( StreamReader reader )
      {
         var tree = new ReplacementTree();
         var state = ReadState.NodeOpened;
         var placeholder = new StringBuilder();
         var replacement = new StringBuilder();

         while ( !reader.EndOfStream )
         {
            var c = (char) reader.Read();

            if ( c == '[' )
            {
               state = ReadState.NodeOpened;

               tree.Nodes.Add( this.ReadNode( reader ) );
            }
            else if ( c == ']' )
            {
               state = ReadState.NodeClosed;
               break;
            }
            else if ( c == '=' )
            {
               state = ReadState.Replacement;
            }
            else if ( ( c == '\r' ) || ( c == '\n' ) )
            {
               if ( state == ReadState.Replacement )
               {
                  tree.Pairs.Add( new ReplacementPair( placeholder.ToString(), replacement.ToString() ) );

                  state = ReadState.PairTransition;
               }
               else if ( state == ReadState.Placeholder )
               {
                  throw new Exception( String.Format( "Error at placeholder \"{0}\"!  Placeholders cannot have carriage returns or newlines.", placeholder ) );
               }
            }
            else
            {
               if ( ( state == ReadState.PairTransition ) || ( state == ReadState.NodeOpened ) || ( state == ReadState.NodeClosed ) )
               {
                  if ( !Char.IsWhiteSpace( c ) && ( c != '[' ) && ( c != ']' ) )
                  {
                     placeholder.Clear();
                     placeholder.Append( c );
                     replacement.Clear();
                     state = ReadState.Placeholder;
                  }
               }
               else if ( state == ReadState.Placeholder )
               {
                  placeholder.Append( c );
               }
               else if ( state == ReadState.Replacement )
               {
                  replacement.Append( c );
               }
            }
         }

         return tree;
      }
   }
}
