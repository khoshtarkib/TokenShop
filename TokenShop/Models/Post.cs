using ORM;
using System;

namespace TokenShop.Models
{
    public class Post : DBContext<Post>
    {
        [MetaData(isPrimary: true, isIdentity: true)]
        public long Id { get; set; }
        public string Subject { get; set; }
        public string PostContent { get; set; }
        public string Cover { get; set; }
        /// <summary>
        /// 1->پابلیش شده
        /// 2->پیش نویس
        /// </summary>
        public int Status { get; set; }
        /// <summary>
        /// 1 خبر
        /// 2 سوالات متداول
        /// 3 اسلایدر
        /// </summary>
        public int Type { get; set; }
        public int Point { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime ModifyDate { get; set; }
        public string FriendlyUrl { get; set; }
        public int Seen { get; set; }
        public int UserId { get; set; }
        public int ModifiedUserId { get; set; }

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
                return "پابلیش شده";
            if (this.Status == 2)
                return "پیش نویس";
            return "";
        }

    }
}
