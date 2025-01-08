using AutoMapper;
using DataContext.EntityFramework.Provider;
using DataContext.EntityFramework;
using Domain.Backoffice;
using Repository.EFContextRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Backoffice
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
        public AddressDO GetById(int id)
        {
            return GetAll().FirstOrDefault(x => x.Id == id);
        }
    }
}
