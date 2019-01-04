using System;
using ORM;

namespace TokenShop.Models
{
    public class UserAccount : DBContext<UserAccount>
    {
        [MetaData(isPrimary: true, isIdentity: true)]
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public DateTime RegisterDate { get; set; }
        /// <summary>
        /// 1->enabel
        /// 2->disable
        /// 10 superadmin
        /// </summary>
        public int State { get; set; }
        public string Address { get; set; }
        public string MobilePhone { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Permission { get; set; }
        public string PasswordResetHash { get; set; }

        [MetaData(notDbField: true)]
        public string RegisterDatePersian
        {
            get
            {
                return Common.PersianDate.ConvertEnglishToPersianDate(this.RegisterDate);
            }
        }
        public string GetStatus()
        {
            if (this.State == 1)
                return "فعال";
            if (this.State == 2)
                return "غیرفعال";
            return "";
        }
    }
}
