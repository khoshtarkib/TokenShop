
using ORM;

namespace TokenShop.Models
{
   public class TokenType : DBContext<TokenType>
    {
        [MetaData(isPrimary: true, isIdentity: true)]
        public int Id { get; set; }
        public string Title { get; set; }
        public long RealValue { get; set; }
        public long TomanValue { get; set; }
        public string Description { get; set; }
    }
}
