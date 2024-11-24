namespace PeerBerry.API.ResponseModels.LoansResponse
{
	public class LoansResponse
	{
		public Loan[] data { get; set; }
		public int total { get; set; }
		public _Meta _meta { get; set; }
	}

	public class _Meta
	{
		public int? pages { get; set; }
		public int? perpage { get; set; }
		public int? offset { get; set; }
		public int? pageSize { get; set; }
	}

	public class Loan
	{
		public int loanId { get; set; }
		public int countryId { get; set; }
		public string country { get; set; }
		public string countryIso { get; set; }
		public string loanOriginator { get; set; }
		public int originatorId { get; set; }
		public string issuedDate { get; set; }
		public string finalPaymentDate { get; set; }
		public string termType { get; set; }
		public string termTypeTitle { get; set; }
		public string status { get; set; }
		public string statusTitle { get; set; }
		public float interestRate { get; set; }
		public int? remainingTerm { get; set; }
		public int? term { get; set; }
		public int? initialTerm { get; set; }
		public int? loanAmount { get; set; }
		public int? assignedAmount { get; set; }
		public float? availableToInvest { get; set; }
		public bool? allowedToInvest { get; set; }
		public int? minimumInvestmentAmount { get; set; }
		public float? investedAmount { get; set; }
		public string currencySign { get; set; }
		public bool? buyback { get; set; }
		public bool? sellback { get; set; }
		public int? days { get; set; }
		public int? order_position { get; set; }
	}
}
