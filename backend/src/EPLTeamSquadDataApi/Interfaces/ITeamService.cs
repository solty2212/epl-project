using EPLTeamSquadDataApi.Dtos;
using EPLTeamSquadDataApi.Models;

namespace EPLTeamSquadDataApi.Interfaces
{
	public interface ITeamService
	{
		Task<List<PlayerDto>> GetSquadAsync(string teamName);
	}
}
