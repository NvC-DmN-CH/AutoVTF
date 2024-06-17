using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using DarkUI.Controls;

namespace AutoVTF
{
    public class RecentsManager
    {
        const int MAX_ENTRIES = 10;
        Color labelColor = DarkUI.Config.Colors.GreyHighlight;
        Color backColor = DarkUI.Config.Colors.DarkBackground;
        Color buttonColor = DarkUI.Config.Colors.BlueHighlight;
        ContextMenuStrip menuStrip;

        public RecentsManager(ContextMenuStrip _menuStrip)
        {
            menuStrip = _menuStrip;
            menuStrip.BackColor = backColor;
            menuStrip.ForeColor = buttonColor;
            menuStrip.ShowItemToolTips = false;
            menuStrip.Items.Add(new ToolStripLabel("Recently watched:") { ForeColor = labelColor });
        }

        public void Register(string path)
        {
            ToolStripButton foundButton = GetButtonByText(path);
            if (foundButton != null)
            {
                // path exists, move button to top
                int indexOfFound = menuStrip.Items.IndexOf(foundButton);
                menuStrip.Items.RemoveAt(indexOfFound);
                menuStrip.Items.Insert(1, foundButton);
            }
            else
            {
                // path doesn't exist, add button
                ToolStripButton newButton = new ToolStripButton(path);
                newButton.Click += OnButtonClicked;
                menuStrip.Items.Insert(1, newButton);

                // remove the last one if we hit the limit
                if (menuStrip.Items.Count > MAX_ENTRIES + 1) // the "recently watched" label counts as an entry
                {
                    menuStrip.Items.RemoveAt(menuStrip.Items.Count - 1);
                }
            }
        }

        public void Show(Point position)
        {
            // uhh, okay, so we need to add an item in order for auto sizing to work?
            // so we add and remove one lol..
            menuStrip.Items.Add(new ToolStripButton(""));
            menuStrip.Items.RemoveAt(menuStrip.Items.Count - 1);

            // if there are no buttons, show a label indicating it
            string noItemsLabelText = "None yet";
            if (menuStrip.Items.Count == 1)
            {
                menuStrip.Items.Add(new ToolStripLabel(noItemsLabelText) { ForeColor = labelColor });
            }

            // and remove it if something's added
            if (menuStrip.Items.Count > 2)
            {
                if (menuStrip.Items[menuStrip.Items.Count - 1].Text == noItemsLabelText)
                {
                    menuStrip.Items.RemoveAt(menuStrip.Items.Count - 1);
                }
            }

            // show
            menuStrip.Show(position);
        }

        public List<string> ToStringList()
        {
            List<string> stringList = new List<string>();

            foreach (ToolStripItem item in menuStrip.Items)
            {
                if (item is ToolStripButton)
                {
                    stringList.Add(item.Text);
                }
            }

            return stringList;
        }
        
        public void SetFromStringList(List<string> stringList)
        {
            // add in reverse order:
            for(int i = stringList.Count - 1; i>=0; i--)
            {
                Register(stringList[i]);
            }
        }

        private void OnButtonClicked(object? sender, EventArgs e)
        {
            if ((sender is ToolStripButton) == false)
                return;

            ToolStripButton b = sender as ToolStripButton;
            string text = b.Text;

            Program.MainFormInstance.SetWatchFolderTextboxValue(text);
        }

        private ToolStripButton GetButtonByText(string text)
        {
            foreach (ToolStripItem item in menuStrip.Items)
            {
                if (item is ToolStripButton)
                {
                    if (item.Text == text)
                    {
                        return (ToolStripButton)item;
                    }
                }
            }

            return null;
        }
    }
}
