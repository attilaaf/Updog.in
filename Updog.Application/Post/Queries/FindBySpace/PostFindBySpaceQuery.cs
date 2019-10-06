using Updog.Domain;
using Updog.Domain.Paging;

namespace Updog.Application {
    public sealed class PostFindBySpaceQuery : AnonymousQuery, IPagableQuery {
        #region Properties
        public string Space { get; set; } = "";
        public PaginationInfo Paging { get; set; } = new PaginationInfo(0, Post.PageSize);
        #endregion
    }
}