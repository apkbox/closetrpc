// --------------------------------------------------------------------------------
// <copyright file="ScreenController.cs" company="Private">
//   Copyright (c) Alex Kozlov.
// </copyright>
// <summary>
//   Defines the ScreenController type.
// </summary>
// --------------------------------------------------------------------------------

namespace MessengerClientUI
{
    using System.ComponentModel;
    using System.Runtime.CompilerServices;

    using MessengerClientUI.Annotations;

    public class ScreenController : INotifyPropertyChanged
    {
        #region Public Events

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion



        #region Methods

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion
    }
}
