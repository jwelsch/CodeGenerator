using System;
using System.IO;
using System.Text;
using System.Collections.Generic;

namespace CodeGenerator
{
   public delegate void OutputFileCallback( string path );
   public delegate void ProgressCallback( int lineNumber );

   public class Generator
   {
      private ProgressCallback ProgressListener
      {
         get;
         set;
      }

      private OutputFileCallback OutputFileListener
      {
         get;
         set;
      }

      public Generator( ProgressCallback progressListener, OutputFileCallback outputFileListener )
      {
         this.ProgressListener = progressListener;
         this.OutputFileListener = outputFileListener;
      }

      public void Generate( string templateFilePath, IEnumerable<IEnumerable<ReplacementPair>> replacementPairs, OutputFilePath outputFilePath, bool overwrite )
      {
         var line = new StringBuilder();

         foreach ( var pairSet in replacementPairs )
         {
            var outputPath = outputFilePath.Generate( pairSet );

            if ( this.OutputFileListener != null )
            {
               this.OutputFileListener( outputPath );
            }

            if ( File.Exists( outputPath ) && !overwrite )
            {
               throw new ArgumentException( String.Format( "The output file \"{0}\" already exists.", outputPath ), "overwrite" );
            }

            using ( var writer = new StreamWriter( outputPath ) )
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
