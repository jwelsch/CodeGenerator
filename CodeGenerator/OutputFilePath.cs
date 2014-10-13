using System;
using System.Text;
using System.Collections.Generic;

namespace CodeGenerator
{
   public class OutputFilePath
   {
      public string FilePathTemplate
      {
         get;
         private set;
      }

      public OutputFilePath( string filePathTemplate )
      {
         this.FilePathTemplate = filePathTemplate;
      }

      public string Generate( IEnumerable<ReplacementPair> replacementPairSet )
      {
         var path = new StringBuilder( this.FilePathTemplate );

         foreach ( var pair in replacementPairSet )
         {
            path.Replace( pair.Placeholder, pair.Replacement );
         }

         return path.ToString();
      }
   }
}
