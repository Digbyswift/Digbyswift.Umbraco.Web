using Umbraco.Cms.Core.Models;
using Umbraco.Cms.Core.Security;
using Umbraco.Extensions;

namespace Digbyswift.Umbraco.Web.Extensions;

public static class MemberExtensions
{
    public static Dictionary<string, object?> GetDirtyProperties(this IMember member)
    {
        return member
            .Properties
            .Where(property => member.IsPropertyDirty(property.Alias))
            .ToDictionary(property => property.Alias, property => property.GetValue());
    }

    public static MemberIdentityUser ToIdentityUser(this IMember member, string memberTypeAlias, bool isApproved = true)
    {
        var identity = MemberIdentityUser.CreateNew(member.Username, member.Email, memberTypeAlias, isApproved, member.Name);

        identity.Id = member.Id.ToInvariantString();
        identity.Key = member.Key;

        return identity;
    }
}
