using Updog.Domain;

namespace Updog.Application {
    public sealed class RemoveModeratorFromSpaceCommand : RoleAlterCommand {
        #region Properties
        public string Space { get; }
        #endregion

        #region Constructor(s)
        public RemoveModeratorFromSpaceCommand(string space, string username, User user) : base(username, user) {
            Space = space;
        }
        #endregion
    }
}