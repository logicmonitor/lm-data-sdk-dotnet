/*
 * Copyright, 2021, LogicMonitor, Inc.
 * This Source Code Form is subject to the terms of the 
 * Mozilla Public License, v. 2.0. If a copy of the MPL 
 * was not distributed with this file, You can obtain 
 * one at https://mozilla.org/MPL/2.0/.
 */
using System;
using System.Collections;
using System.Collections.Generic;
using LogicMonitor.DataSDK.Model;
using LogicMonitor.DataSDK.Utils;
using NUnit.Framework;
using NUnit.Framework.Internal;

namespace TestingLogicMonitor.DataSDK.Tests.Utils
{
    [TestFixture]
    public class TestObjectNameValidator
    {
        ObjectNameValidator o = new ObjectNameValidator();

        public static IEnumerable<TestCaseData> TestStringProvider
        {
            get
            {
                yield return new TestCaseData("SIN");
                yield return new TestCaseData("COS");
                yield return new TestCaseData("LOG");
                yield return new TestCaseData("EXP");
                yield return new TestCaseData("FLOOR");
                yield return new TestCaseData("CEIL");
                yield return new TestCaseData("ROUND");
                yield return new TestCaseData("POW");
                yield return new TestCaseData("ABS");
                yield return new TestCaseData("SQRT");
                yield return new TestCaseData("RANDOM");
                yield return new TestCaseData("LT");
                yield return new TestCaseData("LE");
                yield return new TestCaseData("GT");
                yield return new TestCaseData("GE");
                yield return new TestCaseData("EQ");
                yield return new TestCaseData("NE");
                yield return new TestCaseData("IF");
                yield return new TestCaseData("MIN");
                yield return new TestCaseData("MAX");
                yield return new TestCaseData("DUP");
                yield return new TestCaseData("EXC");
                yield return new TestCaseData("POP");
                yield return new TestCaseData("UN");
                yield return new TestCaseData("UNKN");
                yield return new TestCaseData("NOW");
                yield return new TestCaseData("TIME");
                yield return new TestCaseData("PI");
                yield return new TestCaseData("E");
                yield return new TestCaseData("AND");
                yield return new TestCaseData("OR");
                yield return new TestCaseData("XOR");
                yield return new TestCaseData("INF");
                yield return new TestCaseData("NEGINF");
                yield return new TestCaseData("STEP");
                yield return new TestCaseData("YEAR");
                yield return new TestCaseData("MONTH");
                yield return new TestCaseData("HOUR");
                yield return new TestCaseData("MINUTE");
                yield return new TestCaseData("SECOND");
                yield return new TestCaseData("WEEK");
                yield return new TestCaseData("SIGN");
                yield return new TestCaseData("RND");
                yield return new TestCaseData("SUM2");
                yield return new TestCaseData("AVG2");
                yield return new TestCaseData("PERCENT");
                yield return new TestCaseData("RAWPERCENTILE");
                yield return new TestCaseData("IN");
                yield return new TestCaseData("NANTOZERO");
                yield return new TestCaseData("MIN2");
                yield return new TestCaseData("MAX2");
            }
        }


        [TestCase("ABC", " Name")]
        [TestCase("A123", " Name ")]
        [TestCase("123ABC", "Name ")]
        public void TestPassEmptyAndSpellCheck(string name, string invalidname)
        {
            Assert.AreEqual(false, o.PassEmptyAndSpellCheck(invalidname));
            Assert.AreEqual(true, o.PassEmptyAndSpellCheck(name));

        }

        [Test]
        public void TestIsNameLengthValid()
        {
            String name = new String('a', 266);
            Assert.AreEqual(false, o.IsNameLengthValid(name));
        }


        [TestCase("A123", "Name$")]
        [TestCase("123ABC", "Name ")]
        [TestCase("ABC", "Name,:")]
        public void TestIsValidResourceName(string resourceName, string invalidResourceName)
        {
            Assert.AreEqual(true, o.IsValidResourceName(resourceName));
            Assert.AreEqual(false, o.IsValidResourceName(invalidResourceName));

        }

        [TestCase("")]
        public void TestNullDataSourceName(string name)
        {
            string expected = "Datasource name cannot be empty";
            string actual = o.ValidateDataSourceName(name);
            Assert.AreEqual(expected, actual);
        }

        [TestCase("-")]
        public void TestSingleDashDataSourceName(string name)
        {
            string expected = "Datasource name cannot be single \"-\"";
            string actual = o.ValidateDataSourceName(name);
            Assert.AreEqual(expected, actual);
        }

        [TestCase(" -")]
        public void TestSingleSpaceDashDataSourceName(string name)
        {
            string expected = "Space is not allowed in start and end";
            string actual = o.ValidateDataSourceName(name);
            Assert.AreEqual(expected, actual);
        }

        [TestCase("Data-Source")]
        public void TestDashDataSourceName(string name)
        {
            string expected = "Support \"-\" for datasource name when its the last char";
            string actual = o.ValidateDataSourceName(name);
            Assert.AreEqual(expected, actual);
        }
        public static IEnumerable<TestCaseData> TestStringsProvider
        {
            get
            {
                yield return new TestCaseData("Data?Source");
                yield return new TestCaseData("Instance,name");
                yield return new TestCaseData("a*b");
                yield return new TestCaseData("a<b");
                yield return new TestCaseData("a`b");
            }
        }

        [Test, TestCaseSource(nameof(TestStringsProvider))]
        public void TestValidIstanceName(string name)
        {
            Assert.IsFalse(o.IsValidInstanceName(name));
        }

        [Test, TestCaseSource(nameof(TestStringsProvider))]
        public void TestValidCompanyName(string name)
        {
            Assert.AreEqual(false, o.IsValidCompanyName(name));
        }

        [Test, TestCaseSource(nameof(TestStringsProvider))]
        public void ValidateDeviceDisplayName(string name)
        {
            string expected = name + " is not allowed in instance display name";
            Assert.AreEqual(expected, o.ValidateDeviceDisplayName(name));
        }


        [TestCase(null)]
        public void TestNullDataPoint(string name)
        {
            Assert.AreEqual("Data point name cannot be null", o.ValidDataPointName(name));
        }

        [Test, TestCaseSource(nameof(TestStringsProvider))]
        public void TestInvalidDataPoint(string name)
        {
            Assert.AreEqual("Invalid Data point " + name, o.ValidDataPointName(name));
        }

        [Test, TestCaseSource(nameof(TestStringProvider))]
        public void TestReservedDataPoint(string name)
        {
            string expected = string.Format("{0} is a keyword and cannot be use as datapoint name.", name);
            Assert.AreEqual("" + name + " is a keyword and cannot be use as datapoint name.", o.ValidDataPointName(name));
        }

        [TestCase("")]
        public void TestNullDataSourceDisplayName(string name)
        {
            string expected = "Datasource name cannot be empty";
            string actual = o.ValidateDataSourceName(name);
            Assert.AreEqual(expected, actual);
        }

        [TestCase("-")]
        public void TestSingleDashDataSourceDisplayName(string name)
        {
            string expected = "Datasource name cannot be single \"-\"";
            string actual = o.ValidateDataSourceName(name);
            Assert.AreEqual(expected, actual);
        }

        [TestCase(" -")]
        public void TestSingleSpaceDashDataSourceDisplayName(string name)
        {
            string expected = "Space is not allowed in start and end";
            string actual = o.ValidateDataSourceName(name);
            Assert.AreEqual(expected, actual);
        }

        [TestCase("Data-Source")]
        public void TestDashDataSourceDisplayName(string name)
        {
            string expected = "Support \"-\" for datasource name when its the last char";
            string actual = o.ValidateDataSourceName(name);
            Assert.AreEqual(expected, actual);
        }

        [TestCase(true, null)]
        public void TestNullCheckResourceNameValidation(bool flag, string name)
        {
            string expected = "Resource name is mandatory.";
            string actual = o.CheckResourceNameValidation(flag, name);
            Assert.AreEqual(expected, actual);
        }


        [TestCase(true, "")]
        [TestCase(true, " Name")]
        [TestCase(true, "Name ")]
        public void TestEmptyCheckResourceNameValidation(bool flag, string name)
        {
            string expected = "Resource Name Should not be empty or have tailing spaces.";
            string actual = o.CheckResourceNameValidation(flag, name);
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void TestCheckResourceNamDescriptionValidation()
        {
            String name = new String('a', 65536);
            Assert.AreEqual("Resource Description Size should not be greater than 65535 characters.", o.CheckResourceNamDescriptionValidation(name));
        }

        [TestCase(null)]
        public void TestNullCheckDataSourceNameValidation(string DSname)
        {
            string expected = "Datasource is mandatory.";
            string actual = o.CheckDataSourceNameValidation(DSname);
            Assert.AreEqual(expected, actual);
        }
        [TestCase(" Name")]
        [TestCase("Name ")]
        public void TestCheckDataSourceNameValidation(string source)
        {
            string expected = "Datasource Name Should not be empty or have tailing spaces.";
            string actual = o.CheckDataSourceNameValidation(source);
            Assert.AreEqual(expected, actual);
        }
        [TestCase(null)]
        public void TestNullCheckDataSourceDisplayNameValidation(string DSDname)
        {
            string expected = "";
            string actual = o.CheckDataSourceDisplayNameValidation(DSDname);
            Assert.AreEqual(expected, actual);
        }
        [TestCase(" Name")]
        [TestCase("Name ")]
        public void TestCheckDataSourceDisplayNameValidation(string DSDNsource)
        {
            string expected = "Datasource display name Should not be empty or have tailing spaces.";
            string actual = o.CheckDataSourceDisplayNameValidation(DSDNsource);
            Assert.AreEqual(expected, actual);
        }
        [TestCase(" Name")]
        [TestCase("Name ")]
        public void TestCheckDataSourceGroupNameValidation(string source)
        {
            string expected = "Datasource group name Should not be empty or have tailing spaces.";
            string actual = o.CheckDataSourceGroupNameValidation(source);
            Assert.AreEqual(expected, actual);

        }
        [TestCase(null)]
        public void TestNullCheckInstanceNameValidation(string Iname)
        {
            string expected = "Instance name is mandatory.";
            string actual = o.CheckInstanceNameValidation(Iname);
            Assert.AreEqual(expected, actual);
        }
        [TestCase(" Name")]
        [TestCase("Name ")]
        public void TestCheckInstanceNameValidation(string Isource)
        {
            string expected = "Instance Name Should not be empty or have tailing spaces.";
            string actual = o.CheckInstanceNameValidation(Isource);
            Assert.AreEqual(expected, actual);
        }
        [TestCase(" Name")]
        [TestCase("Name ")]
        public void TestCheckInstanceDisplayNameValidation(string Idsource)
        {
            string expected = "Instance display name Should not be empty or have tailing spaces.";
            string actual = o.CheckInstanceDisplayNameValidation(Idsource);
            Assert.AreEqual(expected, actual);
        }

        [TestCase(null)]
        public void TestNullCheckDataPointNameValidation(string dpname)
        {
            string expected = "Datapoint name is mandatory.";
            string actual = o.CheckDataPointNameValidation(dpname);
            Assert.AreEqual(expected, actual);
        }
        [TestCase(" Name")]
        [TestCase("Name ")]
        public void TestCheckDataPointNameValidation(string dpname)
        {
            string expected = "DataPoint Name Should not be empty or have tailing spaces.";
            string actual = o.CheckDataPointNameValidation(dpname);
            Assert.AreEqual(expected, actual);
        }
        [Test]
        public void TestCheckDataPointDescriptionValidation()
        {
            String name = new String('a', 1025);
            Assert.AreEqual("Datapoint description should not be greater than 1024 characters.", o.CheckDataPointDescriptionValidation(name));
        }

        [TestCase("Name")]
        public void TestIsValidDataSourceDisplayName(string dsname)
        {
            bool expected = true;
            bool actual = o.IsValidDataSourceDisplayName(dsname);
            Assert.AreEqual(expected, actual);
        }
        [TestCase(" ")]
        public void TestEmptyIsValidDataSourceDisplayName(string dsname)
        {
            bool expected = false;
            bool actual = o.IsValidDataSourceDisplayName(dsname);
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void TestCheckInstanceDisplayNameValidationGreaterThan255()
        {
            String name = new String('a', 256);
            Assert.AreEqual("Instance display name size should not be greater than 255 characters.", o.CheckInstanceDisplayNameValidation(name));
        }

        [TestCase("avg234")]
        [TestCase("sum55")]
        [TestCase("none11")]
        [TestCase("none23")]
        public void sum(string s)
        {
            string t = "The datapoint aggeration type is having invalid datapoint aggreation Type " + s + ".";
            var abs = o.CheckDataPointAggerationTypeValidation(s);
            Assert.AreEqual(t, abs);
        }

        [TestCase(1234567890)]
        public void TestCheckDataSourceInstanceIdlength(int id)
        {
          string error = "DataSource Instance Id cannot be more than 9 digit";
          string actual = o.CheckDataSourceInstanceId(id);
          Assert.AreEqual(error, actual);
        }

        [TestCase(1234567890)]
        public void TestCheckDataSourceIdlength(int id)
        {
          string error = "DataSource Id cannot be more than 9 digit";
          string actual = o.CheckDataSourceId(id);
          Assert.AreEqual(error, actual);
        }

        [TestCase(-1)]
        [TestCase(101)]
        public void TestCheckPercentileValue(int percentileValue)
        {
          string expected = "Percentile value muist be in Range of 0-100";
          string actual = o.CheckPercentileValue(percentileValue);
          Assert.AreEqual(expected, actual);
        }

        [TestCase("metrics")]
        [TestCase("seconds")]
        [TestCase("count")]
        public void CheckDataPointTypeValidation(string dataPointType)
        {
          string expected = "The datapoint type is having invalid dataPointType "+dataPointType+".";
          string actual = o.CheckDataPointTypeValidation(dataPointType);
          Assert.AreEqual(expected, actual);

        }

        [TestCase("authkey")]
        [TestCase("1231key")]
        [TestCase("authdf23r9-=key")]
        [TestCase("authkey20i34-2uel;ca")]
        public void  TestIsValidAuthKey(string authKey)
 	      {
	        bool actual= o.IsValidAuthKey(authKey);
          Assert.IsTrue(actual);
 	      }

        [TestCase("authkey2 0i34-2uel;ca")]
        public void TestIsInValidAuthKey(string authKey)
        {
            bool actual = o.IsValidAuthKey(authKey);
            Assert.IsFalse(actual);
        }

        [Test]
    public void TestCheckResourcePropertiesValidation()
    {
      Dictionary<string, string> resourceProperties = new Dictionary<string, string>();
      resourceProperties.Add("", "");
      string expected = "Resource Properties Key should not be null, empty or have trailing spaces.";
      string actual = o.CheckResourcePropertiesValidation(resourceProperties);
      Assert.AreEqual(expected, actual);

      int length = 256;
      Random random = new Random();
      var rString = "";
      for (var i = 0; i < length; i++)
      {
        rString += ((char)(random.Next(1, 26) + 64)).ToString().ToLower();
      }

      resourceProperties.Clear();
      resourceProperties.Add(rString, "");
      expected = "Resource Properties Key should not be greater than 255 characters.";
      actual = o.CheckResourcePropertiesValidation(resourceProperties);
      Assert.AreEqual(expected, actual);

      resourceProperties.Clear();
      resourceProperties.Add("Abc##", "");
      expected = "Cannot use '##' in property name.";
      actual = o.CheckResourcePropertiesValidation(resourceProperties);
      Assert.AreEqual(expected, actual);

      resourceProperties.Clear();
      resourceProperties.Add("system.display", "");
      expected = "Resource Properties Should not contain System or auto properties :: system.display.";
      actual = o.CheckResourcePropertiesValidation(resourceProperties);
      Assert.AreEqual(expected, actual);

      resourceProperties.Clear();
      resourceProperties.Add("auto.display", "");
      expected = "Resource Properties Should not contain System or auto properties :: auto.display.";
      actual = o.CheckResourcePropertiesValidation(resourceProperties);
      Assert.AreEqual(expected, actual);

     
      resourceProperties.Clear();
      resourceProperties.Add("resource.display", "");
      expected = "Resource Properties Value should not be null, empty or have trailing spaces.";
      actual = o.CheckResourcePropertiesValidation(resourceProperties);
      Assert.AreEqual(expected, actual);
    }
  }
}
