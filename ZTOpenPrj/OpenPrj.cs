using System;
using System.Collections.Generic;
using System.Text;
using ZTExtension;
using ZTViewInterface;

namespace ZTOpenPrj
{
    public class OpenPrj:ICommand
    {
        public string strCityCode;
        public string strCityName;
        public override void MyCommand()
        {
            ZTOpenPrj openPrj = new ZTOpenPrj();
			if (openPrj.ShowDialog() == System.Windows.Forms.DialogResult.OK)
			{

				if (openPrj.strJCnf != "" && openPrj.strJCsd != "" && openPrj.txCityCode.Text != "" && openPrj.txCityName.Text != "")
				{
					ExtManager pExtManager = m_GREPara.IMainFrm.GetExtManager() as ExtManager;
					pExtManager.DoCommand("prjtree.dll", "openprj", new object[] { 
                openPrj.strJCnf,openPrj.strJCsd,openPrj.txCityCode.Text, openPrj.txCityName.Text });
				}
			}

           
            openPrj.Close();
        }
    }
}
