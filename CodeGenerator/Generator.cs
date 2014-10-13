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

      public void Generate( string templateFilePath, IEnumerable<IEnumerable<ReplacementPair>> replacementPairs, OutputFilePath outputFilePath, bool overwrite )
      {
         var line = new StringBuilder();

         foreach ( var pairSet in replacementPairs )
         {
            using ( var writer = new StreamWriter( outputFilePath.Generate( pairSet ) ) )
            {
               using ( var reader = new StreamReader( templateFilePath ) )
               {
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

                     if ( line.Length > 0 )
                     {
                        foreach ( var replacementPair in pairSet )
                        {
                           line.Replace( replacementPair.Placeholder, replacementPair.Replacement );
                        }
                     }

                     writer.WriteLine( line.ToString() );
                  }
               }

               writer.WriteLine();
            }
         }
      }
   }
}
