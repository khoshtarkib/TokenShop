using ORM;

namespace TokenShop.Models
{
   public class ModuleAccess : DBContext<ModuleAccess>
    {
        [MetaData(isPrimary: true)]
        public int ModuleId { get; set; }
        [MetaData(isPrimary: true)]
        public int UserId { get; set; }
        public bool ReadAccess { get; set; }
        public bool DeleteAdccess { get; set; }
        public bool WriteAccess { get; set; }
    }
}
