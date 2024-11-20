using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PeerBerry.API.ResponseModels
{
	public class GlobalResponse
	{
		public string lang { get; set; }
		public bool inviteFriends { get; set; }
		public Languages languages { get; set; }
		public string[] months { get; set; }
		public Loanstatuses loanStatuses { get; set; }
		public Frequency frequency { get; set; }
		public Loantype[] loanTypes { get; set; }
		public Transactiontypes transactionTypes { get; set; }
		public object[] settings { get; set; }
		public string lastModified { get; set; }
		public bool cache { get; set; }
		public string timezone { get; set; }
		public DateTime datetime { get; set; }
		public int timestamp { get; set; }
		public Country[] countries { get; set; }
		public Company[] companies { get; set; }
		public Originator[] originators { get; set; }
		public Landing landing { get; set; }
	}

	public class Languages
	{
		public string de { get; set; }
		public string en { get; set; }
		public string es { get; set; }
		public string fr { get; set; }
	}

	public class Loanstatuses
	{
		public string _0 { get; set; }
		public string _2 { get; set; }
		public string _4 { get; set; }
	}

	public class Frequency
	{
		public string _1 { get; set; }
		public string _2 { get; set; }
		public string _3 { get; set; }
		public string _0 { get; set; }
	}

	public class Transactiontypes
	{
		public string _0 { get; set; }
		public string _1 { get; set; }
		public string _2 { get; set; }
		public string _3 { get; set; }
		public string _4 { get; set; }
		public string _11 { get; set; }
		public string _16 { get; set; }
	}

	public class Landing
	{
		public string roi { get; set; }
	}

	public class Loantype
	{
		public int id { get; set; }
		public string type { get; set; }
		public string title { get; set; }
	}

	public class Country
	{
		public int id { get; set; }
		public string title { get; set; }
		public string label { get; set; }
		public string iso { get; set; }
		public string iso3 { get; set; }
		public bool active_originator { get; set; }
		public bool active_strategy { get; set; }
		public bool activeForRegistration { get; set; }
		public bool taxResidenceAvailable { get; set; }
	}

	public class Company
	{
		public int id { get; set; }
		public string title { get; set; }
	}

	public class Originator
	{
		public int id { get; set; }
		public string title { get; set; }
		public int countryId { get; set; }
		public int companyId { get; set; }
		public string countryIso { get; set; }
		public bool groupGuarantee { get; set; }
		public bool buybackGuarantee { get; set; }
		public string loanTermType { get; set; }
		public bool autoinvestActive { get; set; }
		public string logoDark { get; set; }
		public string logoLight { get; set; }
		public float interestRate { get; set; }
	}

}
