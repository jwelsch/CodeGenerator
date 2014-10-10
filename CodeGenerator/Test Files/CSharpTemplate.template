using System;
using System.Reflection;

namespace $<namespace>$
{
   public class $<ValueType>$Value
   {
      public $<ValueType>$ Value
      {
         get;
         set;
      }

      public $<ValueType>$Value( $<ValueType>$ value )
      {
         this.Value = value;
      }

      public static $<ValueType>$ FromString( string value )
      {
         var type = this.Value.GetType();
         var parseMethodInfo = type.GetMethod( "$<ParseMethodName>$", new Type[] { typeof( System.String ) } );

         if ( parseMethodInfo == null )
         {
            throw new NotImplementedException( String.Format( "The method \"$<ParseMethodName>$\" is not implemented on type \"{0}\".", type.Name ) );
         }

         return ($<ValueType>$) parseMethodInfo.Invoke( null, new object[] { value } );
      }
   }
}