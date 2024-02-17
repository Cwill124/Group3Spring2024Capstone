using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace desktop_capstone.model
{

        public static class PasswordHasher
        {
            #region Methods

            /// <summary>
            ///     hashes the password using the PasswordHasher from Microsoft.AspNetCore.Identity
            /// </summary>
            /// <param name="password"></param>
            /// <returns></returns>
            public static string HashPassword(string password)
            {
                var hasher = new PasswordHasher<string>();

                return hasher.HashPassword(null, password);
            }

            /// <summary>
            ///     Checks the hashed password using the PasswordHasher from Microsoft.AspNetCore.Identity
            /// </summary>
            /// <param name="hashedPassword"></param>
            /// <param name="inputtedPassword"></param>
            /// <returns></returns>
            public static PasswordVerificationResult CheckHashedPassword(string hashedPassword, string inputtedPassword)
            {
                var hasher = new PasswordHasher<string>();

                return hasher.VerifyHashedPassword(null, hashedPassword, inputtedPassword);
            }

            #endregion
        }
}
