using System;
using ORM;

namespace TokenShop.Models
{
    public class Order:DBContext<Order>
    {
        [MetaData(isPrimary: true, isIdentity: true)]
        public long Id { get; set; }
        public long CustomerId { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime ModifyDate { get; set; }
        public long Amount { get; set; }
        public string Description { get; set; }
        public long Count { get; set; }
        public long Dollar { get; set; }
        public long Rial { get; set; }
        public long Uro { get; set; }
        public long Pond { get; set; }
        public int IncomePercent { get; set; }
        /// <summary>
        /// 1 rial
        /// 2 dollar
        /// 3 uro
        /// 4 pond
        /// </summary>
        public int Currency { get; set; }
        public long IncomePrice { get; set; }
        /// <summary>
        /// 1 -> pending
        /// 2 -> approve
        /// </summary>
        public int Status { get; set; }

        [MetaData(notDbField: true)]
        public string CreateDatePersian
        {
            get
            {
                return Common.PersianDate.ConvertEnglishToPersianDate(this.CreateDate);
            }
        }
        [MetaData(notDbField: true)]
        public string ModifyDatePersian
        {
            get
            {
                return Common.PersianDate.ConvertEnglishToPersianDate(this.ModifyDate);
            }
        }
        public string getStatus()
        {
            if (this.Status == 1)
                return "معلق";
            if (this.Status == 2)
                return "پرداخت شده";
            return "";
        }
        [MetaData(notDbField:true,viewField:true)]
        public string CustomerName { get; set; }
    }
}