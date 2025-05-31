using API.Services.AssetService;
using Data;
using Microsoft.AspNetCore.Mvc;

namespace API.Controller;

[ApiController]
[Route("api/[controller]")]
public class AssetController : ControllerBase
{
    private IAssetService _assetService;

    public AssetController(IAssetService assetService)
    {
        _assetService = assetService;
    }

    [HttpPost("insertassetlist")]
    public async Task<IActionResult> InsertAssetList(List<tbAsset> assetList)
    {
        var result = await _assetService.InsertJsonList(assetList);
        return Ok(result);
    }
}