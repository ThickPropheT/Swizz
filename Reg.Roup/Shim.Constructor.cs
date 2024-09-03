using System;
using System.Linq;
using System.Linq.Expressions;
using System.Text.RegularExpressions;

namespace Reg.Roup
{
    // TODO
    //  consider making this a "creation" class or something (resembling match.ToValidatedConversion)
    //  i.e. get rid of shim
    public partial class Shim
    {
        //public static Shim ForConstructor(MatchContext match, NewExpression newExpression)
        //{
        //    var constructor = newExpression.Constructor!;
        //    var argumentExpressions = newExpression.Arguments;

        //    var conversions = constructor
        //        .GetParameters()
        //        .Select((p, i) =>
        //        {
        //            if (p.Name == null)
        //            {
        //                throw new NotSupportedException(
        //                    $"Expected constructor parameter at index {i} of type [{p.ParameterType}] to have a name."
        //                );
        //            }

        //            //return Conversion.Extract(p)
        //            //    .From(match)
        //            //    // TODO
        //            //    //  assuming that these are in the correct order
        //            //    //  test named & optional parameters
        //            //    .By(argumentExpressions[i]);
        //        });

        //    // TODO
        //    //return new(constructor.Name, constructor.DeclaringType!, _ =>
        //    //    constructor.Invoke(shims.Select(s => InvokeShim(s, match.Groups, regex)).ToArray())
        //    return new Shim(new Target(constructor.Name, constructor.DeclaringType!), _ =>
        //            constructor.Invoke(conversions.Select(a => a.Apply()).ToArray())
        //        );
        //}
    }
}