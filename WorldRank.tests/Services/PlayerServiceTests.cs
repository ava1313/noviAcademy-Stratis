using Moq;
using System;
using System.Collections.Generic;
using System.Text;

using NoviCode;
using Castle.Core.Logging;
using Microsoft.Extensions.Logging;

namespace NoviCode.tests.Services
{
    public class PlayerServiceTests
    {
        private readonly Mock<IPlayerRepository> _playerRepositoryMock = new();
        private readonly Mock<ICache> _cacheMock = new();
        private readonly Mock<ILogger<PlayerService>> _loggerMock = new();
        private readonly PlayerService _sut;

        public PlayerServiceTests()
        {
            _sut = new PlayerService(_playerRepositoryMock.Object, _cacheMock.Object, _loggerMock.Object);
        }

        [Fact]

        public async Task GetById_IdExists_returnsPlayer()
        {
            var name = "Cisse";
            var playerId = Guid.NewGuid();

            var expectedPlayer = Player.CreateNew(
                playerId,
                name,
                0
            );

            _playerRepositoryMock.Setup(mock=> mock.GetByIdAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>())).ReturnsAsync(expectedPlayer);
            var player = await _sut.CreateAsync(Guid.NewGuid(), name, CancellationToken.None);

            Assert.NotNull(player);
            Assert.Equal(expectedPlayer.Id, player?.Id);
        }
        
    }
}


