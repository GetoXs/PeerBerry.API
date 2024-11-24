namespace PeerBerry.API.ResponseModels.BalanceMainReponse
{
	public class BalanceMainReponse
	{
		public string currencyIso { get; set; }
		public decimal availableMoney { get; set; }
		public string invested { get; set; }
		public string totalProfit { get; set; }
		public decimal totalBalance { get; set; }
		public string balanceGrowth { get; set; }
		public string balanceGrowthAmount { get; set; }
		public string netAnnualReturn { get; set; }
	}

}
