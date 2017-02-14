using Ky.Hbe.Framework.Batch.Utilities;

namespace DScheduler.Framework
{
    /// <summary>
    /// Use this object to construct credentials that can be used by Windows.
    /// </summary>
    public struct WindowsCredentials
    {
        #region Private Properties

        private string _domain;
        private string _password;
        private string _userName;

        #endregion Private Properties

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="WindowsCredentials"/> class.
        /// Constructor which needs the Windows User Name, Password and Domain to be passed in.
        /// </summary>
        /// <param name="userName">Windows User Name</param>
        /// <param name="password">Windows Password</param>
        /// <param name="domain">Windows Domain</param>
        public WindowsCredentials(string userName, string password, string domain)
        {
            ConsoleLogger.WriteLogEntry(string.Format("WindowsCredentials UserName = {0}", userName));
            ConsoleLogger.WriteLogEntry(string.Format("WindowsCredentials Domain = {0}", domain));

            _userName = userName;
            _password = password;
            _domain = domain;
        }

        #endregion Constructor

        #region Public Properties

        /// <summary>
        /// Windows Domain
        /// </summary>
        public string Domain
        {
            get
            {
                return _domain;
            }

            set
            {
                _domain = value;
            }
        }

        /// <summary>
        /// Windows Password
        /// </summary>
        public string Password
        {
            get
            {
                return _password;
            }

            set
            {
                _password = value;
            }
        }

        /// <summary>
        /// Windows User Name
        /// </summary>
        public string UserName
        {
            get
            {
                return _userName;
            }

            set
            {
                _userName = value;
            }
        }

        #endregion Public Properties
    }
}