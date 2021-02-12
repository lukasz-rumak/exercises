using BoardGame.Interfaces;
using BoardGame.Models;
using Xunit;
using Moq;
using EventHandler = BoardGame.Managers.EventHandler;

namespace BoardGameTests
{
    public class EventHandlerUnitTests
    {
        private readonly EventHandler _event;
        private readonly Mock<IPresentation> _presentationMock;
        private readonly string _testDescription;

        public EventHandlerUnitTests()
        {
            _presentationMock = new Mock<IPresentation>();
            _event = new EventHandler(_presentationMock.Object);
            _testDescription = "this is only test";
        }
        
        [Theory]
        [InlineData(EventType.PieceMove)]
        [InlineData(EventType.WallCreationError)]
        [InlineData(EventType.OutsideBoundaries)]
        [InlineData(EventType.FieldTaken)]
        [InlineData(EventType.WallOnTheRoute)]
        [InlineData(EventType.None)]
        public void ReturnExpectedVersusActualForEventHandler(EventType eventType)
        {
            _event.Events[eventType](_testDescription);
            _presentationMock.Verify(presentation => presentation.PrintEventOutput(eventType, It.Is<string>(s => s.Contains("this is only test"))), Times.Once());
            Assert.Contains(_testDescription, _event.EventLog[0]);
        }
    }
}