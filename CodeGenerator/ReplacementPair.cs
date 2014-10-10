using System;

namespace CodeGenerator
{
   public class ReplacementPair
   {
      public string Placeholder
      {
         get;
         private set;
      }

      public string Replacement
      {
         get;
         private set;
      }

      public ReplacementPair( string placeholder, string replacement )
      {
         this.Placeholder = placeholder;
         this.Replacement = replacement;
      }
   }
}
