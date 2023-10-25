
namespace AwesomeMeds.Scheduling.Business
{

    /// <summary>
    /// A DateTime provider that is suitable for mocking in unit tests.
    /// </summary>
    public interface IDateTimeProvider
    {
        DateTime GetCurrentDateTimeUtc();
    }
}