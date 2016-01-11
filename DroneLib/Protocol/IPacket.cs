using System.IO;

namespace DroneLibrary.Protocol
{
    /// <summary>
    /// Stellt ein Interface für alle Kugelmatik-Pakete dar.
    /// </summary>
    public interface IPacket
    {
        /// <summary>
        /// Gibt den Paket-Typ zurück.
        /// </summary>
        PacketType Type { get; }

        /// <summary>
        /// Schreibt den Paket-Inhalt zu einem BinaryWriter.
        /// </summary>
        /// <param name="writer"></param>
        void Write(BinaryWriter writer);
    }
}
