
using ApplicationServices.Mappers;
using AutoMapper;
using Mapper = ApplicationServices.Mappers.Mapper;

namespace ApplicationServices.UnitTests.UtilTest
{
    public class BaseTest
    {
        protected IMapper ConfiguraionMapper()
        {
            var config = new MapperConfiguration(options =>
            {
                options.AddProfile(new Mapper());
            });
            return config.CreateMapper();
        }

    }
}
