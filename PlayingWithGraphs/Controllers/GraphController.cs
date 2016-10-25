using PlayingWithGraphs.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace PlayingWithGraphs.Controllers
{
    public class GraphController : Controller
    {
        //GET: New
        [HttpGet]
        public ActionResult New()
        {
            return View();
        }
        //POST: New
        [HttpPost]
        public ActionResult New(string name)
        {
            var graph = new Graph(name);
            HttpContext.Application["Graph"] = graph;
            return Redirect("/Graph/Graph/" + graph.gid);
        }
        // GET: Graph
        public ActionResult Graph(int id)
        {
            var graph = new Graph(id);
            HttpContext.Application["Graph"] = graph;
            return View(graph);
        }
        //GET: Connections
        [HttpGet]
        public string Connections()
        {
            Graph graph = (Graph) HttpContext.Application["Graph"];
            return graph.GetConnectionJson();
        }

        //POST: AddNode
        [HttpPost]
        public void AddNode(string tid, string text, int x, int y)
        {
            Graph graph = (Graph) HttpContext.Application["Graph"];
            graph.AddNode(tid, text, x, y);
        }
        //POST: AddConnection
        [HttpPost]
        public void AddConnection(string tcid, string n1, string n2)
        {
            Graph graph = (Graph)HttpContext.Application["Graph"];
            graph.AddConnection(tcid, graph.LookupNode(n1), graph.LookupNode(n2));
        }
        //POST: UpdateNodeLocation
        [HttpPost]
        public void UpdateNodeLocation(string nid, int x, int y)
        {
            Graph graph = (Graph)HttpContext.Application["Graph"];
            graph.UpdateNodeLocation(graph.LookupNode(nid), x, y);
        }
        //POST: UpdateNodeText
        [HttpPost]
        public void UpdateNodeText(string nid, string text)
        {
            Graph graph = (Graph)HttpContext.Application["Graph"];
            graph.UpdateNodeText(graph.LookupNode(nid), text);
        }
        //POST: DeleteNode
        [HttpPost]
        public void DeleteNode(string tnid)
        {
            Graph graph = (Graph)HttpContext.Application["Graph"];
            graph.DeleteNode(tnid);
        }
        //POST: DeleteConnection
        [HttpPost]
        public void DeleteConnection(string tcid)
        {
            Graph graph = (Graph)HttpContext.Application["Graph"];
            graph.DeleteConnection(tcid);
        }
    }
}