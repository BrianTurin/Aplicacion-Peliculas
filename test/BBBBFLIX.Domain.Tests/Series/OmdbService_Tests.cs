using System;
using System.Linq;
using System.Threading.Tasks;
using Shouldly;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Modularity;
using Volo.Abp.Validation;
using Xunit;
namespace BBBBFLIX.Series
{
    public abstract class OmdbService_Tests<TStartupModule> : BBBBFLIXDomainTestBase<TStartupModule> where TStartupModule : IAbpModule
    {
        private readonly OmdbService _service;
        public OmdbService_Tests()
        {
            _service = GetRequiredService<OmdbService>();
        }
        [Fact]
        public async Task Should_Search_One_Serie()
        {
            //Arrange
            var title = "Game of Thrones";
            //Act
            var result = await _service.GetSeriesAsync(title, String.Empty);
            //Assert
            result.ShouldNotBeNull();
            result.ShouldContain(b => b.Title == title);
        }
        [Fact]
        public async Task Should_Search_None_Serie()
        {
            //Arrange
            var title = "ufyffflflffñfñififiy";
            //  Act / Assert
            var exception = await Assert.ThrowsAsync<Exception>(async () =>
            {
                await _service.GetSeriesAsync(title, string.Empty);
            });
            throw new ArgumentException("Problema en búsqueda");
        }
    }
}