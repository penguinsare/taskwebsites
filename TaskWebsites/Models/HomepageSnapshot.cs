namespace TaskWebsites.Models
{
    public class HomepageSnapshot
    {
        public int Id { get; set; }
        public string PathToFileOnDisk { get; set; }
        public string UrlPath { get; set; }
        public string Filename { get; set; }
        public string FileType{ get; set; }
    }
}