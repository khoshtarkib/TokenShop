using System;
using ORM;

namespace TokenShop.Models
{
   public class Ticket : DBContext<Ticket>
    {
        #region property
        [MetaData(isPrimary: true, isIdentity: true)]
        public long Id { get; set; }
        public string Subject { get; set; }
        /// <summary>
        /// 1-> در انتظار پاسخ
        /// 2-> پاسخ داده شده
        /// 3-> بسته شده
        /// </summary>
        public int Status { get; set; }
        public long CustomerId { get; set; }
        public int UserId { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime StatusDate { get; set; }
        public int UnitId { get; set; }
        public int Priority { get; set; }
        #endregion

        [MetaData(notDbField: true, viewField: true)]
        public string CustomerName { get; set; }

        #region methods
        [MetaData(notDbField: true)]
        public string CreateDatePersian
        {
            get
            {
                return Common.PersianDate.ConvertEnglishToPersianDate(this.CreateDate);
            }
        }
        [MetaData(notDbField: true)]
        public string StatusDatePersian
        {
            get
            {
                return Common.PersianDate.ConvertEnglishToPersianDate(this.StatusDate);
            }
        }
        [MetaData(notDbField: true)]
        public string getPeriority()
        {
            if (this.Priority == 1)
                return "فوری";
            if (this.Priority == 2)
                return "متوسط";
            if (this.Priority == 3)
                return "کم";
            return "";
        }
        [MetaData(notDbField: true)]
        public string getStatus()
        {
            if (this.Status == 1)
                return "در انتظار پاسخ";
            if (this.Status == 2)
                return "پاسخ داده شده";
            if (this.Status == 3)
                return "بسته شده";
            return "";
        }
        #endregion
    }
}
