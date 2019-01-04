using System;
using ORM;

namespace TokenShop.Models
{
   public class Token : DBContext<Token>
    {
        [MetaData(isPrimary: true, isIdentity: true)]
        public int Id { get; set; }
        public int TokenTypeId { get; set; }
        public string TokenCode { get; set; }
        public DateTime CreateDate { get; set; }
        public int UserId { get; set; }
        public int Status { get; set; }
        public long CustomerId { get; set; }

        [MetaData(notDbField: true)]
        public string CreateDatePersian
        {
            get
            {
                return Common.PersianDate.ConvertEnglishToPersianDate(this.CreateDate);
            }
        }
    }
}
