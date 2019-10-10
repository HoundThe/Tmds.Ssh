// This file is part of Tmds.Ssh which is released under LGPL-3.0.
// See file LICENSE for full license details.

using System;
using Microsoft.Extensions.Logging;

namespace Tmds.Ssh
{
    // https://docs.microsoft.com/en-us/aspnet/core/fundamentals/logging/loggermessage?view=aspnetcore-3.0
    static class LoggingExtensions
    {
        private static readonly Action<ILogger, string, int, Exception?> _connecting;
        private static readonly Action<ILogger, Exception?> _connectionEstablished;
        private static readonly Action<ILogger, string, Exception?> _localVersionString;
        private static readonly Action<ILogger, string, Exception?> _remoteVersionString;
        private static readonly Action<ILogger, Name, Exception?> _kexAlgorithm;
        private static readonly Action<ILogger, Name, Name, Name, Exception?> _algS2C;
        private static readonly Action<ILogger, Name, Name, Name, Exception?> _algC2S;
        private static readonly Action<ILogger, string, Exception?> _authMethod;
        private static readonly Action<ILogger, Exception?> _authSuccess;

        static LoggingExtensions()
        {
            _connecting = LoggerMessage.Define<string, int>(
                eventId: 1,
                logLevel: LogLevel.Information,
                formatString: "Connecting to '{host}' port {port}"
            );

            _connectionEstablished = LoggerMessage.Define(
                eventId: 2,
                logLevel: LogLevel.Information,
                formatString: "Connection established"
            );

            _localVersionString = LoggerMessage.Define<string>(
                eventId: 3,
                logLevel: LogLevel.Information,
                formatString: "Local version string: {identificationString}"
            );

            _remoteVersionString = LoggerMessage.Define<string>(
                eventId: 4,
                logLevel: LogLevel.Information,
                formatString: "Remote version string: {identificationString}"
            );

            _kexAlgorithm = LoggerMessage.Define<Name>(
                eventId: 5,
                logLevel: LogLevel.Information,
                formatString: "Key exchange: {algorithm}"
            );

            _algS2C = LoggerMessage.Define<Name, Name, Name>(
                eventId: 6,
                logLevel: LogLevel.Information,
                formatString: "S->C cipher: {cipherAlgorithm} mac: {macAlgorithm} compression: {compressionAlgorithm}"
            );

            _algC2S = LoggerMessage.Define<Name, Name, Name>(
                eventId: 7,
                logLevel: LogLevel.Information,
                formatString: "C->S cipher: {cipherAlgorithm} mac: {macAlgorithm} compression: {compressionAlgorithm}"
            );

            _authMethod = LoggerMessage.Define<string>(
                eventId: 8,
                logLevel: LogLevel.Information,
                formatString: "Authentication method: {method}"
            );

            _authSuccess = LoggerMessage.Define(
                eventId: 9,
                logLevel: LogLevel.Information,
                formatString: "Authentication succeeded"
            );
        }

        public static void Connecting(this ILogger logger, string host, int port)
        {
            _connecting(logger, host, port, null);
        }

        public static void ConnectionEstablished(this ILogger logger)
        {
            _connectionEstablished(logger, null);
        }

        public static void LocalVersionString(this ILogger logger, string identificationString)
        {
            _localVersionString(logger, identificationString, null);
        }

        public static void RemoteVersionString(this ILogger logger, string identificationString)
        {
            _remoteVersionString(logger, identificationString, null);
        }

        public static void KeyExchangeAlgorithm(this ILogger logger, Name kexAlgorithm)
        {
            _kexAlgorithm(logger, kexAlgorithm, null);
        }

        public static void AlgorithmsServerToClient(this ILogger logger, Name cipherAlgorithm, Name macAlgorithm, Name compressionAlgorithm)
        {
            _algS2C(logger, cipherAlgorithm, macAlgorithm, compressionAlgorithm, null);
        }

        public static void AlgorithmsClientToServer(this ILogger logger, Name cipherAlgorithm, Name macAlgorithm, Name compressionAlgorithm)
        {
            _algC2S(logger, cipherAlgorithm, macAlgorithm, compressionAlgorithm, null);
        }

        public static void AuthenticationMethod(this ILogger logger, string method)
        {
            _authMethod(logger, method, null);
        }

        public static void AuthenticationSucceeded(this ILogger logger)
        {
            _authSuccess(logger, null);
        }
    }
}