namespace BackendCore.Service.Services.BackgroundJobs.Jobs
{

    public class MyRegistry : FluentScheduler.Registry
    {
        public MyRegistry()
        {
            Schedule(() => new TestJob()).ToRunNow();
        }
    }
}