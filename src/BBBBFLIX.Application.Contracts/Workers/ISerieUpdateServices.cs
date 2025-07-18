using System.Threading.Tasks;

namespace BBBBFLIX.Workers
{
    public interface ISerieUpdateService
    {
        Task VerifyAndUpdateSeriesAsync();
    }
}
