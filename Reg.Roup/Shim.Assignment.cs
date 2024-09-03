using System;
using System.IO;
using System.Linq.Expressions;
using System.Reflection;
using System.Text.RegularExpressions;

namespace Reg.Roup
{
    public partial class Shim
    {
        //public static Shim ForAssignment(Target target, Expression assignmentExpression)
        //{
        //    if (assignmentExpression is UnaryExpression conversionExpression)
        //    {
        //        // TODO there's a chance this is actually a valid case. keep your eyes peeled.
        //        if (!target.Type.CanBeAssignedNull())
        //        {
        //            throw new InvalidCastException(
        //                $"Suspicious implicit conversion or boxing operation for non-nullable target with name '{target.Name}' of type [{target.Type}]."
        //            );
        //        }

        //        assignmentExpression = conversionExpression.Operand;
        //    }

        //    if (assignmentExpression is MethodCallExpression methodCallExpression)
        //    {
        //        var s = Shim.ForTypeConversion(target, methodCallExpression);
        //        return s;
        //    }
        //    // TODO
        //    //else if (assignmentExpression is not ConstantExpression)
        //    else if (!(assignmentExpression is ConstantExpression))
        //    {
        //        throw new NotSupportedException(
        //            $"Expression may only contain calls to methods belonging to type [{RegexExtensions.ParseType}], conversion operations, boxing operations, and constant assignment operations, but encountered [{assignmentExpression}]."
        //        );
        //    }
        //    else if (target.Type != typeof(string))
        //    {
        //        var s = Shim.ForDefaultTypeConversion(target);
        //        return s;
        //    }
        //    else
        //    {
        //        // when all they ask for is a string
        //        var s = Shim.ForNoTypeConversion(target);
        //        return s;
        //    }
        //}
    }
}