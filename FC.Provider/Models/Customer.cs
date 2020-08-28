using System;
using System.ComponentModel.DataAnnotations;
using FC.Provider.Enums;

namespace FC.Provider.Models
{
    public class Customer
    {
        [Key]
        public long Id { get; set; }
        public string CustomerName { get; set; }
        public string CustomerLastName { get; set; }

        // Email Setting
        public string CustomerEmailAddress { get; set; }
        public string CustomerOldEmailAddress { get; set; }
        public DateTime EmailAddressChangeDate { get; set; }

        // Phone Setting
        public string CustomerPhoneNumber { get; set; }
        public string CustomerOldPhoneNumber { get; set; }
        public DateTime PhoneNumberChangeDate { get; set; }

        // Password Setting
        public byte[] CustomerPassword { get; set; }
        public byte[] CustomerOldPassword { get; set; }
        public DateTime PasswordChangeDate { get; set; }

        public bool CustomerEmailVerified { get; set; }
        public bool CustomerPhoneVerified { get; set; }

        // Authentication Setting
        public string AuthenticationName { get; set; }
        public int AuthenticationId { get; set; }

        // State Setting
        public string StateName { get; set; }
        public int StateId { get; set; }

        public string Roles { get; set; }
        public string RawId { get; set; }

        public string SecureId { get; set; }
        public DateTime SecureDate { get; set; }

        public DateTime LastSignInDate { get; set; }
        public DateTime UpdateDate { get; set; }
        public DateTime CreateDate { get; set; }
    }
}
