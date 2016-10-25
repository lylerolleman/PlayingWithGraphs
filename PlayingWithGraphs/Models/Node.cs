using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PlayingWithGraphs.Models
{
    public class Node
    {
        public int nid { get; set; }
        public string text { get; set; }
        public int x { get; set; }
        public int y { get; set; }
        public List<Connection> children { get; set; }
        public List<Connection> parents { get; set; }

        public Node(int nid, string text, int x, int y)
        {
            this.children = new List<Connection>();
            this.parents = new List<Connection>();
            this.nid = nid;
            this.text = text;
            this.x = x;
            this.y = y;
        }

        public void AddChild(Connection con)
        {
            children.Add(con);
            con.dest.AddParent(con);
        }
        public void AddParent(Connection parent)
        {
            parents.Add(parent);
        }
        public void Delete()
        {
            foreach (Connection con in parents)
            {
                for (int i=0; i < con.source.children.Count; i++)
                {
                    if (con.source.children[i].dest.nid == nid)
                    {
                        con.source.children.RemoveAt(i);
                    }
                }
            }
            foreach(Connection con in children)
            {
                for (int i=0; i<con.dest.parents.Count; i++)
                {
                    if (con.dest.parents[i].source.nid == nid)
                    {
                        con.dest.parents.RemoveAt(i);
                    }
                }
            }
        }
        public void DeleteChildConnection(int cid)
        {
            for (int i=0; i<children.Count; i++)
            {
                if (children[i].cid == cid)
                {
                    children.RemoveAt(i);
                    break;
                }
            }
        }
        public void DeleteParentConnection(int cid)
        {
            for (int i = 0; i < parents.Count; i++)
            {
                if (parents[i].cid == cid)
                {
                    parents.RemoveAt(i);
                    break;
                }
            }
        }
        override public string ToString()
        {
            return nid + ": " + text;
        }
    }
}