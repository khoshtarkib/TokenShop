using ORM;

namespace TokenShop.Models
{
    public class BankAccount : DBContext<BankAccount>
    {
        [MetaData(isPrimary: true, isIdentity: true)]
        public long Id { get; set; }
        public long CustomerId { get; set; }
        public int Status { get; set; }
        public string CardNumber { get; set; }
        public string Sheba { get; set; }
        public string AccountNumber { get; set; }
        public int BankId { get; set; }
        public string getStatus()
        {
            if (this.Status == 1)
                return "در انتظار تایید";
            if (this.Status == 2)
                return "تایید شده و فعال";
            if (this.Status == 3)
                return "رد شده";
            return "";
        }
        [MetaData(notDbField:true)]
        public string BankName
        {
            get
            {
                if (this.BankId == 1)
                    return "ملی";
                return "";
            }
        }
    }
}