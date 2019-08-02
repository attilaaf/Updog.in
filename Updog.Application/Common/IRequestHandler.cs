using System.Threading.Tasks;

namespace Updog.Application {
    /// <summary>
    /// Interface for interactors to work with.
    /// </summary>
    /// <typeparam name="TInput">The input parameter type.</typeparam>
    public interface IInteractor<TInput> {
        /// <summary>
        /// Process the request.
        /// </summary>
        /// <param name="input">The input type.</param>
        Task Handle(TInput input);
    }

    /// <summary>
    /// Interface for interactors to work with.
    /// </summary>
    /// <typeparam name="TInput">The input parameter type.</typeparam>
    /// <typeparam name="TOutput">The output return type.</typeparam>
    public interface IInteractor<TInput, TOutput> {
        /// <summary>
        /// Process the request.
        /// </summary>
        /// <param name="input">The input type.</param>
        /// <returns>The output of the request.</returns>
        Task<TOutput> Handle(TInput input);
    }
}