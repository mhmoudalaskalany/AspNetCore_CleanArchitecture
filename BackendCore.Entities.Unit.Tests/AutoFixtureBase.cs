using AutoFixture;
using AutoFixture.AutoMoq;

namespace BackendCore.Entities.Unit.Tests
{
    public class AutoFixtureBase
    {
        protected Fixture Fixture { get; }
        protected List<string> Logs { get; private set; }
        public AutoFixtureBase()
        {
            Fixture = new Fixture();
            Fixture.Customize(new AutoMoqCustomization()
            {
                ConfigureMembers = true
            });
            Fixture.Behaviors.OfType<ThrowingRecursionBehavior>().ToList().ForEach(b => Fixture.Behaviors.Remove(b));
            Fixture.Behaviors.Add(new OmitOnRecursionBehavior());
        }
    }
}
