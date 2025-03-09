using System.Text.Json.Serialization;

namespace EPLTeamSquadDataApi.Models
{
	public class FootballApiResponse
	{

		[JsonPropertyName("player")]
		public List<Player> Squad { get; set; }
	}
}
