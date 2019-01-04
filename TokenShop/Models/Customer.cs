using System;
using ORM;

namespace TokenShop.Models
{
    public class Customer : DBContext<Customer>
    {
        [MetaData(isPrimary: true, isIdentity: true)]
        public long Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string NationalCode { get; set; }
        public DateTime RegisterDate { get; set; }
        public DateTime? BirthDate { get; set; }
        public int Country { get; set; }
        /// <summary>
        /// 1->enable
        /// 2->disable
        /// </summary>
        public int State { get; set; }
        public string City { get; set; }
        public string Address { get; set; }
        public string UserCode { get; set; }
        public string MobilePhone { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string WebAddress { get; set; }
        public string ImageAddress { get; set; }
        public string PasswordResetHash { get; set; }

        [MetaData(notDbField:true)]
        public string RegisterDatePersian
        {
            get
            {
                return Common.PersianDate.ConvertEnglishToPersianDate(this.RegisterDate);
            }
        }
        [MetaData(notDbField: true)]
        public string BirthDatePersian
        {
            get
            {
                return Common.PersianDate.ConvertEnglishToPersianDate(this.BirthDate);
            }
        }
        [MetaData(notDbField: true)]
        public string SateText
        {
            get
            {
                if (State == 1)
                    return "فعال";
                if (State == 2)
                    return "مسدود شده";
                return "";
            }
        }
        [MetaData(notDbField:true,viewField:true)]
        public long TokenCount { get; set; }
    }
}
