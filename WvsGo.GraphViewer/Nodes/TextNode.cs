using Northwoods.Go;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WvsGo.Parser.Structures;

namespace WvsGo.GraphViewer.Nodes
{
    public class TextNode : GoTextNode
    {
        public static Color myClassPenColor = Color.LightGray;
        public static Color mySystemNamespaceColor = Color.FromArgb(170, 240, 200);
        public static Color mySystemNamespaceBrushColor = Color.LavenderBlush;

        public static Color myInterfacePenColor = Color.Gray;
        public static Color myNwoodsNamespaceColor = Color.FromArgb(180, 210, 250);
        public static Color myNwoodsNamespaceBrushColor = Color.LavenderBlush;

        bool toggleSwitch = false;
        public TextNode()
        {
            TopPort = null;
            BottomPort = null;
        }

        // Remember the Type object associated with this ClassNode
        public override Object UserObject
        {
            set
            {
                if (value != null)
                {
                    base.UserObject = value;
                    GenerateShortInfo(value);
                }
            }
        }

        /// <summary>
        /// 详情显示开关
        /// </summary>
        public void ToggleInfo()
        {
            //控制是否多行显示来显示详情
            if (toggleSwitch)
            {
                toggleSwitch = false;
                GenerateShortInfo(UserObject);
            }
            else
            {
                toggleSwitch = true;
                GenerateLongInfo(UserObject);
            }
        }

        public void GenerateShortInfo(object value)
        {
            GoShape shape = this.Background as GoShape;
            if (value is string)
            {
                string obj = (string)value;
                
                Text = obj;
                Label.Multiline = false;
                Label.Bold = true;
                if (shape != null)
                {
                    shape.FillHalfGradient(myNwoodsNamespaceColor);
                }
            }
            else if (value is Scan)
            {
                var obj = (Scan)value;
                Text = $"{obj.StartURL}{Environment.NewLine}[{obj.WebServer}  {obj.Banner}  {obj.Technologies}]";
                Label.Multiline = true;
                Label.Bold = true;
                if (shape != null)
                {
                    shape.FillHalfGradient(myClassPenColor);
                }
            }
            else if (value is SiteFile)
            {
                var obj = (SiteFile)value;
                Text = $"[{obj.Name}]    {obj.URL}";
                Label.Multiline = false;
                Label.Bold = false;
                if (shape != null)
                {
                    shape.BrushColor=Color.DarkGray;
                }
            }
            else if (value is ReportItem)
            {
                var obj = (ReportItem)value;
                if (obj.CVEList.Count > 0)
                {
                    Text = $"漏洞位置:[{obj.Affects}] 漏洞名称:{obj.Name}{Environment.NewLine}[{obj.Severity}] [{obj.Type}] CVE:{obj.CVEList[0].Id}";
                }
                else
                {
                    Text = $"[{obj.Severity}] [{obj.Type}] {obj.Name}";
                }

                Label.Multiline = true;
                if (shape != null)
                {
                    switch (obj.Severity)
                    {
                        case "high":
                            shape.BrushColor=Color.Red;
                            break;
                        case "medium":
                            shape.BrushColor = Color.Yellow;
                            break;
                        case "low":
                            shape.BrushColor = Color.Blue;
                            break;
                        case "info":
                            shape.BrushColor = Color.Green;
                            break;
                    }

                }
            }
        }

        public void GenerateLongInfo(object value)
        {
            GoShape shape = this.Background as GoShape;
            if (value is string)
            {
                string obj = (string)value;

                Text = obj;
                Label.Multiline = false;
                Label.Bold = true;
                if (shape != null)
                {
                    shape.FillHalfGradient(myNwoodsNamespaceColor);
                }
            }
            else if (value is Scan)
            {
                var obj = (Scan)value;
                
                Text = $"目标URL：{obj.StartURL}{Environment.NewLine} OS:{obj.Os} WebServer:{obj.WebServer} Banner:{obj.Banner} Technologies:{obj.Technologies}]{Environment.NewLine}";
                Label.Multiline = true;
                Label.Bold = true;
                if (shape != null)
                {
                    shape.FillHalfGradient(myClassPenColor);
                }
            }
            else if (value is SiteFile)
            {
                var obj = (SiteFile)value;
                Text = $"[{obj.Name}]   {obj.URL}";
                Label.Multiline = false;
                Label.Bold = false;
                if (shape != null)
                {
                    shape.FillHalfGradient(mySystemNamespaceBrushColor);
                }
            }
            else if (value is ReportItem)
            {
                var obj = (ReportItem)value;
                if (obj.CVEList.Count > 0)
                {
                    Text = $"漏洞位置:[{obj.Affects}] 漏洞名称:{obj.Name}{Environment.NewLine}[{obj.Severity}] [{obj.Type}] CVE:{obj.CVEList[0].Id}{Environment.NewLine} 请求:{obj.TechnicalDetails.Request}{Environment.NewLine}应答:{obj.TechnicalDetails.Response}";
                }
                else
                {
                    Text = $"[{obj.Severity}] [{obj.Type}] {obj.Name}{Environment.NewLine} 请求:{obj.TechnicalDetails.Request}{Environment.NewLine}应答:{obj.TechnicalDetails.Response}";
                }

                Label.Multiline = true;
                if (shape != null)
                {
                    switch (obj.Severity)
                    {
                        case "high":
                            shape.FillHalfGradient(Color.Red);
                            break;
                        case "medium":
                            shape.FillHalfGradient(Color.Yellow);
                            break;
                        case "low":
                            shape.FillHalfGradient(Color.Blue);
                            break;
                        case "info":
                            shape.FillHalfGradient(Color.Green);
                            break;
                    }

                }
            }
        }



        // Move the object to the top layer, so it is visible to the user.
        public override void OnGotSelection(GoSelection selection)
        {
            this.Document.Layers.Top.Add(this);
            base.OnGotSelection(selection);
        }

        // Just change the GoTextNode's background brush color,
        // rather than adding selection handles.
        public override void AddSelectionHandles(GoSelection selection, GoObject selectedObject)
        {
            GoShape shape = this.Background as GoShape;
            if (shape != null)
            {
                //shape.BrushStyle = GoBrushStyle.Solid;
                //shape.BrushColor = Color.DeepPink;
            }
        }

        // Move the object to the regular layer, where it can be obscured by selected nodes,
        // unless this node is being deleted.
        public override void OnLostSelection(GoSelection selection)
        {
            if (!this.BeingRemoved)
                this.Document.DefaultLayer.Add(this);
            base.OnLostSelection(selection);
        }

        // Just change the GoTextNode's background brush color,
        // rather than adding selection handles.
        public override void RemoveSelectionHandles(GoSelection selection)
        {
            GoShape shape = this.Background as GoShape;
            if (shape != null)
            {
                //shape.FillHalfGradient(myNwoodsNamespaceColor);
            }
        }

        // When the user double-clicks or context-clicks on a node, 
        // change the amount of information visible.
        public override bool OnDoubleClick(GoInputEventArgs evt, GoView view)
        {
            ToggleInfo();
            return true;
        }

        public override bool OnContextClick(GoInputEventArgs evt, GoView view)
        {
            ToggleInfo();
            return true;
        }

    }
}
