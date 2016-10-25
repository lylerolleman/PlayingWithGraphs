using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PlayingWithGraphs.Models
{
    public class IDTranslator
    {
        private Dictionary<string, int> NodeNameToID;
        private Dictionary<int, string> IDToNodeName;
        private Dictionary<string, int> CNameToID;
        private Dictionary<int, string> IDToCName;

        public IDTranslator()
        {
            NodeNameToID = new Dictionary<string, int>();
            IDToNodeName = new Dictionary<int, string>();
            CNameToID = new Dictionary<string, int>();
            IDToCName = new Dictionary<int, string>();
        }
        public void AddNodeTranslation(string tnid, int nid)
        {
            NodeNameToID.Add(tnid, nid);
            IDToNodeName.Add(nid, tnid);
        }
        public void AddConnectionTranslation(string tcid, int cid)
        {
            CNameToID.Add(tcid, cid);
            IDToCName.Add(cid, tcid);
        }

        public void RemoveNodeTranslation(int nid)
        {
            NodeNameToID.Remove(IDToNodeName[nid]);
            IDToNodeName.Remove(nid);
        }
        public void RemoveNodeTranslation(string tnid)
        {
            IDToNodeName.Remove(NodeNameToID[tnid]);
            NodeNameToID.Remove(tnid);
        }
        public void RemoveConnectionTranslation(int cid)
        {
            CNameToID.Remove(IDToCName[cid]);
            IDToCName.Remove(cid);
        }
        public void RemoveConnectionTranslation(string tcid)
        {
            IDToCName.Remove(CNameToID[tcid]);
            CNameToID.Remove(tcid);
        }

        public int GetNodeID(string tnid)
        {
            return NodeNameToID[tnid];
        }
        public string GetTNode(int nid)
        {
            return IDToNodeName[nid];
        }
        public int GetConnectionID(string tcid)
        {
            return CNameToID[tcid];
        }
        public string GetTConnection(int cid)
        {
            return IDToCName[cid];
        }

        public bool NodeContains(string tnid)
        {
            return NodeNameToID.ContainsKey(tnid);
        }
    }
}