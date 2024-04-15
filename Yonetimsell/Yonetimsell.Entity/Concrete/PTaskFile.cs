namespace Yonetimsell.Entity.Concrete
{
    public class PTaskFile
    {
        public int Id { get; set; }
        public int PTaskId { get; set; }
        public PTask PTask { get; set; }
        public string FileName { get; set; }
        public string FileUrl { get; set; }
    }
}
