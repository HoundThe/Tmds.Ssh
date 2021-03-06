using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Tmds.Ssh.Tests
{
    public class ConnectTests
    {
        [Fact]
        public async Task ClientCanConnectToServerSocket()
        {
            TaskCompletionSource<Packet> serverReceivedTcs = new TaskCompletionSource<Packet>();

            await using var server = new TestServer(
                async conn =>
                {
                    var packet = await conn.ReceivePacketAsync(default).ConfigureAwait(false);
                    serverReceivedTcs.SetResult(packet);
                }
            );
            using var client = await server.CreateClientAsync(
                s =>
                {
                    s.NoKeyExchange = true;
                    s.NoProtocolVersionExchange = true;
                    s.NoUserAuthentication = true;
                }
            );
            client.Dispose();

            // Check the server received an EOF.
            var serverReceivedPacket = await serverReceivedTcs.Task;
            Assert.True(serverReceivedPacket.IsEmpty);
        }
    }
}
