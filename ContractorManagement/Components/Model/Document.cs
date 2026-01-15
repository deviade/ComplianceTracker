namespace ContractorManagement.Components.Model
{
    public class Document
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Type { get; set; } = string.Empty;
        public DateTime ExpiryDate { get; set; }
        public DateTime UploadDate { get; set; }
        public string Status => GetStatus();
        public int ContractorId { get; set; }

        private string GetStatus()
        {
            var daysUntilExpiry = (ExpiryDate - DateTime.Today).Days;
            if (daysUntilExpiry < 0) return "Expired";
            if (daysUntilExpiry <= 30) return "Expiring Soon";
            return "Valid";
        }
    }
}
