// (C) KAL ATM Software GmbH, 2021

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO.Ports;
using XFS4IoT;

namespace KAL.XFS4IoTSP.CardReader.HOTS
{
    /// <summary>
    /// Wrap up class for the SerialPort
    /// </summary>
    public class SerialComms
    {
        public SerialComms(string portName, int baudRate, StopBits stopBits, Parity parity, int dataBits, Handshake handshake)
        {
            serialPort.PortName = portName;
            serialPort.BaudRate = baudRate;
            serialPort.StopBits = stopBits;
            serialPort.Parity = parity;
            serialPort.DataBits = dataBits;
            serialPort.Handshake = handshake;
        }

        public bool Open()
        {
            serialPort.Open();
            return serialPort.IsOpen;
        }

        public void Close() => serialPort.Close();

        public bool IsOpen
        {
            get { return serialPort.IsOpen; }
        }

        /// <summary>
        /// Send and receive through serial port
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        public async Task<byte[]> SendAndReceive(string command)
        {
            return await SendAndReceive(command.Select(b => (byte)b).ToArray());
        }

        /// <summary>
        /// Send and receive through serial port
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        public async Task<byte[]> SendAndReceive(byte[] command)
        {
            serialPort.IsOpen.IsTrue($"{nameof(SendAndReceive)} is called when the serial port is closed.");

            byte checkSum = 0;

            List<byte> lowLevelCommand = new List<byte>
            {
                ControlCode.DLE,
                ControlCode.STX
            };
            foreach (byte b in command)
            {
                if (b == ControlCode.DLE)
                    lowLevelCommand.Add(ControlCode.DLE);

                lowLevelCommand.Add(b);

                // Only checksum the data.
                checkSum ^= (byte)(b & 0xff);
            }

            // Add end sequence
            lowLevelCommand.Add(ControlCode.DLE);
            lowLevelCommand.Add(ControlCode.ETX);

            checkSum ^= (byte)(ControlCode.ETX & 0xff);

            // Add checksum
            lowLevelCommand.Add((byte)(checkSum & 0xff));

            byte[] commandToSend = lowLevelCommand.ToArray();
            await serialPort.BaseStream.WriteAsync(commandToSend, 0, commandToSend.Length);
            await serialPort.BaseStream.FlushAsync();

            // Wait for response
            List<byte> response = new List<byte>();

            // read data per byte
            byte[] buffer = new byte[1];
            while (await serialPort.BaseStream.ReadAsync(buffer.AsMemory(0, buffer.Length)) > 0)
            {
                response.Add(buffer[0]);
                if (response[^2] == ControlCode.DLE &&
                    response[^1] == ControlCode.ETX)
                {
                    break;
                }
            }

            // Check the sequence and get rid of rubish before DLE+STX
            int index = 0;
            for (int i = 0; i < response.Count; i++)
            {
                if (response.Count >= 2 &&
                    response[i] == ControlCode.DLE &&
                    response[i + 1] == ControlCode.STX)
                {
                    index = i + 1;
                    index++;
                    break;
                }
            }

            return response.ToArray()[index..^2];
        }

        /// <summary>
        ///  ClearLine
        ///  The function clears the line and resets the device
        /// </summary>
        public async void ClearLine()
        {
            List<byte> command = new List<byte>
            {
                ControlCode.DLE,
                ControlCode.EOT
            };

            byte[] commandToSend = command.ToArray();
            await serialPort.BaseStream.WriteAsync(commandToSend, 0, commandToSend.Length);
            await serialPort.BaseStream.FlushAsync();

            // Wait for response
            List<byte> response = new List<byte>();

            // read data per byte
            byte[] buffer = new byte[1];
            while (await serialPort.BaseStream.ReadAsync(buffer.AsMemory(0, buffer.Length)) > 0)
            {
                response.Add(buffer[0]);
                if (response.Count >= 2 &&
                    response[^2] == ControlCode.DLE &&
                    response[^1] == ControlCode.EOT)
                {
                    break;
                }
            }
        }

        /// <summary>
        /// Data members
        /// </summary>
        private readonly SerialPort serialPort = new SerialPort();

        /// <summary>
        /// Group of control codes
        /// </summary>
        private static class ControlCode
        {
            public const byte ENQ = 0x05;
            public const byte STX = 0x02;
            public const byte ETX = 0x03;
            public const byte ACK = 0x06;
            public const byte NAK = 0x15;
            public const byte DLE = 0x10;
            public const byte EOT = 0x04;
            public const byte CR  = 0x0D;
        }
    }
}
