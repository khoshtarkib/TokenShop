using ORM;

namespace TokenShop.Models
{
    public class Setting : DBContext<Setting>
    {
        [MetaData(isPrimary:true)]
        public int Id { get; set; }
        public string SiteTitle { get; set; }
        public int SiteStatus { get; set; }
        public string SiteDomain { get; set; }
        public int SmtpPort { get; set; }
        public string SmtpAddress { get; set; }
        public string SmtpUserName { get; set; }
        public string SmtpPassword { get; set; }
        public int GlobalIncomePercent { get; set; }
        public string Slogan { get; set; }
        public long Rial { get; set; }
        public long Dollar { get; set; }
        public long Uro { get; set; }
        public long Pond { get; set; }
    }
}