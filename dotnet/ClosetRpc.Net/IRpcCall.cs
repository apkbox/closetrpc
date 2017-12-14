// --------------------------------------------------------------------------------
// <copyright file="IRpcCall.cs" company="Private">
//   Copyright (c) Alex Kozlov.
// </copyright>
// <summary>
//   Defines the IRpcCall type.
// </summary>
// --------------------------------------------------------------------------------

namespace ClosetRpc.Net
{
    /// <summary>
    /// Interface that abstracts structure associated with service call request.
    /// </summary>
    public interface IRpcCall
    {
        #region Public Properties

        /// <summary>
        /// Gets data associated with the call.
        /// </summary>
        byte[] CallData { get; }

        /// <summary>
        /// Gets a value indicating whether the call is asynchronous with
        /// respect to the communication and does not require a reply.
        /// The service implementation may use this flag as a hint
        /// to execute call asynchronously.
        /// </summary>
        bool IsAsync { get; }

        /// <summary>
        /// Gets a service method name.
        /// </summary>
        string MethodName { get; }

        /// <summary>
        /// Gets object ID of transient object.
        /// </summary>
        ulong ObjectId { get; }

        /// <summary>
        /// Gets service name.
        /// </summary>
        string ServiceName { get; }

        #endregion
    }
}
