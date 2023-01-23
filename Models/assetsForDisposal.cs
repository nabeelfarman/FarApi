public class assetsForDisposal
{
    public string SubLocationDescription { get; set; }
    public string OfficeTypeDescription { get; set; }

    public string DisposalDate { get; set; }
    public string PurchaserName { get; set; }
    public string PurchaserAddress { get; set; }
    public string PartyCode { get; set; }
    public float AmountPaid { get; set; }
    public float TaxPaid { get; set; }
    public string Remarks { get; set; }
    public string PurchaserCNIC { get; set; }
    public string PurchaserNTN { get; set; }
    //public string FilePath { get; set; }
    //public string EDocExtension { get; set; }
    public int UserID { get; set; }
    public int DisposalID { get; set; }




    public long MainLocId { get; set; }
    public int SubLocID { get; set; }
    public int OfficeTypeID { get; set; }
    public int AccountsCatID { get; set; }
    public int AssetCatID { get; set; }
    public int AssetID { get; set; }
    public string MainLocationDescription { get; set; }
    public bool ISCondemned { get; set; }

    public string AssetCatDescription { get; set; }
    public string AccountsCatagory { get; set; }
    public string AccountsCatagoryDisplay { get; set; }
    public string Tag { get; set; }
    public string AssetDescription { get; set; }
    public string AssetLocation { get; set; }
    public string Make { get; set; }
    public string Model { get; set; }
    public string Size { get; set; }
    public string Generation { get; set; }
    public string Processor { get; set; }
    public string RAM { get; set; }
    public string DriveType1 { get; set; }
    public string HD1 { get; set; }
    public string DriveType2 { get; set; }
    public string HD2 { get; set; }
    public string Author { get; set; }
    public string Publisher { get; set; }
    public string Edition { get; set; }
    public string OtherIdentification { get; set; }
    public string SerialNo { get; set; }
    public float CostAmount { get; set; }
    public float NetBookAmount { get; set; }
    public string AssetCondition { get; set; }
    public string EDoc { get; set; }
    public string EDoc2 { get; set; }
    public string EDoc3 { get; set; }
    public string ProjectShortName { get; set; }

    public string ProjectName { get; set; }
    public int ProjectID { get; set; }
    public string VehID { get; set; }
    public string ChasisNum { get; set; }
    public string EngineNum { get; set; }
    public string VehMake { get; set; }
    public string VehType { get; set; }
    public string VehModel { get; set; }
    public float RevaluedAmount { get; set; }
    public float AccDepreciationonCost { get; set; }
    public float AccDepreciationonRevalued { get; set; }



    public long OfficeSecID { get; set; }
    public string AccountsSection { get; set; }
    public float BidAmount { get; set; }
    public float ReservePrice { get; set; }
    public float CurrentMarketValue { get; set; }
    public long DisposalPaymentID { get; set; }
}