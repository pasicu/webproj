using AutoMapper;
using BusinessLogicLayer.Contracts;
using Common.Models.Database;
using Common.Models.Inbound;
using Common.Models.Outbound;
using DatabaseLayer.Repositories.Interfaces;
using Microsoft.AspNetCore.Http;

namespace BusinessLogicLayer.Services
{
    public class ProductService : IProductService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public ProductService(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<List<ProductView>> GetAllBySellerId(string sellerId)
        {
            var sellersProducts = _unitOfWork.Products.GetAll().Result.Where(p => p.SellerId.ToString().ToLower().Equals(sellerId.ToLower())).ToList();
            var productsAddapted = _mapper.Map<List<ProductView>>(sellersProducts);
            return productsAddapted;
        }

        public async Task<Product> AddNewProduct(NewProduct newProduct)
        {
            var product = _mapper.Map<Product>(newProduct);
            product.Image = await ParseProductImageToBytes(newProduct.Image);
            await _unitOfWork.Products.Add(product);
            await _unitOfWork.SaveChanges();
            return product;
        }

        #region

        private async Task<byte[]> ParseProductImageToBytes(IFormFile incomingImage)
        {
            byte[] byteImage;
            if (incomingImage != null && incomingImage.Length > 0)
            {
                using (var memoryStream = new MemoryStream())
                {
                    await incomingImage.CopyToAsync(memoryStream);
                    byteImage = memoryStream.ToArray();
                }
            }
            else
                byteImage = new byte[0];
            return byteImage;
        }

        #endregion
    }
}