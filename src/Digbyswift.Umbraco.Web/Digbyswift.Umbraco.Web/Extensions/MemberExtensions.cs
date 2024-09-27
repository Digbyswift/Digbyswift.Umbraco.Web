using Umbraco.Cms.Core.Models;
using Umbraco.Cms.Core.Security;

namespace Digbyswift.Umbraco.Web.Extensions;

public static class MemberExtensions
{
    public static Dictionary<string, object?> GetDirtyProperties(this IMember member)
    {
        var dirtyEntries = new Dictionary<string, object?>();

        foreach (var property in member.Properties)
        {
            if (member.IsPropertyDirty(property.Alias))
            {
                dirtyEntries.Add(property.Alias, property.GetValue());
            }
        }

        return dirtyEntries;
    }

    public static MemberIdentityUser ToIdentityUser(this IMember member, string memberTypeAlias, bool isApproved = true)
    {
        var identity = MemberIdentityUser.CreateNew(member.Username, member.Email, memberTypeAlias, isApproved, member.Name);

        identity.Id = member.Id.ToString();
        identity.Key = member.Key;
            
        return identity;
    }

}