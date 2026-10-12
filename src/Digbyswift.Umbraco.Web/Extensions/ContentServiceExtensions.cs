using Umbraco.Cms.Core.Models;
using Umbraco.Cms.Core.Services;

namespace Digbyswift.Umbraco.Web.Extensions;

public static class ContentServiceExtensions
{
    /// <summary>
    /// Returns a yielded collection of all the children belonging to the specified parent.
    /// This uses the <see cref="IContentService.GetPagedChildren"/> method to retrieve the
    /// children and yields the results per page. This prevents creating one large list in
    /// memory.
    /// </summary>
    public static IEnumerable<IContent> GetAllChildren(this IContentService contentService, int parentId)
    {
        var currentPageIndex = 0;
        long retrievedRecordCount = 0;
        long totalRecords;

        do
        {
            var pagedChildren = contentService.GetPagedChildren(parentId, currentPageIndex++, 100, out totalRecords);
            foreach (var node in pagedChildren)
            {
                retrievedRecordCount++;
                yield return node;
            }
        }
        while (totalRecords > retrievedRecordCount);
    }

    /// <summary>
    /// Returns a yielded collection of all the children belonging to the specified parent.
    /// This uses the <see cref="IContentService.GetPagedOfTypes"/> method to retrieve the
    /// children and yields the results per page. This prevents creating one large list in
    /// memory.
    /// </summary>
    public static IEnumerable<IContent> GetAllOfType(this IContentService contentService, int contentTypeId)
    {
        var currentPageIndex = 0;
        long retrievedRecordCount = 0;
        long totalRecords;

        do
        {
            var pagedChildren = contentService.GetPagedOfTypes([contentTypeId], currentPageIndex++, 100, out totalRecords, null);
            foreach (var node in pagedChildren)
            {
                retrievedRecordCount++;
                yield return node;
            }
        }
        while (totalRecords > retrievedRecordCount);
    }
}
