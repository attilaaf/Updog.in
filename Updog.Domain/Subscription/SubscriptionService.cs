using System;
using System.Threading.Tasks;

namespace Updog.Domain {
    public sealed class SubscriptionService : ISubscriptionService {
        #region Fields
        IEventBus bus;
        ISubscriptionFactory factory;
        ISpaceRepo spaceRepo;
        ISubscriptionRepo repo;
        #endregion

        #region Constructor(s)
        public SubscriptionService(IEventBus bus, ISubscriptionFactory factory, ISpaceRepo spaceRepo, ISubscriptionRepo repo) {
            this.bus = bus;
            this.factory = factory;
            this.spaceRepo = spaceRepo;
            this.repo = repo;
        }
        #endregion

        #region Publics
        public async Task<Subscription> CreateSubscription(SubscriptionCreateData data, User user) {
            Space? space = await spaceRepo.FindByName(data.Space);

            if (space == null) {
                throw new InvalidOperationException();
            }

            Subscription s = factory.CreateFor(user, space);
            await repo.Add(s);
            await bus.Dispatch(new SubscriptionCreateEvent(s));

            return s;
        }

        public async Task<Subscription> DeleteSubscription(SubscriptionDeleteData data, User user) {
            Subscription? s = await repo.FindByUserAndSpace(user.Username, data.Space);

            if (s == null) {
                throw new InvalidOperationException();
            }

            await repo.Delete(s);
            await bus.Dispatch(new SubscriptionDeleteEvent(s));
            return s;
        }
        #endregion
    }
}