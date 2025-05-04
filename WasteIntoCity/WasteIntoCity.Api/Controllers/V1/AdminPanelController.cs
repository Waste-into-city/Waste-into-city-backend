using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WasteIntoCity.Api.Contracts.V1.Responses;
using WasteIntoCity.Application;
using WasteIntoCity.Application.Contracts.V1.Requests;
using WasteIntoCity.Application.Structs;
using WasteIntoCity.Core.Enums;
using WasteIntoCity.Core.Interfaces.Services;
using WasteIntoCity.Core.Models;

namespace WasteIntoCity.Api.Controllers.V1
{
    public class AdminPanelController : ControllerBase
    {
        private readonly IAdminPanelService _adminPanelService;

        public AdminPanelController(IAdminPanelService adminPanelService)
        {
            _adminPanelService = adminPanelService;
        }

        [Authorize(Roles = $"{nameof(RoleEnum.Admin)}")]
        [HttpPost(ApiRoutes.AdminPanel.SAVE_ALL_DATA)]
        public async Task<IActionResult> SaveAllDataAsync([FromBody] List<AdminPanelSaveAllDataRequest> adminPanelSaveAllDataRequest)
        {
            List<ScoreSettingsPairStruct> scoreSettingsPairStructs = adminPanelSaveAllDataRequest.Select(r => new ScoreSettingsPairStruct(
                r.ScoreSettingsTypesId, r.Value)).ToList();

            await _adminPanelService.SaveAllData(scoreSettingsPairStructs);

            return Created();
        }

        [Authorize(Roles = $"{nameof(RoleEnum.Admin)}")]
        [HttpPost(ApiRoutes.AdminPanel.GET_ALL_DATA)]
        public async Task<IActionResult> GetAllDataAsync()
        {
            List<ScoreSettingsType> scoreSettingsTypes = await _adminPanelService.GetAllData();

            List<AdminPanelGetAllDataResponse> adminPanelGetAllDataResponse = scoreSettingsTypes.Select(s => new AdminPanelGetAllDataResponse
            {
                ScoreSettingsTypesId = (int)s.Id,
                Value = s.Value
            }).ToList();

            return Ok(adminPanelGetAllDataResponse);
        }
    }
}
