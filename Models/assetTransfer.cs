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
    public long? ProjectID { get; set; }
    public string SpType { get; set; }
    public string imgFile { get; set; }

    public long IsDeleted { get; set; }
    public long IsUpdated { get; set; }

    public string Transfee { get; set; }
    public string TransfeeCompany { get; set; }
    public string ReceiveBy { get; set; }
    public string ReceiveCompany { get; set; }
    public string ProjectShortName { get; set; }
    public string ProjectName { get; set; }

    public int TSubLocID { get; set; }
    public int TOfficeSecID { get; set; }
    public int TOfficeTypeID { get; set; }
    public int RSubLocID { get; set; }
    public int ROfficeSecID { get; set; }
    public int OfficeTypeID { get; set; }
    public string SubLocationDescription { get; set; }
    public string OfficeTypeDescription { get; set; }

}