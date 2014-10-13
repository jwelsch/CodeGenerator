using System;
using CommandLineLib;

namespace CodeGenerator
{
   public class CommandLineArguments
   {
      [StringValue( 1, Description = "Path to the file containing the template file used to generate code." )]
      public string CodeTemplatePath
      {
         get;
         private set;
      }

      [StringValue( 2, Description = "Path to the file containing the replacement strings that will be used in the template file." )]
      public string ReplacementPath
      {
         get;
         private set;
      }

      [StringValue( 3, Description = "Path of the file that will contain the generated code." )]
      public string GeneratedCodePath
      {
         get;
         private set;
      }

      [Switch( "-", "overwrite", Optional = true, Description = "If this argument is present the generated output file will overwrite any file that has the same path." )]
      public bool Overwrite
      {
         get;
         private set;
      }

      [Switch( "-", "silent", Optional = true, Groups = new int[] { 1 }, Description = "If this argument is present no output is written to the console.  Mutually exclusive with \"-verbose\"." )]
      public bool Silent
      {
         get;
         private set;
      }

      [Switch( "-", "verbose", Optional = true, Groups = new int[] { 2 }, Description = "If this argument is present detailed output is written to the output.  Mutually exclusive with \"-silent\"." )]
      public bool Verbose
      {
         get;
         private set;
      }
   }
}
