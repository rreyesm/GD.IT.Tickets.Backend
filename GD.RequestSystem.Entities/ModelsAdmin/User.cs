using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace GD.RequestSystem.Entities.ModelsAdmin
{
    public class User : GeneralEntity
    {
        [Key]
        public int UserID { get; set; }

        public string? Name { get; set; }
        public string? LastName { get; set; }
        public string? LastName2 { get; set; }
        public string? NickName { get; set; }
        public string? Password { get; set; }
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public bool IsNew { get; set; }

        [ForeignKey("DepartmentID")]
        public int DepartmentID { get; set; }
        public Department? Department { get; set; }

        public User()
        {
            IsNew = true;
        }


        #region TOOLS
        [NotMapped]
        public string RolesString { get; set; }
        [NotMapped]
        public IList<string> Roles { get; set; }
        [NotMapped]
        public string? Token { get; set; }
        [NotMapped]
        public DateTime Expiration { get; set; }

        [NotMapped]
        public string EcryptedPassword
        {
            set { Password = CreateSHA512(value); }
        }

        public string PasswordControl(string password)
        {
            return CreateSHA512(password);
        }


        public static string CreateSHA512(string strData)
        {
            var secretKey = "|mykey";
            var message = Encoding.UTF8.GetBytes(strData + secretKey);
            using (var alg = SHA512.Create())
            {
                string hex = string.Empty;

                var hashValue = alg.ComputeHash(message);
                foreach (byte x in hashValue)
                {
                    hex += String.Format("{0:x2}", x);
                }
                return hex;
            }
        }

        public override string ToString()
        {
            return $"{Name}";
        }

        [NotMapped]
        public string GetName { get { return ToString(); } }
        #endregion
    }
}
