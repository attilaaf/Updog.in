using Updog.Domain;

namespace Updog.Application {
    /// <summary>
    /// Params to find a page of comments for a post.
    /// </summary>
    public sealed class CommentFinderByPostParams : IAnonymousActionParams {
        #region Properties
        /// <summary>
        /// The ID of the post to look for.
        /// </summary>
        public int PostId { get; }

        /// <summary>
        /// The user performing the look up.
        /// </summary>
        public User? User { get; }
        #endregion

        #region Constructor(s)
        public CommentFinderByPostParams(int postId, User? user = null) {
            PostId = postId;
            User = user;
        }
        #endregion
    }
}