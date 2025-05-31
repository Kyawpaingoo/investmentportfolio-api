using Core.Extension;
using Data;
using Data.Dtos;
using Infra.UnitOfWork;

namespace API.Services.AssetService;

public class AssetService : IAssetService
{
    private readonly InvestmentPortfolioDBContext _context;
    private IUnitOfWork _uow;
    private DateTime now;

    public AssetService(InvestmentPortfolioDBContext context)
    {
        _context = context;
        _uow = new UnitOfWork(context);
        now = MyExtension.getUtcTime();
    }
    
    public async Task<string> InsertJsonList(List<tbAsset> assetList)
    {
        try
        {
            await _uow.assetRepo.InsertListAsync(assetList);
            return ReturnMessage.Success;
        }
        catch (Exception ex)
        {
            throw ex;
        }
        
    }
}