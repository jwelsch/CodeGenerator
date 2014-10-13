using System;
using System.IO;
using CommandLineLib;

namespace CodeGenerator
{
   class Program
   {
      static int Main( string[] args )
      {
         var result = 0;
         var silent = false;

         try
         {
            var commandLine = new CommandLine<CommandLineArguments>();
            var arguments = commandLine.Parse( args );
            silent = arguments.Silent;

            if ( !File.Exists( arguments.CodeTemplatePath ) )
            {
               throw new FileNotFoundException( String.Format( "The code template file was not found at the path \"{0}\".", arguments.CodeTemplatePath ), arguments.CodeTemplatePath );
            }

            if ( !File.Exists( arguments.ReplacementPath ) )
            {
               throw new FileNotFoundException( String.Format( "The replacement file was not found at the path \"{0}\".", arguments.ReplacementPath ), arguments.ReplacementPath );
            }

            if ( File.Exists( arguments.GeneratedCodePath ) && !arguments.Overwrite )
            {
               throw new ArgumentException( String.Format( "The output file \"{0}\" already exists.", arguments.GeneratedCodePath ) );
            }

            if ( !silent )
            {
               Console.WriteLine( "Reading replacement file \"{0}\".", arguments.ReplacementPath );
            }

            var replacementFile = new ReplacementFile();
            var replacementPairs = replacementFile.Parse( arguments.ReplacementPath );

            if ( !silent )
            {
               Console.WriteLine( "Found {0} replacement pairs.", replacementPairs.Count );
            }

            Generator generator = null;
            var first = true;

            if ( silent )
            {
               generator = new Generator( null, null );
            }
            else
            {
               generator = new Generator( ( lineNumber ) =>
               {
                  Console.Write( "\r  Processing line {0}.", lineNumber );
               },
               ( outputPath ) =>
               {
                  Console.WriteLine( "{0}Generating output file \"{1}\".", first ? "" : "\n", outputPath );
                  first = false;
               } );
            }

            var outputFilePath = new OutputFilePath( arguments.GeneratedCodePath );

            try
            {
               generator.Generate( arguments.CodeTemplatePath, replacementPairs, outputFilePath, arguments.Overwrite );
            }
            finally
            {
               Console.WriteLine();
            }

            if ( !silent )
            {
               Console.WriteLine( "Code generation completed successfully." );
            }
         }
         catch ( Exception ex )
         {
            result = 1;

            System.Diagnostics.Trace.WriteLine( ex );

            if ( !silent )
            {
               Console.WriteLine( "Error generating code!" );
               Console.WriteLine( ex );
            }
         }

         return result;
      }
   }
}
