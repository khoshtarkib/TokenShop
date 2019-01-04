using System;
using ORM;

namespace TokenShop.Models
{
   public class ErrorLog : DBContext<ErrorLog>
    {
        [MetaData(isPrimary: true, isIdentity: true)]
        public int Id { get; set; }
        public string ErrorType { get; set; }
        public DateTime ErrorDate { get; set; }
        public int UserId { get; set; }
        public long CustomerId { get; set; }
        public int ModuleId { get; set; }
        public string Description { get; set; }

        [MetaData(notDbField: true)]
        public string ErrorDatePersian
        {
            get
            {
                return Common.PersianDate.ConvertEnglishToPersianDate(this.ErrorDate);
            }
        }
    }
}
