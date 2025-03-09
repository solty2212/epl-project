using EPLTeamSquadDataApi.Dtos;
using EPLTeamSquadDataApi.Interfaces;
using EPLTeamSquadDataApi.Models;
using EPLTeamSquadDataApi.Settings;
using Microsoft.Extensions.Options;
using System.Text.Json;

namespace EPLTeamSquadDataApi.Services
{
	public class TeamService : ITeamService
	{
		private readonly HttpClient _httpClient;
		private readonly FootballApiSettings _settings;
		private readonly TeamNicknameService _nicknameService;

		public TeamService(HttpClient httpClient, IOptions<FootballApiSettings> options, TeamNicknameService nicknameService)
		{
			_httpClient = httpClient;
			_settings = options.Value;
			_nicknameService = nicknameService;

			_httpClient.BaseAddress = new Uri(_settings.BaseUrl);
		}

		public async Task<List<PlayerDto>> GetSquadAsync(string teamName)
		{
			if (string.IsNullOrWhiteSpace(teamName))
				throw new ArgumentException("Team name is required.", nameof(teamName));

			string resolvedTeamName = _nicknameService.ResolveNickname(teamName) ?? teamName;
			
			// This part is here just for validation, because free version of APi all times returns Arsenal squad, but premium work fine
			var team = (await GetTeamAsync(resolvedTeamName)).FirstOrDefault();

			if (team is null)
			{
				throw new Exception("Team not found.");
			}

			// TODO: Premium API Feature /api/v1/json/{APIKEY}/searchplayers.php?t={TeamName}
			// Free version return all times Arsenal squad
			var response = await _httpClient.GetAsync($"searchplayers.php?t={team.Name}");

			if (!response.IsSuccessStatusCode)
			{
				var errorContent = await response.Content.ReadAsStringAsync();

				throw new HttpRequestException($"Request failed with status code {response.StatusCode}: {response.ReasonPhrase}. Details: {errorContent}");
			}
			var data = await response.Content.ReadAsStringAsync();

			var squad = JsonSerializer.Deserialize<FootballApiResponse>(data, new JsonSerializerOptions { PropertyNameCaseInsensitive = true })?.Squad;

			if (squad is null)
				throw new Exception("Squad not found.");

			var result = squad.ConvertAll(player => new PlayerDto
			{
				ProfilePicture = player.ProfilePicture,
				FullName = player.FullName,
				Team = player.Team,
				DateOfBirth = player.DateOfBirth,
				Position = player.Position
			});

			return result;
		}

		private async Task<List<Team>> GetTeamAsync(string teamName)
		{
			if (string.IsNullOrWhiteSpace(teamName))
				throw new ArgumentException("Team name is required.", nameof(teamName));

			var response = await _httpClient.GetAsync($"searchteams.php?t={teamName}");

			if (!response.IsSuccessStatusCode)
			{
				var errorContent = await response.Content.ReadAsStringAsync();

				throw new HttpRequestException($"Request failed with status code {response.StatusCode}: {response.ReasonPhrase}. Details: {errorContent}");
			}
			var data = await response.Content.ReadAsStringAsync();

			var teams = JsonSerializer.Deserialize<TeamResponse>(data, new JsonSerializerOptions { PropertyNameCaseInsensitive = true })?.Teams;

			if (teams is null || teams.Count == 0)
				throw new Exception("Team not found.");

			return teams;
		}
	}
}
