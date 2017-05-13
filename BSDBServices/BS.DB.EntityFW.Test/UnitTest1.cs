using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BS.DB.EntityFW.Test
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestStateEntityCRUD()
        {
            StatesCNFG_Activity obj = new StatesCNFG_Activity();
          
         var gnd= obj.InsertState(new TBL_States_CNFG() { StateID = 4, IsActive = true, StateName = "Goa" });

        }

    }
}
