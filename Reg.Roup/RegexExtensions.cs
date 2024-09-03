using System;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text.RegularExpressions;

namespace Reg.Roup
{
    public static class RegexExtensions
    {
        public static T MatchAndDeserializeTo<T>(this Regex regex, string input, Expression<Func<Parse, T>> deserializationSchema) 
            => DeserializeTo<T>(new MatchContext(regex, regex.Match(input)), DeserializationSchema.From(deserializationSchema));

        public static T DeserializeTo<T>(this Match match, Expression<Func<Parse, T>> deserializationSchema, Regex? regex = null)
            => DeserializeTo<T>(new MatchContext(regex, match), DeserializationSchema.From(deserializationSchema));

        private static T DeserializeTo<T>(MatchContext match, NewSchema schema)
        {
            match.Validate();

            try
            {
                return (T)schema.CreateInstanceFrom(match);
            }
            catch (NotSupportedException ex)
            {
                throw new ArgumentException("Expression format is not supported.", nameof(schema), ex);
            }
            catch { throw; }
        }

        //private static object FromMemberInit(MatchContext match, MemberInitExpression memberInitExpression)
        //{
        //    var newExpression = memberInitExpression.NewExpression;

        //    var constructorShim = newExpression.Constructor != null
        //        ? Shim.ForConstructor(match, newExpression)
        //        : Shim.ForDefaultConstructor(newExpression.Type);

        //    var memberBindingShims = memberInitExpression.Bindings.Select(b => Shim.ForMemberBinding(b, match)).ToArray();

        //    var instance = constructorShim.Invoke(null)!;

        //    foreach (var memberBinding in memberBindingShims)
        //    {
        //        memberBinding.Invoke(instance);
        //    }

        //    return instance;
        //}
    }
}
