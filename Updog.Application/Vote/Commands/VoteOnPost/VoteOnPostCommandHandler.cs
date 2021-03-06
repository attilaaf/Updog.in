using System.Threading.Tasks;
using Updog.Domain;

namespace Updog.Application {
    public sealed class VoteOnPostCommandHandler : CommandHandler<VoteOnPostCommand> {
        #region Fields
        private IVoteService service;
        #endregion

        #region Constructor(s)
        public VoteOnPostCommandHandler(IVoteService service) {
            this.service = service;
        }
        #endregion

        #region Private
        [Validate(typeof(VoteOnPostCommandValidator))]
        protected async override Task<Either<CommandResult, Error>> ExecuteCommand(VoteOnPostCommand command) {
            Vote v = await service.VoteOnPost(command.Data, command.User);
            return Insert(v.Id);
        }
        #endregion
    }
}