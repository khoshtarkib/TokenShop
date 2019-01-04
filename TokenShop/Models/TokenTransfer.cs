using System;
using ORM;

namespace TokenShop.Models
{
  public  class TokenTransfer : DBContext<TokenTransfer>
    {
        [MetaData(isPrimary: true, isIdentity: true)]
        public long Id { get; set; }
        public int TokenId { get; set; }
        public int CustomerId { get; set; }
        public DateTime TransferDate { get; set; }
        public string SystemIP { get; set; }
        public string Description { get; set; }
        public string TokenCode { get; set; }
        public int UserId { get; set; }
        public int Status { get; set; }

        [MetaData(notDbField: true)]
        public string TransferDatePersian
        {
            get
            {
                return Common.PersianDate.ConvertEnglishToPersianDate(this.TransferDate);
            }
        }
    }
}
