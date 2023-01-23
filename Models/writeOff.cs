public class writeOff
{
    public long WriteOffID { get; set; }
    public string LossDate { get; set; }
    public string DescriptionofLoss { get; set; }
    public float ValueofLoss { get; set; }
    public string InquiryOfficer { get; set; }
    public string InquiryRemarks { get; set; }
    public string AuthorityApproveWriteOff { get; set; }
    public float WriteOffAmountApproved { get; set; }
    public float ValueofLossToRecover { get; set; }
    public int IsDeleted { get; set; }
    public long SubLocID { get; set; }
    public string Location { get; set; }
}