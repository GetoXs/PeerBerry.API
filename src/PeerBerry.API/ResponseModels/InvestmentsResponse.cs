namespace PeerBerry.API.ResponseModels.InvestmentsResponse
{
	public class InvestmentsResponse
	{
		public Sort sort { get; set; }
		public Datum[] data { get; set; }
		public int total { get; set; }
		public int current_count { get; set; }
		public int finished_count { get; set; }
		public _Meta _meta { get; set; }
		public Request request { get; set; }
		public string q { get; set; }
	}

	public class Sort
	{
		public string loanId { get; set; }
		public string countryId { get; set; }
		public string dateOfPurchase { get; set; }
		public string interestRate { get; set; }
		public string amount { get; set; }
		public string finalPaymentDate { get; set; }
		public string estimatedFinalPaymentDate { get; set; }
		public string estimatedNextPayment { get; set; }
		public string finishedAt { get; set; }
		public string receivedPayments { get; set; }
	}

	public class _Meta
	{
		public int pages { get; set; }
		public int perpage { get; set; }
		public string sort { get; set; }
	}

	public class Request
	{
		public string offset { get; set; }
		public int pageSize { get; set; }
		public string type { get; set; }
		public string sort { get; set; }
		public int investorId { get; set; }
	}

	public class Datum
	{
		public int id { get; set; }
		public int loanId { get; set; }
		public int countryId { get; set; }
		public string countryIso { get; set; }
		public string country { get; set; }
		public int originatorId { get; set; }
		public string originatorTitle { get; set; }
		public string dateOfPurchase { get; set; }
		public string loanType { get; set; }
		public float interest { get; set; }
		public float interestRate { get; set; }
		public float amount { get; set; }
		public string nextPaymentDate { get; set; }
		public string finalPaymentDate { get; set; }
		public string estimatedFinalPaymentDate { get; set; }
		public string finishedAt { get; set; }
		public float estimatedNextPayment { get; set; }
		public string lastPaymentDate { get; set; }
		public int term { get; set; }
		public float receivedPayments { get; set; }
		public float remainingPrincipal { get; set; }
		public string status { get; set; }
		public string currencySign { get; set; }
		public bool isFinished { get; set; }
		public Secondarymarket secondaryMarket { get; set; }
	}

	public class Secondarymarket
	{
		public bool canBeSold { get; set; }
		public object activeSale { get; set; }
		public bool sold { get; set; }
		public object soldAmount { get; set; }
		public Agreements agreements { get; set; }
	}

	public class Agreements
	{
		public bool sellerAgreement { get; set; }
		public bool buyerAgreement { get; set; }
	}

}
