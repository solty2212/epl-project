namespace EPLTeamSquadDataApi.Services
{
	public class TeamNicknameService
	{
		private readonly Dictionary<string, string> _nicknames = new()
	{
		{ "The Hammers", "West Ham United" },
		{ "The Red Devils", "Manchester United" },
		{ "The Gunners", "Arsenal" }
	};

		public string? ResolveNickname(string nickname) =>
			_nicknames.TryGetValue(nickname, out var teamName) ? teamName : null;
	}
}
