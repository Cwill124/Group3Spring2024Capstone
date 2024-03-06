namespace DesktopCapstone.model
{
    /// <summary>
    /// Represents an application user with basic information.
    /// </summary>
    public class AppUser
    {
        /// <summary>
        /// Gets or sets the username of the user.
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        /// Gets or sets the first name of the user.
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// Gets or sets the last name of the user.
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// Gets or sets the email address of the user.
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Gets or sets the phone number of the user.
        /// </summary>
        public string PhoneNumber { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="AppUser"/> class.
        /// Default constructor with empty values.
        /// </summary>
        public AppUser()
        {
            this.Username = string.Empty;
            this.FirstName = string.Empty;
            this.LastName = string.Empty;
            this.Email = string.Empty;
            this.PhoneNumber = string.Empty;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AppUser"/> class with specified values.
        /// </summary>
        /// <param name="username">The username of the user.</param>
        /// <param name="firstName">The first name of the user.</param>
        /// <param name="lastName">The last name of the user.</param>
        /// <param name="email">The email address of the user.</param>
        /// <param name="phoneNumber">The phone number of the user.</param>
        public AppUser(string username, string firstName, string lastName, string email, string phoneNumber)
        {
            this.Username = username;
            this.FirstName = firstName;
            this.LastName = lastName;
            this.Email = email;
            this.PhoneNumber = phoneNumber;
        }
    }
}
