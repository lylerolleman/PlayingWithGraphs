using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PlayingWithGraphs.Models
{
    public static class DatabaseManager
    {
        private static MySqlConnection con = new MySqlConnection();

        public static int InsertNewGraph(string name)
        {
            string create_insert = "INSERT INTO Graph (graph_name) VALUES ('" + name + "')";
            var com = GetCommand(create_insert);
            com.ExecuteNonQuery();
            int gid = (int)com.LastInsertedId;
            con.Close();
            return gid;
        }
        public static string GetGraphName(int id)
        {
            string name_query = "SELECT graph_name FROM Graph WHERE gid = " + id;
            var com = GetCommand(name_query);
            MySqlDataReader reader = com.ExecuteReader();
            string name = "empty";
            if (reader.Read())
                name = reader.GetString(0);
            reader.Close();
            con.Close();
            return name;
        }
        public static List<Tuple<int, string>> GetGraphList()
        {
            List<Tuple<int, string>> graph_list = new List<Tuple<int, string>>();
            string graph_list_query = "SELECT gid, graph_name FROM Graph";
            var com = GetCommand(graph_list_query);
            MySqlDataReader reader = com.ExecuteReader();
            while (reader.Read())
            {
                int gid = reader.GetInt32(0);
                string name = reader.GetString(1);
                System.Diagnostics.Debug.WriteLine(gid + ": " + name);
                graph_list.Add(new Tuple<int, string>(gid, name));
            }
            reader.Close();
            con.Close();
            return graph_list;
        }
        public static List<Node> GetNodes(int gid)
        {
            List<Node> nodes = new List<Node>();
            string nodes_query = "SELECT nid, ntext, x, y FROM Node WHERE gid = " + gid;
            var com = GetCommand(nodes_query);
            MySqlDataReader reader = com.ExecuteReader();
            while (reader.Read())
            {
                int nid = reader.GetInt32(0);
                string ntext = reader.GetString(1);
                int x = reader.GetInt32(2);
                int y = reader.GetInt32(3);
                nodes.Add(new Node(nid, ntext, x, y));
            }
            reader.Close();
            con.Close();
            return nodes;
        }
        public static List<Connection> GetConnections(Graph graph)
        {
            List<Connection> conlist = new List<Connection>();
            string con_query = String.Format("SELECT cid, n1, n2 FROM Con WHERE n1 IN (SELECT nid FROM Node WHERE gid = {0}) AND n2 IN (SELECT nid FROM Node WHERE gid = {1})", graph.gid, graph.gid);
            var com = GetCommand(con_query);
            var reader = com.ExecuteReader();
            while (reader.Read())
            {
                int cid = reader.GetInt32(0);
                int n1 = reader.GetInt32(1);
                int n2 = reader.GetInt32(2);
                Node node1 = graph.LookupNode(n1);
                Node node2 = graph.LookupNode(n2);
                conlist.Add(new Connection(cid, node1, node2));
            }
            reader.Close();
            con.Close();
            return conlist;
        }
        public static int AddNode(int gid, string tid, string ntext, int x, int y)
        {
            string node_insert = String.Format("INSERT INTO Node (ntext, x, y, gid) " +
                "VALUES('{0}', {1}, {2}, {3})", ntext, x, y, gid);
            var com = GetCommand(node_insert);
            com.ExecuteNonQuery();
            int nid = (int)com.LastInsertedId;
            con.Close();
            return nid;
        }
        public static void DeleteNode(Node node)
        {
            string delete_stat = String.Format("DELETE FROM Node WHERE nid = {0}", node.nid);
            var com = GetCommand(delete_stat);
            com.ExecuteNonQuery();
            con.Close();
        }
        public static void DeleteConnection(Connection conn)
        {
            string delete_stat = String.Format("DELETE FROM Con WHERE cid = {0}", conn.cid);
            var com = GetCommand(delete_stat);
            com.ExecuteNonQuery();
            con.Close();
        }
        public static void UpdateNode(Node node)
        {
            string update = String.Format("UPDATE Node SET x = {0}, y = {1}, ntext = '{2}' WHERE nid = {3}", node.x, node.y, node.text, node.nid);
            var com = GetCommand(update);
            com.ExecuteNonQuery();
            con.Close();
        }
        public static int InsertConnection(Node n1, Node n2)
        {
            string con_insert = String.Format("INSERT INTO con (n1, n2) VALUES({0}, {1})", n1.nid, n2.nid);
            var com = GetCommand(con_insert);
            com.ExecuteNonQuery();
            int cid = (int)com.LastInsertedId;
            con.Close();
            return cid;
        }

        private static MySqlCommand GetCommand(string query)
        {
            string cstring = "server=localhost;user id=root;password=dev;database=Graph;allowuservariables=True;persistsecurityinfo=True";
            con.ConnectionString = cstring;
            con.Open();
            MySqlCommand com = con.CreateCommand();
            com.CommandText = query;
            return com;
        }
    }
}