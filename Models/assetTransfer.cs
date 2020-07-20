public class assetTransfer
{
    public long TPostID { get; set; }
    public long RPostID { get; set; }
    public string DateofTransfer { get; set; }
    public string TransferType { get; set; }
    public string TransferDescription { get; set; }
    public string EDoc { get; set; }
    public string EDocExtension { get; set; }
    public long UserId { get; set; }
    public long TransferID { get; set; }
    public long ProjectID { get; set; }
    public string SpType { get; set; }
    public string imgFile { get; set; }

    public long IsDeleted { get; set; }
    public long IsUpdated { get; set; }
    public long TransferId { get; set; }
}