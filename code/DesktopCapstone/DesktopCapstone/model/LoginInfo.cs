namespace DesktopCapstone.model
{
    /// <summary>
    /// Represents login information with a username and password.
    /// </summary>
    public class LoginInfo
    {
        /// <summary>
        /// Gets or sets the username associated with the login information.
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        /// Gets or sets the password associated with the login information.
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="LoginInfo"/> class.
        /// Default constructor with empty values.
        /// </summary>
        public LoginInfo()
        {
            this.Username = string.Empty;
            this.Password = string.Empty;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="LoginInfo"/> class with specified values.
        /// </summary>
        /// <param name="username">The username associated with the login information.</param>
        /// <param name="password">The password associated with the login information.</param>
        public LoginInfo(string username, string password)
        {
            this.Username = username;
            this.Password = password;
        }
    }
}