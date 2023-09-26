namespace AdMoney.Models
{
    public class ModelSelectData
    {
        public int modelId { get; set; }    
        public string? riskProfile { get; set; }
        
       public List<AssetInfo>? assetInfo { get; set; }

    }
}
