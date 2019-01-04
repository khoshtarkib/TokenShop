using System;
using ORM;

namespace TokenShop.Models
{
    public class TicketContent : DBContext<TicketContent>
    {
        [MetaData(isPrimary: true, isIdentity: true)]
        public long Id { get; set; }
        public long TicketId { get; set; }
        public string Content { get; set; }
        public DateTime RegisterDate { get; set; }
        public string FileAddress { get; set; }
        public string FileType { get; set; }
        public long CustomerId { get; set; }
        public int UserId { get; set; }

        [MetaData(notDbField: true)]
        public string RegisterDatePersian
        {
            get
            {
                return Common.PersianDate.ConvertEnglishToPersianDate(this.RegisterDate);
            }
        }
        [MetaData(notDbField:true,viewField:true)]
        public string UserName { get; set; }
        [MetaData(notDbField: true, viewField: true)]
        public string CustomerName { get; set; }
    }
}
