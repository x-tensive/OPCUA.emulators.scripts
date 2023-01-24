using System.Text;
using System.Threading.Tasks;
using Xtensive.DPA.OPCUA;

namespace Xtensive.DPA.Server
{
    internal class EmulUploadScript : Protocols.IProgramUploadScript
    {
        public async Task Upload(int channel, string programName, byte[] content, IOpcUaClient client)
        {
            var pathParent = string.Format("CNC/channels/CHAN{0}/fs", channel);
            var pathChild = string.Format("CNC/channels/CHAN{0}/fs/upload", channel);

            var nodeParent = await client.GetNodeByRoute(pathParent);
            var nodeMethod = await client.GetNodeByRoute(pathChild);

            await client.Invoke(nodeParent, nodeMethod, new object[] { programName, Encoding.UTF8.GetString(content) });
        }
    }
}
