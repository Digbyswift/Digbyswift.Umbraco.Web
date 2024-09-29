using Umbraco.Cms.Core.Models;
using Umbraco.Cms.Core.Services;

namespace Digbyswift.Umbraco.Web.Extensions;

public static class ContentServiceExtensions
{
    public static IEnumerable<IContent> GetAllChildren(this IContentService contentService, int parentId)
    {
        var currentPageIndex = 0;
        long totalRecords;
        var retrievedContent = new List<IContent>();

        do
        {
            var pagedChildren = contentService.GetPagedChildren(parentId, currentPageIndex++, 100, out totalRecords);
            retrievedContent.AddRange(pagedChildren);
        }
        while (totalRecords > retrievedContent.Count);

        return retrievedContent;
    }

    public static IEnumerable<IContent> GetAllOfType(this IContentService contentService, int contentTypeId)
    {
        var currentPageIndex = 0;
        long totalRecords;
        var retrievedContent = new List<IContent>();

        do
        {
            var pagedChildren = contentService.GetPagedOfTypes([contentTypeId], currentPageIndex++, 100, out totalRecords, null);
            retrievedContent.AddRange(pagedChildren);
        }
        while (totalRecords > retrievedContent.Count);

        return retrievedContent;
    }
}
