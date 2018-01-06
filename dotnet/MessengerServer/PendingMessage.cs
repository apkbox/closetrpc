// --------------------------------------------------------------------------------
// <copyright file="PendingMessage.cs" company="Private">
//   Copyright (c) Alex Kozlov.
// </copyright>
// <summary>
//   Defines the PendingMessage type.
// </summary>
// --------------------------------------------------------------------------------

namespace MessengerServer
{
    public class PendingMessage
    {
        #region Fields

        public string FromUser;

        public string Text;

        #endregion

        #region Public Properties

        public int Ordinal { get; set; }

        #endregion
    }
}
