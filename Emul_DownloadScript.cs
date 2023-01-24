using System;
using System.Text;
using System.Threading.Tasks;
using Xtensive.DPA.Contracts;
using Xtensive.DPA.OPCUA;

namespace Xtensive.DPA.Server
{
    internal class EmulDownloadScript : Xtensive.DPA.Protocols.IProgramDownloadScript
    {
        public async Task<DownloadProgramResponseInfo> Download(int channel, string programName, IOpcUaClient client)
        {
            var pathParent = string.Format("CNC/channels/CHAN{0}/fs", channel);
            var pathChild = string.Format("CNC/channels/CHAN{0}/fs/download", channel);

            var nodeParent = await client.GetNodeByRoute(pathParent);
            var nodeMethod = await client.GetNodeByRoute(pathChild);
            
            var result = await client.Invoke(nodeParent, nodeMethod, new object[] { programName});
            return new DownloadProgramResponseInfo() {
                Data = Encoding.UTF8.GetBytes((string)result[0]),
                Format = FileFormat.PlainTextUTF8
            };            
        }
    }
}