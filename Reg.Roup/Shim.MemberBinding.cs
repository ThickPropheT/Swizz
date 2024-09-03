using System;
using System.Linq.Expressions;
using System.Reflection;
using System.Text.RegularExpressions;

namespace Reg.Roup
{

    // TODO
    //  consider making this a "creation" class or something (resembling match.ToValidatedConversion)
    //  i.e. get rid of shim
    public partial class Shim
    {
        //public static Shim ForMemberBinding(MemberBinding binding, MatchContext match)
        //{
        //    // TODO
        //    //if (binding is not MemberAssignment memberAssignment)
        //    if (!(binding is MemberAssignment memberAssignment))
        //    {
        //        throw new NotSupportedException(
        //            "Neither recursive nor collection member initialization are supported."
        //        );
        //    }

        //    var member = SettableSchemaMember.From(binding.Member);

        //    var conversion = Conversion.Extract(member)
        //        .From(match)
        //        .By(memberAssignment.Expression);

        //    // TOD
        //    //return new(name, type, v =>
        //    return new Shim(new Target(member.MemberName, member.MemberType), v =>
        //    {
        //        member.Set(v!, conversion.Apply());
        //        return v;
        //    });
        //}
    }
}