using Common.Models.Database;
using Common.Models.Inbound;
using Common.Models.Outbound;

namespace BusinessLogicLayer.Contracts
{
    public interface IProductService
    {
        Task<Product> AddNewProduct(NewProduct newProduct);

        Task<List<ProductView>> GetAll();
        Task<List<ProductView>> GetAllBySellerId(string sellerId);

        Task<Product> Update(UpdateProduct product);
        Task<string> Delete(DeleteProduct product);
    }
}