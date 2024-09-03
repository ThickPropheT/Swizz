using System;
using System.Linq.Expressions;
using System.Text.RegularExpressions;

namespace Reg.Roup
{
    public class SchemaMember
    {
        private readonly Expression _expression;

        public string Name { get; }
        public Type Type { get; }
        public bool IsOptional { get; }

        public SchemaMember(string name, Type type, Expression expression)
        {
            Name = name;
            Type = type;
            IsOptional = type.CanBeAssignedNull();

            _expression = IgnoreImplicitConversions(expression);
        }

        private Expression IgnoreImplicitConversions(Expression expression)
        {
            if (expression is UnaryExpression conversionExpression)
            {
                // TODO there's a chance this is actually a valid case. keep your eyes peeled.
                if (!IsOptional)
                {
                    throw new InvalidCastException(
                        $"Suspicious implicit conversion or boxing operation for non-nullable target with name '{Name}' of type [{Type}]."
                    );
                }

                return conversionExpression.Operand;
            }

            return expression;
        }

        public Func<string?, object?>? FindConverter()
        {
            if (_expression is not MethodCallExpression methodCall)
                return null;

            return value =>
            {
                var argumentExpression = methodCall.Arguments[0];
                var parseDelegate = (Delegate)Expression.Lambda(argumentExpression).Compile().DynamicInvoke()!;

                return parseDelegate.DynamicInvoke(value);
            };
        }

        public GroupValue FindValueIn(GroupCollection groups)
        {
            var candidate = groups[Name];

            if (candidate != null
                && candidate.Success)
            {
                return GroupValue.FromGroup(this, candidate);
            }
            if (!IsOptional)
            {
                throw new FormatException(
                    $"Regex could not match group '{Name}' for input string, but group is not optional (nullable)."
                );
            }

            return GroupValue.FromOptional(this);
        }
    }
}
