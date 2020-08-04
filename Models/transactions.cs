public class transactions
{
    public long TransID { get; set; }
    public string TransDate { get; set; }
    public long AccountCode { get; set; }
    public long Debit { get; set; }
    public long Credit { get; set; }
    public long UserID { get; set; }
    public string CreatedDateTime { get; set; }
    public long IsUploaded { get; set; }
    public long IsSupervised { get; set; }
    public string SuperviseDateTime { get; set; }
    public long SupervisedBy { get; set; }
    public long IsPosted { get; set; }
    public string PostedDateTime { get; set; }
    public long PostReference { get; set; }
    public string UpdatedDateTime { get; set; }
    public long UpdatedBy { get; set; }
    public long IsDeleted { get; set; }
    public long DeletedBy { get; set; }
    public string DeletedDateTime { get; set; }
    public string TypeofEntry { get; set; }
    public string Year { get; set; }
    public long FaDetailID { get; set; }
    public long FixedAssetID { get; set; }
    public long AccountCatID { get; set; }
    public long ProjectID { get; set; }
    public long OfficeSecID { get; set; }
    public long RoadID { get; set; }
    public string AccountTitle { get; set; }
    public string ProjectShortName { get; set; }
}