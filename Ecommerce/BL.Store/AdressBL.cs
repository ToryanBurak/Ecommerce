using AutoMapper;
using DataContext.EntityFramework;
using DataContext.EntityFramework.Provider;
using Domain.Store;
using Repository.EFContextRepository;

namespace BL.Store
{
    public class AdressBL
    {
        private readonly IMapper _mapper;
        public AdressBL(IMapper mapper)
        {
            _mapper = mapper;
        }
        public List<AddressDO> GetAll()
        {
            using (DbContextProvider dcp = new DbContextProvider())
            {
                Repository<Address> adressRepository = new Repository<Address>(dcp);
                return _mapper.Map<List<Address>, List<AddressDO>>(adressRepository.GetAll().ToList());
            }
        }
        public List<CityDO> GetAllCityList()
        {
            using (DbContextProvider dcp = new DbContextProvider())
            {
                Repository<City> cityRepository = new Repository<City>(dcp);
                return _mapper.Map<List<City>, List<CityDO>>(cityRepository.GetAll().ToList());
            }
        }
        public List<DistrictDO> GetAllDistrictListByCityKey(int cityKey)
        {
            using (DbContextProvider dcp = new DbContextProvider())
            {
                Repository<District> districtRepository = new Repository<District>(dcp);
                return _mapper.Map<List<District>, List<DistrictDO>>(districtRepository.GetAll().Where(x => x.CityKey == cityKey).ToList());
            }
        }
        public List<TownDO> GetAllTownListByDistrictKey(int districtKey)
        {
            using (DbContextProvider dcp = new DbContextProvider())
            {
                Repository<Town> townRepository = new Repository<Town>(dcp);
                return _mapper.Map<List<Town>, List<TownDO>>(townRepository.GetAll().Where(x => x.DistrictKey == districtKey).ToList());
            }
        }
        public List<AddressDO> GetAllAdressListByUserId(int userId)
        {
            return GetAll().Where(x => x.UserId == userId).ToList();
        }
        public AddressDO GetById(int id)
        {
            return GetAll().FirstOrDefault(x => x.Id == id);
        }

        public int SaveAdress(int userId, AddressDO address)
        {
            using (DbContextProvider dcp = new DbContextProvider())
            {
                Repository<Address> adressRepository = new Repository<Address>(dcp);
                Address dbObject = new Address()
                {
                    UserId = userId,
                    Value = address.Value,
                    AddressDescription = address.AddressDescription,
                    TownId = address.TownId
                };
                adressRepository.InsertOnSubmit(dbObject);
                int id = dcp.GetDataContext().ChangeTracker.Entries<Address>().FirstOrDefault(e => e.Entity == dbObject).Entity.Id;
                dcp.CommitChanges();
                return id;
            }
        }
    }
}
