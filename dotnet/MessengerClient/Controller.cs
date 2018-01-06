namespace MessengerClient
{
    using System;
    using System.IO;
    using System.Net.Sockets;

    using NanoMessanger;

    public class Controller
    {
        #region Fields

        private bool exitRequested;

        private LoginService_Proxy loginService= new LoginService_Proxy(null);

        private SessionInfo sessionInfo;

        private WorkflowState state = WorkflowState.NotLoggedIn;

        #endregion

        #region Public Methods and Operators

        public void Run()
        {
            this.exitRequested = false;
            Console.WriteLine("Connected. Press Ctrl+C to exit.");
            while (!this.exitRequested)
            {
                Console.Write(">");
                var text = Console.ReadLine();
                if (text == null)
                {
                    Console.WriteLine("Press Ctrl+H for help.");
                    continue;
                }

                try
                {
                    //var result = this.SendMessage(text);
                    //Console.WriteLine(result);
                }
                catch (IOException)
                {
                    Console.WriteLine("error: Read error.");
                    break;
                }
                catch (SocketException)
                {
                    Console.WriteLine("error: Connection lost.");
                    break;
                }
            }
        }

        #endregion

        #region Methods

        private bool Login()
        {
            while (true)
            {
                Console.Write("Your name? ");
                var userName = Console.ReadLine();
                if (userName == null)
                {
                    Console.WriteLine("Sorry, I do not know you.");
                    continue;
                }

                Console.Write("Password? ");
                var password = Console.ReadLine() ?? string.Empty;

                try
                {
                    this.sessionInfo = this.loginService.Login(
                        new AuthenticationData { Username = userName, Password = password });
                }
                catch (Exception)
                {
                    Console.WriteLine("Login failed.");
                    return false;
                }

                return true;
            }
        }

        private void Logout()
        {
            this.loginService.Logout();
            this.sessionInfo = null;
        }

        private void MainMenu()
        {
            while (true)
            {
                switch (this.state)
                {
                    case WorkflowState.NotLoggedIn:
                        Console.WriteLine("1 (default)  Login");
                        Console.WriteLine("2            Register");
                        Console.WriteLine("3 ('q', 'x') Exit");
                        var c = Console.Read();
                        if (c == '1')
                        {
                            if (this.Login())
                            {
                                this.state = WorkflowState.LoggedIn;
                            }
                        }
                        else if (c == '2')
                        {
                            if (this.Register())
                            {
                                if (this.Login())
                                {
                                    this.state = WorkflowState.LoggedIn;
                                }
                            }
                        }
                        else if (c == '3' || c == 'x' || c == 'X' || c == 'q' || c == 'Q')
                        {
                            return;
                        }
                        else
                        {
                            continue;
                        }

                        break;

                    case WorkflowState.LoggedIn:
                        LoggedIn();
                        break;
                }
            }
        }

        private void LoggedIn()
        {

        }

        private bool Register()
        {
            while (true)
            {
                Console.Write("Screen name? ");
                var userName = Console.ReadLine();
                if (userName == null)
                {
                    Console.WriteLine("Anonymous, is that you?");
                    continue;
                }

                Console.Write("Password? ");
                var password = Console.ReadLine() ?? string.Empty;
                if (string.IsNullOrWhiteSpace(password))
                {
                    Console.WriteLine("This is not an ideal world, there are dangers out there.");
                    continue;
                }

                try
                {
                    this.loginService.Register(new RegistrationData() { Username = userName, Password = password });
                }
                catch (Exception )
                {
                    Console.WriteLine("Registration failed.");
                    return false;
                }

                return true;
            }
        }

        #endregion
    }

    internal enum WorkflowState
    {
        NotLoggedIn,

        LoggedIn
    }
}
