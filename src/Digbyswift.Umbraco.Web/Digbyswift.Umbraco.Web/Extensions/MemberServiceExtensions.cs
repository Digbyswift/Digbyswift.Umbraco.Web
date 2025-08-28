using Umbraco.Cms.Core.Models;
using Umbraco.Cms.Core.Services;

namespace Digbyswift.Umbraco.Web.Extensions;

public static class MemberServiceExtensions
{
    /// <summary>
    /// Returns a yielded collection of all the members. This uses the
    /// <see cref="IMemberService.GetAll"/> method to retrieve the members
    /// and yields the results per page. This prevents creating one large
    /// list in memory.
    /// </summary>
    public static IEnumerable<IMember> GetAll(this IMemberService memberService)
    {
        var currentPageIndex = 0;
        long retrievedRecordCount = 0;
        long totalRecords;

        do
        {
            var pagedChildren = memberService.GetAll(currentPageIndex++, 100, out totalRecords);
            foreach (var node in pagedChildren)
            {
                retrievedRecordCount++;
                yield return node;
            }
        }
        while (totalRecords > retrievedRecordCount);
    }
}
