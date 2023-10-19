//using System.Threading.Tasks;
//using AutoMapper;
//using Flurl;
//using Flurl.Http;
//using CarLinePickup.Domain.Services.Interfaces;
//using Microsoft.Extensions.Options;
//using CarLinePickup.Data.Models;
//using CarLinePickup.Options.DomainOptions;
//using Arch.EntityFrameworkCore.UnitOfWork;

//namespace CarLinePickup.Domain.Services
//{
//    public class TileService : ServiceBase, ITileService
//    {
//        private readonly IRepository<Address> _tileRepository;
//        private readonly IMapper _mapper;
//        private readonly IUnitOfWork _unitOfWork;
//        private readonly DomainOptions _options;

//        public TileService(IOptions<DomainOptions> options, IMapper mapper,
//            IUnitOfWork unitOfWork) : base(unitOfWork, mapper)
//        {
//            _tileRepository = unitOfWork.GetRepository<Address>();
//            _mapper = mapper;
//            _unitOfWork = unitOfWork;
//            _options = options.Value;
//        }

//        public async Task<bool> AlertAsync(int id)
//        {
//            return await _options.TileOptions.BaseUrl
//              .AppendPathSegment(_options.TileOptions.UrlSegment)
//              .WithHeader("apiKey", _options.TileOptions.ApiKey)
//              .WithHeader("Content-Type", "application/json")
//              .PostJsonAsync(new
//              {
//                  id = id
//              }).ReceiveJson<bool>();
//        }
//    }
//}