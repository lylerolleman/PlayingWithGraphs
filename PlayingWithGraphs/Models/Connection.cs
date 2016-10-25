using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PlayingWithGraphs.Models
{
    public class Connection
    {
        public int cid { get; set; }
        public Node source { get; set; }
        public Node dest { get; set; }

        public Connection(int cid, Node source, Node dest)
        {
            this.cid = cid;
            this.source = source;
            this.dest = dest;
        }
        public string ToString()
        {
            return cid + ": " + source.nid + " " + dest.nid;
        }
    }
}