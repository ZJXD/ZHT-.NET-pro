namespace Utils
{
    public class DiskInfo
    {
        private string Name { get; set; }
        private long Size { get; set; }
        private long FreeSpace { get; set; }

        public DiskInfo(string v1, long v2, long v3)
        {
            this.Name = v1;
            this.Size = v2;
            this.FreeSpace = v3;
        }
    }
}