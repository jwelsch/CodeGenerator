using System;
using System.Text;
using System.Collections.Generic;

namespace CodeGenerator
{
   public class OutputFilePath
   {
      private static string[] EscapedCharacters = { "^\\", "^&", "^|", "^>", "^<", "^^" };
      private static string[] UnescapedCharacters = { "\\", "&", "|", ">", "<", "^" };

      public string FilePathTemplate
      {
         get;
         private set;
      }

      public OutputFilePath( string filePathTemplate )
      {
         this.FilePathTemplate = this.UnescapePlaceholders( filePathTemplate );
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

      /// <summary>
      /// Windows may escape certain characters with a caret ('^') if they are passed via the
      /// command line.  This method will attempt to remove escaped characters.
      /// </summary>
      /// <param name="replacementPairSet">Contains placeholders with escaped characters.</param>
      /// <returns>Contains placeholders without escaped characters.</returns>
      private string UnescapePlaceholders( string filePathTemplate )
      {
         var unescaped = new StringBuilder( filePathTemplate );

         for ( var i = 0; i < OutputFilePath.EscapedCharacters.Length; i++ )
         {
            unescaped.Replace( OutputFilePath.EscapedCharacters[i], OutputFilePath.UnescapedCharacters[i] );
         }

         return unescaped.ToString();
      }
   }
}
