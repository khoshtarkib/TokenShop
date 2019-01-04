using System;
using ORM;

namespace TokenShop.Models
{
    public class PayRequest : DBContext<PayRequest>
    {
        [MetaData(isPrimary: true, isIdentity: true)]
        public long Id { get; set; }
        public long Amount { get; set; }
        public DateTime RequestDate { get; set; }
        public DateTime? PayDate { get; set; }
        public long CustomerId { get; set; }
        public int Status { get; set; }
        public int IncomePercent { get; set; }
        public long IncomePrice { get; set; }
        public long BankAccountId { get; set; }
        public string Description { get; set; }
        public long Count { get; set; }
        public long Dollar { get; set; }
        public long Rial { get; set; }
        public long Pond { get; set; }
        public long Uro { get; set; }
        public int Currency { get; set; }
      
        [MetaData(notDbField: true, viewField: true)]
        public string CustomerName { get; set; }
        [MetaData(notDbField:true,viewField:true)]
        public int BankId { get; set; }
        [MetaData(notDbField: true)]
        public string BankName
        {
            get
            {
                if (this.BankId == 1)
                    return "ملی";
                return "";
            }
        }
        [MetaData(notDbField: true, viewField: true)]
        public string CartNumber { get; set; }
        [MetaData(notDbField: true, viewField: true)]
        public string AccountNumber { get; set; }
        [MetaData(notDbField: true, viewField: true)]
        public string Sheba { get; set; }

        [MetaData(notDbField: true)]
        public string PayDatePersian
        {
            get
            {
                return Common.PersianDate.ConvertEnglishToPersianDate(this.PayDate);
            }
        }
        [MetaData(notDbField: true)]
        public string RequestDatePersian
        {
            get
            {
                return Common.PersianDate.ConvertEnglishToPersianDate(this.RequestDate);
            }
        }
        public string getStatus()
        {
            if (this.Status == 1)
                return "در انتظار بررسی";
            if (this.Status == 2)
                return "واریز شد";
            if (this.Status == 3)
                return "مورد تایید نمی باشد";
            return "";
        }
    }
}