// --------------------------------------------------------------------------------
// <copyright file="Program.cs" company="Private">
//   Copyright (c) Alex Kozlov.
// </copyright>
// <summary>
//   Defines the Program type.
// </summary>
// --------------------------------------------------------------------------------

namespace TransportTestClient
{
    class Program
    {
        #region Methods

        static void Main(string[] args)
        {
            new PingPongClient().Run();
        }

        #endregion
    }
}
