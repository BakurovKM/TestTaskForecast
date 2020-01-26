using System.Threading.Tasks;

namespace Forecast.Application.Services.Abstractions
{
    public interface IGismeteoParser
    {
        Task ParseAsync();
    }
}