using EPLTeamSquadDataApi.Dtos;
using EPLTeamSquadDataApi.Interfaces;
using EPLTeamSquadDataApi.Models;
using EPLTeamSquadDataApi.Settings;
using Microsoft.AspNetCore.DataProtection.KeyManagement;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Net.Http;
using System.Text.Json;

namespace EPLTeamSquadDataApi.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class TeamController : ControllerBase
	{
		private readonly ITeamService _teamService;

		public TeamController(ITeamService teamService)
		{
			_teamService = teamService;
		}

		[HttpGet("squad")]
		public async Task<IActionResult> GetSquad([FromQuery] string teamName)
		{
			if (string.IsNullOrWhiteSpace(teamName)) 
				return BadRequest("Team name is required.");

			try
			{
				var result = await _teamService.GetSquadAsync(teamName);

				if (result is null)
				{
					return NotFound($"Not found squad for {teamName}");
				}

				return Ok(result);
			}
			catch (Exception ex)
			{
				return NotFound(ex.Message);
			}
		}
	}
}
