using RED.Models.DataContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RED.Models.ElectronicDiary.ArchivedWrappers
{
    public class ArchivedProductTestW
    {
        public Guid Id { get; set; }
        public Guid ArchivedProductId { get; set; }
        public string TestName { get; set; }
        public string TestUnitName { get; set; }
        public string TestMethods { get; set; }
        public string TestAcredetationLevel { get; set; }
        public string TestTemperature { get; set; }
        public string TestCategory { get; set; }
        public int Units { get; set; }
        public int ArchivedProtocolResultsCount { get; set; }

        //public virtual ArchivedProduct ArchivedProduct { get; set; }
        //public virtual ICollection<ArchivedProtocolResult> ArchivedProtocolResults { get; set; }

        public ArchivedProductTestW()
        {

        }

        public ArchivedProductTestW(ArchivedProductTest atest)
        {
            this.Id = atest.Id;
            this.ArchivedProductId = atest.ArchivedProductId;
            this.TestName = atest.TestName;
            this.TestUnitName = atest.TestUnitName;
            this.TestMethods = atest.TestMethods;
            this.TestAcredetationLevel = atest.TestAcredetationLevel;
            this.TestTemperature = atest.TestTemperature;
            this.Units = atest.Units;

            if (atest.ArchivedProtocolResults != null)
            {
                this.ArchivedProtocolResultsCount = atest.ArchivedProtocolResults.Count();
            }
        }

        public ArchivedProductTest ToBase()
        {
            var atest = new ArchivedProductTest();

            atest.Id = this.Id;
            atest.ArchivedProductId = this.ArchivedProductId;
            atest.TestName = this.TestName;
            atest.TestUnitName = this.TestUnitName;
            atest.TestMethods = this.TestMethods;
            atest.TestAcredetationLevel = this.TestAcredetationLevel;
            atest.TestTemperature = this.TestTemperature;
            atest.Units = this.Units;

            return atest;
        }
    }
}