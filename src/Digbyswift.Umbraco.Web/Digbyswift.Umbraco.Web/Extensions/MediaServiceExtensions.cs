using Umbraco.Cms.Core.Models;
using Umbraco.Cms.Core.Services;

namespace Digbyswift.Umbraco.Web.Extensions;

public static class MediaServiceExtensions
{
    /// <summary>
    /// Returns a yielded collection of all the children belonging to the specified parent.
    /// This uses the <see cref="IMediaService.GetPagedChildren"/> method to retrieve the
    /// children and yields the results per page. This prevents creating one large list in
    /// memory.
    /// </summary>
    public static IEnumerable<IMedia> GetAllChildren(this IMediaService mediaService, int parentId)
    {
        var currentPageIndex = 0;
        long retrievedRecordCount = 0;
        long totalRecords;

        do
        {
            var pagedChildren = mediaService.GetPagedChildren(parentId, currentPageIndex++, 100, out totalRecords);
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
    /// This uses the <see cref="IMediaService.GetPagedOfTypes"/> method to retrieve the
    /// children and yields the results per page. This prevents creating one large list in
    /// memory.
    /// </summary>
    public static IEnumerable<IMedia> GetAllOfType(this IMediaService mediaService, int mediaTypeId)
    {
        var currentPageIndex = 0;
        long retrievedRecordCount = 0;
        long totalRecords;

        do
        {
            var pagedChildren = mediaService.GetPagedOfTypes([mediaTypeId], currentPageIndex++, 100, out totalRecords, null);
            foreach (var node in pagedChildren)
            {
                retrievedRecordCount++;
                yield return node;
            }
        }
        while (totalRecords > retrievedRecordCount);
    }
}
