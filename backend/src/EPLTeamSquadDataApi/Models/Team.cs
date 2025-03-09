using System.Text.Json.Serialization;

namespace EPLTeamSquadDataApi.Models
{
	public class TeamResponse
	{
		[JsonPropertyName("teams")]
		public List<Team> Teams { get; set; }
	}

	public class Team
	{
		[JsonPropertyName("idTeam")]
		public string IdTeam { get; set; }

		[JsonPropertyName("strTeam")]
		public string Name { get; set; }

		[JsonPropertyName("strTeamAlternate")]
		public string AlternateNames { get; set; }
	}
}
