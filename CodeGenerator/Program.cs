using System;
using System.IO;
using CommandLineLib;

namespace CodeGenerator
{
   class Program
   {
      private enum ReportLevel
      {
         Silent,
         Normal,
         Verbose
      }

      static int Main( string[] args )
      {
         var result = 0;
         var reportLevel = ReportLevel.Normal;

         try
         {
            var commandLine = new CommandLine<CommandLineArguments>();
            var arguments = commandLine.Parse( args );

            if ( arguments.Silent )
            {
               reportLevel = ReportLevel.Silent;
            }
            else if ( arguments.Verbose )
            {
               reportLevel = ReportLevel.Verbose;
            }

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

            if ( reportLevel == ReportLevel.Verbose )
            {
               Console.WriteLine( "Reading replacement file \"{0}\".", arguments.ReplacementPath );
            }

            var replacementFile = new ReplacementFile();
            var replacementPairs = replacementFile.Parse( arguments.ReplacementPath );

            if ( reportLevel == ReportLevel.Verbose )
            {
               Console.WriteLine( "Found {0} replacement pairs.", replacementPairs.Count );
            }

            Generator generator = null;
            var fileCount = 0;

            if ( reportLevel != ReportLevel.Verbose )
            {
               generator = new Generator( null, ( outputPath ) =>
               {
                  fileCount++;
               } );
            }
            else
            {
               generator = new Generator( ( lineNumber ) =>
               {
                  Console.Write( "\r  Processing line {0}.", lineNumber );
               },
               ( outputPath ) =>
               {
                  fileCount++;
                  Console.WriteLine( "{0}Generating output file \"{1}\".", fileCount == 1 ? "" : "\n", outputPath );
               } );
            }

            var outputFilePath = new OutputFilePath( arguments.GeneratedCodePath );

            try
            {
               generator.Generate( arguments.CodeTemplatePath, replacementPairs, outputFilePath, arguments.Overwrite );
            }
            finally
            {
               if ( reportLevel == ReportLevel.Verbose )
               {
                  Console.WriteLine();
               }
            }

            if ( reportLevel != ReportLevel.Silent )
            {
               Console.WriteLine( String.Format( "Generated {0} code files successfully.", fileCount ) );
            }
         }
         catch ( Exception ex )
         {
            result = 1;

            System.Diagnostics.Trace.WriteLine( ex );

            if ( reportLevel != ReportLevel.Silent )
            {
               Console.WriteLine( "Error generating code!" );
               Console.WriteLine( ex );
            }
         }

         return result;
      }
   }
}
