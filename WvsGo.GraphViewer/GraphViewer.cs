using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Northwoods.Go;
using WvsGo.GraphViewer.Nodes;
using System.Collections;
using WvsGo.Parser;
using WvsGo.Parser.Structures;

namespace WvsGo.GraphViewer
{
    public partial class GraphViewer: UserControl
    {

        public string XmlFilePath { get;private set; }

        private Hashtable myMap = new Hashtable();
        // node spacing parameters
        protected float myHorizSeparation = 50;
        protected float myVertSeparation = 20;

        // link appearances
        protected Color myExtendsPenColor = Color.Black;
        protected Color myImplementsPenColor = Color.Gray;
        TextNode rootNode, assetNode, threatNode;

        public GraphViewer()
        {
            InitializeComponent();
            myView.KeyDown += MyView_KeyDown;
            GoDocument doc = myView.Document;
            // create a layer in back to hold all the links
            doc.LinksLayer = doc.Layers.CreateNewLayerBefore(doc.Layers.Default);
            doc.LinksLayer.Identifier = "links";
            // create a layer in front to hold selected nodes
            doc.Layers.CreateNewLayerAfter(doc.Layers.Default).Identifier = "selected";
        }

       public  ScanGroup LoadNodesFromXml(string xmlFilePath="")
        {
            ScanGroup retObj = null;
            if (string.IsNullOrEmpty(xmlFilePath))
            {
                return retObj;
            }
            XmlFilePath = xmlFilePath;
            try
            {
                var xmlObj = WvsXmlParser.LoadFromFile(XmlFilePath);
                retObj = xmlObj;
                Cursor oldcursor = myView.Cursor;
                myView.Cursor = Cursors.WaitCursor;

                myView.Document.Clear();
                myMap.Clear();

                // call AddTextNode for all the classes in the Northwoods.Go assembly
                InitNodesFromScanGroup(xmlObj);
                LayoutNodes();

                GoComment comment = new GoComment();
                comment.Text = "双击查看详情";
                comment.Editable = false;
                comment.Label.Editable = false;
                comment.Position = new PointF(10, 70);
                myView.Document.Add(comment);

                myView.Cursor = oldcursor;
            }
            catch (Exception ex)
            {
                
            }

            return retObj;
            
        }

        public void InitNodesFromScanGroup(ScanGroup scanGroup)
        {
            rootNode=AddTextNode(scanGroup.Scan);
            assetNode = AddTextNode("站点资产");
            threatNode = AddTextNode("风险点");
            AddLink(rootNode, assetNode);
            AddLink(rootNode, threatNode);

            for (int i = 0; i < scanGroup.Scan.Crawler.SiteFiles.Count; i++)
            {
                var siteFile = scanGroup.Scan.Crawler.SiteFiles[i];
                var fileNode = AddTextNode(siteFile);
                AddLink(assetNode, fileNode);
            }

            for (int i = 0; i < scanGroup.Scan.ReportItems.Count; i++)
            {
                var reportItem = scanGroup.Scan.ReportItems[i];
                var reportNode = AddTextNode(reportItem);
                AddLink(threatNode, reportNode);
            }

        }

        // Add a TextNode representing a particular class,
        // and add all of its super classes and interfaces.
        // If a TextNode already exists for the given Type,
        // just return it.
        public TextNode AddTextNode(Object t)
        {
            if (t == null) return null;

            TextNode cnode = FindNode(t);
            if (cnode != null) return cnode;

            cnode = GetNode(t);
            

            return cnode;
        }

        public void AddLink(TextNode fromNode,TextNode toNode)
        {
            GoLink link = new GoLink();
            link.FromPort = fromNode.RightPort;
            link.ToPort = toNode.LeftPort;
            link.Selectable = false;
            link.PenColor = myExtendsPenColor;
            link.PenWidth = 2;

            myView.Document.LinksLayer.Add(link);
        }

        // If we already know about a TextNode for a given object,
        // return it, else return null
        public TextNode FindNode(Object key)
        {
            return myMap[key] as TextNode;
        }

        // Get a TextNode for a given object, using an existing one
        // if possible, otherwise creating one
        public TextNode GetNode(Object key)
        {
            TextNode node = FindNode(key);
            if (node == null)
            {
                node = new TextNode();
                node.UserObject = key;  // keep back pointer
                myView.Document.Add(node);
                myMap[key] = node;
            }
            return node;
        }

        // First layout Object and its subclasses, as a tree.
        // Then layout all interfaces without moving any already positioned nodes.
        public void LayoutNodes()
        {
            float oldx = 150;  // initial top-left position
            float oldy = 50;


            float newy = LayoutTree(rootNode, oldx, oldy);
            oldy = newy + rootNode.Height + myVertSeparation;

            // find all interface nodes and lay them out
            oldx = 50;  // initial top-left position
            oldy = 300;
            foreach (GoObject obj in myView.Document)
            {
                if (obj is TextNode)
                {
                    TextNode node = (TextNode)obj;
       
                    if (node.LeftPort.LinksCount == 0)
                    {
                        newy = LayoutTree(node, oldx, oldy);
                        oldy = newy + node.Height + myVertSeparation;
                    }
                }
            }
        }

        // Implement a simple tree-layout algorithm
        public float LayoutTree(TextNode node, float oldx, float oldy)
        {
            float origy = oldy;
            float newy = oldy;

            // recurse through all the node's children
            GoPort outp = node.RightPort;

            // for each link attached to the output port...
            foreach (GoLink link in outp.Links)
            {
                // in case we link things up in the wrong direction sometime in the future,
                // this gets the other end of the link no matter which way we're going
                GoPort inp = link.GetOtherPort(outp) as GoPort;
                if (inp == null) continue;

                // get the child node, and lay it out recursively if not already positioned
                TextNode child = (TextNode)inp.Parent;
                if (child.Top <= 0)
                {
                    newy = LayoutTree(child, oldx + node.Width + myHorizSeparation, oldy);

                    // increment the Y position for the next child, if any
                    oldy = newy + node.Height + myVertSeparation;
                }
            }

            // position this node at the average Y position of the children
            node.Position = new PointF(oldx, (origy + newy) / 2);

            return newy;  // updated by recursive layoutTree calls
        }


        

        private void MyView_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Return)
            {
                TextNode cn = myView.Selection.Primary as TextNode;
                if (cn != null)
                    cn.ToggleInfo();
            }
            else if (e.Control && e.KeyCode == Keys.Q)
            {
                //
            }
            else if (e.Shift && e.Control && e.KeyCode == Keys.P)
            {
                myView.PrintPreview();
            }
            else if (e.Control && e.KeyCode == Keys.P)
            {
                myView.Print();
            }
            else if (e.Control && e.KeyCode == Keys.L)
            {
                LoadNodesFromXml();
            }
        }
    }
}
