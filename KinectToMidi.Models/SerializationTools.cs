using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace KinectToMidi.Models
{
    /// <summary>
    /// Write and read operations
    /// </summary>
    public static class SerializationTools
    {
        /// <summary>
        /// Save the current set to the specified file
        /// </summary>
        /// <param name="fileName">full file path to save</param>
        public static void WriteObject(string fileName, BlocksSet blocksSet)
        {
            using (var writer = new FileStream(fileName, FileMode.Create))
            {
                var ser =
                    new DataContractSerializer(typeof(BlocksSet));
                ser.WriteObject(writer, blocksSet);
            }
        }

        /// <summary>
        /// Load a set of blocks from the specified file
        /// </summary>
        /// <param name="fileName">full file path</param>
        public static BlocksSet ReadObject(string fileName)
        {
            BlocksSet blocksSet = null;
            try
            {
                using (var fs = new FileStream(fileName, FileMode.Open))
                {
                    XmlDictionaryReader reader =
                        XmlDictionaryReader.CreateTextReader(fs, new XmlDictionaryReaderQuotas());
                    var ser = new DataContractSerializer(typeof(BlocksSet));

                    // Deserialize the data and read it from the instance.
                    blocksSet =
                        (BlocksSet)ser.ReadObject(reader, true);
                }
            }
            catch
            {
                //handle read exeption
            }

            return blocksSet;
        }
    }
}
