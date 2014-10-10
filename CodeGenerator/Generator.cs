using System;
using System.IO;
using System.Text;
using System.Collections.Generic;

namespace CodeGenerator
{
   public delegate void ProgressCallback( int lineNumber );

   public class Generator
   {
      private ProgressCallback ProgressListener
      {
         get;
         set;
      }

      public Generator( ProgressCallback progressListener )
      {
         this.ProgressListener = progressListener;
      }

      public void Generate( string templateFilePath, IEnumerable<ReplacementPair> replacementPairs, string outputFilePath, bool overwrite )
      {
         using ( var writer = new StreamWriter( outputFilePath ) )
         {
            using ( var reader = new StreamReader( templateFilePath ) )
            {
               var line = new StringBuilder();
               var lineNumber = 0;

               while ( !reader.EndOfStream )
               {
                  lineNumber++;

                  if ( this.ProgressListener != null )
                  {
                     this.ProgressListener( lineNumber );
                  }

                  line.Clear();
                  line.Append( reader.ReadLine() );

                  foreach ( var replacementPair in replacementPairs )
                  {
                     line.Replace( replacementPair.Placeholder, replacementPair.Replacement );
                  }

                  writer.WriteLine( line.ToString() );
               }
            }
         }
      }
   }
}
