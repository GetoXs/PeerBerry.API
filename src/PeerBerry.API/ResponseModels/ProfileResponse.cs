namespace PeerBerry.API.ResponseModels.ProfileResponse
{
	public class ProfileResponse
	{
		public string firstName { get; set; }
		public string lastName { get; set; }
		public string publicId { get; set; }
		public string email { get; set; }
		public int accountId { get; set; }
		public string companyName { get; set; }
		public object idNumber { get; set; }
		public int countryId { get; set; }
		public string dateOfBirth { get; set; }
		public string city { get; set; }
		public string address { get; set; }
		public string zip { get; set; }
		public string status { get; set; }
		public string phone { get; set; }
		public bool phoneConfirmed { get; set; }
		public string userType { get; set; }
		public bool agreeWithPolicy { get; set; }
		public Category category { get; set; }
		public bool tfaActive { get; set; }
		public string verificationStatus { get; set; }
		public string registrationDate { get; set; }
		public bool agreeWithTerms { get; set; }
		public object[] personalInterests { get; set; }
		public object taxResidence { get; set; }
		public Quizstatuses quizStatuses { get; set; }
		public int totalStrategies { get; set; }
		public int activeStrategies { get; set; }
		public string referralCode { get; set; }
		public Account[] accounts { get; set; }
		public bool isValid { get; set; }
		public Secondarymarket secondaryMarket { get; set; }
		public News news { get; set; }
		public string frontChatHash { get; set; }
	}

	public class Category
	{
		public int id { get; set; }
		public string title { get; set; }
		public string icon { get; set; }
		public float interestRate { get; set; }
		public bool isLoyalty { get; set; }
		public bool isDefault { get; set; }
	}

	public class Quizstatuses
	{
		public object KYC { get; set; }
		public object AML { get; set; }
	}

	public class Secondarymarket
	{
		public Counteroffers counterOffers { get; set; }
	}

	public class Counteroffers
	{
		public int count { get; set; }
	}

	public class News
	{
		public int unreadCount { get; set; }
	}

	public class Account
	{
		public int id { get; set; }
		public string account { get; set; }
	}

}
