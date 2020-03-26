using System;
using System.Runtime.Serialization;

namespace MiniGamblerNetworking
{
	[Serializable]
	internal class PacketReservedException : Exception
	{
		public PacketReservedException() : base("This packet is reserved!")
		{
		}

		public PacketReservedException(string message) : base(message)
		{
		}

		public PacketReservedException(string message, Exception innerException) : base(message, innerException)
		{
		}

		protected PacketReservedException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}