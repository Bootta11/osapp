using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using log4net;
using System.Reflection;

namespace OsnovnaSredstva
{
    public partial class Podesavanja : Form
    {
        private static readonly ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        public bool automatskoZavrsavanjeRijeci;
        public Podesavanja()
        {
            InitializeComponent();
            string pod = DBManager.GetPodesavanje("automatskoZavrsavanjeRijeci");
            if (pod == null)
                DBManager.KreirajPodesavanje("automatskoZavrsavanjeRijeci", false);
            else
            {
                bool setPod = bool.TryParse(pod, out automatskoZavrsavanjeRijeci);
                if (!setPod)
                {
                    automatskoZavrsavanjeRijeci = false;
                    DBManager.IzmijeniPodesavanje("automatskoZavrsavanjeRijeci", automatskoZavrsavanjeRijeci);
                }
            }

            if (automatskoZavrsavanjeRijeci)
                inputOmogucitiAutomatskoZavrsavanjeRijeci.Checked = true;

            addCheckIsOptionChanged(tblOptions);

        }

        private void btnSnimiti_Click(object sender, EventArgs e)
        {
            try
            {
                string pod = DBManager.GetPodesavanje("automatskoZavrsavanjeRijeci");
                if (pod == null)
                    DBManager.KreirajPodesavanje("automatskoZavrsavanjeRijeci", inputOmogucitiAutomatskoZavrsavanjeRijeci.Checked);
                else
                {
                    DBManager.IzmijeniPodesavanje("automatskoZavrsavanjeRijeci", inputOmogucitiAutomatskoZavrsavanjeRijeci.Checked);
                }
                automatskoZavrsavanjeRijeci = inputOmogucitiAutomatskoZavrsavanjeRijeci.Checked;
                btnSnimiti.Enabled = false;
            }
            catch (Exception ex)
            {
                log.Error(ex.Message, ex);
            }

        }

        private void btnZatvoriti_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        public void addCheckIsOptionChanged(Control control)
        {

            var controls = control.Controls;
            foreach (Control c in controls)
            {
                Console.WriteLine("Name: " + c.Name);
                addCheckIsOptionChanged(c);
                if (c.Name.StartsWith("input"))
                {
                    if (c.GetType() == typeof(CheckBox))
                    {
                        CheckBox cb = (CheckBox)c;
                        cb.CheckedChanged += Checkbox_Changed_Event;


                    }
                }

            }
        }

        private void Checkbox_Changed_Event(object sender, EventArgs e)
        {
            btnSnimiti.Enabled = true;
        }

        private void Podesavanja_Shown(object sender, EventArgs e)
        {
            string pod = DBManager.GetPodesavanje("automatskoZavrsavanjeRijeci");
            if (pod == null)
                DBManager.KreirajPodesavanje("automatskoZavrsavanjeRijeci", false);
            else
            {
                bool setPod = bool.TryParse(pod, out automatskoZavrsavanjeRijeci);
                if (!setPod)
                {
                    automatskoZavrsavanjeRijeci = false;
                    DBManager.IzmijeniPodesavanje("automatskoZavrsavanjeRijeci", automatskoZavrsavanjeRijeci);
                }
            }

            if (automatskoZavrsavanjeRijeci)
                inputOmogucitiAutomatskoZavrsavanjeRijeci.Checked = true;

            btnSnimiti.Enabled = false;
        }
    }
}
