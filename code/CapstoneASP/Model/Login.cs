using System.ComponentModel.DataAnnotations.Schema;

namespace CapstoneASP.Model
{
    public class Login
    {
        #region Properties

        [Column("username")]public string Username { get; set; }
        [Column("password")]public string Password { get; set; }

        #endregion

        #region Methods

        public override bool Equals(object? obj)
        {
            if (obj is null || GetType() != obj.GetType())
            {
                return false;
            }
            var other = (Login)obj;

            return Username == other.Username && Password == other.Password;
        }
        #endregion
    }
}
