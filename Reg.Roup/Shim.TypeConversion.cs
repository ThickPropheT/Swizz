using System;
using System.Linq.Expressions;

namespace Reg.Roup
{
    public partial class Shim
    {
        //public static Shim ForTypeConversion(Target target, MethodCallExpression parseCallExpression)
        //{
        //    var method = parseCallExpression.Method;

        //    if (method.DeclaringType != RegexExtensions.ParseType)
        //    {
        //        throw new NotSupportedException(
        //            $"Expression may only contain calls to methods belonging to type [{RegexExtensions.ParseType}], but encountered method [{method}]."
        //        );
        //    }

        //    // TODO
        //    //=> new(targetName, targetType, v =>
        //    return new Shim(target, v =>
        //    {
        //        var argumentExpression = parseCallExpression.Arguments[0];
        //        var parseDelegate = (Delegate)Expression.Lambda(argumentExpression).Compile().DynamicInvoke()!;

        //        return parseDelegate.DynamicInvoke(v);
        //    });
        //}
    }
}