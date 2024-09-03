using System;
using System.Linq.Expressions;

namespace Reg.Roup
{
    public class DeserializationSchema
    {
        public static NewSchema From(LambdaExpression schema)
        {
            if (schema.Body is MemberInitExpression memberInitExpression)
            {
                return MemberInitSchema.From(memberInitExpression);
            }
            else if (schema.Body is NewExpression newExpression)
            {
                return NewSchema.From(newExpression);
            }
            else
            {
                throw new ArgumentException(
                    "Expected expression to construct an object.", nameof(schema)
                );
            }
        }
    }
}
