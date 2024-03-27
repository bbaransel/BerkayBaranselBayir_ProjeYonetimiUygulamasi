namespace Yonetimsell.Entity.Abstract
{
    public abstract class BaseEntity
    {
        public string Name { get; set; }
        public DateTime ModifiedDate { get; set; } = DateTime.Now;

    }
}
