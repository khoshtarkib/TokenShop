using ORM;

namespace TokenShop.Models
{
  public  class Modules : DBContext<Modules>
    {
        [MetaData(isIdentity:true,isPrimary:true)]
        public int Id { get; set; }
        public string FaName { get; set; }
        public string EnName { get; set; }
    }
}
