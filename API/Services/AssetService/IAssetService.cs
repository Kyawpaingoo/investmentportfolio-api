using Data;

namespace API.Services.AssetService;

public interface IAssetService
{
    Task<string> InsertJsonList(List<tbAsset> assetList);
}