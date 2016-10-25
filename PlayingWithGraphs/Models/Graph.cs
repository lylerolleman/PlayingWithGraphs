using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MySql.Data.MySqlClient;
using System.Configuration;
using System.Text;

namespace PlayingWithGraphs.Models
{  
    public class Graph
    {
        public string GraphName { get; set; }
        public int gid { get; set; }
        public Dictionary<int, Node> nodes { get; set; }
        public Dictionary<int, Connection> cons { get; set; }
        private IDTranslator id_trans;

        //init common fields
        private Graph()
        {
            nodes = new Dictionary<int, Node>();
            cons = new Dictionary<int, Connection>();
            id_trans = new IDTranslator();
        }
        //New graph from name
        public Graph(string name) : this()
        {
            gid = DatabaseManager.InsertNewGraph(name);
        }

        //Get existing graph from DB
        public Graph(int id) : this()
        {
            gid = id;
            string con_query = String.Format("SELECT cid, n1, n2 FROM Con WHERE n1 IN (SELECT nid FROM Node WHERE gid = {0}) AND n2 IN (SELECT nid FROM Node WHERE gid = {1})", id, id);
            GraphName = DatabaseManager.GetGraphName(gid);
            List<Node> nodelist = DatabaseManager.GetNodes(gid);
            foreach (Node node in nodelist) { 
                nodes.Add(node.nid, node);
                id_trans.AddNodeTranslation("node-" + node.nid, node.nid);
            }
            List<Connection> conlist = DatabaseManager.GetConnections(this);
            foreach (Connection con in conlist)
            {
                id_trans.AddConnectionTranslation("cid-" + con.cid, con.cid);
                AddConnection(con);
            }
            
        }
        public void AddNode(string tid, string ntext, int x, int y)
        {
            int nid = DatabaseManager.AddNode(gid, tid, ntext, x, y);
            id_trans.AddNodeTranslation(tid, nid);
            nodes.Add(nid, new Node(nid, ntext, x, y));
        }
        public void AddConnection(string tcid, Node n1, Node n2)
        {
            int cid = DatabaseManager.InsertConnection(n1, n2);
            id_trans.AddConnectionTranslation(tcid, cid);
            AddConnection(new Connection(cid, n1, n2));
        }
        private void AddConnection(Connection con)
        {
            cons.Add(con.cid, con);
            con.source.AddChild(con);
        }

        public Node LookupNode(int nid)
        {
            if (nodes.ContainsKey(nid))
            {
                return nodes[nid];
            }
            return null;
        }
        public Node LookupNode(string tid)
        {
            if (id_trans.NodeContains(tid))
            {
                return LookupNode(id_trans.GetNodeID(tid));
            }
            return null;
        }
        public string GetConnectionJson()
        {
            StringBuilder sb = new StringBuilder("[");
            foreach (KeyValuePair<int, Connection> con in cons)
            {
                sb.AppendFormat("{{\"cid\": {0}, \"nodes\":[{1}, {2}]}},", con.Key, con.Value.source.nid, con.Value.dest.nid);
            }
            sb[sb.Length-1] = ']';
            return sb.ToString();
        }
        public void UpdateNodeLocation(Node node, int x, int y)
        {
            node.x = x;
            node.y = y;
            DatabaseManager.UpdateNode(node);
        }
        public void UpdateNodeText(Node node, string ntext)
        {
            node.text = ntext;
            DatabaseManager.UpdateNode(node);
        }
        public void DeleteNode(string tnid)
        {
            Node node = LookupNode(id_trans.GetNodeID(tnid));
            id_trans.RemoveNodeTranslation(tnid);
            foreach (Connection con in node.children)
            {
                cons.Remove(con.cid);
                id_trans.RemoveConnectionTranslation(con.cid);
            }
            foreach(Connection con in node.parents)
            {
                cons.Remove(con.cid);
                id_trans.RemoveConnectionTranslation(con.cid);
            }
            node.Delete();
            DatabaseManager.DeleteNode(node);
        }
        public void DeleteConnection(string tcid)
        {
            int cid = id_trans.GetConnectionID(tcid);
            Connection con = cons[cid];
            con.source.DeleteChildConnection(con.cid);
            con.dest.DeleteParentConnection(con.cid);
            id_trans.RemoveConnectionTranslation(tcid);
            cons.Remove(cid);
            DatabaseManager.DeleteConnection(con);
        }
    }
}