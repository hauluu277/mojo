using System;
using mojoPortal.Business;
using mojoPortal.Business.WebHelpers;
using ArticleFeature.Business;

namespace mojoPortal.Features
{
    public class ArticleContentDeleteHandler : ContentDeleteHandlerProvider
    {
        public override void DeleteContent(int moduleId, Guid moduleGuid)
        {
            Article.DeleteByModule(moduleId);

            ContentMetaRespository metaRepository = new ContentMetaRespository();
            metaRepository.DeleteByModule(moduleGuid);
            FriendlyUrl.DeleteByPageGuid(moduleGuid, 1, string.Empty);
        }
    }
}
