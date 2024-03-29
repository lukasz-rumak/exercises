using System.Collections.Generic;
using System.Linq;
using Draughts.Interfaces;
using Draughts.Managers;
using Draughts.Models;
using Xunit;
using Moq;
using FluentAssertions;

namespace Draughts.Tests
{
    public class EventsHandlerTests
    {
        [Fact]
        public void GivenVeryLargeBoardWhenIReturnTheBestEventsToExecuteForPlayer2ThenTheBestEventsForPlayer2AreReturned()
        {
            var board = new Dictionary<(int, int), Field>
            {
                { (0, 0), new Field { Playable = true, Player = Players.None } },
                { (1, 0), new Field { Playable = true, Player = Players.None } },
                { (2, 0), new Field { Playable = true, Player = Players.None } },
                { (3, 0), new Field { Playable = true, Player = Players.None } },
                { (4, 0), new Field { Playable = true, Player = Players.None } },
                { (5, 0), new Field { Playable = true, Player = Players.None } },
                { (6, 0), new Field { Playable = true, Player = Players.None } },
                { (7, 0), new Field { Playable = true, Player = Players.None } },
                { (0, 1), new Field { Playable = true, Player = Players.None } },
                { (1, 1), new Field { Playable = true, Player = Players.Player2 } },
                { (2, 1), new Field { Playable = true, Player = Players.None } },
                { (3, 1), new Field { Playable = true, Player = Players.None } },
                { (4, 1), new Field { Playable = true, Player = Players.None } },
                { (5, 1), new Field { Playable = true, Player = Players.None } },
                { (6, 1), new Field { Playable = true, Player = Players.None } },
                { (7, 1), new Field { Playable = true, Player = Players.None } },
                { (0, 2), new Field { Playable = true, Player = Players.None } },
                { (1, 2), new Field { Playable = true, Player = Players.None } },
                { (2, 2), new Field { Playable = true, Player = Players.Player1 } },
                { (3, 2), new Field { Playable = true, Player = Players.None } },
                { (4, 2), new Field { Playable = true, Player = Players.Player1 } },
                { (5, 2), new Field { Playable = true, Player = Players.None } },
                { (6, 2), new Field { Playable = true, Player = Players.Player2 } },
                { (7, 2), new Field { Playable = true, Player = Players.None } },
                { (0, 3), new Field { Playable = true, Player = Players.None } },
                { (1, 3), new Field { Playable = true, Player = Players.None } },
                { (2, 3), new Field { Playable = true, Player = Players.None } },
                { (3, 3), new Field { Playable = true, Player = Players.None } },
                { (4, 3), new Field { Playable = true, Player = Players.None } },
                { (5, 3), new Field { Playable = true, Player = Players.None } },
                { (6, 3), new Field { Playable = true, Player = Players.None } },
                { (7, 3), new Field { Playable = true, Player = Players.None } },
                { (0, 4), new Field { Playable = true, Player = Players.None } },
                { (1, 4), new Field { Playable = true, Player = Players.None } },
                { (2, 4), new Field { Playable = true, Player = Players.Player1 } },
                { (3, 4), new Field { Playable = true, Player = Players.None } },
                { (4, 4), new Field { Playable = true, Player = Players.Player2 } },
                { (5, 4), new Field { Playable = true, Player = Players.None } },
                { (6, 4), new Field { Playable = true, Player = Players.Player1 } },
                { (7, 4), new Field { Playable = true, Player = Players.None } },
                { (0, 5), new Field { Playable = true, Player = Players.None } },
                { (1, 5), new Field { Playable = true, Player = Players.None } },
                { (2, 5), new Field { Playable = true, Player = Players.None } },
                { (3, 5), new Field { Playable = true, Player = Players.None } },
                { (4, 5), new Field { Playable = true, Player = Players.None } },
                { (5, 5), new Field { Playable = true, Player = Players.None } },
                { (6, 5), new Field { Playable = true, Player = Players.None } },
                { (7, 5), new Field { Playable = true, Player = Players.None } },
                { (0, 6), new Field { Playable = true, Player = Players.None } },
                { (1, 6), new Field { Playable = true, Player = Players.None } },
                { (2, 6), new Field { Playable = true, Player = Players.Player1 } },
                { (3, 6), new Field { Playable = true, Player = Players.None } },
                { (4, 6), new Field { Playable = true, Player = Players.Player1 } },
                { (5, 6), new Field { Playable = true, Player = Players.None } },
                { (6, 6), new Field { Playable = true, Player = Players.None } },
                { (7, 6), new Field { Playable = true, Player = Players.None } },
                { (0, 7), new Field { Playable = true, Player = Players.None } },
                { (1, 7), new Field { Playable = true, Player = Players.None } },
                { (2, 7), new Field { Playable = true, Player = Players.None } },
                { (3, 7), new Field { Playable = true, Player = Players.Player2 } },
                { (4, 7), new Field { Playable = true, Player = Players.None } },
                { (5, 7), new Field { Playable = true, Player = Players.None } },
                { (6, 7), new Field { Playable = true, Player = Players.None } },
                { (7, 7), new Field { Playable = true, Player = Players.None } }
            };

            var boardMock = new Mock<IBoardCreator>();
            boardMock.Setup(s => s.GetBoard()).Returns(board);
            boardMock.Setup(s => s.GetBoardSize()).Returns(8);
            var eventsHandler = new EventsHandler(boardMock.Object, new EventsCreator(boardMock.Object, new PositionProcessor()));
            
            var result = eventsHandler.ReturnTheBestEventsToExecute(Players.Player2);
            var expected = new List<Event>
            {
                new() { Source = (3, 7), Destination = (2, 6), Action = Action.Kill },
                new() { Source = (3, 7), Destination = (1, 5), Action = Action.Move },
                new() { Source = (1, 5), Destination = (2, 4), Action = Action.Kill },
                new() { Source = (1, 5), Destination = (3, 3), Action = Action.Move },
                new() { Source = (3, 3), Destination = (4, 2), Action = Action.Kill },
                new() { Source = (3, 3), Destination = (5, 1), Action = Action.Move }
            };
            expected.Should().BeEquivalentTo(result);
        }
        
        [Fact]
        public void GivenVeryLargeBoardWhenICreateAllEventsForGivenPlayerThenAllEventsForGivenPlayerAreCreated()
        {
            var board = new Dictionary<(int, int), Field>
            {
                { (0, 0), new Field { Playable = true, Player = Players.None } },
                { (1, 0), new Field { Playable = true, Player = Players.None } },
                { (2, 0), new Field { Playable = true, Player = Players.Player1 } },
                { (3, 0), new Field { Playable = true, Player = Players.None } },
                { (4, 0), new Field { Playable = true, Player = Players.None } },
                { (5, 0), new Field { Playable = true, Player = Players.None } },
                { (6, 0), new Field { Playable = true, Player = Players.None } },
                { (7, 0), new Field { Playable = true, Player = Players.None } },
                { (0, 1), new Field { Playable = true, Player = Players.None } },
                { (1, 1), new Field { Playable = true, Player = Players.Player2 } },
                { (2, 1), new Field { Playable = true, Player = Players.None } },
                { (3, 1), new Field { Playable = true, Player = Players.None } },
                { (4, 1), new Field { Playable = true, Player = Players.None } },
                { (5, 1), new Field { Playable = true, Player = Players.None } },
                { (6, 1), new Field { Playable = true, Player = Players.None } },
                { (7, 1), new Field { Playable = true, Player = Players.Player1 } },
                { (0, 2), new Field { Playable = true, Player = Players.None } },
                { (1, 2), new Field { Playable = true, Player = Players.None } },
                { (2, 2), new Field { Playable = true, Player = Players.Player1 } },
                { (3, 2), new Field { Playable = true, Player = Players.None } },
                { (4, 2), new Field { Playable = true, Player = Players.Player1 } },
                { (5, 2), new Field { Playable = true, Player = Players.None } },
                { (6, 2), new Field { Playable = true, Player = Players.Player2 } },
                { (7, 2), new Field { Playable = true, Player = Players.None } },
                { (0, 3), new Field { Playable = true, Player = Players.None } },
                { (1, 3), new Field { Playable = true, Player = Players.None } },
                { (2, 3), new Field { Playable = true, Player = Players.None } },
                { (3, 3), new Field { Playable = true, Player = Players.Player2 } },
                { (4, 3), new Field { Playable = true, Player = Players.None } },
                { (5, 3), new Field { Playable = true, Player = Players.None } },
                { (6, 3), new Field { Playable = true, Player = Players.None } },
                { (7, 3), new Field { Playable = true, Player = Players.None } },
                { (0, 4), new Field { Playable = true, Player = Players.None } },
                { (1, 4), new Field { Playable = true, Player = Players.None } },
                { (2, 4), new Field { Playable = true, Player = Players.Player1 } },
                { (3, 4), new Field { Playable = true, Player = Players.None } },
                { (4, 4), new Field { Playable = true, Player = Players.None } },
                { (5, 4), new Field { Playable = true, Player = Players.None } },
                { (6, 4), new Field { Playable = true, Player = Players.Player1 } },
                { (7, 4), new Field { Playable = true, Player = Players.None } },
                { (0, 5), new Field { Playable = true, Player = Players.None } },
                { (1, 5), new Field { Playable = true, Player = Players.None } },
                { (2, 5), new Field { Playable = true, Player = Players.None } },
                { (3, 5), new Field { Playable = true, Player = Players.None } },
                { (4, 5), new Field { Playable = true, Player = Players.None } },
                { (5, 5), new Field { Playable = true, Player = Players.None } },
                { (6, 5), new Field { Playable = true, Player = Players.None } },
                { (7, 5), new Field { Playable = true, Player = Players.None } },
                { (0, 6), new Field { Playable = true, Player = Players.None } },
                { (1, 6), new Field { Playable = true, Player = Players.None } },
                { (2, 6), new Field { Playable = true, Player = Players.Player1 } },
                { (3, 6), new Field { Playable = true, Player = Players.None } },
                { (4, 6), new Field { Playable = true, Player = Players.Player1 } },
                { (5, 6), new Field { Playable = true, Player = Players.None } },
                { (6, 6), new Field { Playable = true, Player = Players.None } },
                { (7, 6), new Field { Playable = true, Player = Players.None } },
                { (0, 7), new Field { Playable = true, Player = Players.None } },
                { (1, 7), new Field { Playable = true, Player = Players.None } },
                { (2, 7), new Field { Playable = true, Player = Players.None } },
                { (3, 7), new Field { Playable = true, Player = Players.Player2 } },
                { (4, 7), new Field { Playable = true, Player = Players.None } },
                { (5, 7), new Field { Playable = true, Player = Players.None } },
                { (6, 7), new Field { Playable = true, Player = Players.None } },
                { (7, 7), new Field { Playable = true, Player = Players.None } }
            };

            var boardMock = new Mock<IBoardCreator>();
            boardMock.Setup(s => s.GetBoard()).Returns(board);
            boardMock.Setup(s => s.GetBoardSize()).Returns(8);
            var eventsHandler = new EventsHandler(boardMock.Object, new EventsCreator(boardMock.Object, new PositionProcessor()));
            
            var result = eventsHandler.CreateAllEventsForGivenPlayer(Players.Player2);
            var expected = new List<List<Event>>
            {
                new()
                {
                    new Event { Source = (1, 1), Destination = (0, 0), Action = Action.Move }
                },
                new()
                {
                    new Event { Source = (6, 2), Destination = (5, 1), Action = Action.Move }
                },
                new()
                {
                    new Event { Source = (3, 3), Destination = (4, 2), Action = Action.Kill },
                    new Event { Source = (3, 3), Destination = (5, 1), Action = Action.Move },
                },
                new()
                {
                    new Event { Source = (3, 7), Destination = (4, 6), Action = Action.Kill },
                    new Event { Source = (3, 7), Destination = (5, 5), Action = Action.Move },
                    new Event { Source = (5, 5), Destination = (6, 4), Action = Action.Kill },
                    new Event { Source = (5, 5), Destination = (7, 3), Action = Action.Move }
                }
            };
            expected.Should().BeEquivalentTo(result);
        }
        
        [Fact]
        public void GivenOnlyOneEventWhenISelectTheBestPathThenTheBestPathIsReturned()
        {
            var board = new Dictionary<(int, int), Field>
            {
                { (0, 0), new Field { Playable = true, Player = Players.None } },
            };

            var events = new List<Event>
            {
                new() { Source = (4, 4), Destination = (5, 5), Action = Action.Move }
            };

            var boardMock = new Mock<IBoardCreator>();
            boardMock.Setup(s => s.GetBoard()).Returns(board);
            boardMock.Setup(s => s.GetBoardSize()).Returns(1);
            var eventsHandler = new EventsHandler(boardMock.Object, new EventsCreator(boardMock.Object, new PositionProcessor()));
            
            var result = eventsHandler.SelectTheBestPathForPawn(events);
            var expected = new List<Event>
            {
                new() { Source = (4, 4), Destination = (5, 5), Action = Action.Move }
            };
            expected.Should().BeEquivalentTo(result);
        }
        
        [Fact]
        public void GivenVerySimpleSetOfEventsWhenISelectTheBestPathThenTheBestPathIsReturned()
        {
            var board = new Dictionary<(int, int), Field>
            {
                { (0, 0), new Field { Playable = true, Player = Players.None } },
            };

            var events = new List<Event>
            {
                new() { Source = (4, 4), Destination = (5, 5), Action = Action.Move },
                new() { Source = (4, 4), Destination = (3, 5), Action = Action.Move }
            };

            var boardMock = new Mock<IBoardCreator>();
            boardMock.Setup(s => s.GetBoard()).Returns(board);
            boardMock.Setup(s => s.GetBoardSize()).Returns(1);
            var eventsHandler = new EventsHandler(boardMock.Object, new EventsCreator(boardMock.Object, new PositionProcessor()));
            
            var result = eventsHandler.SelectTheBestPathForPawn(events);
            Assert.Single(result);
            Assert.True(result.All(r => r.Action == Action.Move));
        }
        
        [Fact]
        public void GivenSimpleSetOfEventsWhenISelectTheBestPathThenTheBestPathIsReturned()
        {
            var board = new Dictionary<(int, int), Field>
            {
                { (0, 0), new Field { Playable = true, Player = Players.None } },
            };
            
            var events = new List<Event>
            {
                new() { Source = (3, 7), Destination = (2, 6), Action = Action.Kill },
                new() { Source = (3, 7), Destination = (1, 5), Action = Action.Move },
                new() { Source = (3, 7), Destination = (4, 6), Action = Action.Move }
            };
            
            var boardMock = new Mock<IBoardCreator>();
            boardMock.Setup(s => s.GetBoard()).Returns(board);
            boardMock.Setup(s => s.GetBoardSize()).Returns(1);
            var eventsHandler = new EventsHandler(boardMock.Object, new EventsCreator(boardMock.Object, new PositionProcessor()));
            
            var result = eventsHandler.SelectTheBestPathForPawn(events);
            var expected = new List<Event>
            {
                new() { Source = (3, 7), Destination = (2, 6), Action = Action.Kill },
                new() { Source = (3, 7), Destination = (1, 5), Action = Action.Move }
            };
            expected.Should().BeEquivalentTo(result);
        }

        [Fact]
        public void GivenAnotherSimpleSetOfEventsWhenISelectTheBestPathThenTheBestPathIsReturned()
        {
            var board = new Dictionary<(int, int), Field>
            {
                { (0, 0), new Field { Playable = true, Player = Players.None } },
            };

            var events = new List<Event>
            {
                new() { Source = (3, 7), Destination = (2, 6), Action = Action.Move },
                new() { Source = (3, 7), Destination = (4, 6), Action = Action.Kill },
                new() { Source = (3, 7), Destination = (5, 5), Action = Action.Move }
            };

            var boardMock = new Mock<IBoardCreator>();
            boardMock.Setup(s => s.GetBoard()).Returns(board);
            boardMock.Setup(s => s.GetBoardSize()).Returns(1);
            var eventsHandler = new EventsHandler(boardMock.Object, new EventsCreator(boardMock.Object, new PositionProcessor()));
            
            var result = eventsHandler.SelectTheBestPathForPawn(events);
            var expected = new List<Event>
            {
                new() { Source = (3, 7), Destination = (4, 6), Action = Action.Kill },
                new() { Source = (3, 7), Destination = (5, 5), Action = Action.Move }
            };
            expected.Should().BeEquivalentTo(result);
        }

        [Fact]
        public void GivenMediumSetOfEventsWhenISelectTheBestPathThenTheBestPathIsReturned()
        {
            var board = new Dictionary<(int, int), Field>
            {
                { (0, 0), new Field { Playable = true, Player = Players.None } },
            };

            var events = new List<Event>
            {
                new() { Source = (2, 2), Destination = (3, 3), Action = Action.Kill },
                new() { Source = (2, 2), Destination = (4, 4), Action = Action.Move },
                new() { Source = (2, 2), Destination = (1, 3), Action = Action.Kill },
                new() { Source = (2, 2), Destination = (0, 4), Action = Action.Move }
            };
            
            var boardMock = new Mock<IBoardCreator>();
            boardMock.Setup(s => s.GetBoard()).Returns(board);
            boardMock.Setup(s => s.GetBoardSize()).Returns(1);
            var eventsHandler = new EventsHandler(boardMock.Object, new EventsCreator(boardMock.Object, new PositionProcessor()));
            
            var result = eventsHandler.SelectTheBestPathForPawn(events);
            Assert.Equal(2, result.Count);
            Assert.Contains(result, r => r.Action == Action.Kill);
            Assert.Contains(result, r => r.Action == Action.Move);
        }
        
        [Fact]
        public void GivenAnotherMediumSetOfEventsWhenISelectTheBestPathThenTheBestPathIsReturned()
        {
            var board = new Dictionary<(int, int), Field>
            {
                { (0, 0), new Field { Playable = true, Player = Players.None } },
            };

            var events = new List<Event>
            {
                new() { Source = (2, 2), Destination = (3, 3), Action = Action.Kill },
                new() { Source = (2, 2), Destination = (4, 4), Action = Action.Move },
                new() { Source = (4, 4), Destination = (5, 5), Action = Action.Kill },
                new() { Source = (4, 4), Destination = (6, 6), Action = Action.Move },
                new() { Source = (2, 2), Destination = (3, 3), Action = Action.Kill },
                new() { Source = (2, 2), Destination = (4, 4), Action = Action.Move },
                new() { Source = (2, 2), Destination = (1, 3), Action = Action.Kill },
                new() { Source = (2, 2), Destination = (0, 4), Action = Action.Move }
            };
            
            var boardMock = new Mock<IBoardCreator>();
            boardMock.Setup(s => s.GetBoard()).Returns(board);
            boardMock.Setup(s => s.GetBoardSize()).Returns(1);
            var eventsHandler = new EventsHandler(boardMock.Object, new EventsCreator(boardMock.Object, new PositionProcessor()));
            
            var result = eventsHandler.SelectTheBestPathForPawn(events);
            var expected = new List<Event>
            {
                new() { Source = (2, 2), Destination = (3, 3), Action = Action.Kill },
                new() { Source = (2, 2), Destination = (4, 4), Action = Action.Move },
                new() { Source = (4, 4), Destination = (5, 5), Action = Action.Kill },
                new() { Source = (4, 4), Destination = (6, 6), Action = Action.Move },
            };
            expected.Should().BeEquivalentTo(result);
        }
        
        [Fact]
        public void GivenLargeSetOfEventsWhenISelectTheBestPathThenTheBestPathIsReturned()
        {
            var board = new Dictionary<(int, int), Field>
            {
                { (0, 0), new Field { Playable = true, Player = Players.None } },
            };
            
            var events = new List<Event>
            {
                new() { Source = (3, 7), Destination = (2, 6), Action = Action.Kill },
                new() { Source = (3, 7), Destination = (1, 5), Action = Action.Move },
                new() { Source = (1, 5), Destination = (2, 4), Action = Action.Kill },
                new() { Source = (1, 5), Destination = (3, 3), Action = Action.Move },
                new() { Source = (3, 3), Destination = (4, 2), Action = Action.Kill },
                new() { Source = (3, 3), Destination = (5, 1), Action = Action.Move },
                new() { Source = (3, 7), Destination = (2, 6), Action = Action.Kill },
                new() { Source = (3, 7), Destination = (1, 5), Action = Action.Move },
                new() { Source = (1, 5), Destination = (2, 4), Action = Action.Kill },
                new() { Source = (1, 5), Destination = (3, 3), Action = Action.Move },
                new() { Source = (3, 3), Destination = (2, 2), Action = Action.Kill },
                new() { Source = (3, 3), Destination = (1, 1), Action = Action.Move },
                new() { Source = (1, 1), Destination = (0, 0), Action = Action.Kill },
                new() { Source = (1, 1), Destination = (-1, -1), Action = Action.Move },
                new() { Source = (3, 7), Destination = (2, 6), Action = Action.Kill },
                new() { Source = (3, 7), Destination = (1, 5), Action = Action.Move },
                new() { Source = (1, 5), Destination = (2, 4), Action = Action.Kill },
                new() { Source = (1, 5), Destination = (3, 3), Action = Action.Move },
                new() { Source = (3, 3), Destination = (2, 2), Action = Action.Kill },
                new() { Source = (3, 3), Destination = (1, 1), Action = Action.Move },
                new() { Source = (3, 7), Destination = (2, 6), Action = Action.Kill },
                new() { Source = (3, 7), Destination = (1, 5), Action = Action.Move },
                new() { Source = (1, 5), Destination = (2, 4), Action = Action.Kill },
                new() { Source = (1, 5), Destination = (3, 3), Action = Action.Move },
                new() { Source = (3, 7), Destination = (2, 6), Action = Action.Kill },
                new() { Source = (3, 7), Destination = (1, 5), Action = Action.Move },
                new() { Source = (3, 7), Destination = (4, 6), Action = Action.Kill },
                new() { Source = (3, 7), Destination = (5, 5), Action = Action.Move },
                new() { Source = (5, 5), Destination = (6, 4), Action = Action.Kill },
                new() { Source = (5, 5), Destination = (7, 3), Action = Action.Move },
                new() { Source = (7, 3), Destination = (6, 2), Action = Action.Kill },
                new() { Source = (7, 3), Destination = (5, 1), Action = Action.Move },
                new() { Source = (3, 7), Destination = (4, 6), Action = Action.Kill },
                new() { Source = (3, 7), Destination = (5, 5), Action = Action.Move },
                new() { Source = (5, 5), Destination = (6, 4), Action = Action.Kill },
                new() { Source = (5, 5), Destination = (7, 3), Action = Action.Move },
                new() { Source = (3, 7), Destination = (4, 6), Action = Action.Kill },
                new() { Source = (3, 7), Destination = (5, 5), Action = Action.Move }
            };
            
            var boardMock = new Mock<IBoardCreator>();
            boardMock.Setup(s => s.GetBoard()).Returns(board);
            boardMock.Setup(s => s.GetBoardSize()).Returns(1);
            var eventsHandler = new EventsHandler(boardMock.Object, new EventsCreator(boardMock.Object, new PositionProcessor()));
            
            var result = eventsHandler.SelectTheBestPathForPawn(events);
            var expected = new List<Event>
            {
                new() { Source = (3, 7), Destination = (2, 6), Action = Action.Kill },
                new() { Source = (3, 7), Destination = (1, 5), Action = Action.Move },
                new() { Source = (1, 5), Destination = (2, 4), Action = Action.Kill },
                new() { Source = (1, 5), Destination = (3, 3), Action = Action.Move },
                new() { Source = (3, 3), Destination = (2, 2), Action = Action.Kill },
                new() { Source = (3, 3), Destination = (1, 1), Action = Action.Move },
                new() { Source = (1, 1), Destination = (0, 0), Action = Action.Kill },
                new() { Source = (1, 1), Destination = (-1, -1), Action = Action.Move }
            };
            expected.Should().BeEquivalentTo(result);
        }
        
        [Fact]
        public void GivenAnotherLargeSetOfEventsWhenISelectTheBestPathThenTheBestPathIsReturned()
        {
            var board = new Dictionary<(int, int), Field>
            {
                { (0, 0), new Field { Playable = true, Player = Players.None } },
            };
            var events = new List<Event>
            {
                new() { Source = (3, 0), Destination = (2, 1), Action = Action.Kill },
                new() { Source = (3, 0), Destination = (1, 2), Action = Action.Move },
                new() { Source = (1, 2), Destination = (2, 3), Action = Action.Kill },
                new() { Source = (1, 2), Destination = (3, 4), Action = Action.Move },
                new() { Source = (3, 0), Destination = (2, 1), Action = Action.Kill },
                new() { Source = (3, 0), Destination = (1, 2), Action = Action.Move },
                new() { Source = (3, 0), Destination = (4, 1), Action = Action.Kill },
                new() { Source = (3, 0), Destination = (5, 2), Action = Action.Move },
                new() { Source = (5, 2), Destination = (4, 3), Action = Action.Kill },
                new() { Source = (5, 2), Destination = (3, 4), Action = Action.Move },
                new() { Source = (3, 4), Destination = (4, 5), Action = Action.Kill },
                new() { Source = (3, 4), Destination = (5, 6), Action = Action.Move },
                new() { Source = (3, 0), Destination = (4, 1), Action = Action.Kill },
                new() { Source = (3, 0), Destination = (5, 2), Action = Action.Move },
                new() { Source = (5, 2), Destination = (4, 3), Action = Action.Kill },
                new() { Source = (5, 2), Destination = (3, 4), Action = Action.Move },
                new() { Source = (3, 0), Destination = (4, 1), Action = Action.Kill },
                new() { Source = (3, 0), Destination = (5, 2), Action = Action.Move },
                new() { Source = (5, 2), Destination = (6, 3), Action = Action.Kill },
                new() { Source = (5, 2), Destination = (7, 4), Action = Action.Move },
                new() { Source = (3, 0), Destination = (4, 1), Action = Action.Kill },
                new() { Source = (3, 0), Destination = (5, 2), Action = Action.Move }
            };

            var boardMock = new Mock<IBoardCreator>();
            boardMock.Setup(s => s.GetBoard()).Returns(board);
            boardMock.Setup(s => s.GetBoardSize()).Returns(1);
            var eventsHandler = new EventsHandler(boardMock.Object, new EventsCreator(boardMock.Object, new PositionProcessor()));
            
            var result = eventsHandler.SelectTheBestPathForPawn(events);
            var expected = new List<Event>
            {
                new() { Source = (3, 0), Destination = (4, 1), Action = Action.Kill },
                new() { Source = (3, 0), Destination = (5, 2), Action = Action.Move },
                new() { Source = (5, 2), Destination = (4, 3), Action = Action.Kill },
                new() { Source = (5, 2), Destination = (3, 4), Action = Action.Move },
                new() { Source = (3, 4), Destination = (4, 5), Action = Action.Kill },
                new() { Source = (3, 4), Destination = (5, 6), Action = Action.Move }
            };
            expected.Should().BeEquivalentTo(result);
        }
        
        [Fact]
        public void GivenSmallBoardWhenICreateAllEventsForGivenPawnThenAllEventsForGivenPawnAreReturned()
        {
            var board = new Dictionary<(int, int), Field>
            {
                { (0, 0), new Field { Playable = true, Player = Players.None } },
                { (1, 0), new Field { Playable = true, Player = Players.None } },
                { (2, 0), new Field { Playable = true, Player = Players.Player1 } },
                { (3, 0), new Field { Playable = true, Player = Players.None } },
                { (0, 1), new Field { Playable = true, Player = Players.None } },
                { (1, 1), new Field { Playable = true, Player = Players.None } },
                { (2, 1), new Field { Playable = true, Player = Players.None } },
                { (3, 1), new Field { Playable = true, Player = Players.None } },
                { (0, 2), new Field { Playable = true, Player = Players.None } },
                { (1, 2), new Field { Playable = true, Player = Players.None } },
                { (2, 2), new Field { Playable = true, Player = Players.None } },
                { (3, 2), new Field { Playable = true, Player = Players.None } },
                { (0, 3), new Field { Playable = true, Player = Players.None } },
                { (1, 3), new Field { Playable = true, Player = Players.None } },
                { (2, 3), new Field { Playable = true, Player = Players.None } },
                { (3, 3), new Field { Playable = true, Player = Players.None } }
            };

            var boardMock = new Mock<IBoardCreator>();
            boardMock.Setup(s => s.GetBoard()).Returns(board);
            boardMock.Setup(s => s.GetBoardSize()).Returns(5);
            var eventsHandler = new EventsHandler(boardMock.Object, new EventsCreator(boardMock.Object, new PositionProcessor()));
            
            var result = eventsHandler.CreateAllEventsForGivenPawn(Players.Player1, 2, 0);
            var expected = new List<List<Event>>
            {
                new()
                {
                    new Event { Source = (2, 0), Destination = (1, 1), Action = Action.Move }
                },
                new() 
                { 
                    new Event { Source = (2, 0), Destination = (3, 1), Action = Action.Move } 
                }
            };
            expected.Should().BeEquivalentTo(result);
        }
        
        [Fact]
        public void GivenLargeBoardWhenICreateAllEventsForGivenPawnThenAllEventsForGivenPawnAreReturned()
        {
            var board = new Dictionary<(int, int), Field>
            {
                { (0, 0), new Field { Playable = true, Player = Players.None } },
                { (1, 0), new Field { Playable = true, Player = Players.None } },
                { (2, 0), new Field { Playable = true, Player = Players.Player1 } },
                { (3, 0), new Field { Playable = true, Player = Players.None } },
                { (4, 0), new Field { Playable = true, Player = Players.None } },
                { (0, 1), new Field { Playable = true, Player = Players.None } },
                { (1, 1), new Field { Playable = true, Player = Players.None } },
                { (2, 1), new Field { Playable = true, Player = Players.None } },
                { (3, 1), new Field { Playable = true, Player = Players.Player2 } },
                { (4, 1), new Field { Playable = true, Player = Players.None } },
                { (0, 2), new Field { Playable = true, Player = Players.None } },
                { (1, 2), new Field { Playable = true, Player = Players.None } },
                { (2, 2), new Field { Playable = true, Player = Players.None } },
                { (3, 2), new Field { Playable = true, Player = Players.None } },
                { (4, 2), new Field { Playable = true, Player = Players.None } },
                { (0, 3), new Field { Playable = true, Player = Players.None } },
                { (1, 3), new Field { Playable = true, Player = Players.None } },
                { (2, 3), new Field { Playable = true, Player = Players.None } },
                { (3, 3), new Field { Playable = true, Player = Players.None } },
                { (4, 3), new Field { Playable = true, Player = Players.None } },
                { (0, 4), new Field { Playable = true, Player = Players.None } },
                { (1, 4), new Field { Playable = true, Player = Players.None } },
                { (2, 4), new Field { Playable = true, Player = Players.None } },
                { (3, 4), new Field { Playable = true, Player = Players.None } },
                { (4, 4), new Field { Playable = true, Player = Players.None } }
            };

            var boardMock = new Mock<IBoardCreator>();
            boardMock.Setup(s => s.GetBoard()).Returns(board);
            boardMock.Setup(s => s.GetBoardSize()).Returns(5);
            var eventsHandler = new EventsHandler(boardMock.Object, new EventsCreator(boardMock.Object, new PositionProcessor()));
            
            var result = eventsHandler.CreateAllEventsForGivenPawn(Players.Player1, 2, 0);
            var expected = new List<List<Event>>
            {
                new()
                {
                    new Event { Source = (2, 0), Destination = (1, 1), Action = Action.Move }
                },
                new() 
                { 
                    new Event { Source = (2, 0), Destination = (3, 1), Action = Action.Kill }, 
                    new Event { Source = (2, 0), Destination = (4, 2), Action = Action.Move } 
                }
            };
            expected.Should().BeEquivalentTo(result);
        }
        
        [Fact]
        public void GivenAnotherLargeBoardWhenICreateAllEventsForGivenPawnThenAllEventsForGivenPawnAreReturned()
        {
            var board = new Dictionary<(int, int), Field>
            {
                { (0, 0), new Field { Playable = true, Player = Players.None } },
                { (1, 0), new Field { Playable = true, Player = Players.None } },
                { (2, 0), new Field { Playable = true, Player = Players.Player1 } },
                { (3, 0), new Field { Playable = true, Player = Players.None } },
                { (4, 0), new Field { Playable = true, Player = Players.None } },
                { (0, 1), new Field { Playable = true, Player = Players.None } },
                { (1, 1), new Field { Playable = true, Player = Players.Player2 } },
                { (2, 1), new Field { Playable = true, Player = Players.None } },
                { (3, 1), new Field { Playable = true, Player = Players.None } },
                { (4, 1), new Field { Playable = true, Player = Players.None } },
                { (0, 2), new Field { Playable = true, Player = Players.None } },
                { (1, 2), new Field { Playable = true, Player = Players.None } },
                { (2, 2), new Field { Playable = true, Player = Players.None } },
                { (3, 2), new Field { Playable = true, Player = Players.None } },
                { (4, 2), new Field { Playable = true, Player = Players.None } },
                { (0, 3), new Field { Playable = true, Player = Players.None } },
                { (1, 3), new Field { Playable = true, Player = Players.None } },
                { (2, 3), new Field { Playable = true, Player = Players.None } },
                { (3, 3), new Field { Playable = true, Player = Players.None } },
                { (4, 3), new Field { Playable = true, Player = Players.None } },
                { (0, 4), new Field { Playable = true, Player = Players.None } },
                { (1, 4), new Field { Playable = true, Player = Players.None } },
                { (2, 4), new Field { Playable = true, Player = Players.None } },
                { (3, 4), new Field { Playable = true, Player = Players.None } },
                { (4, 4), new Field { Playable = true, Player = Players.None } }
            };

            var boardMock = new Mock<IBoardCreator>();
            boardMock.Setup(s => s.GetBoard()).Returns(board);
            boardMock.Setup(s => s.GetBoardSize()).Returns(5);
            var eventsHandler = new EventsHandler(boardMock.Object, new EventsCreator(boardMock.Object, new PositionProcessor()));
            
            var result = eventsHandler.CreateAllEventsForGivenPawn(Players.Player1, 2, 0);
            var expected = new List<List<Event>>
            {
                new()
                {
                    new Event { Source = (2, 0), Destination = (1, 1), Action = Action.Kill },
                    new Event { Source = (2, 0), Destination = (0, 2), Action = Action.Move }
                },
                new() 
                { 
                    new Event { Source = (2, 0), Destination = (3, 1), Action = Action.Move }
                }
            };
            expected.Should().BeEquivalentTo(result);
        }
        
        [Fact]
        public void GivenVeryLargeBoardWhenICreateAllEventsForGivenPawnThenAllEventsForGivenPawnAreReturned()
        {
            var board = new Dictionary<(int, int), Field>
            {
                { (0, 0), new Field { Playable = true, Player = Players.None } },
                { (1, 0), new Field { Playable = true, Player = Players.None } },
                { (2, 0), new Field { Playable = true, Player = Players.None } },
                { (3, 0), new Field { Playable = true, Player = Players.None } },
                { (4, 0), new Field { Playable = true, Player = Players.None } },
                { (5, 0), new Field { Playable = true, Player = Players.None } },
                { (6, 0), new Field { Playable = true, Player = Players.None } },
                { (7, 0), new Field { Playable = true, Player = Players.None } },
                { (0, 1), new Field { Playable = true, Player = Players.None } },
                { (1, 1), new Field { Playable = true, Player = Players.Player2 } },
                { (2, 1), new Field { Playable = true, Player = Players.None } },
                { (3, 1), new Field { Playable = true, Player = Players.None } },
                { (4, 1), new Field { Playable = true, Player = Players.None } },
                { (5, 1), new Field { Playable = true, Player = Players.None } },
                { (6, 1), new Field { Playable = true, Player = Players.None } },
                { (7, 1), new Field { Playable = true, Player = Players.None } },
                { (0, 2), new Field { Playable = true, Player = Players.None } },
                { (1, 2), new Field { Playable = true, Player = Players.None } },
                { (2, 2), new Field { Playable = true, Player = Players.Player1 } },
                { (3, 2), new Field { Playable = true, Player = Players.None } },
                { (4, 2), new Field { Playable = true, Player = Players.Player1 } },
                { (5, 2), new Field { Playable = true, Player = Players.None } },
                { (6, 2), new Field { Playable = true, Player = Players.Player2 } },
                { (7, 2), new Field { Playable = true, Player = Players.None } },
                { (0, 3), new Field { Playable = true, Player = Players.None } },
                { (1, 3), new Field { Playable = true, Player = Players.None } },
                { (2, 3), new Field { Playable = true, Player = Players.None } },
                { (3, 3), new Field { Playable = true, Player = Players.None } },
                { (4, 3), new Field { Playable = true, Player = Players.None } },
                { (5, 3), new Field { Playable = true, Player = Players.None } },
                { (6, 3), new Field { Playable = true, Player = Players.None } },
                { (7, 3), new Field { Playable = true, Player = Players.None } },
                { (0, 4), new Field { Playable = true, Player = Players.None } },
                { (1, 4), new Field { Playable = true, Player = Players.None } },
                { (2, 4), new Field { Playable = true, Player = Players.Player1 } },
                { (3, 4), new Field { Playable = true, Player = Players.None } },
                { (4, 4), new Field { Playable = true, Player = Players.Player2 } },
                { (5, 4), new Field { Playable = true, Player = Players.None } },
                { (6, 4), new Field { Playable = true, Player = Players.Player1 } },
                { (7, 4), new Field { Playable = true, Player = Players.None } },
                { (0, 5), new Field { Playable = true, Player = Players.None } },
                { (1, 5), new Field { Playable = true, Player = Players.None } },
                { (2, 5), new Field { Playable = true, Player = Players.None } },
                { (3, 5), new Field { Playable = true, Player = Players.None } },
                { (4, 5), new Field { Playable = true, Player = Players.None } },
                { (5, 5), new Field { Playable = true, Player = Players.None } },
                { (6, 5), new Field { Playable = true, Player = Players.None } },
                { (7, 5), new Field { Playable = true, Player = Players.None } },
                { (0, 6), new Field { Playable = true, Player = Players.None } },
                { (1, 6), new Field { Playable = true, Player = Players.None } },
                { (2, 6), new Field { Playable = true, Player = Players.Player1 } },
                { (3, 6), new Field { Playable = true, Player = Players.None } },
                { (4, 6), new Field { Playable = true, Player = Players.Player1 } },
                { (5, 6), new Field { Playable = true, Player = Players.None } },
                { (6, 6), new Field { Playable = true, Player = Players.None } },
                { (7, 6), new Field { Playable = true, Player = Players.None } },
                { (0, 7), new Field { Playable = true, Player = Players.None } },
                { (1, 7), new Field { Playable = true, Player = Players.None } },
                { (2, 7), new Field { Playable = true, Player = Players.None } },
                { (3, 7), new Field { Playable = true, Player = Players.Player2 } },
                { (4, 7), new Field { Playable = true, Player = Players.None } },
                { (5, 7), new Field { Playable = true, Player = Players.None } },
                { (6, 7), new Field { Playable = true, Player = Players.None } },
                { (7, 7), new Field { Playable = true, Player = Players.None } }
            };

            var boardMock = new Mock<IBoardCreator>();
            boardMock.Setup(s => s.GetBoard()).Returns(board);
            boardMock.Setup(s => s.GetBoardSize()).Returns(8);
            var eventsHandler = new EventsHandler(boardMock.Object, new EventsCreator(boardMock.Object, new PositionProcessor()));
            
            var result = eventsHandler.CreateAllEventsForGivenPawn(Players.Player2, 3, 7);
            var expected = new List<List<Event>>
            {
                new()
                {
                    new Event { Source = (3, 7), Destination = (2, 6), Action = Action.Kill },
                    new Event { Source = (3, 7), Destination = (1, 5), Action = Action.Move },
                    new Event { Source = (1, 5), Destination = (2, 4), Action = Action.Kill },
                    new Event { Source = (1, 5), Destination = (3, 3), Action = Action.Move },
                    new Event { Source = (3, 3), Destination = (4, 2), Action = Action.Kill },
                    new Event { Source = (3, 3), Destination = (5, 1), Action = Action.Move }
                },
                new()
                {
                    new Event { Source = (3, 7), Destination = (2, 6), Action = Action.Kill },
                    new Event { Source = (3, 7), Destination = (1, 5), Action = Action.Move },
                    new Event { Source = (1, 5), Destination = (2, 4), Action = Action.Kill },
                    new Event { Source = (1, 5), Destination = (3, 3), Action = Action.Move }
                },
                new()
                {
                    new Event { Source = (3, 7), Destination = (2, 6), Action = Action.Kill },
                    new Event { Source = (3, 7), Destination = (1, 5), Action = Action.Move }
                },
                new()
                {
                    new Event { Source = (3, 7), Destination = (4, 6), Action = Action.Kill },
                    new Event { Source = (3, 7), Destination = (5, 5), Action = Action.Move },
                    new Event { Source = (5, 5), Destination = (6, 4), Action = Action.Kill },
                    new Event { Source = (5, 5), Destination = (7, 3), Action = Action.Move }
                },
                new()
                {
                    new Event { Source = (3, 7), Destination = (4, 6), Action = Action.Kill },
                    new Event { Source = (3, 7), Destination = (5, 5), Action = Action.Move }
                }
            };
            expected.Should().BeEquivalentTo(result);
        }
    }
}