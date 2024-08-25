namespace InsurancePoliciesCRUDApp.Models
{
    public class InsurancePolicy
    {
        public int Id { get; set; }
        public string PolicyNumber { get; set; }
        public string PolicyHolderName { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Type { get; set; }
        public decimal PremiumAmount { get; set; }
    }
}
