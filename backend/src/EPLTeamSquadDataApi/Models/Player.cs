using System.Text.Json.Serialization;

namespace EPLTeamSquadDataApi.Models
{
	public class Player
	{
		[JsonPropertyName("strThumb")]
		public string ProfilePicture { get; set; }
		[JsonPropertyName("strPlayer")]
		public string FullName { get; set; }
		[JsonPropertyName("strTeam")]
		public string Team { get; set; }
		[JsonPropertyName("strPosition")]
		public string Position { get; set; }
		[JsonPropertyName("DateBorn")]
		public string DateOfBirth { get; set; }
	}
}
