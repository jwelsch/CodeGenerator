using System;
using System.IO;
using CommandLineLib;

namespace CodeGenerator
{
   class Program
   {
      static void Main( string[] args )
      {
         try
         {
            var commandLine = new CommandLine<CommandLineArguments>();
            var arguments = commandLine.Parse( args );

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

            Console.WriteLine( "Reading replacement file \"{0}\".", arguments.ReplacementPath );
            var replacementFile = new ReplacementFile();
            var replacementPairs = replacementFile.Parse( arguments.ReplacementPath );
            Console.WriteLine( "Found {0} replacement pairs.", replacementPairs.Count );

            Console.WriteLine( "Generating output file \"{0}\".", arguments.GeneratedCodePath );
            var generator = new Generator( ( lineNumber ) =>
            {
               Console.Write( "\r  Processing line {0}.", lineNumber );
            } );

            try
            {
               generator.Generate( arguments.CodeTemplatePath, replacementPairs, arguments.GeneratedCodePath, arguments.Overwrite );
            }
            finally
            {
               Console.WriteLine();
            }

            Console.WriteLine( "Code generation completed successfully." );
         }
         catch ( Exception ex )
         {
            System.Diagnostics.Trace.WriteLine( ex );
            Console.WriteLine();
            Console.WriteLine( "Error generating code!" );
            Console.WriteLine( ex );
         }
      }
   }
}
