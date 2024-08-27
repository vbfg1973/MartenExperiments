namespace Domain.Tests
{
    using Centres;
    using Centres.Create;
    using Centres.Update;
    using FluentAssertions;

    public class CentreTests
    {
        [Fact]
        public void CentreVersionIsUpdated()
        {
            var command1 = new CreateCentre("Centre Name", "Centre Code");
            var centre = new CentreAggregate(command1);
            centre.Version.Should().Be(1);

            var command2 = new UpdateCentre("New Centre Name", "New Centre Code");
            centre.Command(command2);
            centre.Version.Should().Be(2);
        }

        [Fact]
        public void CentreNameAndCodeIsCorrect()
        {
            var command1 = new CreateCentre("Centre Name", "Centre Code");
            var centre = new CentreAggregate(command1);
            centre.Name.Should().Be(command1.Name);
            centre.Code.Should().Be(command1.Code);

            var command2 = new UpdateCentre("New Centre Name", "New Centre Code");
            centre.Command(command2);
            centre.Name.Should().Be(command2.Name);
            centre.Code.Should().Be(command2.Code);
        }
    }
}
