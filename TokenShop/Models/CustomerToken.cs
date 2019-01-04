using ORM;
using System;

namespace TokenShop.Models
{
    public class CustomerToken : DBContext<CustomerToken>
    {
        [MetaData(isPrimary: true, isIdentity: true)]
        public long Id { get; set; }
        public long? OrderId { get; set; }
        public long? PayRequestId { get; set; }
        public long CustomerId { get; set; }
        public long Count { get; set; }
        public long RialPrice { get; set; }
        public long DollarPrice { get; set; }
        public long PondPrice { get; set; }
        public long UroPrice { get; set; }
        public DateTime Date { get; set; }
        [MetaData(notDbField: true)]
        public string DatePersian
        {
            get
            {
                return Common.PersianDate.ConvertEnglishToPersianDate(this.Date);
            }
        }
        /// <summary>
        /// 1 increase
        /// 2 descrease
        /// </summary>
        public int Action { get; set; }
        /// <summary>
        /// 1 buy
        /// 2 transfer to
        /// 3 fransfer from
        /// 4 return price
        /// </summary>
        public int ActionBy { get; set; }
        public long ActionCustomerId { get; set; }
        [MetaData(notDbField:true,viewField:true)]
        public string ActionCustomerName { get; set; }
    }
}
