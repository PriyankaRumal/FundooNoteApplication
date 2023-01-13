using System;
using System.Collections.Generic;
using System.Text;

namespace CommonLayer.Model
{
    public class PasswordReset
    {
        public string Email { get; set; }
        public string New_Password { get; set; }
        public string Confirm_Password { get; set; }

    }
}
