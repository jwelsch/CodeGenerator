using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace CodeGenerator
{
   public class ReplacementTree
   {
      public List<ReplacementPair> Pairs
      {
         get;
         private set;
      }

      public List<ReplacementTree> Nodes
      {
         get;
         private set;
      }

      public ReplacementTree()
      {
         this.Pairs = new List<ReplacementPair>();
         this.Nodes = new List<ReplacementTree>();
      }

      public ReadOnlyCollection<ReadOnlyCollection<ReplacementPair>> GetReplacements()
      {
         var result = ReplacementTree.Collapse( this, new List<ReplacementPair>() );

         return result.AsReadOnly();
      }

      private static List<ReadOnlyCollection<ReplacementPair>> Collapse( ReplacementTree tree, List<ReplacementPair> knownPairs )
      {
         var combinedPairs = new List<ReplacementPair>( knownPairs );
         var output = new List<ReadOnlyCollection<ReplacementPair>>();

         for ( var i = 0; i < tree.Pairs.Count; i++ )
         {
            var found = false;

            for ( var j = 0; j < combinedPairs.Count; j++ )
            {
               if ( tree.Pairs[i].Placeholder == combinedPairs[j].Placeholder )
               {
                  combinedPairs[i] = tree.Pairs[i];
                  found = true;
                  break;
               }
            }

            if ( !found )
            {
               combinedPairs.Add( tree.Pairs[i] );
            }
         }

         output.Add( combinedPairs.AsReadOnly() );

         foreach ( var node in tree.Nodes )
         {
            output.AddRange( ReplacementTree.Collapse( node, combinedPairs ) );
         }

         return output;
      }
   }
}
