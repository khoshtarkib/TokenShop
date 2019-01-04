using System;
using ORM;

namespace TokenShop.Models
{
    public class BankTransactions : DBContext<BankTransactions>
    {
        [MetaData(isPrimary: true, isIdentity: true)]
        public long Id { get; set; }
        public long CustomerId { get; set; }
        public long OrderId { get; set; }
        public DateTime PayDate { get; set; }
        public long Amount { get; set; }
        public string Description { get; set; }
        public int Status { get; set; }
        public string TrackingCode { get; set; }
        public string ReciptionNumber { get; set; }
        public int GatewayId { get; set; }

        [MetaData(notDbField: true)]
        public string PayDatePersian
        {
            get
            {
                return Common.PersianDate.ConvertEnglishToPersianDate(this.PayDate);
            }
        }
        [MetaData(notDbField:true,viewField:true)]
        public string CustomerName { get; set; }
        [MetaData(notDbField: true, viewField: true)]
        public string GatewayName { get; set; }
        
    }
}
