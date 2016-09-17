using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZephorasWorld.UDPCLientServer
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    namespace ZephorasWorld.UDPCLientServer
    {
        public enum DataIdentifier
        {
            Message,
            LogIn,
            LogOut,
            Null,
            Password
        }

        public class LoginPacket
        {
            #region Private Members
            private DataIdentifier dataIdentifier;
            private string name;
            private string message;
            private string passwords;
            #endregion

            #region Public Properties
            public DataIdentifier ChatDataIdentifier
            {
                get { return dataIdentifier; }
                set { dataIdentifier = value; }
            }

            public string ChatName
            {
                get { return name; }
                set { name = value; }
            }

            public string ChatMessage
            {
                get { return message; }
                set { message = value; }
            }
            public string Password
            {
                get { return passwords; }
                set { passwords = value; }

            }
            #endregion

            #region Methods

            // Default Constructor
            public LoginPacket()
            {
                this.dataIdentifier = DataIdentifier.Null;
                this.message = null;
                this.name = null;
                this.passwords = null;
            }

            public LoginPacket(byte[] dataStream)
            {
                // Read the data identifier from the beginning of the stream (4 bytes)
                this.dataIdentifier = (DataIdentifier)BitConverter.ToInt32(dataStream, 0);

                // Read the length of the name (4 bytes)
                int nameLength = BitConverter.ToInt32(dataStream, 4);

                // Read the length of the message (4 bytes)
                int msgLength = BitConverter.ToInt32(dataStream, 8);

                long passlength = BitConverter.ToInt32(dataStream, 12);

                int pass = Convert.ToInt32(passlength);



                // Read the name field
                if (nameLength > 0)
                    this.name = Encoding.UTF8.GetString(dataStream, 12, nameLength);
                else
                    this.name = null;

                // Read the message field
                if (msgLength > 0)
                    this.message = Encoding.UTF8.GetString(dataStream, 12 + nameLength, msgLength);
                else
                    this.message = null;
                //Read the password
                if (passlength > 0)
                    this.passwords = Encoding.UTF8.GetString(dataStream, 16 + pass, msgLength);
                else
                    this.passwords = null;
            }

            // Converts the packet into a byte array for sending/receiving 
            public byte[] GetDataStream()
            {
                List<byte> dataStream = new List<byte>();

                // Add the dataIdentifier
                dataStream.AddRange(BitConverter.GetBytes((int)this.dataIdentifier));

                // Add the name length
                if (this.name != null)
                    dataStream.AddRange(BitConverter.GetBytes(this.name.Length));
                else
                    dataStream.AddRange(BitConverter.GetBytes(0));

                // Add the message length
                if (this.message != null)
                    dataStream.AddRange(BitConverter.GetBytes(this.message.Length));
                else
                    dataStream.AddRange(BitConverter.GetBytes(0));

                if (this.passwords != null)
                    dataStream.AddRange(BitConverter.GetBytes(this.passwords.Length));
                else
                    dataStream.AddRange(BitConverter.GetBytes(0));

                // Add the name
                if (this.name != null)
                    dataStream.AddRange(Encoding.UTF8.GetBytes(this.name));

                // Add the message
                if (this.message != null)
                    dataStream.AddRange(Encoding.UTF8.GetBytes(this.message));
                if (this.passwords != null)
                    dataStream.AddRange(Encoding.UTF8.GetBytes(this.passwords));

                return dataStream.ToArray();
            }

            #endregion
        }
    }


}
