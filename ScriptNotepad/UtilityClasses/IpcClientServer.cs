#region license
/*
This file is public domain.
You may freely do anything with it.

Copyright (c) VPKSoft 2019
*/
#endregion

using System;
using System.Runtime.Remoting.Channels.Ipc;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Messaging;
using System.Security.Permissions;

// Based on the Microsoft's article on IpcChannel class: https://docs.microsoft.com/en-us/dotnet/api/system.runtime.remoting.channels.ipc.ipcchannel

#pragma warning disable CS1587 // XML comment is not placed on a valid language element
                              /// <summary>
                              /// A name space for the IpcClientServer class.
                              /// </summary>
namespace VPKSoft.IPC
#pragma warning restore CS1587 // XML comment is not placed on a valid language element
{
    /// <summary>
    /// A class for IPC channel messaging.
    /// </summary>
    public class IpcClientServer
    {
        /// <summary>
        /// The IPC server channel.
        /// </summary>
        private IpcChannel serverChannel = null;

        /// <summary>
        /// The IPC client channel.
        /// </summary>
        private IpcChannel clientChannel = null;

        /// <summary>
        /// Creates the IPC server channel.
        /// </summary>
        /// <param name="endPoint">The end point address.</param>
        /// <param name="port">The port to be used with the <paramref name="endPoint"/>.</param>
        /// <returns>True if the operation was successful; otherwise false.</returns>
        [SecurityPermission(SecurityAction.Demand)]
        public bool CreateServer(string endPoint, ushort port)
        {
            try
            {
                // create a new IpcChannel..
                serverChannel = new IpcChannel($"{endPoint}:{port}");

                // register the server channel..
                ChannelServices.RegisterChannel(serverChannel, true);

                // expose an object for remote calls..
                RemotingConfiguration.
                    RegisterWellKnownServiceType(
                        typeof(RemoteMessage), "RemoteMessage.rem",
                        WellKnownObjectMode.Singleton);

                // success..
                return true;
            }
            catch
            {
                // fail..
                return false;
            }
        }

        /// <summary>
        /// The class to be used with both client and server for interprocess communication using strings as messages.
        /// </summary>
        RemoteMessage service = null;

        /// <summary>
        /// Creates the IPC client channel.
        /// </summary>
        /// <param name="endPoint">The end point address.</param>
        /// <param name="port">The port to be used with the <paramref name="endPoint"/>.</param>
        /// <returns>True if the operation was successful; otherwise false.</returns>
        [SecurityPermission(SecurityAction.Demand)]
        public bool CreateClient(string endPoint, ushort port)
        {
            try
            {
                // create the channel..
                clientChannel = new IpcChannel();

                // register the channel..
                ChannelServices.RegisterChannel(clientChannel, true);

                // register as client for remote object..
                WellKnownClientTypeEntry remoteType =
                    new WellKnownClientTypeEntry(
                        typeof(RemoteMessage),
                        $"ipc://{endPoint}:{port}/RemoteMessage.rem");

                // register the client end as a well-known type..
                RemotingConfiguration.RegisterWellKnownClientType(remoteType);

                // create a message sink..
                string objectUri;
                IMessageSink messageSink =
                    clientChannel.CreateMessageSink(
                        $"ipc://{endPoint}:{port}/RemoteMessage.rem", null,
                        out objectUri);

                // create the class to be used for passing messages between the client and the server channels..
                service = new RemoteMessage();

                // success..
                return true;
            }
            catch
            {
                // fail..
                return false;
            }
        }

        /// <summary>
        /// Sends a string message to the server.
        /// </summary>
        /// <param name="message">The message to sent to the server.</param>
        /// <returns>True if the operation was successful; otherwise false.</returns>
        public bool SendMessage(string message)
        {
            try
            {
                // check that there is an instance created of the RemoteMessage class..
                service?.SendString(message);
                // success..
                return true;
            }
            catch
            {
                // fail..
                return false;
            }
        }

        /// <summary>
        /// A singleton class to be used to send messages to the IPC server.
        /// </summary>
        /// <seealso cref="System.MarshalByRefObject" />
        public class RemoteMessage: MarshalByRefObject
        {
            /// <summary>
            /// A delegate for the MessageReceived event.
            /// </summary>
            /// <param name="sender">The sender of the event.</param>
            /// <param name="e">The <see cref="MessageReceivedEventArgs"/> instance containing the event data.</param>
            public delegate void OnMessageReceived(object sender, MessageReceivedEventArgs e);

            /// <summary>
            /// Occurs when <see cref="SendString"/> method has been called.
            /// </summary>
            public static event OnMessageReceived MessageReceived = null;

            /// <summary>
            /// Sends a string message to the IPC server via the <see cref="MessageReceived"/> event.
            /// <note type="note">The event is raised from another thread so with GUI interaction an invocation is required.</note>
            /// </summary>
            /// <param name="value">The value of the message to be sent.</param>
            public void SendString(string value)
            {
                // raise the event if subscribed..
                MessageReceived?.Invoke(this, new MessageReceivedEventArgs() { Message = value });
            }
        }
    }

    /// <summary>
    /// Event arguments for the RemoteMessage.MessageReceived event.
    /// </summary>
    /// <seealso cref="System.EventArgs" />
    public class MessageReceivedEventArgs: EventArgs
    {
        /// <summary>
        /// Gets or sets the message for the IPC client / server channels.
        /// </summary>
        public string Message { get; set; }
    }
}
