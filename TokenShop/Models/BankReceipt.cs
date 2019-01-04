using System;
using ORM;

namespace TokenShop.Models
{
  public  class BankReceipt:DBContext<BankReceipt>
    {
        [MetaData(isPrimary:true,isIdentity:true)]
        public long Id { get; set; }
        public string BankName { get; set; }
        public string ReceiptNumber { get; set; }
        public long Amount { get; set; }
        public DateTime PayDate { get; set; }
        public int UserId { get; set; }
        public long CustomerId { get; set; }
        /// <summary>
        /// 1 در انتظار تایید
        /// </summary>
        public int Statuse { get; set; }
        /// <summary>
        /// خرید توکن
        /// </summary>
        public int ReciptType { get; set; }
        public string Description { get; set; }
        public long BankAccountId { get; set; }
        public long OrderId { get; set; }
        public string Attachment { get; set; }
        [MetaData(notDbField: true, viewField: true)]
        public string CustomerName { get; set; }

        [MetaData(notDbField: true)]
        public string PayDatePersian {
            get
            {
                return Common.PersianDate.ConvertEnglishToPersianDate(this.PayDate);
            }
        }
        [MetaData(notDbField: true)]
        public string StatusText
        {
            get
            {
                if (this.Statuse == 1)
                    return TokenShop.Common.LanguageManagment.Translate("draft");
                if (this.Statuse == 2)
                    return TokenShop.Common.LanguageManagment.Translate("valid");
                if (this.Statuse == 3)
                    return TokenShop.Common.LanguageManagment.Translate("invalid");
                return "";
            }
        }
        [MetaData(notDbField: true)]
        public string ReciptTypeText
        {
            get
            {
                if (this.ReciptType == 1)
                    return TokenShop.Common.LanguageManagment.Translate("token_buy");
                if (this.ReciptType == 2)
                    return TokenShop.Common.LanguageManagment.Translate("pick_price");
                if (this.ReciptType == 3)
                    return TokenShop.Common.LanguageManagment.Translate("return_price");
                return "";
            }
        }
    }
}
