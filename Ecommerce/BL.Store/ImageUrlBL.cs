using AutoMapper;

namespace BL.Store
{
    public class ImageUrlBL
    {
        private readonly IMapper _mapper;

        public ImageUrlBL(IMapper mapper)
        {
            _mapper = mapper;

        }
    }
}
