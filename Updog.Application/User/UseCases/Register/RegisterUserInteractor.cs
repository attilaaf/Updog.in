using System;
using System.Threading.Tasks;
using Updog.Domain;
using FluentValidation;

namespace Updog.Application {
    /// <summary>
    /// Use case interactor for registering a new user with the site.
    /// </summary>
    public sealed class RegisterUserInteractor : IInteractor<RegisterUserParams, UserLogin> {
        #region Fields
        private IUserRepo userRepo;

        private IPasswordHasher passwordHasher;

        private IAuthenticationTokenHandler tokenHandler;

        private AbstractValidator<RegisterUserParams> validator;
        #endregion

        #region Constructor(s)
        public RegisterUserInteractor(IUserRepo userRepo, IPasswordHasher passwordHasher, IAuthenticationTokenHandler tokenHandler, AbstractValidator<RegisterUserParams> validator) {
            this.userRepo = userRepo;
            this.passwordHasher = passwordHasher;
            this.tokenHandler = tokenHandler;
            this.validator = validator;
        }
        #endregion

        #region Publics
        public async Task<UserLogin> Handle(RegisterUserParams input) {
            await validator.ValidateAndThrowAsync(input);

            User user = new User() {
                Username = input.Username,
                PasswordHash = passwordHasher.Hash(input.Password),
                Email = input.Email,
                JoinedDate = System.DateTime.UtcNow
            };

            await userRepo.Add(user);
            string authToken = tokenHandler.IssueToken(user);

            return new UserLogin(new UserInfo(user.Email, user.Username, user.JoinedDate), authToken);
        }
        #endregion
    }
}