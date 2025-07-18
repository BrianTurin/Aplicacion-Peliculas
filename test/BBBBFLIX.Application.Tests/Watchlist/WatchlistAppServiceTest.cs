using BBBBFLIX.EntityFrameworkCore;
using BBBBFLIX.Series;
using BBBBFLIX.Watchlists;
using Microsoft.EntityFrameworkCore;
using Shouldly;
using System.Threading.Tasks;
using Volo.Abp.Modularity;
using Xunit;

namespace BBBBFLIX.Watchlist
{
    public abstract class WatchlistAppServiceTest<TStartupModule> : BBBBFLIXTestBase<TStartupModule>
        where TStartupModule : IAbpModule
    {
        private readonly IWatchlistAppService _watchlistAppService;
        private readonly BBBBFLIXDbContext _dbContext;

        protected WatchlistAppServiceTest()
        {
            _watchlistAppService = GetRequiredService<IWatchlistAppService>();
            _dbContext = GetRequiredService<BBBBFLIXDbContext>();
        }

        [Fact]
        public async Task ShowSeriesAsync_Should_Show_Series_Of_The_List()
        {
            var seriesDto = await _watchlistAppService.ShowSeriesAsync();

            Assert.NotEmpty(seriesDto);
        }

        [Fact]
        public async Task DeleteSerieAsync_Should_Delete_One_serie()
        {
            await _watchlistAppService.DeleteSerieAsync("TestDeBorrado123");
            var seriesDto = await _watchlistAppService.ShowSeriesAsync();

            seriesDto.ShouldBeEmpty();
        }

        [Fact]
        public async Task AddSerieAsync_Should_Add_One_Serie()
        {
            var serieDto = new SerieDto
            {
                Title = "Add Serie Test",
                Year = "2000",
                ReleasedDate = "2000-01-01",
                Duration = "2h",
                Genre = "Action",
                Director = "Director Name",
                Writer = "Writer Name",
                Actors = "Actor Name",
                Plot = "Any Plot Test",
                Language = "English",
                Country = "USA",
                Poster = "Poster's URL",
                ImdbRating = "6.8",
                ImdbVotes = 9999,
                ImdbId = "ab23op999",
                Type = "Serie",
            };

            await _watchlistAppService.AddSerieAsync(serieDto);

            var dbSerie = await _dbContext.Series.FirstOrDefaultAsync(s => s.ImdbId == "ab23op999");

            dbSerie.ShouldNotBeNull();
            dbSerie.Title.ShouldBe("Add Serie Test");
        }

        [Fact]
        public async Task DeleteSerieAsync_Should_Erase_One_Serie()
        {
            //Act
            await _watchlistAppService.DeleteSerieAsync("tt1234567");
            var seriesDto = await _watchlistAppService.ShowSeriesAsync();

            //Assert
            seriesDto.ShouldBeEmpty();
        }
    }
}
