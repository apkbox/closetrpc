// --------------------------------------------------------------------------------
// <copyright file="IEventHandlerStub.cs" company="Private">
//   Copyright (c) Alex Kozlov.
// </copyright>
// <summary>
//   Defines the IEventHandlerStub type.
// </summary>
// --------------------------------------------------------------------------------

namespace ClosetRpc
{
    public interface IEventHandlerStub
    {
        #region Public Methods and Operators

        void CallMethod(IRpcCall rpcCall);

        #endregion
    }
}
