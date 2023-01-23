public class sudDisposalDetail
{
    public long DisposalPaymentID { get; set; }
    public long DisposalID { get; set; }
    public long AssetID { get; set; }
    public float DisposalValue { get; set; }
    public float ReservePrice { get; set; }
    public float CurrentMarketvalue { get; set; }
    public float BookValue { get; set; }
    public float GainLoss { get; set; }
    public string Remarks { get; set; }
    public string Edoc { get; set; }
    public string EDocExtension { get; set; }
    public string imgFile { get; set; }
    public long UserId { get; set; }
    public string SpType { get; set; }
}