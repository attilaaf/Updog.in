namespace Updog.Api {
    /// <summary>
    /// Property and error message pair.
    /// </summary>
    public sealed class ValidationFailure {
        #region Properties
        /// <summary>
        /// The field name.
        /// </summary>
        /// <value></value>
        public string Field { get; }

        /// <summary>
        /// The error message.
        /// </summary>
        public string Message { get; }
        #endregion

        #region Constructor(s)
        /// <summary>
        /// Create a new validation failure.
        /// </summary>
        /// <param name="field">The name of the field.</param>
        /// <param name="message">The error message.</param>
        public ValidationFailure(string field, string message) {
            Field = field;
            Message = message;
        }
        #endregion
    }
}