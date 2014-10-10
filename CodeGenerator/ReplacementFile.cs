using System;
using System.IO;
using System.Text;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace CodeGenerator
{
   public class ReplacementFile
   {
      public string Delimiter
      {
         get;
         private set;
      }

      public ReplacementFile( string delimiter = "=" )
      {
         this.Delimiter = delimiter;
      }

      public ReadOnlyCollection<ReplacementPair> Parse( string replacementFilePath )
      {
         var output = new List<ReplacementPair>();

         using ( var reader = new StreamReader( replacementFilePath ) )
         {
            while ( !reader.EndOfStream )
            {
               var line = reader.ReadLine();

               var replacementPair = this.ParseLine( line );

               output.Add( replacementPair );
            }
         }

         return output.AsReadOnly();
      }

      private ReplacementPair ParseLine( string line )
      {
         var placeholder = new StringBuilder();
         var text = new StringBuilder();

         var foundDelimiter = false;
         var i = 0;
         for ( ; i < line.Length; i++ )
         {
            if ( line[i].ToString() == this.Delimiter )
            {
               foundDelimiter = true;
               break;
            }

            placeholder.Append( line[i] );
         }

         if ( placeholder.Length == 0 )
         {
            throw new ArgumentException( String.Format( "The line \"{0}\" did not contain a placeholder.", line ) );
         }

         if ( !foundDelimiter )
         {
            throw new ArgumentException( String.Format( "The line \"{0}\" did not contain a delimiter (\"{1}\").", line, this.Delimiter ) );
         }

         for ( i += 1; i < line.Length; i++ )
         {
            text.Append( line[i] );
         }

         if ( text.Length == 0 )
         {
            throw new ArgumentException( String.Format( "The line \"{0}\" did not contain replacement text.", line ) );
         }

         return new ReplacementPair( placeholder.ToString(), text.ToString() );
      }
   }
}
