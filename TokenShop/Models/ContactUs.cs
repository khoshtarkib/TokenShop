using System;
using ORM;

namespace TokenShop.Models
{
    public class ContactUs:DBContext<ContactUs>
    {
        [MetaData(isPrimary: true, isIdentity: true)]
        public long Id { get; set; }
        public DateTime Date { get; set; }
        public string Name { get; set; }
        public string Subject { get; set; }
        public string Email { get; set; }
        public string Message { get; set; }
        public string Ip { get; set; }
        public bool Read { get; set; }

        [MetaData(notDbField: true)]
        public string DatePersian
        {
            get
            {
                return Common.PersianDate.ConvertEnglishToPersianDate(this.Date);
            }
        }
    }
}