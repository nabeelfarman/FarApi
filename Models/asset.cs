public class asset
{
    public long SubLocID { get; set; }
    public long OfficeTypeID { get; set; }
    public long AssetCatID { get; set; }
    public long AssetNo { get; set; }
    public long OfficeSecID { get; set; }
    public long? PostID { get; set; }
    public string AssetLocation { get; set; }
    public string AssetDescription { get; set; }
    public string otherIdentification { get; set; }
    public string SerialNo { get; set; }
    public long? VehicleID { get; set; }
    public long? ProjectID { get; set; }
    public string PreviousTag { get; set; }
    public float costAmount { get; set; }
    public float NetBookAmount { get; set; }
    public string PurchaseDate { get; set; }
    public long? IPCRef { get; set; }
    public string AssetCondition { get; set; }
    public bool IsUseable { get; set; }
    public bool IsSurplus { get; set; }
    public bool IsServiceAble { get; set; }
    public bool IsCondemned { get; set; }
    public bool IsMissing { get; set; }
    public string Remarks { get; set; }
    public int IsDeleted { get; set; }
    public string DeletionDate { get; set; }
    public long DeleteBy { get; set; }
    public int IsUpdated { get; set; }
    public string UpdatedDate { get; set; }
    public long Updatedby { get; set; }
    public long AssetID { get; set; }
    public long UserId { get; set; }
    public string SpType { get; set; }

    public string EDoc { get; set; }
    public string EDoc2 { get; set; }
    public string EDoc3 { get; set; }
    public string EDocExtension { get; set; }
    public string imgFile { get; set; }
    public string imgFile2 { get; set; }
    public string imgFile3 { get; set; }
    public int Qty { get; set; }
    public bool isTransfer { get; set; }
    public long? TransferID { get; set; }
}