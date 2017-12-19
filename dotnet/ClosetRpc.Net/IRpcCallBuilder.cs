﻿// --------------------------------------------------------------------------------
// <copyright file="IRpcCallBuilder.cs" company="Private">
//   Copyright (c) Alex Kozlov.
// </copyright>
// <summary>
//   Defines the IRpcCallBuilder type.
// </summary>
// --------------------------------------------------------------------------------

namespace ClosetRpc
{
    public interface IRpcCallBuilder
    {
        #region Public Properties

        byte[] CallData { get; set; }

        bool IsAsync { get; set; }

        string MethodName { get; set; }

        ulong ObjectId { get; set; }

        string ServiceName { get; set; }

        #endregion
    }
}
