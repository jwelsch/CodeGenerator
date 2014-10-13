using System;
using System.IO;
using System.Text;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace CodeGenerator
{
   public class ReplacementFile
   {
      public ReplacementFile()
      {
      }

      public ReadOnlyCollection<ReadOnlyCollection<ReplacementPair>> Parse( string replacementFilePath )
      {
         var replacementTreeReader = new ReplacementTreeReader();
         var tree = replacementTreeReader.Read( replacementFilePath );

         //tree.Debug();

         return tree.GetReplacements();
      }
   }
}
