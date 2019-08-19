
using System;
using System.Threading.Tasks;
using Updog.Domain;
using FluentValidation;

namespace Updog.Application {
    /// <summary>
    /// Updater to handler updating comments.
    /// </summary>
    public sealed class CommentUpdater : IInteractor<CommentUpdateParams, Comment> {
        #region Fields
        private IPermissionHandler<Comment> commentPermissionHandler;

        private ICommentRepo commentRepo;

        private AbstractValidator<CommentUpdateParams> commentValidator;
        #endregion

        #region Constructor(s)
        public CommentUpdater(IPermissionHandler<Comment> commentPermissionHandler, ICommentRepo commentRepo, AbstractValidator<CommentUpdateParams> commentValidator) {
            this.commentPermissionHandler = commentPermissionHandler;
            this.commentRepo = commentRepo;
            this.commentValidator = commentValidator;
        }
        #endregion

        #region Publics
        public async Task<Comment> Handle(CommentUpdateParams input) {
            Comment comment = await commentRepo.FindById(input.CommentId);

            if (comment == null) {
                throw new NotFoundException();
            }

            if (!(await this.commentPermissionHandler.HasPermission(input.User, PermissionAction.UpdateComment, comment))) {
                throw new AuthorizationException();
            }

            await commentValidator.ValidateAndThrowAsync(input);

            comment.Body = input.Body;
            comment.WasUpdated = true;
            await commentRepo.Update(comment);
            return comment;
        }
        #endregion
    }
}